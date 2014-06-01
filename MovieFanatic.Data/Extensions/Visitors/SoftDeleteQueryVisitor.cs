﻿using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using MovieFanatic.Utility;

namespace MovieFanatic.Data.Extensions.Visitors
{
    public class SoftDeleteQueryVisitor : DefaultExpressionVisitor
    {
        public override DbExpression Visit(DbScanExpression expression)
        {
            var column = SoftDeleteAttribute.GetSoftDeleteColumnName(expression.Target.ElementType);
            if (column != null)
            {
                var binding = DbExpressionBuilder.Bind(expression);
                return DbExpressionBuilder.Filter(
                    binding,
                    DbExpressionBuilder.NotEqual(
                        DbExpressionBuilder.Property(
                            DbExpressionBuilder.Variable(binding.VariableType, binding.VariableName),
                            column),
                        DbExpression.FromBoolean(true)));
            }

            return base.Visit(expression);
        }
    }
}