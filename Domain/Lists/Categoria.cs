using System.Collections.Generic;
using System.Linq;

namespace Domain.Lists {
  public class Categoria : ListBase {
    public Categoria() {
      Items = new Dictionary<int, string> {
        { 0, string.Empty },
        { 1, "Leve" },
        { 2, "Pesado" },
        { 3, "Trucado" },
        { 4, "Especial" }
      };
    }

    public override IEnumerable<KeyValuePair<int, string>> ToList() {
      return Items.Where(p => p.Key > 0).ToList();
    }
  }
}
