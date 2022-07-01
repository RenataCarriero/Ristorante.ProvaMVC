using Ristorante.ProvaMVC.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ristorante.ProvaMVC.Core.BusinessLayer
{
    public interface IBusinessLayer
    {
        public List<Piatto> GetAllPiatti();
        public Esito AggiungiPiatto(Piatto p);
        public Esito ModificaPiatto(Piatto p);
        public Esito RimuoviPiatto(Piatto p);

        public List<Piatto> GetAllPiattiByMenu(Menu m);

        public List<Menu> GetAllMenu();
        public Esito AggiungiMenu(Menu m);
        public Esito ModificaMenu(Menu m);
        public Esito RimuoviMenu(Menu m);
        public Esito AggiungiPiattoMenu(Piatto p, Menu m);
        public Esito RimuoviPiattoMenu(Piatto p, Menu m);
        public Utente GetAccount(string username);

    }
}
