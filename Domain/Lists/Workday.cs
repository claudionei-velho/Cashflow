using System;
using System.Collections.Generic;

namespace Domain.Lists {
  public static class Workday {
    public static IDictionary<int, string> Items = new Dictionary<int, string> {
        { 0, string.Empty },
        { 1, "Dias Úteis" },
        { 2, "Sábados" },
        { 3, "Dom./Feriados" }
    };

    public static IDictionary<int, string> Short = new Dictionary<int, string> {
        { 1, "Úteis" },
        { 2, "Sab." },
        { 3, "Dom." }
    };

    public static int GetWorkday(DateTime reference) {
      return reference.DayOfWeek switch {
        DayOfWeek.Saturday => 2,
        DayOfWeek.Sunday => 3,
        _ => 1
      };
    }
  }
}
