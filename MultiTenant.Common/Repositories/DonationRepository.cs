using MultiTenant.Common.Interfaces;
using MultiTenant.Common.Models.App;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenant.Common.Repositories
{
    public class DonationRepository : IRepository<Donation>
    {
        public string Connection { get; set; }
        public string DbUser { get; set; }
        public string DbPassword { get; set; }

        public const string READ_DML = "select * from Donation";

        public async Task<List<Donation>> GetAllAsync()
        {
            if (Connection == null)
                throw new ArgumentException("Connection property not set.");

            var list = new List<Donation>();

            using (var connection = new SqlConnection(Connection))
            {
                await connection.OpenAsync();

                // Query the data
                using (var sqlCommand = new SqlCommand(READ_DML, connection))
                {
                    var reader = await sqlCommand.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        list.Add(new Donation
                        {
                            DonationId = int.Parse(reader[0].ToString()),
                            Name = reader[1].ToString(),
                            Amount = double.Parse(reader[2].ToString()),
                            Created = DateTime.Parse(reader[3].ToString())
                        });
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
