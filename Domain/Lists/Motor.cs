using System.Collections.Generic;
using System.Linq;

namespace Domain.Lists {
  public class Motor : ListBase {
    public Motor() {
      Items = new Dictionary<int, string> {
        { 0, string.Empty },
        { 1, "Motor em Linha" },
        { 2, "Motor em V" },
        { 3, "Motor Elétrico" },
        { 4, "Motor Híbrido" }
      };
    }

    public override IEnumerable<KeyValuePair<int, string>> ToList() {
      return Items.Where(p => p.Key > 0).ToList();
    }
  }
}
