using System.Collections.Generic;

namespace Domain.Lists {
  public class NfProposito : ListBase {
    public NfProposito() {
      Items = new Dictionary<int, string>() {
        { 1, "NF-e normal" },
        { 2, "NF-e complementar" },
        { 3, "NF-e de ajuste" },
        { 4, "Devolução de mercadoria" }
      };
    }
  }
}
