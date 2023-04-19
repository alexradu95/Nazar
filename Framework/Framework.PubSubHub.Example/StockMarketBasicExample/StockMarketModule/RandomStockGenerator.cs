namespace Nazar.PubSubHub.Example.StockMarketBasicExample.StockMarketModule;

public class RandomStockGenerator
{
    private readonly Random _random = new();
    private readonly StockMarket _stockMarket;
    private readonly List<Stock> _stocks;
    private bool _running = true;

    public RandomStockGenerator(StockMarket stockMarket, List<Stock> stocks)
    {
        _stockMarket = stockMarket;
        _stocks = stocks;
    }

    public void Start()
    {
        Thread thread = new(GenerateStockUpdates);
        thread.Start();
    }

    private void GenerateStockUpdates()
    {
        while (_running)
        {
            int stockIndex = _random.Next(_stocks.Count);
            Stock stock = _stocks[stockIndex];
            stock.Price = Math.Round(stock.Price + _random.NextDouble() * 2 - 1, 2);
            _stockMarket.PublishStockUpdate(stock);

            Thread.Sleep(_random.Next(1000, 5000));
        }
    }

    public void Stop()
    {
        _running = false;
    }
}