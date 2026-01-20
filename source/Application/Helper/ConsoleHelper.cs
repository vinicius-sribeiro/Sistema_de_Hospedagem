using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDeHospedagem.Helper
{
    internal class ConsoleHelper
    {

        public static T ReadValue<T>(string label) where T : struct
        {
            Console.Write(label);

            string errorMessage = "Entrada inválida. Tente novamente: ";

            while (true)
            {
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.Write(errorMessage);
                    continue;
                }
                    
                try
                {
                    T value = (T)Convert.ChangeType(input!, typeof(T));
                    return value;
                }
                catch
                {
                    Console.Write(errorMessage);
                    continue;
                }
            }
        }

        public static E ReadEnum<E>(string label) where E : Enum
        {
            Console.Write(label);
            string errorMessage = "Entrada inválida. Tente novamente: ";
            while (true)
            {
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.Write(errorMessage);
                    continue;
                }

                try
                {
                    E value = (E)Enum.Parse(typeof(E), input!, true);
                    return value;
                }
                catch
                {
                    Console.Write(errorMessage);
                    continue;
                }
            }
        }

    }
}
