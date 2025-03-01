using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace bb_project.DAL.Helpers
{
    internal static class ConnectionHelper
    {
        internal static async Task<R> ConnectAsync<R>(string connectionString, Func<IDbConnection, Task<R>> f, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync(cancellationToken);
                R result = await f(conn);
                cancellationToken.ThrowIfCancellationRequested();
                return result;
            }
        }

    }
}
