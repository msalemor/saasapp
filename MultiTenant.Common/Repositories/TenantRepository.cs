using MultiTenant.Common.Interfaces;
using MultiTenant.Common.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MultiTenant.Common.Repositories
{
    public class TenantRepository : IRepository<Tenant>
    {

        public string Connection { get; set; }
        public string DbUser { get; set; }
        public string DbPassword { get; set; }

        const string READ_DML = "select * from Tenant";

        public async Task<List<Tenant>> GetAllAsync()
        {
            Connection = Connection ?? ConfigurationManager.ConnectionStrings["catalogdb"].ConnectionString;

            if (Connection == null)
                throw new ArgumentException("Connection property not set.");

            var list = new List<Tenant>();

            using (var connection = new SqlConnection(Connection))
            {
                await connection.OpenAsync();

                // Query the data
                using (var sqlCommand = new SqlCommand(READ_DML, connection))
                {
                    var reader = await sqlCommand.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        var tenant = new Tenant
                        {
                            TenantId = reader[0].ToString(),
                            Server = reader[1].ToString(),
                            Database = reader[2].ToString(),
                            Port = int.Parse(reader[3].ToString()),
                            Created = DateTime.Parse(reader[4].ToString()),
                            Header = reader[5].ToString(),
                            Body = reader[6].ToString(),
                            Footer = reader[7].ToString(),
                            Title = reader[8].ToString()
                        };

                        tenant.ConnectionString = "Server=tcp:{Server}.database.windows.net,1433;Initial Catalog={Database};Persist Security Info=False;User ID={DbUser};Password={DbPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                            .Replace("{Server}", tenant.Server)
                            .Replace("{Database}", tenant.Database)
                            .Replace("{DbUser}", DbUser)
                            .Replace("{DbPassword}",DbPassword);


                        list.Add(tenant);
                    }
                }

                // Close the connection
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }

            return list;
        }
    }
}
