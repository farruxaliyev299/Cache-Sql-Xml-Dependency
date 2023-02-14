using SQL_Dependency.Models;
using System.Data;
using System.Data.SqlClient;

namespace SQL_Dependency.Repositories
{
    public class ProductRepository
    {
        string _connectionString;

        public ProductRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public List<Product> GetProducts()
        {
            var data = GetProductsFromDb();

            List<Product> products = new List<Product>();

            foreach (DataRow row in data.Rows)
            {
                Product newProduct = new()
                {
                    ProductID = Convert.ToInt32(row["ProductID"]),
                    ProductName = row["ProductName"].ToString(),
                    QuantityPerUnit = row["QuantityPerUnit"].ToString(),
                    UnitPrice = Convert.ToDecimal(row["UnitPrice"])
                };

                products.Add(newProduct);
            }

            return products;
        }

        public DataTable GetProductsFromDb()
        {
            string query = "select [ProductID], [ProductName], [QuantityPerUnit], [UnitPrice] from Product";

            DataTable dt = new DataTable();

            using(SqlConnection conn = new SqlConnection(this._connectionString))
            {
                conn.Open();
                try
                {
                    using(SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }

                    return dt;
                }
                catch(Exception ex)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
