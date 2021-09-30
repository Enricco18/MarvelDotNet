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
        [HttpGet]
        public IEnumerable<Client> GetAll()
        {
            return context.Clients;
        }
        [HttpGet("{cpf}")]
        public Client Get(String cpf)
        {
            return context.Clients.FirstOrDefault(obj => obj.CPF == cpf);
        }
        
        [HttpPost]
        public IActionResult Create(ClientCreateDTO newClient)
        {
            Client client = newClient.toModel();
            context.Clients.Add(client);
            context.SaveChanges();

            return CreatedAtAction(nameof(Create), new { id = client.Id }, client);
        }

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
