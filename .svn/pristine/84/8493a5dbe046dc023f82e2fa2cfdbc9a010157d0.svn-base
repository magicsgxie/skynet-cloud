using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq
{
    internal sealed class FingerprintingExpressionVisitor : ExpressionVisitor
    {
        // Fields
        private readonly ExpressionFingerprintChain _currentChain = new ExpressionFingerprintChain();
        private bool _gaveUp;
        private readonly List<object> _seenConstants = new List<object>();
        private readonly List<ParameterExpression> _seenParameters = new List<ParameterExpression>();

        // Methods
        private FingerprintingExpressionVisitor()
        {
        }

        public static ExpressionFingerprintChain GetFingerprintChain(Expression expr, out List<object> capturedConstants)
        {
            FingerprintingExpressionVisitor visitor = new FingerprintingExpressionVisitor();
            visitor.Visit(expr);
            if (visitor._gaveUp)
            {
                capturedConstants = null;
                return null;
            }
            capturedConstants = visitor._seenConstants;
            return visitor._currentChain;
        }

        private T GiveUp<T>(T node)
        {
            this._gaveUp = true;
            return node;
        }

        public override Expression Visit(Expression node)
        {
            if (node == null)
            {
                this._currentChain.Elements.Add(null);
                return null;
            }
            return base.Visit(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (this._gaveUp)
            {
                return node;
            }
            this._currentChain.Elements.Add(new BinaryExpressionFingerprint(node.NodeType, node.Type, node.Method));
            return base.VisitBinary(node);
        }

        protected override Expression VisitBlock(BlockExpression node)
        {
            return this.GiveUp<BlockExpression>(node);
        }

        protected override CatchBlock VisitCatchBlock(CatchBlock node)
        {
            return this.GiveUp<CatchBlock>(node);
        }

        protected override Expression VisitConditional(ConditionalExpression node)
        {
            if (this._gaveUp)
            {
                return node;
            }
            this._currentChain.Elements.Add(new ConditionalExpressionFingerprint(node.NodeType, node.Type));
            return base.VisitConditional(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (this._gaveUp)
            {
                return node;
            }
            this._seenConstants.Add(node.Value);
            this._currentChain.Elements.Add(new ConstantExpressionFingerprint(node.NodeType, node.Type));
            return base.VisitConstant(node);
        }

        protected override Expression VisitDebugInfo(DebugInfoExpression node)
        {
            return this.GiveUp<DebugInfoExpression>(node);
        }

        protected override Expression VisitDefault(DefaultExpression node)
        {
            if (this._gaveUp)
            {
                return node;
            }
            this._currentChain.Elements.Add(new DefaultExpressionFingerprint(node.NodeType, node.Type));
            return base.VisitDefault(node);
        }

        protected override Expression VisitDynamic(DynamicExpression node)
        {
            return this.GiveUp<DynamicExpression>(node);
        }

        protected override ElementInit VisitElementInit(ElementInit node)
        {
            return this.GiveUp<ElementInit>(node);
        }

        protected override Expression VisitExtension(Expression node)
        {
            return this.GiveUp<Expression>(node);
        }

        protected override Expression VisitGoto(GotoExpression node)
        {
            return this.GiveUp<GotoExpression>(node);
        }

        protected override Expression VisitIndex(IndexExpression node)
        {
            if (this._gaveUp)
            {
                return node;
            }
            this._currentChain.Elements.Add(new IndexExpressionFingerprint(node.NodeType, node.Type, node.Indexer));
            return base.VisitIndex(node);
        }

        protected override Expression VisitInvocation(InvocationExpression node)
        {
            return this.GiveUp<InvocationExpression>(node);
        }

        protected override Expression VisitLabel(LabelExpression node)
        {
            return this.GiveUp<LabelExpression>(node);
        }

        protected override LabelTarget VisitLabelTarget(LabelTarget node)
        {
            return this.GiveUp<LabelTarget>(node);
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            if (this._gaveUp)
            {
                return node;
            }
            this._currentChain.Elements.Add(new LambdaExpressionFingerprint(node.NodeType, node.Type));
            return base.VisitLambda<T>(node);
        }

        protected override Expression VisitListInit(ListInitExpression node)
        {
            return this.GiveUp<ListInitExpression>(node);
        }

        protected override Expression VisitLoop(LoopExpression node)
        {
            return this.GiveUp<LoopExpression>(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (this._gaveUp)
            {
                return node;
            }
            this._currentChain.Elements.Add(new MemberExpressionFingerprint(node.NodeType, node.Type, node.Member));
            return base.VisitMember(node);
        }

        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            return this.GiveUp<MemberAssignment>(node);
        }

        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            return this.GiveUp<MemberBinding>(node);
        }

        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            return this.GiveUp<MemberInitExpression>(node);
        }

        protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
        {
            return this.GiveUp<MemberListBinding>(node);
        }

        protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
        {
            return this.GiveUp<MemberMemberBinding>(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (this._gaveUp)
            {
                return node;
            }
            this._currentChain.Elements.Add(new MethodCallExpressionFingerprint(node.NodeType, node.Type, node.Method));
            return base.VisitMethodCall(node);
        }

        protected override Expression VisitNew(NewExpression node)
        {
            return this.GiveUp<NewExpression>(node);
        }

        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            return this.GiveUp<NewArrayExpression>(node);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (this._gaveUp)
            {
                return node;
            }
            int index = this._seenParameters.IndexOf(node);
            if (index < 0)
            {
                index = this._seenParameters.Count;
                this._seenParameters.Add(node);
            }
            this._currentChain.Elements.Add(new ParameterExpressionFingerprint(node.NodeType, node.Type, index));
            return base.VisitParameter(node);
        }

        protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
        {
            return this.GiveUp<RuntimeVariablesExpression>(node);
        }

        protected override Expression VisitSwitch(SwitchExpression node)
        {
            return this.GiveUp<SwitchExpression>(node);
        }

        protected override SwitchCase VisitSwitchCase(SwitchCase node)
        {
            return this.GiveUp<SwitchCase>(node);
        }

        protected override Expression VisitTry(TryExpression node)
        {
            return this.GiveUp<TryExpression>(node);
        }

        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            if (this._gaveUp)
            {
                return node;
            }
            this._currentChain.Elements.Add(new TypeBinaryExpressionFingerprint(node.NodeType, node.Type, node.TypeOperand));
            return base.VisitTypeBinary(node);
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (this._gaveUp)
            {
                return node;
            }
            this._currentChain.Elements.Add(new UnaryExpressionFingerprint(node.NodeType, node.Type, node.Method));
            return base.VisitUnary(node);
        }
    }



}
