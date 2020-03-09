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
  public class FornecedoresController : ControllerBase {
    private readonly IFornecedorService _fornecedores;
    private readonly IMapper _mapper;

    public FornecedoresController(IFornecedorService fornecedores, IMapper mapper) {
      _fornecedores = fornecedores;
      _mapper = mapper;
    }

    // GET: Fornecedores
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_fornecedores) {
        return Ok(_mapper.Map<IEnumerable<FornecedorDto>>(
                      await _fornecedores.ListAsync(
                                order: f => f.OrderBy(q => q.Fantasia))));
      }
    }

    // GET: Fornecedores/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_fornecedores) {
        Fornecedor fornecedor = await _fornecedores.GetFirstAsync(f => f.Id == id);
        if (fornecedor == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<FornecedorDto>(fornecedor));
      }
    }

    // PUT: Fornecedores/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, FornecedorDto dto) {
      using (_fornecedores) {
        if (_fornecedores.Exists(f => f.Id == id)) {
          FornecedorValidator validator = new FornecedorValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _fornecedores.Update(_mapper.Map<Fornecedor>(dto));
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

    // POST: Fornecedores
    [HttpPost]
    public async Task<IActionResult> Post(FornecedorDto dto) {
      Fornecedor fornecedor = new Fornecedor();
      using (_fornecedores) {
        FornecedorValidator validator = new FornecedorValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _fornecedores.Insert(fornecedor = _mapper.Map<Fornecedor>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<FornecedorDto>(fornecedor));
    }

    // DELETE: Fornecedores/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_fornecedores) {
        Fornecedor fornecedor = await _fornecedores.GetByIdAsync(id);
        if (fornecedor == null) {
          return NotFound();
        }
        try {
          await _fornecedores.Delete(fornecedor);
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
      using (_fornecedores) {
        return Ok(_mapper.Map<IEnumerable<FornecedorDto>>(
                      await _fornecedores.PagedListAsync(
                                order: f => f.OrderBy(q => q.Fantasia),
                                skip: p, take: k)));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_fornecedores) {
        return Ok(await _fornecedores.SelectListAsync(
                            f => new { f.Id, f.Fantasia, f.Cnpj },
                            order: f => f.OrderBy(q => q.Fantasia)));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_fornecedores) {
        return Ok(new KeyValuePair<int, int>(_fornecedores.Count(),
                                             _fornecedores.Pages(size: k ?? 16)));
      }
    }
  }
}
