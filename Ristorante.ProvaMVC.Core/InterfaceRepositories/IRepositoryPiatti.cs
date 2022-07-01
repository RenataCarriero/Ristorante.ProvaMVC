using Ristorante.ProvaMVC.Core.Entities;

namespace Ristorante.ProvaMVC.Core.InterfaceRepositories { 
    public interface IRepositoryPiatti:IRepository<Piatto>
    {
        public Piatto GetPiatto(int id);
    }
}