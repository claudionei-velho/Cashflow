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
  public class NfCombustiveisController : ControllerBase {
    private readonly INfCombustivelService _combustiveis;
    private readonly IMapper _mapper;

    public NfCombustiveisController(INfCombustivelService combustiveis, IMapper mapper) {
      _combustiveis = combustiveis;
      _mapper = mapper;
    }

    // GET: NfCombustiveis
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_combustiveis) {
        return Ok(_mapper.Map<IEnumerable<NfCombustivelDto>>(
                      await _combustiveis.ListAsync(
                                order: n => n.OrderBy(q => q.NotaId)
                                             .ThenBy(q => q.ItemId))));
      }
    }

    // GET: NfCombustiveis/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_combustiveis) {
        NfCombustivel combustivel = await _combustiveis.GetFirstAsync(n => n.Id == id);
        if (combustivel == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<NfCombustivelDto>(combustivel));
      }
    }

    // PUT: NfCombustiveis/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, NfCombustivelDto dto) {
      using (_combustiveis) {
        if (_combustiveis.Exists(n => n.Id == id)) {
          NfCombustivelValidator validator = new NfCombustivelValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _combustiveis.Update(_mapper.Map<NfCombustivel>(dto));
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

    // POST: NfCombustiveis
    [HttpPost]
    public async Task<IActionResult> Post(NfCombustivelDto dto) {
      NfCombustivel combustivel = new NfCombustivel();
      using (_combustiveis) {
        NfCombustivelValidator validator = new NfCombustivelValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _combustiveis.Insert(combustivel = _mapper.Map<NfCombustivel>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<NfCombustivelDto>(combustivel));
    }

    // DELETE: NfCombustiveis/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_combustiveis) {
        NfCombustivel combustivel = await _combustiveis.GetByIdAsync(id);
        if (combustivel == null) {
          return NotFound();
        }
        try {
          await _combustiveis.Delete(combustivel);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_combustiveis) {
        return Ok(_mapper.Map<IEnumerable<NfCombustivelDto>>(
                      await _combustiveis.ListAsync(
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
      using (_combustiveis) {
        return Ok(_mapper.Map<IEnumerable<NfCombustivelDto>>(
                      await _combustiveis.PagedListAsync(
                                order: n => n.OrderBy(q => q.NotaId)
                                             .ThenBy(q => q.ItemId),
                                skip: p, take: k)));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_combustiveis) {
        return Ok(await _combustiveis.SelectListAsync(
                            n => new { n.Id, n.NFiscal.Numero, n.AnpProduto.Denominacao },
                            order: n => n.OrderBy(q => q.NotaId)
                                         .ThenBy(q => q.ItemId)));
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_combustiveis) {
        return Ok(await _combustiveis.SelectListAsync(
                            n => new { n.Id, n.NFiscal.Numero, n.AnpProduto.Denominacao },
                            n => n.NFiscal.EmpresaId == id,
                            n => n.OrderBy(q => q.NotaId)
                                  .ThenBy(q => q.ItemId)));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public ActionResult<KeyValuePair<int, int>> Pages(int? k) {
      using (_combustiveis) {
        return new KeyValuePair<int, int>(_combustiveis.Count(),
                                          _combustiveis.Pages(size: k ?? 8));
      }
    }
  }
}
