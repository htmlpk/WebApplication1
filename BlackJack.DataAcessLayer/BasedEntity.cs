using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.DataAcessLayer
{
    public class BasedEntity
    {
        [ExplicitKey]
        public Guid ID { get; set; }

       
    }
}
