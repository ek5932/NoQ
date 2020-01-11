using System;

namespace NoQ.Merchant.Service.Domain.Entities
{
    public interface IPersistedEntity
    {
        public int Id { get; set; }
        public int Version { get; set; }
        public DateTime UtcTimeStamp { get; set; }
    }
}
