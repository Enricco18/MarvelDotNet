using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Controllers.Request;
using WebApplication.Database;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ClientsDbContext context;


        public ClientsController(ClientsDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Get all Clients
        /// </summary>
        /// <response code="200">Ok</response>
        [HttpGet]
        public IEnumerable<Client> GetAll()
        {
            return context.Clients;
        }

        /// <summary>
        /// Get a Client
        /// </summary>
        ///  <response code="200">Ok</response>
        ///   <response code="404">Not Found</response>
        [HttpGet("{cpf}")]
        public IActionResult Get(String cpf)
        {
            Client client = context.Clients.FirstOrDefault(obj => obj.CPF == cpf);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        /// <summary>
        /// Creates a Client
        /// </summary>
        /// <response code="201">Created</response>
        [HttpPost]
        public IActionResult Create(ClientCreateDTO newClient)
        {
            Client client = newClient.toModel();
            context.Clients.Add(client);
            context.SaveChanges();

            return CreatedAtAction(nameof(Create), new { id = client.Id }, client);
        }

        /// <summary>
        /// Update a Client
        /// </summary>
        ///  <response code="200">Ok</response>
        ///   <response code="404">Not Found</response>
        [HttpPut("{cpf}")]
        public IActionResult Update(String cpf, ClientUpdateDTO newClient)
        {
            Client client = context.Clients.FirstOrDefault(obj => obj.CPF == cpf);
            if(client == null)
            {
                return NotFound();
            }

            client.Name = newClient.Name ?? client.Name;
            client.Age = newClient.Age ?? client.Age;
            client.Address = newClient.Address ?? client.Address;
            client.Email = newClient.Email ?? client.Email;
            client.Phone = newClient.Phone ?? client.Phone;

            context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Deletes a Client
        /// </summary>
        ///  <response code="200">Ok</response>
        ///   <response code="404">Not Found</response>
        [HttpDelete("{cpf}")]
        public IActionResult Delete(String cpf)
        {
            Client client = context.Clients.FirstOrDefault(obj => obj.CPF == cpf);
            if (client == null)
            {
                return NotFound();
            }

            context.Remove(client);
            context.SaveChanges();
            return Ok();
        }


    }
}
