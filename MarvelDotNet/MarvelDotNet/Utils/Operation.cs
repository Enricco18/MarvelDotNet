using MarvelDotNet.Models;
using MarvelDotNet.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MarvelDotNet.Utils
{
    static class OperationsMethod {

        static public void Execute(this Operation op)
        {
            using (var db = new SQLiteContext())
            {
                String name, CPF, email, phone, address;
                Int32? age;
                Client client;
                switch (op)
                {
                    case Operation.QUIT:
                        Menu.QuitDialog();
                        System.Environment.Exit(0);
                        break;

                    case Operation.CREATE:
                        name = Menu.GetName();
                        age = Menu.GetAge();
                        if (age == null)
                        {
                            age = 0;
                        }
                        CPF = Menu.GetCPF();
                        email = Menu.GetEmail();
                        phone = Menu.GetPhone();
                        address = Menu.GetAdress();

                        client = new Client(name, (int)age, CPF, email, phone, address);
                        db.Add(client);
                        db.SaveChanges();
                        Console.WriteLine($"Cliente inserido com sucesso: {client.ToString()}");

                        break;

                    case Operation.READ:
                        String allOrCPF = Menu.GetAllOrByCPF();
                        
                        List < Client > clients = new List<Client>();
                        if (allOrCPF== "A")
                        {
                           clients = db.Clients.ToList();
                        }
                        else
                        {
                            CPF = Menu.GetCPF();
                            client = db.Clients.Where<Client>(x => x.CPF == CPF).FirstOrDefault();
                            
                            if (client != null)
                            {
                                
                                clients.Add(client);
                            }
                        }
                        Menu.PrintClient(clients);
                        break;

                    case Operation.UPDATE:
                        CPF = Menu.GetCPF();
                        client = db.Clients.Where<Client>(x => x.CPF == CPF).FirstOrDefault();
                        if (client == null)
                        {
                            Console.WriteLine("Cliente não encontrado.");
                            break;
                        }

                        Console.WriteLine("Caso não queira fazer alteração, só aperte Enter sem digitar nada.");
                        Console.WriteLine($"Idade: {client.Age}");
                        age = Menu.GetAge();
                        if (age!=null)
                        {
                            client.Age = (int)age;
                        }
                        Console.WriteLine($"Name: {client.Name}");
                        name = Menu.GetName();
                        if (!String.IsNullOrEmpty(name))
                        {
                            client.Name = name;
                        }
                        Console.WriteLine($"Email: {client.Email}");
                        email = Menu.GetEmail();
                        if (!String.IsNullOrEmpty(email))
                        {
                            client.Email = email;
                        }
                        Console.WriteLine($"Tel: {client.Phone}");
                        phone = Menu.GetPhone();
                        if (!String.IsNullOrEmpty(phone))
                        {
                            client.Phone = phone;
                        }
                        Console.WriteLine($"Endereço: {client.Address}");
                        address = Menu.GetAdress();
                        if (!String.IsNullOrEmpty(address))
                        {
                            client.Address = address;
                        }
                        db.SaveChanges();
                        Console.WriteLine($"Informações atualizadas com sucesso: {client}");
                        break;

                    case Operation.DELETE:
                        CPF = Menu.GetCPF();
                        client = db.Clients.Where<Client>(x => x.CPF == CPF).FirstOrDefault();
                        if(client == null)
                        {
                            Console.WriteLine("Cliente não encontrado na base de dados!");
                            break;
                        }
                        db.Remove(client);
                        db.SaveChanges();
                        Console.WriteLine($"Cliente de CPF: {client.CPF} deletado com sucesso!");
                        break;
                 }        
            }
        }
    }
    enum Operation
    {
        QUIT,CREATE, READ, UPDATE,DELETE
    }
}
