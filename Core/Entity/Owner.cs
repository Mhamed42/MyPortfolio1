using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity
{
    public class Owner : EntityBase
    {

        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string Profile { get; set; }
        public Adress? Adress { get; set; }
    }
}
