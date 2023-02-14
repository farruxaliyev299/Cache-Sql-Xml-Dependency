using Microsoft.AspNetCore.SignalR;
using SQL_Dependency.Repositories;

namespace SQL_Dependency.Hubs
{
    public class ProductHub : Hub
    {
        private ProductRepository _productRepository { get; set; }

        public ProductHub(IConfiguration configuration)
        {
            _productRepository = new ProductRepository(configuration.GetConnectionString("Default"));
        }

        public async Task SendProducts()
        {
            var products = _productRepository.GetProducts();

            await Clients.All.SendAsync("RecieveProducts", products);
        }


    }
}
