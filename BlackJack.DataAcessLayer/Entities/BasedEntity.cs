using Dapper.Contrib.Extensions;
using System;

namespace BlackJack.DataAcessLayer.Entities
{
    public class BasedEntity
    {
        [ExplicitKey]
        public Guid Id { get; set; }

        public BasedEntity()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
