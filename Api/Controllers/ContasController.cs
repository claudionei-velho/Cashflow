using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AutoMapper;
using FluentValidation;

using Api.Models;
using Api.Models.Validations;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Api.Controllers {
  [Route("[controller]")]
  [ApiController]
  public class ContasController : ControllerBase {
    private readonly IContaService _contas;
    private readonly IMapper _mapper;

    public ContasController(IContaService contas, IMapper mapper) {
      _contas = contas;
      _mapper = mapper;
    }

    // GET: Contas
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_contas) {
        return Ok(_mapper.Map<IEnumerable<ContaDto>>(
                      await _contas.GetData(
                                order: c => c.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.Classificacao)
                            ).ToListAsync()));
      }
    }

    // GET: Contas/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_contas) {
        Conta conta = await _contas.GetByIdAsync(id);
        if (conta == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<ContaDto>(conta));
      }
    }

    // PUT: Contas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ContaDto dto) {
      using (_contas) {
        if (_contas.Exists(c => c.Id == id)) {
          ContaValidator validator = new ContaValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _contas.Update(_mapper.Map<Conta>(dto));
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

    // POST: Contas
    [HttpPost]
    public async Task<IActionResult> Post(ContaDto dto) {
      using (_contas) {
        ContaValidator validator = new ContaValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _contas.Insert(_mapper.Map<Conta>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }        
      }
      return Ok();
    }

    // DELETE: Contas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_contas) {
        Conta conta = await _contas.GetByIdAsync(id);
        if (conta == null) {
          return NotFound();
        }
        await _contas.Delete(conta);
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_contas) {
        return Ok(_mapper.Map<IEnumerable<ContaDto>>(
                      await _contas.GetData(
                                c => c.EmpresaId == id,
                                c => c.OrderBy(q => q.Classificacao)
                            ).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      using (_contas) {
        if (p < 1 || k < 1) {
          return BadRequest();
        }
        return Ok(_mapper.Map<IEnumerable<ContaDto>>(
                      await _contas.GetData(
                                order: c => c.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.Classificacao)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_contas) {
        return Ok(await _contas.SelectList(
                            c => new { c.Id, c.Classificacao, c.Denominacao },
                            order: c => c.OrderBy(q => q.EmpresaId)
                                         .ThenBy(q => q.Classificacao)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_contas) {
        return Ok(await _contas.SelectList(
                            c => new { c.Id, c.Classificacao, c.Denominacao },
                            c => c.EmpresaId == id,
                            c => c.OrderBy(q => q.Classificacao)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_contas) {
        return Ok(new KeyValuePair<int, int>(_contas.Count(),
                                             _contas.Pages(size: k ?? 16)));
      }
    }
  }
}
