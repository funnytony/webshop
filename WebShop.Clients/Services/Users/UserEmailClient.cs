using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebShop.Clients.Base;
using WebShop.Domain.Entities;
using WebShop.Interfaces.Services;

namespace WebShop.Clients.Services.Users
{
    public class UserEmailClient: BaseClient, IUserEmailStore<User>
    {

        private readonly IUserStore<User> _userStoreClient;

        public UserEmailClient(IConfiguration configuration, IUserStore<User> userStoreClient) : base(configuration)
        {
            _userStoreClient = userStoreClient;
            ServiceAddress = "api/useremail";
        }

        protected sealed override string ServiceAddress { get; set; }

        public void Dispose()
        {
            Client.Dispose();
        }

        #region IUserStore
        public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return await _userStoreClient.GetUserIdAsync(user, cancellationToken);
        }

        public async Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return await _userStoreClient.GetUserNameAsync(user, cancellationToken);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            return _userStoreClient.SetUserNameAsync(user, userName, cancellationToken);
        }

        public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return await _userStoreClient.GetNormalizedUserNameAsync(user, cancellationToken);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            return _userStoreClient.SetNormalizedUserNameAsync(user, normalizedName, cancellationToken);
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            return await _userStoreClient.CreateAsync(user, cancellationToken);
        }


        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            return await _userStoreClient.UpdateAsync(user, cancellationToken);
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            return await _userStoreClient.DeleteAsync(user, cancellationToken);
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await _userStoreClient.FindByIdAsync(userId, cancellationToken);
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await _userStoreClient.FindByNameAsync(normalizedUserName, cancellationToken);
        }

        #endregion

        #region IUserEmailStore

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            var url = $"{ServiceAddress}/{email}";
            return PostAsync(url, user);
        }

        public async Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>();
        }

        public async Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/confirmed";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>();
        }
        

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            var url = $"{ServiceAddress}/confirmed/{confirmed}";
            return PostAsync(url, user);
        }

        public Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/{normalizedEmail}";
            return GetAsync<User>(url);
        }

        public async Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/normalized";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>();
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            var url = $"{ServiceAddress}/normalized/{normalizedEmail}";
            return PostAsync(url, user);
        }

        #endregion

    }
}
