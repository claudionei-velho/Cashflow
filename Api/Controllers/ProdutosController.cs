﻿using System;
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
  public class ProdutosController : ControllerBase {
    private readonly IProdutoService _produtos;
    private readonly IMapper _mapper;

    public ProdutosController(IProdutoService produtos, IMapper mapper) {
      _produtos = produtos;
      _mapper = mapper;
    }

    // GET: Produtos
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_produtos) {
        return Ok(_mapper.Map<IEnumerable<ProdutoDto>>(
                      await _produtos.ListAsync(
                                order: p => p.OrderBy(q => q.Descricao))));
      }
    }

    // GET: Produtos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_produtos) {
        Produto produto = await _produtos.GetFirstAsync(p => p.Id == id);
        if (produto == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<ProdutoDto>(produto));
      }
    }

    // PUT: Produtos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ProdutoDto dto) {
      using (_produtos) {
        if (_produtos.Exists(p => p.Id == id)) {
          ProdutoValidator validator = new ProdutoValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _produtos.Update(_mapper.Map<Produto>(dto));
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

    // POST: Produtos
    [HttpPost]
    public async Task<IActionResult> Post(ProdutoDto dto) {
      Produto produto = new Produto();
      using (_produtos) {
        ProdutoValidator validator = new ProdutoValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _produtos.Insert(produto = _mapper.Map<Produto>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<ProdutoDto>(produto));
    }

    // DELETE: Produtos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_produtos) {
        Produto produto = await _produtos.GetByIdAsync(id);
        if (produto == null) {
          return NotFound();
        }
        try {
          await _produtos.Delete(produto);
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
      using (_produtos) {
        return Ok(_mapper.Map<IEnumerable<ProdutoDto>>(
                      await _produtos.PageListAsync(
                                order: p => p.OrderBy(q => q.Descricao),
                                skip: p, take: k)));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_produtos) {
        return Ok(await _produtos.SelectListAsync(
                            p => new { p.Id, p.Descricao },
                            order: p => p.OrderBy(q => q.Descricao)));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_produtos) {
        return Ok(new KeyValuePair<int, int>(_produtos.Count(),
                                             _produtos.Pages(size: k ?? 16)));
      }
    }
  }
}
