using Dapper.Contrib.Extensions;
using System;

namespace BlackJack.DataAccessLayer.Entities
{
    public class BaseEntity
    {
        [ExplicitKey]
        public Guid Id { get; set; }

        public BaseEntity()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
