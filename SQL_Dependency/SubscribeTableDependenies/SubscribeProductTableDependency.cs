using SQL_Dependency.Hubs;
using SQL_Dependency.Models;
using SQL_Dependency.Repositories;
using TableDependency.SqlClient;

namespace SQL_Dependency.SubscribeTableDependenies;

public class SubscribeProductTableDependency
{
    SqlTableDependency<Product> _tableDependency;

    ProductRepository _productRepository;

    ProductHub _productHub;

    public SubscribeProductTableDependency(IConfiguration config, ProductHub productHub)
    {
        _productHub = productHub;
        _productRepository = new ProductRepository(config.GetConnectionString("Default"));
    }

    public void SubscibeTableDependency()
    {
        string connectionStr = "Server=DESKTOP-MLES57C;Database=MarketDB;Trusted_Connection=true;TrustServerCertificate=true";
        _tableDependency = new SqlTableDependency<Product>(connectionStr);

        _tableDependency.OnChanged += TableDependency_OnChange;
        _tableDependency.OnError += TableDependency_OnError;
        _tableDependency.Start();

    }

    public void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
    {
        Console.WriteLine($"SqlDependecy error: ${e.Error.Message}");
    }

    public void TableDependency_OnChange(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Product> e)
    {
        if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
        {
            _productRepository.ProductsToXml();
            Console.WriteLine("SqlDependency change: Products are sending");
        }
    }
}
