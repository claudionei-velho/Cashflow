using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

using Api.Models;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Api.Controllers {
  [Route("[controller]")]
  [ApiController]
  public class CVeiculosController : ControllerBase {
    private readonly IServiceBase<CVeiculo> _cVeiculos;
    private readonly IMapper _mapper;

    public CVeiculosController(IServiceBase<CVeiculo> cVeiculos, IMapper mapper) {
      _cVeiculos = cVeiculos;
      _mapper = mapper;
    }

    // GET: CVeiculos
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_cVeiculos) {
        return Ok(_mapper.Map<IEnumerable<CVeiculoDto>>(
                      await _cVeiculos.GetData().ToListAsync()));
      }
    }

    // GET: CVeiculos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_cVeiculos) {
        CVeiculo cVeiculo = await _cVeiculos.GetByIdAsync(id);
        if (cVeiculo == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<CVeiculoDto>(cVeiculo));
      }
    }

    // PUT: CVeiculos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CVeiculoDto dto) {
      using (_cVeiculos) {
        if (_cVeiculos.Exists(v => v.Id == id)) {
          await _cVeiculos.Update(_mapper.Map<CVeiculo>(dto));
        }
        else {
          return BadRequest();
        }
      }
      return Ok();
    }

    // POST: CVeiculos
    [HttpPost]
    public async Task<IActionResult> Post(CVeiculoDto dto) {
      CVeiculo cVeiculo = new CVeiculo();
      using (_cVeiculos) {
        if (dto == null) {
          return BadRequest();
        }
        await _cVeiculos.Insert(cVeiculo = _mapper.Map<CVeiculo>(dto));
      }
      return Ok(_mapper.Map<CVeiculoDto>(cVeiculo));
    }

    // DELETE: CVeiculos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_cVeiculos) {
        CVeiculo cVeiculo = await _cVeiculos.GetByIdAsync(id);
        if (cVeiculo == null) {
          return NotFound();
        }
        try {
          await _cVeiculos.Delete(cVeiculo);          
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_cVeiculos) {
        return Ok(_mapper.Map<IEnumerable<CVeiculoDto>>(
                      await _cVeiculos.GetData().Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_cVeiculos) {
        return Ok(await _cVeiculos.SelectList(
                            v => new { v.Id, v.Classe }
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_cVeiculos) {
        return Ok(new KeyValuePair<int, int>(_cVeiculos.Count(),
                                             _cVeiculos.Pages(size: k ?? 16)));
      }
    }
  }
}
