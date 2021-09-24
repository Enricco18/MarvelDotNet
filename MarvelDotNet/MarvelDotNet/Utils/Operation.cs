using MarvelDotNet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using MarvelDotNet.Database;

namespace MarvelDotNet.Utils
{
    static class OperationsMethod { 
        static public void Execute(this Operation op)
        {
            String name, CPF, email, phone, address;
            Int32? age;
            Client client;
            DatabaseManager manager = new DatabaseManager();
            using (manager)
            {
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
                        manager.createClient(client);
                        Console.WriteLine($"Cliente inserido com sucesso: {client.ToString()}");

                        break;

                    case Operation.READ:
                        String allOrCPF = Menu.GetAllOrByCPF();

                        List<Client> clients = new List<Client>();
                        if (allOrCPF == "A")
                        {
                           manager.findAll();
                        }
                        else
                        {
                            CPF = Menu.GetCPF();
                            manager.findByCPF(CPF);
                        }
                        
                        break;

                    case Operation.UPDATE:
                        CPF = Menu.GetCPF();
                        Console.WriteLine("Caso não queira fazer alteração, só aperte Enter sem digitar nada.");
                        age = Menu.GetAge();
                        if (age != null)
                        {
                            manager.updateClient(CPF, "Age", age.ToString());
                        }
                        name = Menu.GetName();
                        if (!String.IsNullOrEmpty(name))
                        {
                            manager.updateClient(CPF, "Name", name);
                        }
                        email = Menu.GetEmail();
                        if (!String.IsNullOrEmpty(email))
                        {
                            manager.updateClient(CPF, "Email", email);
                        }
                        phone = Menu.GetPhone();
                        if (!String.IsNullOrEmpty(phone))
                        {
                            manager.updateClient(CPF, "Phone", phone);
                        }
                        address = Menu.GetAdress();
                        if (!String.IsNullOrEmpty(address))
                        {
                            manager.updateClient(CPF, "Address", address);
                        }
                        Console.WriteLine($"Informações atualizadas com sucesso");
                        break;

                    case Operation.DELETE:
                        CPF = Menu.GetCPF();
                        manager.deleteClient(CPF);
                        Console.WriteLine($"Cliente de CPF: {CPF} deletado com sucesso!");
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
