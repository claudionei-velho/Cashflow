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
  public class MunicipiosController : ControllerBase {
    private readonly IMunicipioService _municipios;
    private readonly IMapper _mapper;

    public MunicipiosController(IMunicipioService municipios, IMapper mapper) {
      _municipios = municipios;
      _mapper = mapper;
    }

    // GET: Municipios
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_municipios) {
        return Ok(_mapper.Map<IEnumerable<MunicipioDto>>(
                      await _municipios.GetData(
                                order: m => m.OrderBy(p => p.Uf.Sigla).ThenBy(p => p.Nome)
                            ).ToListAsync()));
      }      
    }

    // GET: Municipios/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_municipios) {
        Municipio municipio = await _municipios.GetFirstAsync(m => m.Id == id);
        if (municipio == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<MunicipioDto>(municipio));
      }
    }

    // GET: Municipios
    [HttpGet, Route("List/{uf}")]
    public async Task<IActionResult> List(string uf) {
      using (_municipios) {
        return Ok(_mapper.Map<IEnumerable<MunicipioDto>>(
                      await _municipios.GetData(
                                m => m.Uf.Sigla.Equals(uf),
                                m => m.OrderBy(p => p.Nome)
                            ).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_municipios) {
        return Ok(_mapper.Map<IEnumerable<MunicipioDto>>(
                      await _municipios.GetData(
                                order: m => m.OrderBy(p => p.Uf.Sigla).ThenBy(p => p.Nome)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{uf}/{p}/{k}")]
    public async Task<IActionResult> PagedList(string uf, int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_municipios) {
        return Ok(_mapper.Map<IEnumerable<MunicipioDto>>(
                      await _municipios.GetData(
                                m => m.Uf.Sigla.Equals(uf),
                                m => m.OrderBy(p => p.Uf.Sigla).ThenBy(p => p.Nome)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_municipios) {
        return Ok(await _municipios.SelectList(
                            m => new { m.Id, m.Nome },
                            order: m => m.OrderBy(p => p.Uf.Sigla)
                                         .ThenBy(p => p.Nome)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{uf}")]
    public async Task<IActionResult> SelectList(string uf) {
      using (_municipios) {
        return Ok(await _municipios.SelectList(
                            m => new { m.Id, m.Nome },
                            m => m.Uf.Sigla.Equals(uf),
                            m => m.OrderBy(p => p.Nome)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_municipios) {
        return Ok(new KeyValuePair<int, int>(_municipios.Count(),
                                             _municipios.Pages(size: k ?? 16)));
      }
    }

    [HttpGet, Route("Pages/{uf}/{k}")]
    public IActionResult Pages(string uf, int k) {
      using (_municipios) {
        return Ok(new KeyValuePair<int, int>(_municipios.Count(m => m.Uf.Sigla.Equals(uf)),
                                             _municipios.Pages(m => m.Uf.Sigla.Equals(uf), k)));
      }
    }

    [HttpGet, Route("Expertise")]
    public async Task<IActionResult> Expertise() {
      using (_municipios) {
        return Ok(_mapper.Map<IEnumerable<MunicipioDto>>(
                      await _municipios.GetExpertise().ToListAsync()));
      }
    }
  }
}
