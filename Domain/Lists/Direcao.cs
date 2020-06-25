using System.Collections.Generic;

namespace Domain.Lists {
  public static class Direcao {
    public static IDictionary<int, string> Items = new Dictionary<int, string> {
        { 0, string.Empty },
        { 1, "Manual" },
        { 2, "Hidráulica" },
        { 3, "Elétrica" }
    };
  }
}
