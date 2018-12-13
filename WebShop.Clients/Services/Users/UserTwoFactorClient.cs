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
    public class UserTwoFactorClient: BaseClient, IUserTwoFactorStore<User>
    {
        private readonly IUserStoreClient _userStoreClient;

        public UserTwoFactorClient(IConfiguration configuration, IUserStoreClient userStoreClient) : base(configuration)
        {
            _userStoreClient = userStoreClient;
            ServiceAddress = "api/usertwofactor";
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

        #region IUserTwoFactorStore
        public Task SetTwoFactorEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
            var url = $"{ServiceAddress}/{enabled}";
            return PostAsync(url, user);
        }

        public async Task<bool> GetTwoFactorEnabledAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>();
        }

        #endregion

    }
}
