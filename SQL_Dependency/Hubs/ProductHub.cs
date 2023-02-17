using Microsoft.AspNetCore.SignalR;
using SQL_Dependency.Models;
using SQL_Dependency.Repositories;
using System.Xml.Serialization;

namespace SQL_Dependency.Hubs
{
    public class ProductHub : Hub
    {
        private ProductRepository _productRepository { get; set; }
        private XmlSerializer _serializer { get; set; }

        public ProductHub(IConfiguration configuration)
        {
            _productRepository = new ProductRepository(configuration.GetConnectionString("Default"));
            _serializer = new XmlSerializer(typeof(List<Product>));


            //FileSystemWatcher watcher = new FileSystemWatcher();

            //watcher.Path = Directory.GetCurrentDirectory();

            //watcher.Filter = "*.xml";

            //watcher.Changed += XmlDependency_OnChange;

            //watcher.IncludeSubdirectories = true;

            //watcher.EnableRaisingEvents = true;
        }

        //private void XmlDependency_OnChange(object sender, FileSystemEventArgs e)
        //{
        //    SendProducts();
        //    Console.WriteLine("Changes in xml file detected");
        //}

        public async Task SendProducts()
        {
            //_productRepository.ProductsToXml();


            object products = new();
            using (FileStream fs = new FileStream(@"C:\C#\ASP.NET\MVC\SQL_Dependency\SQL_Dependency\bin\Debug\net7.0\products.xml", FileMode.Open, FileAccess.Read))
            {
                try
                {
                    products = _serializer.Deserialize(fs);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            await Clients.All.SendAsync("RecieveProducts", products);
        }



        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }


    }
}
