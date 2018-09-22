/*
 Завдання 1.
Робот рухається по території(матриці). Робот може повертати тільки на кут 90 градусів(направо чи наліво).
Для керуванням робом задається рядок з послідовністю команд виду "7 R 50 L  2"(вперед на 7 кроків, повернути направо, 50 кроків вперед, 
повернути наліво, 2 кроки вперед). Створити програму для відображення траєкторіїї руху роботу. 
Користувач задає стартову точку робота та рядок з набором команд.
Використати прямокутну матрицю, що є полем по якому рухається робот.

Якщо чергову команду неможливо виконати, то повідомити про це повідомленням на екрані та спробувати виконати наступну команду.
Вивести загальну довжину шляху, що пройшов робот, кількість лівих та правих поворотів, кількість невиконаних команд.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_robot_matrix
{
    class Program
    {
        static void Fill(char[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    arr[i, j] = '.';
                }
        }

        static void Print(char[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                    Console.Write($"{arr[i, j]} ");
                Console.WriteLine();
            }
            Console.WriteLine("\n");
        }

        static void Main(string[] args)
        {
            char[,] matrix = new char[20, 50];
            Fill(matrix);
            Print(matrix);
            Console.WriteLine($"Enter first position of (R)obot:");
            int x, y;
            Console.Write($"x < {matrix.GetLength(0)}: ");
            string tmp = Console.ReadLine();
            if (int.TryParse(tmp, out x)) ;
            else
                x = 0;
            Console.Write($"y < {matrix.GetLength(1)}: ");
            tmp = Console.ReadLine();
            if (int.TryParse(tmp, out y)) ;
            else
                y = 0;
            matrix[x, y] = 'R';
            Print(matrix);
            string command = "7 R 20 L 2";
            string[] digits;
            digits = command.Split(" .!:;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string d in digits)
            {
                Console.WriteLine(d);
            }
            for (int i = 0; i < Convert.ToInt32(digits[0]); i++)
            {
                matrix[x + i, y] = 'O';
            }
            x += Convert.ToInt32(digits[0]);
            for (int i = 0; i < Convert.ToInt32(digits[2]); i++)
                matrix[x, y + i] = 'O';
            y += Convert.ToInt32(digits[2]);
            matrix[x, y] = 'R';
            Print(matrix);
            Console.WriteLine("\n");
        }
    }
}
