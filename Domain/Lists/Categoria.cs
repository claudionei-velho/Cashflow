using System.Collections.Generic;

namespace Domain.Lists {
  public static class Categoria {
    public static IDictionary<int, string> Items = new Dictionary<int, string> {
        { 0, string.Empty },
        { 1, "Leve" },
        { 2, "Pesado" },
        { 3, "Trucado" },
        { 4, "Especial" }
    };
  }
}
