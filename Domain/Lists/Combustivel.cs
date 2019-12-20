using System.Collections.Generic;

namespace Domain.Lists {
  public class Combustivel : ListBase {
    public Combustivel() {
      Items = new Dictionary<int, string>() {
        {  1, "Álcool" },
        {  2, "Gasolina" },
        {  3, "Diesel" },
        { 16, "Álcool / Gasolina" },
        { 17, "Gasolina / Álcool / GNV" },
        { 18, "Gasolina / Elétrico" }
      };
    }
  }
}
