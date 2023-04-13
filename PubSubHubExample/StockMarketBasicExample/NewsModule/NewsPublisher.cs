using PubSubHub.Pub.Interfaces;
using PubSubHubExample.StockMarketBasicExample.NewsModule;

public class NewsPublisher
{
    private readonly List<News> _newsList;
    private readonly IPublisher _publisher;
    private readonly Random _random = new();
    private Timer _timer;

    public NewsPublisher(IPublisher publisher, List<News> newsList)
    {
        _publisher = publisher;
        _newsList = newsList;
    }

    public void Start()
    {
        _timer = new Timer(PublishNews, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10));
    }

    public void Stop()
    {
        _timer.Dispose();
    }

    private void PublishNews(object state)
    {
        int index = _random.Next(_newsList.Count);
        _publisher.Publish(_newsList[index]);
        Console.WriteLine($"News published: {_newsList[index].Headline}");
    }
}