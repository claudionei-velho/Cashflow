using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Extensions {
  public class ReplaceParameterVisitor<TResult> : ExpressionVisitor {
    private readonly ParameterExpression _parameter;
    private readonly Expression _replacement;

    public ReplaceParameterVisitor(ParameterExpression parameter, Expression replacement) {
      _parameter = parameter;
      _replacement = replacement;
    }

    public Expression<TResult> Visit<T>(Expression<T> node) {
      IEnumerable<ParameterExpression> parameters = node.Parameters.Where(p => p != _parameter);
      return Expression.Lambda<TResult>(Visit(node.Body), parameters);
    }

    protected override Expression VisitParameter(ParameterExpression node) {
      return node == _parameter ? _replacement : base.VisitParameter(node);
    }
  }
}
