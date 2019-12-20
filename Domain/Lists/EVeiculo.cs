using System.Collections.Generic;

namespace Domain.Lists {
  public class EVeiculo : ListBase {
    public EVeiculo() {
      Items = new Dictionary<int, string>() {
        { 1, "PASSAGEIRO" },
        { 2, "CARGA" },
        { 3, "MISTO" },
        { 4, "CORRIDA" },
        { 5, "TRAÇÃO" },
        { 6, "ESPECIAL" }
      };
    }
  }
}
