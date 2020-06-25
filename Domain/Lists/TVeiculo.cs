using System.Collections.Generic;

namespace Domain.Lists {
  public static class TVeiculo {
    public static IDictionary<int, string> Items = new Dictionary<int, string>() {
        {  2, "CICLOMOTO" },
        {  3, "MOTONETA" },
        {  4, "MOTOCICLO" },
        {  5, "TRICICLO" },
        {  6, "AUTOMÓVEL" },
        {  7, "MICROÔNIBUS" },
        {  8, "ÔNIBUS" },
        { 10, "REBOQUE" },
        { 11, "SEMIRREBOQUE" },
        { 13, "CAMINHONETA" },
        { 14, "CAMINHÃO" },
        { 17, "C.TRATOR" },
        { 22, "ESP / ÔNIBUS" },
        { 23, "MISTO / CAM" },
        { 24, "CARGA / CAM" }
    };
  }
}
