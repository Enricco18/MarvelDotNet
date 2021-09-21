using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MarvelDotNet.Models;

namespace MarvelDotNet.Utils
{
    class Menu
    {

        static public Operation generateMenu()
        {
            Console.WriteLine("Qual operação deseja executar?");

            foreach (Operation operation in Enum.GetValues(typeof(Operation)))
            {
                Console.WriteLine($"{((int)operation)}- {operation.ToString()}");
            }
            String inputStr = Console.ReadLine();
            Int32 input;

            Boolean isNumber = Int32.TryParse(inputStr, out input);
            if (!isNumber || !Enum.IsDefined(typeof(Operation), input))
            {
                throw new InvalidOperationException("Entrada fora dos padrões válidos!");
            }

            return (Operation) Enum.ToObject(typeof(Operation),input);
        }

        internal static bool Continue()
        {
            Console.WriteLine("Deseja realizar mais alguma operação? [Y]yes/[N]no");
            String response = Console.ReadLine();
            Console.WriteLine("");
            return  response.ToUpper() == "Y";
        }

        internal static string GetPhone()
        {
            Console.WriteLine("Digite seu telefone:");
            return Console.ReadLine();
        }

        internal static string GetAdress()
        {
            Console.WriteLine("Digite seu endereço:");
            return Console.ReadLine();
        }

        internal static String GetAllOrByCPF()
        {
            Console.WriteLine("Digite 'A' para recuperar todos os dados, ou qualquer tecla pra recuperar o dado pelo CPF ");
            return Console.ReadLine();
        }

        internal static string GetEmail()
        {
            Console.WriteLine("Digite seu e-mail:");
            return Console.ReadLine();
        }

        internal static int? GetAge()
        {
            Console.WriteLine("Digite sua idade:");
            String numberStr = Console.ReadLine();
            if (String.IsNullOrEmpty(numberStr))
            {
                return null;
            }
            return Int32.Parse(numberStr);
        }

        internal static void PrintClient(List<Client> clients)
        {
            if (clients.Count > 0)
            {
                Console.WriteLine("");
                clients.ForEach(x => Console.WriteLine(x));
                Console.WriteLine("");
                return;
            }
            Console.WriteLine("");
            Console.WriteLine("Sem dados no momento :/");
            Console.WriteLine("");
        }

        internal static string GetCPF()
        {
            Console.WriteLine("Digite seu CPF:");
            return Console.ReadLine();
        }

        internal static string GetName()
        {
            Console.WriteLine("Digite seu nome:");
            return Console.ReadLine();
        }

        internal static void QuitDialog()
        {
            Console.WriteLine("Seu programa será encerrado!");
        }
    }
}
