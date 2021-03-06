﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using Api.Models;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Api.Controllers {
  [Route("[controller]")]
  [ApiController]
  public class DepreciacoesController : ControllerBase {
    private readonly IDepreciacaoService _depreciacoes;
    private readonly IMapper _mapper;

    public DepreciacoesController(IDepreciacaoService depreciacoes, IMapper mapper) {
      _depreciacoes = depreciacoes;
      _mapper = mapper;
    }

    // GET: Depreciacoes
    [HttpGet]
    public IActionResult Get() {
      return Ok(_mapper.Map<IEnumerable<DepreciacaoDto>>(GetDataSet().ToList()));
    }

    // GET: Depreciacoes/5/5
    [HttpGet("{id}/{fx}")]
    public IActionResult Get(int id, int fx) {
      return Ok(_mapper.Map<DepreciacaoDto>(
                    GetDataSet(q => q.ClasseId == id &&
                                    q.EtariaId == fx).FirstOrDefault()));
    }

    [HttpGet, Route("List/{id}")]
    public IActionResult List(int id) {
      return Ok(_mapper.Map<IEnumerable<DepreciacaoDto>>(
                    GetDataSet(q => q.ECVeiculo.EmpresaId == id).ToList()));
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public IActionResult PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      return Ok(_mapper.Map<IEnumerable<DepreciacaoDto>>(
                    GetDataSet().Skip((p - 1) * k).Take(k).ToList()));
    }

    [HttpGet, Route("Pages/{k?}")]
    public ActionResult<KeyValuePair<int, int>> Pages(int k = 8) {
      int pages = this.GetDataSet().Count() / k;
      if (this.GetDataSet().Count() % k > 0) {
        ++pages;
      }
      return new KeyValuePair<int, int>(GetDataSet().Count(), pages);
    }

    private IEnumerable<Depreciacao> GetDataSet(Expression<Func<Depreciacao, bool>> condition = null) {
      ICollection<Depreciacao> result = new HashSet<Depreciacao>();
      using (_depreciacoes) {
        foreach (Depreciacao dp in _depreciacoes.Query(condition)) {
          dp.Coeficiente = _depreciacoes.GetCoeficiente(dp.ClasseId, dp.EtariaId);
          dp.Acumulado = _depreciacoes.GetAcumulado(dp.ClasseId, dp.Anos);

          result.Add(dp);
        }
      }
      return result.ToList();
    }
  }
}
