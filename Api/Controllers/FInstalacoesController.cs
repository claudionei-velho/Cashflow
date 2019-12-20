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
  public class FInstalacoesController : ControllerBase {
    private readonly IServiceBase<FInstalacao> _fInstalacoes;
    private readonly IMapper _mapper;

    public FInstalacoesController(IServiceBase<FInstalacao> fInstalacoes, IMapper mapper) {
      _fInstalacoes = fInstalacoes;
      _mapper = mapper;
    }

    // GET: FInstalacoes
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_fInstalacoes) {
        return Ok(_mapper.Map<IEnumerable<FInstalacaoDto>>(
                      await _fInstalacoes.GetData().ToListAsync()));
      }
    }

    // GET: FInstalacoes/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_fInstalacoes) {
        FInstalacao fInstalacao = await _fInstalacoes.GetByIdAsync(id);
        if (fInstalacao == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<FInstalacaoDto>(fInstalacao));
      }
    }

    // PUT: FInstalacoes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, FInstalacaoDto dto) {
      using (_fInstalacoes) {
        if (_fInstalacoes.Exists(f => f.Id == id)) {
          await _fInstalacoes.Update(_mapper.Map<FInstalacao>(dto));
        }
        else {
          return BadRequest();
        }
      }
      return Ok();
    }

    // POST: FInstalacoes
    [HttpPost]
    public async Task<IActionResult> Post(FInstalacaoDto dto) {
      using (_fInstalacoes) {
        if (dto == null) {
          return BadRequest();
        }
        await _fInstalacoes.Insert(_mapper.Map<FInstalacao>(dto));
      }
      return Ok();
    }

    // DELETE: FInstalacoes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_fInstalacoes) {
        FInstalacao fInstalacao = await _fInstalacoes.GetByIdAsync(id);
        if (fInstalacao == null) {
          return NotFound();
        }
        await _fInstalacoes.Delete(fInstalacao);
      }
      return NoContent();
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_fInstalacoes) {
        return Ok(_mapper.Map<IEnumerable<FInstalacaoDto>>(
                      await _fInstalacoes.GetData().Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_fInstalacoes) {
        return Ok(await _fInstalacoes.SelectList(
                            f => new { f.Id, f.Denominacao }
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_fInstalacoes) {
        return Ok(new KeyValuePair<int, int>(_fInstalacoes.Count(),
                                             _fInstalacoes.Pages(size: k ?? 16)));
      }
    }
  }
}
