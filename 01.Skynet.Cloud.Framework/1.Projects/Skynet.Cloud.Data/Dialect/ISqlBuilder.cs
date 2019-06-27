using System;
using System.Linq.Expressions;
namespace UWay.Skynet.Cloud.Data.Dialect
{
    public enum Indentation
    {
        Same,
        Inner,
        Outer
    }

    public interface ISqlBuilder
    {
        ISqlBuilder Append(object value);
        void AppendFormat(string fmt, params object[] args);
        void AppendIndentation();
        void AppendLine(Indentation style);
        void AppendOutdentation();
        ISqlBuilder Do(Action handler);
        int IndentationWidth { get; set; }
        Expression Visit(Expression exp);
        void VisitEnumerable(Expression[] items);
        void VisitEnumerable(Expression[] items, string separetor);
    }
}
