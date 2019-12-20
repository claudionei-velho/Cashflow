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
  public class EncargosController : ControllerBase {
    private readonly IServiceBase<Encargo> _encargos;
    private readonly IMapper _mapper;

    public EncargosController(IServiceBase<Encargo> encargos, IMapper mapper) {
      _encargos = encargos;
      _mapper = mapper;
    }

    // GET: Encargos
    [HttpGet]
    public async Task<IActionResult> Get() {      
      using (_encargos) {
        return Ok(_mapper.Map<IEnumerable<EncargoDto>>(
                      await _encargos.GetData(
                                order: e => e.OrderBy(q => q.Grupo)
                                             .ThenBy(q => q.Id)
                            ).ToListAsync()));
      }      
    }

    // GET: Encargos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_encargos) {
        Encargo encargo = await _encargos.GetByIdAsync(id);
        if (encargo == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<EncargoDto>(encargo));
      }
    }

    // PUT: Encargos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, EncargoDto dto) {
      using (_encargos) {
        if (_encargos.Exists(e => e.Id == id)) {
          EncargoValidator validator = new EncargoValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _encargos.Update(_mapper.Map<Encargo>(dto));
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

    // POST: Encargos
    [HttpPost]
    public async Task<IActionResult> Post(EncargoDto dto) {
      using (_encargos) {
        EncargoValidator validator = new EncargoValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _encargos.Insert(_mapper.Map<Encargo>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }        
      }
      return Ok();
    }

    // DELETE: Encargos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_encargos) {
        Encargo encargo = await _encargos.GetByIdAsync(id);
        if (encargo == null) {
          return NotFound();
        }
        await _encargos.Delete(encargo);
      }
      return NoContent();
    }

    [HttpGet, Route("List/{ab}")]
    public async Task<IActionResult> List(char ab) {
      using (_encargos) {
        return Ok(_mapper.Map<IEnumerable<EncargoDto>>(
                      await _encargos.GetData(
                                e => e.Grupo.Equals(ab),
                                e => e.OrderBy(q => q.Id)
                            ).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_encargos) {
        return Ok(_mapper.Map<IEnumerable<EncargoDto>>(
                      await _encargos.GetData(
                                order: e => e.OrderBy(q => q.Grupo)
                                             .ThenBy(q => q.Id)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_encargos) {
        return Ok(await _encargos.SelectList(
                            e => new { e.Id, e.Grupo, e.Denominacao }
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_encargos) {
        return Ok(new KeyValuePair<int, int>(_encargos.Count(),
                                             _encargos.Pages(size: k ?? 16)));
      }
    }
  }
}
