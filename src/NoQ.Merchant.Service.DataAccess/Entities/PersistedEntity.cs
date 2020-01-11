using System;
using NoQ.Merchant.Service.Domain.Entities;

namespace NoQ.Merchant.Service.DataAccess.Entities
{
    public class PersistedEntity : IPersistedEntity
    {
        public int Id { get; set; }
        public int Version { get; set; }
        public DateTime UtcTimeStamp { get; set; }
    }
}
