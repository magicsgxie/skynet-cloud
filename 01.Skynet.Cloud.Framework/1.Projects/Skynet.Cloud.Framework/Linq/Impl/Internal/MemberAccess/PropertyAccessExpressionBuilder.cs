namespace UWay.Skynet.Cloud.Linq.Impl.Internal
{
    using System;
    using System.Linq.Expressions;

    internal class PropertyAccessExpressionBuilder : MemberAccessExpressionBuilderBase
    {
        public PropertyAccessExpressionBuilder(Type itemType, string memberName) : base(itemType, memberName)
        {
        }

        public override Expression CreateMemberAccessExpression()
        {
            //if no property specified then return the item itself
            if (string.IsNullOrEmpty(MemberName))
            {
                return this.ParameterExpression;
            }

            return ExpressionFactory.MakeMemberAccess(ParameterExpression, MemberName, Options.LiftMemberAccessToNull);
        }
    }
}
