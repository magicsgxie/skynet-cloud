namespace UWay.Skynet.Cloud.Data.Dialect
{
    public interface IDialect
    {
        bool SupportMultipleCommands { get; }
        bool SupportDistinctInAggregates { get; }
        bool SupportSubqueryInSelectWithoutFrom { get; }
        char OpenQuote { get; }
        char CloseQuote { get; }
        string Quote(string name);
        bool SupportDelete { get; }
        bool SupportInsert { get; }
        bool SupportSchema { get; }
        bool SupportSelect { get; }
        bool SupportUpdate { get; }
    }
}
