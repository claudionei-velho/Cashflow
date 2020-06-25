using System.Collections.Generic;

namespace Domain.Lists {
  public static class Motor {
    public static IDictionary<int, string> Items = new Dictionary<int, string> {
        { 0, string.Empty },
        { 1, "Motor em Linha" },
        { 2, "Motor em V" },
        { 3, "Motor Elétrico" },
        { 4, "Motor Híbrido" }
    };
  }
}
