﻿namespace Zerphin.RentACar.Server.Domain.Abstractions
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        
        public Guid? DeletedBy { get; set; }
    }
}
