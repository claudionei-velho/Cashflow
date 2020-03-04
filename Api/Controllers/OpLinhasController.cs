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
  public class OpLinhasController : ControllerBase {
    private readonly IServiceBase<OpLinha> _opLinhas;
    private readonly IMapper _mapper;

    public OpLinhasController(IServiceBase<OpLinha> opLinhas, IMapper mapper) {
      _opLinhas = opLinhas;
      _mapper = mapper;
    }

    // GET: OpLinhas
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_opLinhas) {
        return Ok(_mapper.Map<IEnumerable<OpLinhaDto>>(
                      await _opLinhas.GetData().ToListAsync()));
      }
    }

    // GET: OpLinhas/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_opLinhas) {
        OpLinha opLinha = await _opLinhas.GetByIdAsync(id);
        if (opLinha == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<OpLinhaDto>(opLinha));
      }
    }

    // PUT: OpLinhas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, OpLinhaDto dto) {
      using (_opLinhas) {
        if (_opLinhas.Exists(p => p.Id == id)) {
          await _opLinhas.Update(_mapper.Map<OpLinha>(dto));
        }
        else {
          return BadRequest();
        }
      }
      return Ok();
    }

    // POST: OpLinhas
    [HttpPost]
    public async Task<IActionResult> Post(OpLinhaDto dto) {
      OpLinha opLinha = new OpLinha();
      using (_opLinhas) {
        if (dto == null) {
          return BadRequest();
        }
        await _opLinhas.Insert(opLinha = _mapper.Map<OpLinha>(dto));
      }
      return Ok(_mapper.Map<OpLinha>(opLinha));
    }

    // DELETE: OpLinhas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_opLinhas) {
        OpLinha opLinha = await _opLinhas.GetByIdAsync(id);
        if (opLinha == null) {
          return NotFound();
        }
        try {
          await _opLinhas.Delete(opLinha);
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
      using (_opLinhas) {
        return Ok(_mapper.Map<IEnumerable<OpLinhaDto>>(
                      await _opLinhas.GetData().Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_opLinhas) {
        return Ok(await _opLinhas.SelectList(
                            p => new { p.Id, p.Denominacao }
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_opLinhas) {
        return Ok(new KeyValuePair<int, int>(_opLinhas.Count(),
                                             _opLinhas.Pages(size: k ?? 16)));
      }
    }
  }
}
