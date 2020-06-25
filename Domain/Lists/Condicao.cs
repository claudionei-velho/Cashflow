using System.Collections.Generic;

namespace Domain.Lists {
  public static class Condicao {
    public static IDictionary<int, string> Items = new Dictionary<int, string> {
        { 0, string.Empty },
        { 1, "Excelente" },
        { 2, "Ótimo(a)" },
        { 3, "Bom" },
        { 4, "Regular" },
        { 5, "Ruim" },
        { 6, "Péssimo(a)" }
    };
  }
}
