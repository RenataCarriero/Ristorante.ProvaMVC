using Ristorante.ProvaMVC.Core.Entities;

namespace Ristorante.ProvaMVC.Core.InterfaceRepositories
{
    public interface IRepositoryUtenti:IRepository<Utente>
    {
        public Utente GetByUsername(string username);
    }
}