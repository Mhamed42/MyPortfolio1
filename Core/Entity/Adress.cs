using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity
{
   public class Adress : EntityBase
    {
        public int Number { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
    }
}
