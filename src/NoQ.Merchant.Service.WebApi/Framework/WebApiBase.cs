using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NoQ.Framework.Extensions;
using NoQ.Framework.Mapping;

namespace NoQ.Merchant.Service.WebApi.Framework
{
    public abstract class WebApiBase<TController, TResource> : ControllerBase
    {
        public WebApiBase(ILogger<TController> logger) => Logger = logger.VerifyNotNull(nameof(logger));

        protected ILogger<TController> Logger { get; }

        //[DebuggerStepThrough]
        public Task<IActionResult> Single<TEntity>(IObjectMapper<TEntity, TResource> mapper, Func<Task<TEntity>> action, [CallerMemberName] string callingMethodName = "", [CallerFilePath] string callingFileName = "")
        {
            return LogCall(callingMethodName, callingFileName, async () => {
                TEntity entity = await action();
                if (entity == null)
                    return NotFound();

                TResource mappedResult = mapper.Map(entity);
                if (mappedResult == null)
                    throw new ApplicationException("Unexpected error mapping response entity to resource");

                return Ok(mappedResult);
            });
        }

        //[DebuggerStepThrough]
        public Task<IActionResult> Multiple<TEntity>(IObjectMapper<TEntity, TResource> mapper, Func<Task<TEntity[]>> action, [CallerMemberName] string callingMethodName = "", [CallerFilePath] string callingFileName = "")
        {
            return LogCall(callingMethodName, callingFileName, async () => {
                TEntity[] entity = await action();
                if (entity == null)
                    return NotFound();

                TResource[] mappedResult = mapper.Map(entity);
                return Ok(mappedResult);
            });
        }

        //[DebuggerStepThrough]
        private Task<IActionResult> LogCall(string actionName, string fileName, Func<Task<IActionResult>> action)
        {
            var fullyQualifiedAction = GenerateActionFullName(actionName, fileName);
            var logScope = Logger.BeginScope("Action {action}", fullyQualifiedAction);
            Logger.LogDebug("Started {action}", fullyQualifiedAction);

            try
            {
                return action();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unexpected error calling {action}", fullyQualifiedAction);
                throw;
            }
            finally
            {
                Logger.LogDebug("Completed {action}", fullyQualifiedAction);
               
                if (logScope != null)
                    logScope.Dispose();

            }
        }

        private object GenerateActionFullName(string actionName, string fileName)
        {
            var fileInfo = new FileInfo(fileName);
            return $"{fileInfo.Name.Split('.')[0]}.{actionName}";
        }
    }
}
