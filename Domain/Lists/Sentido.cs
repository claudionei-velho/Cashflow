using System.Collections.Generic;
using System.Linq;

namespace Domain.Lists {
  public class Sentido {
    public readonly Dictionary<string, string> Items;

    public Sentido() {
      Items = new Dictionary<string, string> {
        { "AB", "A -> B" },
        { "BA", "B -> A" }
      };
    }

    public IEnumerable<KeyValuePair<string, string>> ToList() {
      return Items.ToList();
    }
  }
}
