using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebShop.Clients.Base;
using WebShop.Domain.DTO.Users;
using WebShop.Domain.Entities;
using WebShop.Interfaces.Services;

namespace WebShop.Clients.Services.Users
{
    public class UserPasswordClient: BaseClient, IUserPasswordStore<User>
    {
        private readonly IUserStore<User> _userStoreClient;

        public UserPasswordClient(IConfiguration configuration, IUserStore<User> userStoreClient) : base(configuration)
        {
            _userStoreClient = userStoreClient;
            ServiceAddress = "api/userpassword";
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

        #region IUserPasswordStore
        public async Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            var url = $"{ServiceAddress}/set";
            await PostAsync(url, new PasswordHashDto() { User = user, Hash = passwordHash });
        }

        public async Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/get";
            var result = await PostAsync(url, user);
            var ret = await result.Content.ReadAsAsync<string>();
            return ret;
        }

        public async Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/has";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>();
        }

        #endregion

    }
}
