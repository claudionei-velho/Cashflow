using System.Collections.Generic;

namespace Domain.Lists {
  public static class Semana {
    public static IDictionary<int, string> Items = new Dictionary<int, string> {
        { 1, "Domingo" },
        { 2, "Segunda-feira" },
        { 3, "Terça-feira" },
        { 4, "Quarta-feira" },
        { 5, "Quinta-feira" },
        { 6, "Sexta-feira" },
        { 7, "Sábado" }
    };

    public static IDictionary<int, string> Short = new Dictionary<int, string> {
        { 1, "Dom" },
        { 2, "Seg" },
        { 3, "Ter" },
        { 4, "Qua" },
        { 5, "Qui" },
        { 6, "Sex" },
        { 7, "Sab" }
    };
  }
}
