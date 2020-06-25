using System.Collections.Generic;

namespace Domain.Lists {
  public static class Posicao {
    public static IDictionary<int, string> Items = new Dictionary<int, string> {
        { 0, string.Empty },
        { 1, "Dianteiro" },
        { 2, "Central" },
        { 3, "Traseiro" }
    };
  }
}
