using System.Collections.Generic;

namespace Domain.Lists {
  public static class Sentido {
    public static IDictionary<string, string> Items = new Dictionary<string, string> {
        { "AB", "A -> B" },
        { "BA", "B -> A" }
    };
  }
}
