using System.Collections.Generic;
using System.Linq;

namespace Domain.Lists {
  public class Condicao : ListBase {
    public Condicao() {
      Items = new Dictionary<int, string> {
        { 0, string.Empty },
        { 1, "Excelente" },
        { 2, "Ótimo(a)" },
        { 3, "Bom" },
        { 4, "Regular" },
        { 5, "Ruim" },
        { 6, "Péssimo(a)" }
      };
    }

    public override IEnumerable<KeyValuePair<int, string>> ToList() {
      return Items.Where(p => p.Key > 0).ToList();
    }
  }
}
