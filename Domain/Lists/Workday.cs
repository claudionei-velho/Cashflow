using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Lists {
  public class Workday : ListBase {
    public readonly IDictionary<int, string> Short;

    public Workday() {
      Items = new Dictionary<int, string> {
        { 0, string.Empty },
        { 1, "Dias Úteis" },
        { 2, "Sábados" },
        { 3, "Dom./Feriados" }
      };

      Short = new Dictionary<int, string> {
        { 1, "Úteis" },
        { 2, "Sab." },
        { 3, "Dom." }
      };
    }

    public override IEnumerable<KeyValuePair<int, string>> ToList() {
      return Items.Where(p => p.Key > 0).ToList();
    }

    public static int GetWorkday(DateTime reference) {
      int result = reference.DayOfWeek switch {
          DayOfWeek.Saturday => 2,
          DayOfWeek.Sunday => 3,
          _ => 1
      };
      return result;
    }
  }
}
