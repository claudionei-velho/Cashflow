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
  public class AnpProdutosController : ControllerBase {
    private readonly IServiceBase<AnpProduto> _anpProdutos;
    private readonly IMapper _mapper;

    public AnpProdutosController(IServiceBase<AnpProduto> anpProdutos, IMapper mapper) {
      _anpProdutos = anpProdutos;
      _mapper = mapper;
    }

    // GET: AnpProdutos
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_anpProdutos) {
        return Ok(_mapper.Map<IEnumerable<AnpProdutoDto>>(
                      await _anpProdutos.GetData().ToListAsync()));
      }
    }

    // GET: AnpProdutos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_anpProdutos) {
        AnpProduto anpProduto = await _anpProdutos.GetByIdAsync(id);
        if (anpProduto == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<AnpProdutoDto>(anpProduto));
      }
    }

    // PUT: AnpProdutos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, AnpProdutoDto dto) {
      using (_anpProdutos) {
        if (_anpProdutos.Exists(p => p.Id == id)) {
          await _anpProdutos.Update(_mapper.Map<AnpProduto>(dto));
        }
        else {
          return BadRequest();
        }
      }
      return Ok();
    }

    // POST: AnpProdutos
    [HttpPost]
    public async Task<IActionResult> Post(AnpProdutoDto dto) {
      AnpProduto anpProduto = new AnpProduto();
      using (_anpProdutos) {
        if (dto == null) {
          return BadRequest();
        }
        await _anpProdutos.Insert(anpProduto = _mapper.Map<AnpProduto>(dto));
      }
      return Ok(_mapper.Map<AnpProdutoDto>(anpProduto));
    }

    // DELETE: AnpProdutos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_anpProdutos) {
        AnpProduto anpProduto = await _anpProdutos.GetByIdAsync(id);
        if (anpProduto == null) {
          return NotFound();
        }
        try {
          await _anpProdutos.Delete(anpProduto);          
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
      using (_anpProdutos) {
        return Ok(_mapper.Map<IEnumerable<AnpProdutoDto>>(
                      await _anpProdutos.GetData().Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_anpProdutos) {
        return Ok(await _anpProdutos.SelectList(
                            p => new { p.Id, p.Denominacao },
                            order: p => p.OrderBy(q => q.Denominacao)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_anpProdutos) {
        return Ok(new KeyValuePair<int, int>(_anpProdutos.Count(),
                                             _anpProdutos.Pages(size: k ?? 16)));
      }
    }
  }
}
