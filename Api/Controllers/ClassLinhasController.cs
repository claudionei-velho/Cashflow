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
  public class ClassLinhasController : ControllerBase {
    private readonly IServiceBase<ClassLinha> _classLinhas;
    private readonly IMapper _mapper;

    public ClassLinhasController(IServiceBase<ClassLinha> classLinhas, IMapper mapper) {
      _classLinhas = classLinhas;
      _mapper = mapper;
    }

    // GET: ClassLinhas
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_classLinhas) {
        return Ok(_mapper.Map<IEnumerable<ClassLinhaDto>>(
                      await _classLinhas.GetData().ToListAsync()));
      }
    }

    // GET: ClassLinhas/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_classLinhas) {
        ClassLinha classLinha = await _classLinhas.GetByIdAsync(id);
        if (classLinha == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<ClassLinhaDto>(classLinha));
      }
    }    

    // PUT: ClassLinhas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ClassLinhaDto dto) {
      using (_classLinhas) {
        if (_classLinhas.Exists(c => c.Id == id)) {
          await _classLinhas.Update(_mapper.Map<ClassLinha>(dto));
        }
        else {
          return BadRequest();
        }
      }
      return Ok();
    }

    // POST: ClassLinhas
    [HttpPost]
    public async Task<IActionResult> Post(ClassLinhaDto dto) {
      ClassLinha classLinha = new ClassLinha();
      using (_classLinhas) {
        if (dto == null) {
          return BadRequest();          
        }
        await _classLinhas.Insert(classLinha = _mapper.Map<ClassLinha>(dto));
      }
      return Ok(_mapper.Map<ClassLinhaDto>(classLinha));
    }

    // DELETE: ClassLinhas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_classLinhas) {
        ClassLinha classLinha = await _classLinhas.GetByIdAsync(id);
        if (classLinha == null) {
          return NotFound();
        }
        try { 
          await _classLinhas.Delete(classLinha);
          return NoContent();
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }      
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_classLinhas) {
        return Ok(_mapper.Map<IEnumerable<ClassLinhaDto>>(
                      await _classLinhas.GetData().Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_classLinhas) {
        return Ok(await _classLinhas.SelectList(
                            c => new { c.Id, c.Denominacao }
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_classLinhas) {
        return Ok(new KeyValuePair<int, int>(_classLinhas.Count(),
                                             _classLinhas.Pages(size: k ?? 16)));
      }
    }
  }
}
