using Dapper;
using IdentityWithDapper.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityWithDapper.Data
{
    public class UserStore : 
        IUserStore<ApplicationUser>,
        IUserEmailStore<ApplicationUser>,
        IUserPhoneNumberStore<ApplicationUser>,
        IUserTwoFactorStore<ApplicationUser>,
        IUserPasswordStore<ApplicationUser>,
        IUserRoleStore<ApplicationUser>
    {
        private readonly string _connectionString;

        public UserStore(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested(); // caso passo um token de cancelamento a operação(requisição) é cancelada

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                var query = $@"INSERT INTO [dbo].[APPLICATIONUSER]
                                           ([USERNAME]
                                           ,[NORMALIZEDUSERNAME]
                                           ,[EMAIL]
                                           ,[NORMALIZEDEMAIL]
                                           ,[EMAILCONFIRMED]
                                           ,[PASSWORDHASH]
                                           ,[PHONENUMBER]
                                           ,[PHONENUMBERCONFIRMED]
                                           ,[TWOFACTORENABLED])
                                     VALUES
                                           (
                                           @{nameof(ApplicationUser.UserName)} ,
                                           @{nameof(ApplicationUser.NormalizedUserName)} ,
                                           @{nameof(ApplicationUser.Email)} ,
                                           @{nameof(ApplicationUser.NormalizedEmail)} ,
                                           @{nameof(ApplicationUser.EmailConfirmed)} ,
                                           @{nameof(ApplicationUser.PasswordHash)} ,
                                           @{nameof(ApplicationUser.PhoneNumber)} ,
                                           @{nameof(ApplicationUser.PhoneNumberConfirmed)} ,
                                           @{nameof(ApplicationUser.TwoFactorEnabled)} );
                                           SELECT CAST(SCOPE_IDENTITY() AS int)";

                user.Id = await connection.ExecuteScalarAsync<int>(query, user);

            }
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($@"DELETE FROM APPLICATIONUSER WHERE ID=@{nameof(ApplicationUser.Id)}", user);
            }
            return IdentityResult.Success;
        }

        
        public async Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                return await connection.QuerySingleOrDefaultAsync<ApplicationUser>($@"SELECT * FROM APPLICATIONUSER WHERE NORMALIZEDEMAIL= @{nameof(normalizedEmail)}", new { normalizedEmail });
            }
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                return await connection.QuerySingleOrDefaultAsync<ApplicationUser>($@"SELECT * FROM APPLICATIONUSER WHERE ID= @{nameof(userId)}", new { userId });
            }
            
        }

        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                return await connection.QuerySingleOrDefaultAsync<ApplicationUser>($@"SELECT * FROM APPLICATIONUSER WHERE NORMALIZEDUSERNAME= @{nameof(normalizedUserName)}", new { normalizedUserName });
            }
        }

        public Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetPhoneNumberAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberAsync(ApplicationUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($@"UPDATE [dbo].[APPLICATIONUSER]
                                                               SET [USERNAME] = @{nameof(ApplicationUser.UserName)} ,
                                                                  ,[NORMALIZEDUSERNAME] = @{nameof(ApplicationUser.NormalizedUserName)} ,
                                                                  ,[EMAIL] = @{nameof(ApplicationUser.Email)} ,
                                                                  ,[NORMALIZEDEMAIL] = @{nameof(ApplicationUser.NormalizedEmail)} ,
                                                                  ,[EMAILCONFIRMED] = @{nameof(ApplicationUser.EmailConfirmed)} ,
                                                                  ,[PASSWORDHASH] = @{nameof(ApplicationUser.PasswordHash)} ,
                                                                  ,[PHONENUMBER] = @{nameof(ApplicationUser.PhoneNumber)} ,
                                                                  ,[PHONENUMBERCONFIRMED] = @{nameof(ApplicationUser.PhoneNumberConfirmed)} ,
                                                                  ,[TWOFACTORENABLED] = @{nameof(ApplicationUser.TwoFactorEnabled)} ,
                                                             WHERE ID = @{nameof(ApplicationUser.Id)}", user);
            }
            return IdentityResult.Success;
        }



        public async Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var normalizedName = roleName.ToUpper();
                var roleId = await connection.ExecuteScalarAsync<int?>($@"SELECT ID FROM APPLICATIONROLE WHERE [NORMALIZEDUSERNAME] = @{nameof(normalizedName)}", new { normalizedName });
                if (!roleId.HasValue)
                {
                    roleId = await connection.ExecuteAsync($@"INSERT INTO [dbo].[APPLICATIONROLE]
                                                                   ([USERNAME]
                                                                   ,[NORMALIZEDUSERNAME])
                                                                    VALUES
                                                                   (@{nameof(roleName)} ,
                                                                    @{nameof(normalizedName)})"
                                                                    , new { roleName, normalizedName});
                }

                await connection.ExecuteAsync($@"IF NOT EXISTS(SELECT 1 FROM APPLICATIONUSERROLE WHERE USERID = @userId AND ROLEID = @roleId) " +
                                                 @"INSERT INTO [dbo].[APPLICATIONUSERROLE]([USERID],[ROLEID]) VALUES (@userId,@roleId),",
                                                 new { userId = user.Id, roleId});
            }
            
        }

        public async Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var normalizedName = roleName.ToUpper();
                var roleId = await connection.ExecuteScalarAsync<int?>($@"SELECT ID FROM APPLICATIONROLE WHERE [NORMALIZEDUSERNAME] = @{nameof(normalizedName)}", new { normalizedName });
                if (!roleId.HasValue)
                {
                    await connection.ExecuteAsync($@"DELETE FROM APPLICATIONUSERROLE WHERE USERID = @userId AND ROLEID = @roleId", new { userId = user.Id, roleId});
                }
            }
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var queryResult = await connection.QueryAsync<string>(@"SELECT R.[USERNAME] FROM APPLICATIONROLE R 
                                                                        INNER JOIN APPLICATIONUSERROLE UR ON UR.ROLEID = R.ID
                                                                        WHERE UR.USERID = @userId", new { userId = user.Id });

                return queryResult.ToList();               
            }
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var normalizedName = roleName.ToUpper();
                var roleId = await connection.ExecuteScalarAsync<int?>($@"SELECT ID FROM APPLICATIONROLE WHERE [NORMALIZEDUSERNAME] = @{nameof(normalizedName)}", new { normalizedName });
                if (roleId == default(int)) return false;

                var matchingRoles = await connection.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM APPLICATIONUSERROLE WHERE USERID = @userId AND ROLEID = @roleId", new { userId = user.Id, roleId });

                return matchingRoles > 0;
                
                
            }
        }

        public async Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var queryResult = await connection.QueryAsync<ApplicationUser>(@"SELECT U.* FROM APPLICATIONUSER U
                                                                        INNER JOIN APPLICATIONUSERROLE UR ON UR.USERID = U.ID
                                                                        INNER JOIN APPLICATIONROLE R ON R.ID = UR.ROLEID    
                                                                        WHERE R.[NORMALIZEDUSERNAME] = @roleName", new { roleName = roleName.ToUpper() });

                return queryResult.ToList();
            }
        }

        public void Dispose()
        {
            // nada
        }
    }
}
