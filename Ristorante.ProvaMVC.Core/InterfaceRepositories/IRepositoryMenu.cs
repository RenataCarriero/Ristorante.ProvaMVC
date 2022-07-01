

using Ristorante.ProvaMVC.Core.Entities;

namespace Ristorante.ProvaMVC.Core.InterfaceRepositories
{
    public interface IRepositoryMenu:IRepository<Menu>
    {
        public Menu GetMenu(int id);
    }
}