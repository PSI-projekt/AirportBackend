using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Domain
{
    class Airplane
    {
        public int Id { get; set; }
        public string Producent { get; set; }
        public string Model { get; set; }
        public string Identifier { get; set; }
        public string Airline { get; set; }
        public string IsInRepair { get; set; }
        public int? Location { get; set; } // id lotniska na którym jest samolot,
                                           // dałem ? bo jak jest w naprawie niekoniecznie musi mieć
                                           // jakieś lotnisko

        public virtual Airport Airport { get; set; }
    }
}
