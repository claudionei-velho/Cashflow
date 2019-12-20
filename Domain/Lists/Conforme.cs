using System.Collections.Generic;
using System.Linq;

namespace Domain.Lists {
  public class Conforme : ListBase {
    public Conforme() {
      Items = new Dictionary<int, string> {
        { 0, string.Empty },
        { 1, "Disponível" },
        { 2, "Indisponível" },
        { 3, "Terceirizado" }
      };
    }

    public override IEnumerable<KeyValuePair<int, string>> ToList() {
      return Items.Where(p => p.Key > 0).ToList();
    }
  }
}
