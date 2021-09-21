using MarvelDotNet.Utils.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MarvelDotNet.Models
{
    // nome, idade, CPF, e-mail, telefone e endereço.
    public class Client
    {
        public Guid Id {  get; set; }

        [Required]
        public String Name { get;  set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public Int32 Age { get;  set; }

        [Required]
        [CPFValidator]
        public String CPF { get;  set; }

        [Required]
        [EmailAddress(ErrorMessage = "Not Valid Format")]
        public String Email { get;  set; }

        [Required]
        [Phone]
        public String Phone { get;  set; }

        [Required]
        public String Address { get;  set; }

        public Client(string name, int age, string cPF, string email, string phone, string address)
        {
            Name = name;
            Age = age;
            CPF = cPF;
            Email = email;
            Phone = phone;
            Address = address;
        }

        public override string ToString()
        {
            return $"ID={this.Id}, {this.Name}, {this.Email}, {this.Phone}, {this.CPF}, {this.Age}";
        }
    }
}
