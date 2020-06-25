using System.Collections.Generic;

namespace Domain.Lists {
  public static class Tributario {
    public static IDictionary<int, string> Items = new Dictionary<int, string> {
        { 0, string.Empty },
        { 1, "Simples Nacional" },
        { 2, "Simples Nacional, excesso sublimite de receita bruta" },
        { 3, "Regime Normal" }
    };
  }
}
