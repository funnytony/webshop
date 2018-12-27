using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Interfaces.Clients
{
    public interface IValuesService
    {
        IEnumerable<string> Get();

        Task<IEnumerable<string>> GetAsync();

        string Get(int id);

        Task<string> GetAsync(int id);

        HttpStatusCode Post(string value);

        Task<HttpStatusCode> PostAsync(string value);

        HttpStatusCode Put(int id, string value);

        Task<HttpStatusCode> PutAsync(int id, string value);

        HttpStatusCode Delete(int id);

        Task<HttpStatusCode> DeleteAsync(int id);

    }
}
