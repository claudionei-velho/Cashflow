using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using Domain.Lists;

namespace Api.Controllers {
  [Route("[controller]")]
  [ApiController]
  public class ListsController : ControllerBase {
    [HttpGet]
    [Route("ListCategorias")]
    public static IEnumerable<KeyValuePair<int, string>> ListCategorias() {
      return Categoria.Items.Where(p => p.Key > 0).ToList();
    }

    [HttpGet]
    [Route("ListCombustiveis")]
    public static IEnumerable<KeyValuePair<int, string>> ListCombustiveis() {
      return Combustivel.Items.ToList();
    }

    [HttpGet]
    [Route("ListCondicoes")]
    public static IEnumerable<KeyValuePair<int, string>> ListCondicoes() {
      return Condicao.Items.Where(p => p.Key > 0).ToList();
    }

    [HttpGet]
    [Route("ListConformes")]
    public static IEnumerable<KeyValuePair<int, string>> ListConformes() {
      return Conforme.Items.Where(p => p.Key > 0).ToList();
    }

    [HttpGet]
    [Route("ListCores")]
    public static IEnumerable<KeyValuePair<int, string>> ListCores() {
      return Cor.Items.Where(p => p.Key > 0).ToList();
    }

    [HttpGet]
    [Route("ListDirecoes")]
    public static IEnumerable<KeyValuePair<int, string>> ListDirecoes() {
      return Direcao.Items.Where(p => p.Key > 0).ToList();
    }

    [HttpGet]
    [Route("ListEVeiculos")]
    public static IEnumerable<KeyValuePair<int, string>> ListEVeiculos() {
      return EVeiculo.Items.ToList();
    }

    [HttpGet]
    [Route("ListMeses")]
    public static IEnumerable<KeyValuePair<int, string>> ListMeses() {
      return Mes.Items.ToList();
    }

    [HttpGet]
    [Route("ListMesesAb")]
    public static IEnumerable<KeyValuePair<int, string>> ListMesesAb() {
      return Mes.Short.ToList();
    }

    [HttpGet]
    [Route("ListMotores")]
    public static IEnumerable<KeyValuePair<int, string>> ListMotores() {
      return Motor.Items.Where(p => p.Key > 0).ToList();
    }

    [HttpGet]
    [Route("ListPosicoes")]
    public static IEnumerable<KeyValuePair<int, string>> ListPosicoes() {
      return Posicao.Items.Where(p => p.Key > 0).ToList();
    }

    [HttpGet]
    [Route("ListSemanas")]
    public static IEnumerable<KeyValuePair<int, string>> ListSemanas() {
      return Semana.Items.ToList();
    }

    [HttpGet]
    [Route("ListSemanasAb")]
    public static IEnumerable<KeyValuePair<int, string>> ListSemanasAb() {
      return Semana.Short.ToList();
    }

    [HttpGet]
    [Route("ListSentidos")]
    public static IEnumerable<KeyValuePair<string, string>> ListSentidos() {
      return Sentido.Items.ToList();
    }

    [HttpGet]
    [Route("ListTransmissoes")]
    public static IEnumerable<KeyValuePair<int, string>> ListTransmissoes() {
      return Transmissao.Items.ToList();
    }

    [HttpGet]
    [Route("ListTributarios")]
    public static IEnumerable<KeyValuePair<int, string>> ListTributarios() {
      return Tributario.Items.Where(p => p.Key > 0).ToList();
    }

    [HttpGet]
    [Route("ListTVeiculos")]
    public static IEnumerable<KeyValuePair<int, string>> ListTVeiculos() {
      return TVeiculo.Items.ToList();
    }

    [HttpGet]
    [Route("ListWorkday")]
    public static IEnumerable<KeyValuePair<int, string>> ListWorkday() {
      return Workday.Items.Where(p => p.Key > 0).ToList();
    }

    [HttpGet]
    [Route("ListWorkdayAb")]
    public static IEnumerable<KeyValuePair<int, string>> ListWorkdayAb() {
      return Workday.Short.ToList();
    }
  }
}
