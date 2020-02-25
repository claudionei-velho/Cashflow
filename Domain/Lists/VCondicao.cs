using System.Collections.Generic;

namespace Domain.Lists {
  public class VCondicao : ListBase {
    public VCondicao() {
      Items = new Dictionary<int, string>() {
        { 1, "Acabado" },
        { 2, "Inacabado" },
        { 3, "Semiacabado" }
      };
    }
  }
}
