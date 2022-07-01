
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
    public class RepositoryPiattiEF : IRepositoryPiatti
    {
        private readonly Context ctx;
        public RepositoryPiattiEF(Context context)
        {
            ctx = context;
        }
        public Piatto Add(Piatto item)
        {
            ctx.Piatti.Add(item);
            ctx.SaveChanges();
            return item;
        }

        public bool Delete(Piatto item)
        {
            ctx.Piatti.Remove(item);
            ctx.SaveChanges();
            return true;
        }

        public List<Piatto> GetAll()
        {
            return ctx.Piatti.Include(p => p.Menu).AsNoTracking().ToList();
        }

        public Piatto GetPiatto(int id)
        {
            return ctx.Piatti.Include(p => p.Menu).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public Piatto Update(Piatto item)
        {
            ctx.Piatti.Update(item);
            ctx.SaveChanges();
            return item;
        }
    }
}
