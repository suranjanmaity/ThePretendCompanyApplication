using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using LINQExample_Northewind.Models;

namespace LINQExample_Northwind.DAL
{
    public class CustomerDAL
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public CustomerDAL(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._connectionString = this._configuration.GetConnectionString("Default");
        }
        public List<Customer> GetAllCustomer()
        {
            var lstCustomer = new List<Customer>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [Task].[dbo].[Customers]", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        lstCustomer.Add(new Customer
                        {
                            CustomerID = rdr.IsDBNull(rdr.GetOrdinal("CustomerID")) ? string.Empty : rdr.GetString(rdr.GetOrdinal("CustomerID")),
                            CompanyName = rdr.IsDBNull(rdr.GetOrdinal("CompanyName")) ? string.Empty : rdr.GetString(rdr.GetOrdinal("CompanyName")),
                            ContactName = rdr.IsDBNull(rdr.GetOrdinal("ContactName")) ? string.Empty : rdr.GetString(rdr.GetOrdinal("ContactName")),
                            ContactTitle = rdr.IsDBNull(rdr.GetOrdinal("ContactTitle")) ? string.Empty : rdr.GetString(rdr.GetOrdinal("ContactTitle")),
                            Address = rdr.IsDBNull(rdr.GetOrdinal("Address")) ? string.Empty : rdr.GetString(rdr.GetOrdinal("Address")),
                            City = rdr.IsDBNull(rdr.GetOrdinal("City")) ? string.Empty : rdr.GetString(rdr.GetOrdinal("City")),
                            Region = rdr.IsDBNull(rdr.GetOrdinal("Region")) ? string.Empty : rdr.GetString(rdr.GetOrdinal("Region")),
                            PostalCode = rdr.IsDBNull(rdr.GetOrdinal("PostalCode")) ? string.Empty : rdr.GetString(rdr.GetOrdinal("PostalCode")),
                            Country = rdr.IsDBNull(rdr.GetOrdinal("Country")) ? string.Empty : rdr.GetString(rdr.GetOrdinal("Country")),
                            Phone = rdr.IsDBNull(rdr.GetOrdinal("Phone")) ? string.Empty : rdr.GetString(rdr.GetOrdinal("Phone")),
                            Fax = rdr.IsDBNull(rdr.GetOrdinal("Fax")) ? string.Empty : rdr.GetString(rdr.GetOrdinal("Fax"))
                        });
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
            return lstCustomer;
        }
    }
}
