using Ristorante.ProvaMVC.Core.Entities;
using Ristorante.ProvaMVC.Core.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ristorante.ProvaMVC.Core.BusinessLayer
{
    public class MainBusinessLayer : IBusinessLayer
    {
        private readonly IRepositoryMenu repositoryMenu;
        private readonly IRepositoryPiatti repositoryPiatti;
        private readonly IRepositoryUtenti repositoryUtenti;

        public MainBusinessLayer(IRepositoryMenu repositoryMenu, IRepositoryPiatti repositoryPiatti, IRepositoryUtenti repositoryUtenti)
        {
            this.repositoryMenu = repositoryMenu;
            this.repositoryPiatti = repositoryPiatti;
            this.repositoryUtenti = repositoryUtenti;
        }
        public Esito AggiungiMenu(Menu m)
        {
            Menu exisitingMenu = repositoryMenu.GetMenu(m.Id);
            if(exisitingMenu == null)
            {
                repositoryMenu.Add(m);
                return new Esito { IsOk = true, Messaggio = "Menu aggiunto corretamente" };
            }
            return new Esito { IsOk = false, Messaggio = "Impossibile aggiungere il menu" };
        }

        public Esito AggiungiPiatto(Piatto p)
        {
            Piatto piatto = repositoryPiatti.GetPiatto(p.Id);
            if(piatto == null)
            {
                repositoryPiatti.Add(p);
                return new Esito { IsOk = true, Messaggio = "Piatto aggiunto correttamente" };
            }
            return new Esito { IsOk = false, Messaggio = "Impossibile aggiungere il piatto" };
        }

        public Esito AggiungiPiattoMenu(Piatto p, Menu m)
        {
            Piatto piatto = repositoryPiatti.GetPiatto(p.Id);
            Menu menu = repositoryMenu.GetMenu(m.Id);
            if(menu == null || piatto == null)
            {
                return new Esito { IsOk = false, Messaggio = "Piatto o Menu inesistenti" };
            }
            if (piatto.Menu != null)
            {
                return new Esito { IsOk = false, Messaggio = "Piatto già assegnato" };
            }

            piatto.IdMenu = menu.Id;            
            repositoryPiatti.Update(piatto);
            
            return new Esito { IsOk = true, Messaggio = "Piatto aggiunto al menu" };
        }

        public List<Piatto> GetAllPiatti()
        {
            return repositoryPiatti.GetAll();
        }

        public List<Piatto> GetAllPiattiByMenu(Menu m)
        {
            return repositoryMenu.GetMenu(m.Id).Piatti;
        }

        public Esito ModificaMenu(Menu m)
        {
            Menu menu =repositoryMenu.GetMenu(m.Id);
            if (menu == null)
            {
                return new Esito { IsOk = false, Messaggio = "Nessun Menu trovato" };
            }
            repositoryMenu.Update(menu);
            return new Esito { IsOk = true, Messaggio="Menu aggiornato" };
        }

        public Esito ModificaPiatto(Piatto piatto)
        {
            if(piatto == null)
            {
                return new Esito { IsOk = false, Messaggio = "Nessun Piatto trovato" };
            }
            repositoryPiatti.Update(piatto);
            return new Esito { IsOk = true, Messaggio = "Piatto aggiornato" };
        }

        public Esito RimuoviMenu(Menu m)
        {
            Menu menu = repositoryMenu.GetMenu(m.Id);
            if (menu == null)
            {
                return new Esito { IsOk = false, Messaggio = "Nessun Menu trovato" };
            }
            repositoryMenu.Delete(menu);
            return new Esito { IsOk = true, Messaggio = "Menu rimosso" };
        }

        public Esito RimuoviPiatto(Piatto piatto)
        {
            if (piatto == null)
            {
                return new Esito { IsOk = false, Messaggio = "Nessun Piatto trovato" };
            }
            repositoryPiatti.Delete(piatto);
            return new Esito { IsOk = true, Messaggio = "Piatto rimosso" };
        }
        public Utente GetAccount(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }
            return repositoryUtenti.GetByUsername(username);
        }

        public List<Menu> GetAllMenu()
        {
            return repositoryMenu.GetAll();
        }

        public Esito RimuoviPiattoMenu(Piatto p, Menu m)
        {
            Piatto piatto = repositoryPiatti.GetPiatto(p.Id);
            Menu menu = repositoryMenu.GetMenu(m.Id);
            if (menu == null || piatto == null)
            {
                return new Esito { IsOk = false, Messaggio = "Piatto o Menu inesistenti" };
            }
            if (piatto.Menu == null)
            {
                return new Esito { IsOk = false, Messaggio = "Piatto senza menù" };
            }
            piatto.Menu = null;
            piatto.IdMenu = null;
            menu.Piatti.Remove(piatto);
            repositoryPiatti.Update(piatto);
            repositoryMenu.Update(menu);
            return new Esito { IsOk = true, Messaggio = "Piatto rimosso al menu" };
        }
    }
}
