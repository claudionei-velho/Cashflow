using System;
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
  public class BaciasController : ControllerBase {
    private readonly IBaciaService _bacias;
    private readonly IMapper _mapper;

    public BaciasController(IBaciaService bacias, IMapper mapper) {
      _bacias = bacias;
      _mapper = mapper;
    }

    // GET: Bacias
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_bacias) {
        return Ok(_mapper.Map<IEnumerable<BaciaDto>>(
                      await _bacias.GetData().ToListAsync()));
      }
    }

    // GET: Bacias/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_bacias) {
        Bacia bacia = await _bacias.GetFirstAsync(b => b.Id == id);
        if (bacia == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<BaciaDto>(bacia));
      }
    }

    // PUT: Bacias/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, BaciaDto dto) {
      using (_bacias) {
        if (_bacias.Exists(b => b.Id == id)) {
          BaciaValidator validator = new BaciaValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _bacias.Update(_mapper.Map<Bacia>(dto));
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

    // POST: Bacias
    [HttpPost]
    public async Task<IActionResult> Post(BaciaDto dto) {
      Bacia bacia = new Bacia();
      using (_bacias) {
        BaciaValidator validator = new BaciaValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _bacias.Insert(bacia = _mapper.Map<Bacia>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<BaciaDto>(bacia));
    }

    // DELETE: Bacias/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_bacias) {
        Bacia bacia = await _bacias.GetByIdAsync(id);
        if (bacia == null) {
          return NotFound();
        }
        try {
          await _bacias.Delete(bacia);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_bacias) {
        return Ok(_mapper.Map<IEnumerable<BaciaDto>>(
                      await _bacias.GetData(b => b.MunicipioId == id).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_bacias) {
        return Ok(_mapper.Map<IEnumerable<BaciaDto>>(
                      await _bacias.GetData().Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_bacias) {
        return Ok(await _bacias.SelectList(b => new { b.Id, b.Denominacao }).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_bacias) {
        return Ok(await _bacias.SelectList(
                            b => new { b.Id, b.Denominacao },
                            b => b.MunicipioId == id
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_bacias) {
        return Ok(new KeyValuePair<int, int>(_bacias.Count(),
                                             _bacias.Pages(size: k ?? 16)));
      }
    }
  }
}
