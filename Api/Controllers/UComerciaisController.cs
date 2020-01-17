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
  public class UComerciaisController : ControllerBase {
    private readonly IServiceBase<UComercial> _ucomerciais;
    private readonly IMapper _mapper;

    public UComerciaisController(IServiceBase<UComercial> ucomerciais, IMapper mapper) {
      _ucomerciais = ucomerciais;
      _mapper = mapper;
    }

    // GET: UComerciais
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_ucomerciais) {
        return Ok(_mapper.Map<IEnumerable<UComercialDto>>(
                      await _ucomerciais.GetData().ToListAsync()));
      }      
    }

    // GET: UComerciais/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id) {
      using (_ucomerciais) {
        UComercial UComercial = await _ucomerciais.GetByIdAsync(id);
        if (UComercial == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<UComercialDto>(UComercial));
      }
    }

    // PUT: UComerciais/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, UComercialDto dto) {
      using (_ucomerciais) {
        if (_ucomerciais.Exists(u => u.Id == id)) {
          await _ucomerciais.Update(_mapper.Map<UComercial>(dto));
        }
        else {
          return BadRequest();
        }
      }
      return Ok();
    }

    // POST: UComerciais
    [HttpPost]
    public async Task<IActionResult> Post(UComercialDto dto) {
      UComercial unidade = new UComercial();
      using (_ucomerciais) {
        if (dto == null) {
          return BadRequest();
        }
        await _ucomerciais.Insert(unidade = _mapper.Map<UComercial>(dto));
      }
      return Ok(_mapper.Map<UComercial>(unidade));
    }

    // DELETE: UComerciais/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) {
      using (_ucomerciais) {
        UComercial unidade = await _ucomerciais.GetByIdAsync(id);
        if (unidade == null) {
          return NotFound();
        }
        try { 
          await _ucomerciais.Delete(unidade);
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
      using (_ucomerciais) {
        return Ok(_mapper.Map<IEnumerable<UComercialDto>>(
                      await _ucomerciais.GetData().Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_ucomerciais) {
        return Ok(await _ucomerciais.SelectList(
                            u => new { u.Id, u.Denominacao }
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_ucomerciais) {
        return Ok(new KeyValuePair<int, int>(_ucomerciais.Count(),
                                             _ucomerciais.Pages(size: k ?? 16)));
      }
    }
  }
}
