using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using LINQExample_Northewind.Models;
using System.Net;

namespace LINQExample_Northwind.DAL
{
    public class OrderDAL
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public OrderDAL(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._connectionString = this._configuration.GetConnectionString("Default");
        }
        public List<Order> GetAllOrder(int count = 1000)
        {
            var lstOrder = new List<Order>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    // using stored procedure
                    SqlCommand cmd = new SqlCommand($"EXEC GetOrderInfo @Count = {count}", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        lstOrder.Add(new Order
                        {
                            OrderID = rdr.IsDBNull(rdr.GetOrdinal("OrderID"))?0:rdr.GetInt32(rdr.GetOrdinal("OrderID")),
                            CustomerID = rdr.IsDBNull(rdr.GetOrdinal("CustomerID"))?"":rdr.GetString(rdr.GetOrdinal("CustomerID")),
                            EmployeeID = rdr.IsDBNull(rdr.GetOrdinal("EmployeeID"))?0:rdr.GetInt32(rdr.GetOrdinal("EmployeeID")),
                            OrderDate = rdr.IsDBNull(rdr.GetOrdinal("OrderDate"))?DateTime.MinValue:rdr.GetDateTime(rdr.GetOrdinal("OrderDate")),
                            RequiredDate = rdr.IsDBNull(rdr.GetOrdinal("RequiredDate"))? DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("RequiredDate")),
                            ShippedDate = rdr.IsDBNull(rdr.GetOrdinal("ShippedDate"))? DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("ShippedDate")),
                            ShipVia = rdr.IsDBNull(rdr.GetOrdinal("ShipVia"))?0:rdr.GetInt32(rdr.GetOrdinal("ShipVia")),
                            Freight = rdr.IsDBNull(rdr.GetOrdinal("Freight"))?0:rdr.GetDecimal(rdr.GetOrdinal("Freight")),
                            ShipName = rdr.IsDBNull(rdr.GetOrdinal("ShipName"))?"":rdr.GetString(rdr.GetOrdinal("ShipName")),
                            ShipAddress = rdr.IsDBNull(rdr.GetOrdinal("ShipAddress"))?"":rdr.GetString(rdr.GetOrdinal("ShipAddress")),
                            ShipCity = rdr.IsDBNull(rdr.GetOrdinal("ShipCity"))?"":rdr.GetString(rdr.GetOrdinal("ShipCity")),
                            ShipRegion = rdr.IsDBNull(rdr.GetOrdinal("ShipRegion"))?"":rdr.GetString(rdr.GetOrdinal("ShipRegion")),
                            ShipPostalCode = rdr.IsDBNull(rdr.GetOrdinal("ShipPostalCode"))?"":rdr.GetString(rdr.GetOrdinal("ShipPostalCode")),
                            ShipCountry = rdr.IsDBNull(rdr.GetOrdinal("ShipCountry"))?"":rdr.GetString(rdr.GetOrdinal("ShipCountry")),
                        });
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
            return lstOrder;
        }
    }
}
