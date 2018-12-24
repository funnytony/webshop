using Microsoft.AspNetCore.Identity;
using WebShop.Domain.Entities;

namespace WebShop.Interfaces.Services
{
    public interface IUserStoreClient: IUserStore<User>
    {
    }
}
