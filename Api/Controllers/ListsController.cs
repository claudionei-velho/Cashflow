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
    public IEnumerable<KeyValuePair<int, string>> ListCategorias() {
      return new Categoria().ToList();
    }

    [HttpGet]
    [Route("ListCombustiveis")]
    public IEnumerable<KeyValuePair<int, string>> ListCombustiveis() {
      return new Combustivel().ToList();
    }

    [HttpGet]
    [Route("ListCondicoes")]
    public IEnumerable<KeyValuePair<int, string>> ListCondicoes() {
      return new Condicao().ToList();
    }

    [HttpGet]
    [Route("ListConformes")]
    public IEnumerable<KeyValuePair<int, string>> ListConformes() {
      return new Conforme().ToList();
    }

    [HttpGet]
    [Route("ListCores")]
    public IEnumerable<KeyValuePair<int, string>> ListCores() {
      return new Cor().ToList();
    }

    [HttpGet]
    [Route("ListDirecoes")]
    public IEnumerable<KeyValuePair<int, string>> ListDirecoes() {
      return new Direcao().ToList();
    }

    [HttpGet]
    [Route("ListEVeiculos")]
    public IEnumerable<KeyValuePair<int, string>> ListEVeiculos() {
      return new EVeiculo().ToList();
    }

    [HttpGet]
    [Route("ListMeses")]
    public IEnumerable<KeyValuePair<int, string>> ListMeses() {
      return new Mes().ToList();
    }

    [HttpGet]
    [Route("ListMesesAb")]
    public IEnumerable<KeyValuePair<int, string>> ListMesesAb() {
      return new Mes().Short.ToList();
    }

    [HttpGet]
    [Route("ListMotores")]
    public IEnumerable<KeyValuePair<int, string>> ListMotores() {
      return new Motor().ToList();
    }

    [HttpGet]
    [Route("ListPosicoes")]
    public IEnumerable<KeyValuePair<int, string>> ListPosicoes() {
      return new Posicao().ToList();
    }

    [HttpGet]
    [Route("ListSemanas")]
    public IEnumerable<KeyValuePair<int, string>> ListSemanas() {
      return new Semana().ToList();
    }

    [HttpGet]
    [Route("ListSemanasAb")]
    public IEnumerable<KeyValuePair<int, string>> ListSemanasAb() {
      return new Semana().Short.ToList();
    }

    [HttpGet]
    [Route("ListSentidos")]
    public IEnumerable<KeyValuePair<string, string>> ListSentidos() {
      return new Sentido().ToList();
    }

    [HttpGet]
    [Route("ListTransmissoes")]
    public IEnumerable<KeyValuePair<int, string>> ListTransmissoes() {
      return new Transmissao().ToList();
    }

    [HttpGet]
    [Route("ListTributarios")]
    public IEnumerable<KeyValuePair<int, string>> ListTributarios() {
      return new Tributario().ToList();
    }

    [HttpGet]
    [Route("ListTVeiculos")]
    public IEnumerable<KeyValuePair<int, string>> ListTVeiculos() {
      return new TVeiculo().ToList();
    }

    [HttpGet]
    [Route("ListWorkday")]
    public IEnumerable<KeyValuePair<int, string>> ListWorkday() {
      return new Workday().ToList();
    }

    [HttpGet]
    [Route("ListWorkdayAb")]
    public IEnumerable<KeyValuePair<int, string>> ListWorkdayAb() {
      return new Workday().Short.ToList();
    }
  }
}
