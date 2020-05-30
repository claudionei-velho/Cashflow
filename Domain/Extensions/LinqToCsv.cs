using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Domain.Extensions {
  public static class LinqToCSV {
    public static StringBuilder ToCsv<T>(this IEnumerable<T> items) where T : class {
      if (items is null) {
        throw new ArgumentNullException(nameof(items));
      }

      StringBuilder csvBuilder = new StringBuilder();
      PropertyInfo[] properties = typeof(T).GetProperties();
      foreach (PropertyInfo prop in properties) {
        csvBuilder.Append($"{prop.Name};");
      }
      foreach (T item in items) {
        string line = string.Join(",", properties.Select(p => p.GetValue(item, null).ToCsvValue()).ToArray());
        csvBuilder.AppendLine(line);
      }
      return csvBuilder;
    }

    private static string ToCsvValue<T>(this T item) {
      switch (item) {
        case null:
          return "\"\"";
        case string _:
          return $"\"{item.ToString().Replace("\"", "\\\"")}\"";
      }

      if (double.TryParse(item.ToString(), out _)) {
        return $"{item}";
      }
      return $"\"{item}\"";
    }
  }
}
