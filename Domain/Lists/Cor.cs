using System.Collections.Generic;
using System.Linq;

namespace Domain.Lists {
  public class Cor : ListBase {
    public Cor() {
      Items = new Dictionary<int, string>() {
        {  0, string.Empty },
        {  1, "AMARELO" },
        {  2, "AZUL" },
        {  3, "BEGE" },
        {  4, "BRANCA" },
        {  5, "CINZA" },
        {  6, "DOURADA" },
        {  7, "GRENÁ" },
        {  8, "LARANJA" },
        {  9, "MARROM" },
        { 10, "PRATA" },
        { 11, "PRETA" },
        { 12, "ROSA" },
        { 13, "ROXA" },
        { 14, "VERDE" },
        { 15, "VERMELHA" },
        { 16, "FANTASIA" }
      };
    }

    public override IEnumerable<KeyValuePair<int, string>> ToList() {
      return Items.Where(p => p.Key > 0).ToList();
    }
  }
}
