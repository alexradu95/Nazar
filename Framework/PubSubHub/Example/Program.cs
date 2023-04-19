using Framework.PubSubHub;
using Framework.PubSubHub.Hub;
using Framework.PubSubHub.Interfaces;
using Nazar.PubSubHub.Example.StockMarketBasicExample.NewsModule;
using Nazar.PubSubHub.Example.StockMarketBasicExample.StockMarketModule;

internal class Program
{
    private static void Main(string[] args)
    {
        // Initialize the publisher and subscriber instances
        IPublisher publisher = new Publisher(MessagingHub.Default);
        ISubscriber subscriber = new Subscriber(MessagingHub.Default);

        // Create a list of stocks
        var stocks = new List<Stock>
        {
            new() {Symbol = "AAPL", Price = 100.00},
            new() {Symbol = "GOOG", Price = 200.00},
            new() {Symbol = "MSFT", Price = 150.00}
        };

        // Initialize the stock market
        StockMarket stockMarket = new(publisher);

        // Create the random stock generator and stock watchers
        RandomStockGenerator stockGenerator = new(stockMarket, stocks);
        StockWatcher watcher1 = new(MessagingHub.Default, new List<string> {"AAPL"});
        StockWatcher watcher2 = new(MessagingHub.Default, new List<string> {"GOOG"});


        // Create the news list
        var newsList = new List<News>
        {
            new()
            {
                Symbol = "AAPL", Headline = "Apple announces new iPhone",
                Content = "Apple has announced the release of the latest iPhone model..."
            },
            new()
            {
                Symbol = "GOOG", Headline = "Google announces new AI breakthrough",
                Content = "Google's AI team has announced a new breakthrough in machine learning..."
            },
            new()
            {
                Symbol = "MSFT", Headline = "Microsoft acquires new gaming company",
                Content = "Microsoft has acquired a new gaming company to expand its gaming portfolio..."
            }
        };

        // Create the news publisher and news watchers
        NewsPublisher newsPublisher = new(publisher, newsList);
        NewsWatcher newsWatcher1 = new(subscriber, "AAPL");
        NewsWatcher newsWatcher2 = new(subscriber, "GOOG");

        // Start the random stock generator, news publisher, and stock watchers
        stockGenerator.Start();
        newsPublisher.Start();

        // Run the program for a while
        // Run the program for a while
        Thread.Sleep(TimeSpan.FromSeconds(60));

        // Stop the random stock generator, news publisher, and stock watchers
        stockGenerator.Stop();
        newsPublisher.Stop();

        watcher1.StopWatching();
        watcher2.StopWatching();

        newsWatcher1.StopWatching();
        newsWatcher2.StopWatching();

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}