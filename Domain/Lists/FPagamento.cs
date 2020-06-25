using System.Collections.Generic;
using System.Linq;

namespace Domain.Lists {
  public static class FPagamento {
    public static IDictionary<int, string> Items = new Dictionary<int, string>() {
        { 0, "Pagamento à vista" },
        { 1, "Pagamento a prazo" },
        { 2, "Outros" }
    };

    public static IEnumerable<KeyValuePair<int, string>> ToList() {
      return Items.ToList();
    }
  }
}
