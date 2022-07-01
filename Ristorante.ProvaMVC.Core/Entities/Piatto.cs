using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ristorante.ProvaMVC.Core.Entities
{
    public class Piatto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descrizione { get; set; }
        public Tipologia Tipologia { get; set; }
        public double Prezzo { get; set; }
        public int? IdMenu { get; set; }
        public Menu? Menu { get; set; }
    }

    public enum Tipologia
    {
        Primo,
        Secondo,
        Contorno,
        Dolce
    }
}
