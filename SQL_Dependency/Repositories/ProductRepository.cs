using SQL_Dependency.Models;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Serialization;

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

        public void ProductsToXml()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Product>));
            var products = GetProducts();

            try
            {
                using (var writer = new StreamWriter(@"C:\C#\ASP.NET\MVC\SQL_Dependency\SQL_Dependency\bin\Debug\net7.0\products.xml"))
                {
                    serializer.Serialize(writer, products);
                }

                Console.WriteLine("Products serialized");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetProductsFromDb()
        {
            string query = "select [ProductID], [ProductName], [QuantityPerUnit], [UnitPrice] from Product";

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(this._connectionString))
            {
                conn.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                    dt.TableName = "Product";
                    return dt;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public string GetXml(string path)
        {
            var xml = new XmlDocument();
            using (StreamReader sr = new StreamReader(path))
            {
                xml.LoadXml(sr.ReadToEnd());
            }
            return xml.DocumentElement.InnerXml;
        }

        public void WriteXml(string str, string path)
        {
            using(StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(str);
            }
        }
    }
}
