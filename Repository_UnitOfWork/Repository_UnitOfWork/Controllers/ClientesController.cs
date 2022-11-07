using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository_UnitOfWork.Interfaces;
using Repository_UnitOfWork.Models;
using Repository_UnitOfWork.Utilities;

namespace Repository_UnitOfWork.Controllers
{
    public class ClientesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<Pager<Cliente>>> GetClientes([FromQuery] Params clientParams)
        {
            //return Ok(await _unitOfWork.Cliente.GetAllAsync());
            var rpta = await _unitOfWork.Cliente.GetAllAsync(clientParams.PageIndex,clientParams.PageSize, clientParams.Search);
            return Ok(new Pager<Cliente>(rpta.registros,rpta.totalRegistros,clientParams.PageIndex,clientParams.PageSize,clientParams.Search));
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            return Ok(await _unitOfWork.Cliente.GetByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Cliente>> PutCliente(int id, Cliente cliente)
        {
            if (cliente == null)
            {
                return NotFound();
            }
            _unitOfWork.Cliente.Update(cliente);
            await _unitOfWork.SaveAsync();
            return cliente;
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            _unitOfWork.Cliente.Add(cliente);
            await _unitOfWork.SaveAsync();
            if (cliente==null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(PostCliente), new {id=cliente.Id},cliente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _unitOfWork.Cliente.GetByIdAsync(id);
            if (cliente==null)
            {
                return NotFound();
            }
            _unitOfWork.Cliente.Remove(cliente);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

    }
}
