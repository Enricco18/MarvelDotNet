using MarvelDotNet.Models;
using MarvelDotNet.Repository;
using MarvelDotNet.Utils;
using System;
using System.Linq;

namespace MarvelDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            
                Boolean showMenu = true;

                while(showMenu== true)
                {
                    try
                    {
                        Operation op = Menu.generateMenu();
                        op.Execute();
                    
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        showMenu = Menu.Continue();
                    }

                }

        }
    }
}

