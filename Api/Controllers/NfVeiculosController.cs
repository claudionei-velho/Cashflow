using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using FluentValidation;

using Api.Models;
using Api.Models.Validations;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Api.Controllers {
  [Route("[controller]")]
  [ApiController]
  public class NfVeiculosController : ControllerBase {
    private readonly INfVeiculoService _veiculos;
    private readonly IMapper _mapper;

    public NfVeiculosController(INfVeiculoService veiculos, IMapper mapper) {
      _veiculos = veiculos;
      _mapper = mapper;
    }

    // GET: NfVeiculos
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_veiculos) {
        return Ok(_mapper.Map<IEnumerable<NfVeiculoDto>>(
                      await _veiculos.ListAsync(
                                order: n => n.OrderBy(q => q.NotaId)
                                             .ThenBy(q => q.ItemId))));
      }
    }

    // GET: NfVeiculos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_veiculos) {
        NfVeiculo veiculo = await _veiculos.GetFirstAsync(n => n.Id == id);
        if (veiculo == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<NfVeiculoDto>(veiculo));
      }
    }

    // PUT: NfVeiculos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, NfVeiculoDto dto) {
      using (_veiculos) {
        if (_veiculos.Exists(n => n.Id == id)) {
          NfVeiculoValidator validator = new NfVeiculoValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _veiculos.Update(_mapper.Map<NfVeiculo>(dto));
          }
          catch (ValidationException ex) {
            return BadRequest(ex.Errors);
          }
        }
        else {
          return BadRequest();
        }
      }
      return Ok();
    }

    // POST: NfVeiculos
    [HttpPost]
    public async Task<IActionResult> Post(NfVeiculoDto dto) {
      NfVeiculo veiculo = new NfVeiculo();
      using (_veiculos) {
        NfVeiculoValidator validator = new NfVeiculoValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _veiculos.Insert(veiculo = _mapper.Map<NfVeiculo>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<NfVeiculoDto>(veiculo));
    }

    // DELETE: NfVeiculos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_veiculos) {
        NfVeiculo veiculo = await _veiculos.GetByIdAsync(id);
        if (veiculo == null) {
          return NotFound();
        }
        try {
          await _veiculos.Delete(veiculo);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_veiculos) {
        return Ok(_mapper.Map<IEnumerable<NfVeiculoDto>>(
                      await _veiculos.ListAsync(
                                n => n.NFiscal.EmpresaId == id,
                                n => n.OrderBy(q => q.NotaId)
                                      .ThenBy(q => q.ItemId))));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_veiculos) {
        return Ok(_mapper.Map<IEnumerable<NfVeiculoDto>>(
                      await _veiculos.PageListAsync(
                                order: n => n.OrderBy(q => q.NotaId)
                                             .ThenBy(q => q.ItemId),
                                skip: p, take: k)));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_veiculos) {
        return Ok(await _veiculos.SelectListAsync(
                            n => new { n.Id, n.NFiscal.Numero, n.ChassiNo },
                            order: n => n.OrderBy(q => q.NotaId)
                                         .ThenBy(q => q.ItemId)));
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_veiculos) {
        return Ok(await _veiculos.SelectListAsync(
                            n => new { n.Id, n.NFiscal.Numero, n.ChassiNo },
                            n => n.NFiscal.EmpresaId == id,
                            n => n.OrderBy(q => q.NotaId).ThenBy(q => q.ItemId)));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public ActionResult<KeyValuePair<int, int>> Pages(int? k) {
      using (_veiculos) {
        return new KeyValuePair<int, int>(_veiculos.Count(),
                                          _veiculos.Pages(size: k ?? 8));
      }
    }
  }
}
