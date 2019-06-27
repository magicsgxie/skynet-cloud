namespace UWay.Skynet.Cloud.Linq.Impl
{ 
    using System.Collections.Generic;
    public static class FilterTokenExtensions
    {
        private static readonly IDictionary<string, FilterOperator> tokenToOperator = new Dictionary<string, FilterOperator>
        {
            { "eq", FilterOperator.IsEqualTo },
            { "neq", FilterOperator.IsNotEqualTo },
            { "lt", FilterOperator.IsLessThan },
            { "lte", FilterOperator.IsLessThanOrEqualTo },
            { "gt", FilterOperator.IsGreaterThan },
            { "gte", FilterOperator.IsGreaterThanOrEqualTo },
            { "startswith", FilterOperator.StartsWith },
            { "contains", FilterOperator.Contains },
            // js 中 DoesNotContain 包含两种
            { "notsubstringof", FilterOperator.DoesNotContain },
            { "doesnotcontain", FilterOperator.DoesNotContain },
            { "endswith", FilterOperator.EndsWith },
            { "in", FilterOperator.In}
        };

        private static readonly IDictionary<FilterOperator, string> operatorToToken = new Dictionary<FilterOperator, string>
        {
            { FilterOperator.IsEqualTo, "eq" },
            { FilterOperator.IsNotEqualTo, "neq" },
            { FilterOperator.IsLessThan, "lt" },
            { FilterOperator.IsLessThanOrEqualTo, "lte" },
            { FilterOperator.IsGreaterThan, "gt" },
            { FilterOperator.IsGreaterThanOrEqualTo, "gte" },
            { FilterOperator.In,"in"},
            { FilterOperator.StartsWith, "startswith" },
            { FilterOperator.Contains, "contains" },
            { FilterOperator.DoesNotContain,"notsubstringof" },
            { FilterOperator.EndsWith, "endswith" }
        };

        public static FilterOperator ToFilterOperator(this FilterToken token)
        {
            return tokenToOperator[token.Value];
        }

        public static string ToToken(this FilterOperator filterOperator)
        {
            return operatorToToken[filterOperator];
        }
    }
}
