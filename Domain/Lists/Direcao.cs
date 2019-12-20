using System.Collections.Generic;
using System.Linq;

namespace Domain.Lists {
  public class Direcao : ListBase {
    public Direcao() {
      Items = new Dictionary<int, string> {
        { 0, string.Empty },
        { 1, "Manual" },
        { 2, "Hidráulica" },
        { 3, "Elétrica" }
      };
    }

    public override IEnumerable<KeyValuePair<int, string>> ToList() {
      return Items.Where(p => p.Key > 0).ToList();
    }
  }
}
