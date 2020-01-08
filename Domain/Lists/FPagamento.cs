using System.Collections.Generic;

namespace Domain.Lists {
  public class FPagamento : ListBase {
    public FPagamento() {
      Items = new Dictionary<int, string>() {
        { 0, "Pagamento à vista" },
        { 1, "Pagamento a prazo" },
        { 2, "Outros" }
      };
    }
  }
}
