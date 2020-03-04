using System.Collections.Generic;
using System.Linq;

namespace Domain.Lists {
  public class ListBase {
    public IDictionary<int, string> Items;

    public int Count() {
      return Items.Count;
    }

    public virtual IEnumerable<KeyValuePair<int, string>> ToList() {
      return Items.ToList();
    }
  }
}
