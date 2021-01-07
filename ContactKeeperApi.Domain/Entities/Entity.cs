using System;

namespace ContactKeeperApi.Domain.Entities
{
    public abstract class Entity
    {
        /// <summary>
        /// Dada de criação do registro
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Data de atualização do registro
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
