
using Microsoft.EntityFrameworkCore;
using Ristorante.ProvaMVC.Core.Entities;
using Ristorante.ProvaMVC.Core.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ristorante.ProvaMVC.Core.RepositoryEF.RepositoryEF
{
    public class RepositoryMenuEF : IRepositoryMenu
    {
        private readonly Context ctx;

        public RepositoryMenuEF(Context context)
        {
            ctx = context;
        }
        public Menu Add(Menu item)
        {
            ctx.Menu.Add(item);
            ctx.SaveChanges();
            return item;
        }

        public bool Delete(Menu item)
        {
            ctx.Menu.Remove(item);
            ctx.SaveChanges();
            return true;
        }

        public List<Menu> GetAll()
        {
            return ctx.Menu.Include(x => x.Piatti).ToList();
        }

        public Menu GetMenu(int id)
        {
            return ctx.Menu.Include(x => x.Piatti).FirstOrDefault(x => x.Id == id);
        }

        public Menu Update(Menu item)
        {
            ctx.Menu.Update(item);
            ctx.SaveChanges();
            return item;
        }
    }
}
