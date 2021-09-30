using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Controllers.Request
{
    public class ClientUpdateDTO 
    {
        public String Name { get; set; }
        public Int32? Age { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }

        public String Address { get; set; }

        public ClientUpdateDTO()
        {
        }

        public override string ToString()
        {
            return $"{this.Name}, {this.Email}, {this.Phone} {this.Age}";
        }
    }
}
