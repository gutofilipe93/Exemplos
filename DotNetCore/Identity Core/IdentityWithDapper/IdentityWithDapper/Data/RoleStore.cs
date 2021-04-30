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
    public class RoleStore : IRoleStore<ApplicationRole>
    {

        private readonly string _connectionString;

        public RoleStore(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested(); // caso passo um token de cancelamento a operação(requisição) é cancelada

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                role.Id = await connection.QuerySingleAsync<int>($@"INSERT INTO [dbo].[APPLICATIONROLE]
                                            ([USERNAME]
                                           ,[NORMALIZEDUSERNAME])
                                            VALUES
                                           (@{nameof(ApplicationRole.UserName)},
                                            @{nameof(ApplicationRole.NormalizedUserName)});
                                            SELECT CAST(SCOPE_IDENTITY() AS int)", role);

            }
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($@"DELETE FROM APPLICATIONROLE WHERE ID=@{nameof(ApplicationRole.Id)}", role);
            }
            return IdentityResult.Success;
        }
       
        public async Task<ApplicationRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                return await connection.QuerySingleOrDefaultAsync<ApplicationRole>($@"SELECT * FROM APPLICATIONROLE WHERE ID= @{nameof(roleId)}", new { roleId });
            }
        }

        public async Task<ApplicationRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                return await connection.QuerySingleOrDefaultAsync<ApplicationRole>($@"SELECT * FROM APPLICATIONROLE WHERE NORMALIZEDUSERNAME= @{nameof(normalizedRoleName)}", new { normalizedRoleName });
            }
        }

        public Task<string> GetNormalizedRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedUserName);
        }

        public Task<string> GetRoleIdAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.UserName);
        }

        public Task SetNormalizedRoleNameAsync(ApplicationRole role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetRoleNameAsync(ApplicationRole role, string roleName, CancellationToken cancellationToken)
        {
            role.UserName = roleName;
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested(); // caso passo um token de cancelamento a operação(requisição) é cancelada

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                role.Id = await connection.ExecuteAsync($@"UPDATE [dbo].[APPLICATIONROLE]
                                                           SET [USERNAME] = @{nameof(ApplicationRole.UserName)}
                                                              ,[NORMALIZEDUSERNAME] = @{nameof(ApplicationRole.NormalizedUserName)}
                                                         WHERE ID = @{nameof(ApplicationRole.Id)}", role);

            }
            return IdentityResult.Success;
        }

        public void Dispose()
        {
            //
        }
    }
}
