using System.Collections.Generic;
using System.Linq;

namespace Domain.Lists {
  public class Tributario : ListBase {
    public Tributario() {
      Items = new Dictionary<int, string> {
        { 0, string.Empty },
        { 1, "Simples Nacional" },
        { 2, "Simples Nacional, excesso sublimite de receita bruta" },
        { 3, "Regime Normal" }
      };
    }

    public override IEnumerable<KeyValuePair<int, string>> ToList() {
      return Items.Where(p => p.Key > 0).ToList();
    }
  }
}
