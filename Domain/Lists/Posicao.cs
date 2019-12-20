using System.Collections.Generic;
using System.Linq;

namespace Domain.Lists {
  public class Posicao : ListBase {
    public Posicao() {
      Items = new Dictionary<int, string> {
        { 0, string.Empty },
        { 1, "Dianteiro" },
        { 2, "Central" },
        { 3, "Traseiro" }
      };
    }

    public override IEnumerable<KeyValuePair<int, string>> ToList() {
      return Items.Where(p => p.Key > 0).ToList();
    }
  }
}
