﻿// Control de errores input

using System;
//using System.Data;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("CALCULADORA");

        //Verificación correcta de la entrada de datos
        string cuenta = Input.leerInputValido();

        //Cálculo

        Console.WriteLine("FINALIZADO. PRESIONE ENTER PARA CONTINUAR...");
        string cerrar = Console.ReadLine();
    }
}


class Input
{
    public static string leerInputValido()
    {
        string input = "";
        Console.WriteLine("Ingrese la cuenta a realizar");
        input = Console.ReadLine()!;
        

        // Quitar espacios
        input = input.Replace(" ", "");


        // Input no vacío
        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("El valor ingresado no puede estar vacío. Intente de nuevo.");
            return Input.leerInputValido();
        }
        

        // Reemplazar comas por puntos
        input = input.Replace(",", ".");


        // Caracteres inválidos. Ej: 4+t
        foreach (char c in input)
        {
            if (!char.IsDigit(c) && c != '.' && c != '(' && c != ')' && c != '+' && c != '-' && c != '*' && c != '/')
            {
                Console.WriteLine("Caracter no válido: " + c + "\nIntente nuevamente\n");
                return Input.leerInputValido();
            }
        }


        // Paridad de paréntesis
        int openParentheses = 0;
        int closeParentheses = 0;
        foreach (char c in input)
        {
            if (c == '(')
            {
                openParentheses++;
            }
            else if (c == ')')
            {
                closeParentheses++;
            }
        }
        if (openParentheses < closeParentheses) //Ej: 3+(2*4))
        {
            Console.WriteLine("Error de sintaxis: Paréntesis.\nIntente nuevamente\n");
            return Input.leerInputValido();
        }
        else
        {
            if (openParentheses > closeParentheses) //Ej: 6*(2+3
            {
                for (int i = 0; i < (openParentheses - closeParentheses); i++)
                input = input + ")";
            }
        }


        // Operadores consecutivos. Ej: +/
        for (int i = 0; i < input.Length - 1; i++)
        {
            if ("+-*/".Contains(input[i]) && "+-*/".Contains(input[i + 1]))
            {
                Console.WriteLine("Error de sintaxis: Operadores consecutivos " + input[i] + input[i + 1] + "\nIntente nuevamente\n");
                return Input.leerInputValido();
            }
        }


        // Input con operador como caracter final
        if (input.EndsWith("+") || input.EndsWith("-") || input.EndsWith("*") || input.EndsWith("/"))
        {
            Console.WriteLine("Error de sintaxis: Operador sin número\nIntente nuevamente\n");
            return Input.leerInputValido();
        }


        // Operador previo a cierre de paréntesis. Ej: (12+)
        for (int i = 0; i < input.Length - 1; i++)
        {
            if ("+-*/".Contains(input[i]) && input[i + 1] == ')')
            {
                Console.WriteLine("Error de sintaxis: Operador sin número\nIntente nuevamente\n");
                return Input.leerInputValido();
            }
        }


        // Paréntesis vacíos. Ej: ()
        for (int i = 0; i < input.Length - 1; i++)
        {
            if (input[i] == '(' && input[i + 1] == ')')
            {
                Console.WriteLine("Error de sintaxis: Paréntesis vacío\nIntente nuevamente\n");
                return Input.leerInputValido();
            }
        }


        // Agregar símbolo '*' antes o después de paréntesis. Ej: 2(3+1)5
        for (int i = 0; i < input.Length - 1; i++)
        {
            if (char.IsDigit(input[i]) && input[i + 1] == '(' || input[i] == ')' && char.IsDigit(input[i + 1]))
            {
                input = input.Insert(i + 1, "*");
                Console.WriteLine("Añadido/s símbolo/s *");
            }
        }


        // Puntos decimales consecutivos. Ej: 12..3
        for (int i = 0; i < input.Length - 1; i++)
        {
            if (input[i] == '.' && input[i + 1] == '.')
            {
                Console.WriteLine("Error de sintaxis: Puntos decimales consecutivos\nIntente nuevamente\n");
                return Input.leerInputValido();
            }
        }


        // Separamos los números del input
        string[] numbers = input.Split(new char[] { '+', '-', '*', '/', '(', ')' });


        // Más de un punto decimal. Ej: 2.41.3
        foreach (string number in numbers)
        {
            int decimalPoints = 0;
            foreach (char c in number)
            {
                if (c == '.')
                {
                    decimalPoints++;
                }
                if (decimalPoints > 1)
                {
                    Console.WriteLine("Error de sintaxis: Puntos decimales múltiples en " + number + "\nIntente nuevamente\n");
                    return Input.leerInputValido();
                }

                // Revisar que los números no terminen en '.'
                if (number.EndsWith("."))
                {
                    Console.WriteLine("Error de sintaxis: Punto decimal como final de número" + number + "\nIntente nuevamente\n");
                    return Input.leerInputValido();
                }
            }
        }



        //TODO CORRECTO
        Console.WriteLine("Valor ingresado correctamente. Input corregido:");
        Console.WriteLine(input);
        return input;
    }
}



