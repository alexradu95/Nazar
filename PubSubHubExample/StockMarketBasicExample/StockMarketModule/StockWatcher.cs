using PubSubHub.Sub.Interfaces;
using PubSub.Subscriber;
using PubSubHub.Hub;
using PubSubHubExample.StockMarketBasicExample.StockMarketModule;

public class StockWatcher
{
    private readonly ISubscriber _subscriber;
    private readonly HashSet<string> _symbols;

    public StockWatcher(Hub hub, IEnumerable<string> symbols)
    {
        _subscriber = new Subscriber(hub);
        _symbols = new HashSet<string>(symbols);
        _subscriber.Subscribe<Stock>(this, OnStockUpdate);
    }

    private void OnStockUpdate(Stock stock)
    {
        if (_symbols.Contains(stock.Symbol))
        {
            Console.WriteLine($"Watcher for {_symbols}: Stock update: {stock.Symbol} - {stock.Price}");
        }
    }

    public void StopWatching()
    {
        _subscriber.Unsubscribe<Stock>(this);
    }
}
