using System;
using System.Linq.Expressions;

namespace Domain.Extensions {
  public static class Predicate {
    public static Func<bool> True() => () => true;

    public static Func<T, bool> True<T>() => a => true;

    public static Func<T, T2, bool> True<T, T2>() => (a, b) => true;

    public static Func<bool> False() => () => false;

    public static Func<T, bool> False<T>() => a => false;

    public static Func<T, T2, bool> False<T, T2>() => (a, b) => false;

    public static Expression<Func<bool>> TrueExpression() => () => true;

    public static Expression<Func<T, bool>> TrueExpression<T>() => a => true;

    public static Expression<Func<T, T2, bool>> TrueExpression<T, T2>() => (a, b) => true;

    public static Expression<Func<bool>> FalseExpression() => () => false;

    public static Expression<Func<T, bool>> FalseExpression<T>() => a => false;

    public static Expression<Func<T, T2, bool>> FalseExpression<T, T2>() => (a, b) => false;

    public static Func<bool> Not(this Func<bool> predicate) {
      return () => !predicate();
    }

    public static Func<T, bool> Not<T>(this Func<T, bool> predicate) {
      return a => !predicate(a);
    }

    public static Func<T, T2, bool> Not<T, T2>(this Func<T, T2, bool> predicate) {
      return (a, b) => !predicate(a, b);
    }

    public static Func<bool> And(this Func<bool> left, Func<bool> right) {
      return () => left() && right();
    }

    public static Func<T, bool> And<T>(this Func<T, bool> left, Func<T, bool> right) {
      return a => left(a) && right(a);
    }

    public static Func<T, T2, bool> And<T, T2>(this Func<T, T2, bool> left, Func<T, T2, bool> right) {
      return (a, b) => left(a, b) && right(a, b);
    }

    public static Func<bool> Or(this Func<bool> left, Func<bool> right) {
      return () => left() || right();
    }

    public static Func<T, bool> Or<T>(this Func<T, bool> left, Func<T, bool> right) {
      return a => left(a) || right(a);
    }

    public static Func<T, T2, bool> Or<T, T2>(this Func<T, T2, bool> left, Func<T, T2, bool> right) {
      return (a, b) => left(a, b) || right(a, b);
    }

    public static Expression<Func<bool>> Not(this Expression<Func<bool>> predicate) {
      return Expression.Lambda<Func<bool>>(Expression.Not(predicate.Body));
    }

    public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> predicate) {
      return Expression.Lambda<Func<T, bool>>(Expression.Not(predicate.Body), predicate.Parameters);
    }

    public static Expression<Func<T, T2, bool>> Not<T, T2>(this Expression<Func<T, T2, bool>> predicate) {
      return Expression.Lambda<Func<T, T2, bool>>(Expression.Not(predicate.Body), predicate.Parameters);
    }

    public static Expression<Func<bool>> And(this Expression<Func<bool>> left, Expression<Func<bool>> right) {
      return Expression.Lambda<Func<bool>>(Expression.AndAlso(left.Body, right.Body));
    }

    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right) {
      return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left.Body, right.WithParametersOf(left).Body), left.Parameters);
    }

    public static Expression<Func<T, T2, bool>> And<T, T2>(this Expression<Func<T, T2, bool>> left, Expression<Func<T, T2, bool>> right) {
      return Expression.Lambda<Func<T, T2, bool>>(Expression.AndAlso(left.Body, right.WithParametersOf(left).Body), left.Parameters);
    }

    public static Expression<Func<bool>> Or(this Expression<Func<bool>> left, Expression<Func<bool>> right) {
      return Expression.Lambda<Func<bool>>(Expression.OrElse(left.Body, right.Body));
    }

    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right) {
      return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left.Body, right.WithParametersOf(left).Body), left.Parameters);
    }

    public static Expression<Func<T, T2, bool>> Or<T, T2>(this Expression<Func<T, T2, bool>> left, Expression<Func<T, T2, bool>> right) {
      return Expression.Lambda<Func<T, T2, bool>>(Expression.OrElse(left.Body, right.WithParametersOf(left).Body), left.Parameters);
    }

    private static Expression<Func<TResult>> WithParametersOf<T, TResult>(this Expression<Func<T, TResult>> left, Expression<Func<T, TResult>> right) {
      return new ReplaceParameterVisitor<Func<TResult>>(left.Parameters[0], right.Parameters[0]).Visit(left);
    }

    private static Expression<Func<TResult>> WithParametersOf<T, T2, TResult>(this Expression<Func<T, T2, TResult>> left, Expression<Func<T, T2, TResult>> right) {
      Expression<Func<T, TResult>> replaced = new ReplaceParameterVisitor<Func<T, TResult>>(left.Parameters[1], right.Parameters[1]).Visit(left);
      return new ReplaceParameterVisitor<Func<TResult>>(replaced.Parameters[0], right.Parameters[0]).Visit(replaced);
    }
  }
}
