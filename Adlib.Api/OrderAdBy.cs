namespace Adlib.Api
{
    public enum OrderAdBy
    {
        // TODO: possibly change names to NewestToOldest, LowestToHighestPrice etc
        Time,
        Price,
        TimeDesc,
        TimeAsc,
        PriceDesc,
        PriceAsc,
        TimeAndPriceDesc,
        TimeAndPriceAsc,
        TimeDescPriceAsc,
        TimeAscPriceDesc
    }
}