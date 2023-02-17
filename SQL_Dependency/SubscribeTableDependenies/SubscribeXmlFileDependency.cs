using SQL_Dependency.Hubs;
using SQL_Dependency.Repositories;

namespace SQL_Dependency.SubscribeTableDependenies;

public class SubscribeXmlFileDependency
{
    ProductRepository _productRepository;

    private readonly ProductHub _productHub;

    public SubscribeXmlFileDependency(IConfiguration config, ProductHub productHub)
	{
        this._productRepository = new ProductRepository(config.GetConnectionString("Default"));
        this._productHub = productHub;
    }

    public void SubscribeXmlDependency()
    {
        FileSystemWatcher watcher = new FileSystemWatcher();

        watcher.Path = Directory.GetCurrentDirectory();

        watcher.Filter = "*.xml";

        watcher.Changed += XmlDependency_OnChange;

        watcher.IncludeSubdirectories = true;

        watcher.EnableRaisingEvents = true;

    }

    private void XmlDependency_OnChange(object sender, FileSystemEventArgs e)
    {
        _productHub.SendProducts();
        Console.WriteLine("Changes in xml file detected");
    }
}
