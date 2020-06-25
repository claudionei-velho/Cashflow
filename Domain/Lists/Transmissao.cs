using System.Collections.Generic;

namespace Domain.Lists {
  public static class Transmissao {
    public static IDictionary<int, string> Items = new Dictionary<int, string> {
        { 1, "Manual" },
        { 2, "Automática" }
    };
  }
}
