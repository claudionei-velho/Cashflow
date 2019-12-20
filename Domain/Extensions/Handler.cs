using System.Collections.Generic;

namespace Domain.Extensions {
  public static class Handler {
    public static T? NullIf<T>(T left, T right) where T : struct {
      return EqualityComparer<T>.Default.Equals(left, right) ? (T?)null : left;
    }
  }
}
