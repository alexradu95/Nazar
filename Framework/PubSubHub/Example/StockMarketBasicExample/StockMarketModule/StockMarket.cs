﻿using Framework.PubSubHub.Interfaces;

namespace Nazar.PubSubHub.Example.StockMarketBasicExample.StockMarketModule;

public class StockMarket
{
    private readonly IPublisher _publisher;

    public StockMarket(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public void PublishStockUpdate(Stock stock)
    {
        _publisher.Publish(stock);
    }
}