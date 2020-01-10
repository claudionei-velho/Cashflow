﻿using System.Collections.Generic;
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
  public class LotesController : ControllerBase {
    private readonly ILoteService _lotes;
    private readonly IMapper _mapper;

    public LotesController(ILoteService lotes, IMapper mapper) {
      _lotes = lotes;
      _mapper = mapper;
    }

    // GET: Lotes
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_lotes) {
        return Ok(_mapper.Map<IEnumerable<LoteDto>>(
                      await _lotes.GetData().ToListAsync()));
      }
    }

    // GET: Lotes/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_lotes) {
        Lote lote = await _lotes.GetFirstAsync(l => l.Id == id);
        if (lote == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<LoteDto>(lote));
      }
    }

    // PUT: Lotes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, LoteDto dto) {
      using (_lotes) {
        if (_lotes.Exists(l => l.Id == id)) {
          LoteValidator validator = new LoteValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _lotes.Update(_mapper.Map<Lote>(dto));
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

    // POST: Lotes
    [HttpPost]
    public async Task<IActionResult> Post(LoteDto dto) {
      using (_lotes) {
        LoteValidator validator = new LoteValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _lotes.Insert(_mapper.Map<Lote>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }        
      }
      return Ok();
    }

    // DELETE: Lotes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_lotes) {
        Lote lote = await _lotes.GetByIdAsync(id);
        if (lote == null) {
          return NotFound();
        }
        await _lotes.Delete(lote);
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_lotes) {
        return Ok(_mapper.Map<IEnumerable<LoteDto>>(
                      await _lotes.GetData(l => l.BaciaId == id).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_lotes) {
        return Ok(_mapper.Map<IEnumerable<LoteDto>>(
                      await _lotes.GetData().Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_lotes) {
        return Ok(await _lotes.SelectList(l => new { l.Id, l.Denominacao }).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_lotes) {
        return Ok(await _lotes.SelectList(
                            l => new { l.Id, l.Denominacao },
                            l => l.BaciaId == id
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_lotes) {
        return Ok(new KeyValuePair<int, int>(_lotes.Count(),
                                             _lotes.Pages(size: k ?? 16)));
      }
    }
  }
}
