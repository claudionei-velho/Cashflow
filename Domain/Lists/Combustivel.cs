using System.Collections.Generic;

namespace Domain.Lists {
  public static class Combustivel {
    public static IDictionary<int, string> Items = new Dictionary<int, string>() {
        {  1, "Álcool" },
        {  2, "Gasolina" },
        {  3, "Diesel" },
        { 16, "Álcool / Gasolina" },
        { 17, "Gasolina / Álcool / GNV" },
        { 18, "Gasolina / Elétrico" }
    };
  }
}
