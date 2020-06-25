using System.Collections.Generic;

namespace Domain.Lists {
  public static class VCondicao {
    public static IDictionary<int, string> Items = new Dictionary<int, string>() {
        { 1, "Acabado" },
        { 2, "Inacabado" },
        { 3, "Semiacabado" }
    };
  }
}
