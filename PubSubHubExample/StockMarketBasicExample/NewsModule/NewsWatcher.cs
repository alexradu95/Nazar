namespace PubSubHubExample.StockMarketBasicExample.NewsModule;

using System;
using PubSubHub.Hub;
using PubSubHub.Sub.Interfaces;

public class NewsWatcher
{
    private readonly ISubscriber _subscriber;

    public NewsWatcher(ISubscriber subscriber, string symbol)
    {
        _subscriber = subscriber;
        _subscriber.Subscribe<News>(this, news =>
        {
            if (news.Symbol == symbol)
            {
                Console.WriteLine($"News for {symbol}: {news.Headline}");
            }
        });
    }

    public void StopWatching()
    {
        _subscriber.Unsubscribe<News>(this);
    }
}


