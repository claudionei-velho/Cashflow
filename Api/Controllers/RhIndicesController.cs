﻿using System.Collections.Generic;
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
  public class RhIndicesController : ControllerBase {
    private readonly IServiceBase<RhIndice> _rhIndices;
    private readonly IMapper _mapper;

    public RhIndicesController(IServiceBase<RhIndice> rhIndices, IMapper mapper) {
      _rhIndices = rhIndices;
      _mapper = mapper;
    }

    // GET: RhIndices
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_rhIndices) {
        return Ok(_mapper.Map<IEnumerable<RhIndiceDto>>(
                      await _rhIndices.GetData().ToListAsync()));
      }      
    }

    // GET: RhIndices/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_rhIndices) {
        RhIndice rhIndice = await _rhIndices.GetByIdAsync(id);
        if (rhIndice == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<RhIndiceDto>(rhIndice));
      }
    }

    // PUT: RhIndices/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, RhIndiceDto dto) {
      using (_rhIndices) {
        if (_rhIndices.Exists(rh => rh.Id == id)) {
          await _rhIndices.Update(_mapper.Map<RhIndice>(dto));
        }
        else {
          return BadRequest();
        }
      }
      return Ok();
    }

    // POST: RhIndices
    [HttpPost]
    public async Task<IActionResult> Post(RhIndiceDto dto) {
      using (_rhIndices) {
        if (dto == null) {
          return BadRequest();
        }
        await _rhIndices.Insert(_mapper.Map<RhIndice>(dto));
      }
      return Ok();
    }

    // DELETE: RhIndices/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_rhIndices) {
        RhIndice rhIndice = await _rhIndices.GetByIdAsync(id);
        if (rhIndice == null) {
          return NotFound();
        }
        await _rhIndices.Delete(rhIndice);
      }
      return NoContent();
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_rhIndices) {
        return Ok(_mapper.Map<IEnumerable<RhIndiceDto>>(
                      await _rhIndices.GetData().Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }
    
    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_rhIndices) {
        return Ok(await _rhIndices.SelectList(
                            rh => new { rh.Id, rh.Indice, rh.Denominacao },
                            order: rh => rh.OrderBy(q => q.Indice)
                        ).ToListAsync());
      }
    }
    
    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_rhIndices) {
        return Ok(new KeyValuePair<int, int>(_rhIndices.Count(),
                                             _rhIndices.Pages(size: k ?? 16)));
      }
    }
  }
}