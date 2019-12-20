using System.Text;

namespace Api.Models.Validations {
  public static class CustomValidator {
    public static bool IsCnpj(string cnpj) {
      int[] fator = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

      cnpj = cnpj.Trim()
                 .Replace(".", string.Empty)
                 .Replace("-", string.Empty)
                 .Replace("/", string.Empty);
      if (cnpj.Length != 14) {
        return false;
      }

      StringBuilder tempCnpj = new StringBuilder(cnpj.Substring(0, 12));
      StringBuilder digito = new StringBuilder();
      for (int i = 0; i < 2; i++) {
        int soma = 0;
        for (int j = 0; j < 12 + i; j++) {
          soma += int.Parse(tempCnpj[j].ToString()) * fator[j + (1 - i)];
        }
        int resto = soma % 11 < 2 ? 0 : 11 - soma % 11;
        digito.Append(resto.ToString());
        tempCnpj.Append(digito);
      }
      return cnpj.EndsWith(digito.ToString());
    }

    public static bool IsCpf(string cpf) {
      int[] fator = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

      cpf = cpf.Trim()
               .Replace(".", string.Empty)
               .Replace("-", string.Empty);
      if (cpf.Length != 11) {
        return false;
      }

      StringBuilder tempCpf = new StringBuilder(cpf.Substring(0, 9));
      StringBuilder digito = new StringBuilder();
      for (int i = 0; i < 2; i++) {
        int soma = 0;
        for (int j = 0; j < 9 + i; j++) {
          soma += int.Parse(tempCpf[j].ToString()) * fator[j + (1 - i)];
        }
        int resto = soma % 11 < 2 ? 0 : 11 - soma % 11;
        digito.Append(resto.ToString());
        tempCpf.Append(digito);
      }
      return cpf.EndsWith(digito.ToString());
    }

    public static bool IsPis(string pis) {
      int[] fator = { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

      if (pis.Trim().Length != 11) {
        return false;
      }
      pis = pis.Trim()
               .Replace("-", string.Empty)
               .Replace(".", string.Empty)
               .PadLeft(11, '0');

      int soma = 0;
      for (int i = 0; i < 10; i++) {
        soma += int.Parse(pis[i].ToString()) * fator[i];
      }
      int resto = soma % 11 < 2 ? 0 : 11 - soma % 11;

      return pis.EndsWith(resto.ToString());
    }
  }
}
