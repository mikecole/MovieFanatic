﻿using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using MovieFanatic.Data.Extensions.Visitor;
using MovieFanatic.Utility;

namespace MovieFanatic.Data.Extensions.Interceptors
{
    public class SoftDeleteInterceptor : IDbCommandTreeInterceptor
    {
        public void TreeCreated(DbCommandTreeInterceptionContext interceptionContext)
        {
            if (interceptionContext.OriginalResult.DataSpace == DataSpace.SSpace)
            {
                var queryCommand = interceptionContext.Result as DbQueryCommandTree;
                if (queryCommand != null)
                {
                    var context = (DataContext)interceptionContext.DbContexts.Single(c => c is DataContext);

                    if (context.SoftDeleteFilterIsActive)
                    {
                        var newQuery = queryCommand.Query.Accept(new SoftDeleteQueryVisitor());
                        interceptionContext.Result = new DbQueryCommandTree(
                            queryCommand.MetadataWorkspace,
                            queryCommand.DataSpace,
                            newQuery);
                    }
                }

                var deleteCommand = interceptionContext.OriginalResult as DbDeleteCommandTree;
                if (deleteCommand != null)
                {
                    var column = SoftDeleteAttribute.GetSoftDeleteColumnName(deleteCommand.Target.VariableType.EdmType);
                    if (column != null)
                    {
                        var setClause =
                            DbExpressionBuilder.SetClause(
                                    DbExpressionBuilder.Property(
                                        DbExpressionBuilder.Variable(deleteCommand.Target.VariableType, deleteCommand.Target.VariableName),
                                        column),
                                    DbExpression.FromBoolean(true));

                        var update = new DbUpdateCommandTree(
                            deleteCommand.MetadataWorkspace,
                            deleteCommand.DataSpace,
                            deleteCommand.Target,
                            deleteCommand.Predicate,
                            new List<DbModificationClause> { setClause }.AsReadOnly(),
                            null);

                        interceptionContext.Result = update;
                    }
                }
            }
        }
    }
}