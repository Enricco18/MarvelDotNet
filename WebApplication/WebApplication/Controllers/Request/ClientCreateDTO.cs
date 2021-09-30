using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Controllers.Request
{
    public class ClientCreateDTO
    {
        [Required]
        public String Name { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public Int32 Age { get; set; }

        [Required]
        public String CPF { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Not Valid Format")]
        public String Email { get; set; }

        [Required]
        [Phone]
        public String Phone { get; set; }

        [Required]
        public String Address { get; set; }

        public ClientCreateDTO()
        {
        }


        public override string ToString()
        {
            return $"{this.Name}, {this.Email}, {this.Phone}, {this.CPF}, {this.Age}";
        }

        public Client toModel()
        {
            Client client = new Client(Name, Age, CPF, Email, Phone, Address);

            return client;
        }
    }
}
