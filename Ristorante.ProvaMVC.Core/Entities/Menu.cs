using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ristorante.ProvaMVC.Core.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Piatto> Piatti { get; set; }
    }
}
