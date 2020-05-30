using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using FluentValidation;

using Api.Models;
using Api.Models.Validations;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Api.Controllers {
  [Route("[controller]")]
  [ApiController]
  public class ConsorciosController : ControllerBase {
    private readonly IConsorcioService _consorcios;
    private readonly IMapper _mapper;

    public ConsorciosController(IConsorcioService consorcios, IMapper mapper) {
      _consorcios = consorcios;
      _mapper = mapper;
    }

    // GET: Consorcios
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_consorcios) {
        return Ok(_mapper.Map<IEnumerable<ConsorcioDto>>(await _consorcios.ListAsync()));
      }
    }

    // GET: Consorcios/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_consorcios) {
        Consorcio consorcio = await _consorcios.GetFirstAsync(c => c.Id == id);
        if (consorcio == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<ConsorcioDto>(consorcio));
      }
    }

    // PUT: Consorcios/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ConsorcioDto dto) {
      using (_consorcios) {
        if (_consorcios.Exists(c => c.Id == id)) {
          ConsorcioValidator validator = new ConsorcioValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _consorcios.Update(_mapper.Map<Consorcio>(dto));
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

    // POST: Consorcios
    [HttpPost]
    public async Task<IActionResult> Post(ConsorcioDto dto) {
      Consorcio consorcio = new Consorcio();
      using (_consorcios) {
        ConsorcioValidator validator = new ConsorcioValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _consorcios.Insert(consorcio = _mapper.Map<Consorcio>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<Consorcio>(consorcio));
    }

    // DELETE: Consorcios/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_consorcios) {
        Consorcio consorcio = await _consorcios.GetByIdAsync(id);
        if (consorcio == null) {
          return NotFound();
        }
        try {
          await _consorcios.Delete(consorcio);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{mn}")]
    public async Task<IActionResult> List(int mn) {
      using (_consorcios) {
        return Ok(_mapper.Map<IEnumerable<ConsorcioDto>>(
                      await _consorcios.ListAsync(c => c.MunicipioId == mn,
                                                c => c.OrderBy(q => q.Fantasia))));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_consorcios) {
        return Ok(_mapper.Map<IEnumerable<ConsorcioDto>>(
                      await _consorcios.PageListAsync(skip: p, take: k)));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_consorcios) {
        return Ok(await _consorcios.SelectListAsync(
                            c => new { c.Id, c.Fantasia, c.Cnpj },
                            order: c => c.OrderBy(q => q.Fantasia)));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_consorcios) {
        return Ok(new KeyValuePair<int, int>(_consorcios.Count(),
                                             _consorcios.Pages(size: k ?? 16)));
      }
    }
  }
}
