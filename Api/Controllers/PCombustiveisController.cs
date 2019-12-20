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
  public class PCombustiveisController : ControllerBase {
    private readonly IPCombustivelService _pCombustiveis;
    private readonly IMapper _mapper;

    public PCombustiveisController(IPCombustivelService pCombustivels, IMapper mapper) {
      _pCombustiveis = pCombustivels;
      _mapper = mapper;
    }

    // GET: Planos
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_pCombustiveis) {
        return Ok(_mapper.Map<IEnumerable<PCombustivelDto>>(
                      await _pCombustiveis.GetData(
                                order: p => p.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                            ).ToListAsync()));
      }      
    }

    // GET: Planos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_pCombustiveis) {
        PCombustivel pCombustivel = await _pCombustiveis.GetFirstAsync(p => p.Id == id);
        if (pCombustivel == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<PCombustivelDto>(pCombustivel));
      }
    }

    // PUT: Planos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, PCombustivelDto dto) {
      using (_pCombustiveis) {
        if (_pCombustiveis.Exists(p => p.Id == id)) {
          PCombustivelValidator validator = new PCombustivelValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _pCombustiveis.Update(_mapper.Map<PCombustivel>(dto));
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

    // POST: Planos
    [HttpPost]
    public async Task<IActionResult> Post(PCombustivelDto dto) {
      using (_pCombustiveis) {
        PCombustivelValidator validator = new PCombustivelValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _pCombustiveis.Insert(_mapper.Map<PCombustivel>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }        
      }
      return Ok();
    }

    // DELETE: Planos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_pCombustiveis) {
        PCombustivel pCombustivel = await _pCombustiveis.GetByIdAsync(id);
        if (pCombustivel == null) {
          return NotFound();
        }
        await _pCombustiveis.Delete(pCombustivel);
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_pCombustiveis) {
        return Ok(_mapper.Map<IEnumerable<PCombustivelDto>>(
                      await _pCombustiveis.GetData(p => p.EmpresaId == id).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_pCombustiveis) {
        return Ok(_mapper.Map<IEnumerable<PCombustivelDto>>(
                      await _pCombustiveis.GetData(
                                order: p => p.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_pCombustiveis) {
        return Ok(await _pCombustiveis.SelectList(
                            p => new { p.Id, p.Empresa.Fantasia, p.Ano, p.Mes, 
                                       p.CVeiculo.Classe, p.Combustivel.Denominacao },
                            order: p => p.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_pCombustiveis) {
        return Ok(await _pCombustiveis.SelectList(
                            p => new { p.Id, p.Empresa.Fantasia, p.Ano, p.Mes,
                                       p.CVeiculo.Classe, p.Combustivel.Denominacao },
                            p => p.EmpresaId == id
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_pCombustiveis) {
        return Ok(new KeyValuePair<int, int>(_pCombustiveis.Count(),
                                             _pCombustiveis.Pages(size: k ?? 16)));
      }
    }
  }
}
