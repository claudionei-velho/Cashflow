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
  public class CLinhasController : ControllerBase {
    private readonly ICLinhaService _cLinhas;
    private readonly IMapper _mapper;

    public CLinhasController(ICLinhaService cLinhas, IMapper mapper) {
      _cLinhas = cLinhas;
      _mapper = mapper;
    }

    // GET: CLinhas
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_cLinhas) {
        return Ok(_mapper.Map<IEnumerable<CLinhaDto>>(
                      await _cLinhas.GetData().ToListAsync()));
      }
    }

    // GET: CLinhas/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_cLinhas) {
        CLinha cLinha = await _cLinhas.GetFirstAsync(c => c.Id == id);
        if (cLinha == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<CLinhaDto>(cLinha));
      }
    }

    // PUT: CLinhas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CLinhaDto dto) {
      using (_cLinhas) {
        if (_cLinhas.Exists(c => c.Id == id)) {
          await _cLinhas.Update(_mapper.Map<CLinha>(dto));
        }
        else {
          return BadRequest();
        }
      }
      return Ok();
    }

    // POST: CLinhas
    [HttpPost]
    public async Task<IActionResult> Post(CLinhaDto dto) {
      CLinha cLinha = new CLinha();
      using (_cLinhas) {
        if (dto == null) {
          return BadRequest();
        }
        await _cLinhas.Insert(cLinha = _mapper.Map<CLinha>(dto));
      }
      return Ok(_mapper.Map<CLinhaDto>(cLinha));
    }

    // DELETE: CLinhas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_cLinhas) {
        CLinha cLinha = await _cLinhas.GetByIdAsync(id);
        if (cLinha == null) {
          return NotFound();
        }
        try { 
          await _cLinhas.Delete(cLinha);
          return NoContent();
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }      
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_cLinhas) {
        return Ok(_mapper.Map<IEnumerable<CLinhaDto>>(
                      await _cLinhas.GetData(c => c.EmpresaId == id).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_cLinhas) {
        return Ok(_mapper.Map<IEnumerable<CLinhaDto>>(
                      await _cLinhas.GetData().Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_cLinhas) {
        return Ok(await _cLinhas.SelectList(
                            c => new { c.Id, c.ClassLinha.Denominacao }
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_cLinhas) {
        return Ok(await _cLinhas.SelectList(
                            c => new { c.Id, c.ClassLinha.Denominacao }, 
                            c => c.EmpresaId == id
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_cLinhas) {
        return Ok(new KeyValuePair<int, int>(_cLinhas.Count(),
                                             _cLinhas.Pages(size: k ?? 16)));
      }
    }
  }
}
