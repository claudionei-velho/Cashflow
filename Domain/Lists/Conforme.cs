using System.Collections.Generic;

namespace Domain.Lists {
  public static class Conforme {
    public static IDictionary<int, string> Items = new Dictionary<int, string> {
        { 0, string.Empty },
        { 1, "Disponível" },
        { 2, "Indisponível" },
        { 3, "Terceirizado" }
    };
  }
}
