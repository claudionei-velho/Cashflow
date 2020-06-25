using System.Collections.Generic;

namespace Domain.Lists {
  public static class Restricao {
    public static IDictionary<int, string> Items = new Dictionary<int, string>() {
        { 0, "Não há" },
        { 1, "Alienação Fiduciária" },
        { 2, "Arrendamento Mercantil" },
        { 3, "Reserva de Domínio" },
        { 4, "Penhor de Veículos" },
        { 9, "Outras" }
    };
  }
}
