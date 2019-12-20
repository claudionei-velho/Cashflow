using System.Collections.Generic;

namespace Domain.Lists {
  public class Transmissao : ListBase {
    public Transmissao() {
      Items = new Dictionary<int, string> {
        { 1, "Manual" },
        { 2, "Automática" }
      };
    }
  }
}
