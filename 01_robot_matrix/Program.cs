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
        }

        static void Main(string[] args)
        {
            char[,] matrix = new char[20, 50];
            Fill(matrix);

            Console.WriteLine($"Enter first position of (R)obot:");
            int x, y;
            int left = 0, right = 0, error = 0, way = 0;

            Console.Write($"x < {matrix.GetLength(0)}: ");
            string tmp = Console.ReadLine();
            if (!int.TryParse(tmp, out x)) 
               x = 0;

            Console.Write($"y < {matrix.GetLength(1)}: ");
            tmp = Console.ReadLine();
            if (!int.TryParse(tmp, out y)) 
                y = 0;

            matrix[x, y] = 'r';
            string command = "5 R 4 L 3 R 2 L 10 L 3 R 2";
            string[] moves;
            moves = command.Split(" .!:;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            Console.Write($"\nSize of matrix: {matrix.GetLength(0)} x {matrix.GetLength(1)}. First position for robot [{x}, {y}]. Commands for robot:\t");
            foreach (string d in moves)
            {
                Console.Write($"{d} ");
            }
            Console.WriteLine();
            int count = 0;
            int xc = 1, yc = 1;
            bool turn = true;
            while (count < moves.Length)
            {
                if (int.TryParse(moves[count], out int go))
                {
                    way += go;
                    if (turn)
                    {
                        if (y + go < matrix.GetLength(1))
                            for (int i = 0; i < go; i++)
                                matrix[x, y += yc] = 'O';
                        else
                            ++error;
                        turn = !turn;
                    }
                    else
                    {
                        if (x + go < matrix.GetLength(0))
                            for (int i = 0; i < go; i++)
                                matrix[x += xc, y] = 'O';
                        else
                            ++error;
                        turn = !turn;
                    }
                }
                else
                {
                    if (!turn)
                    {
                        if (moves[count] == "R")
                        {
                            xc = 1;
                            ++right;
                        }

                        if (moves[count] == "L")
                        {
                            xc = -1;
                            ++left;
                        }
                    }
                    else
                    if (turn)
                    {
                        if (moves[count] == "R")
                        {
                            yc = -1;
                            ++right;
                        }

                        if (moves[count] == "L")
                        {
                            yc = 1;
                            ++left;
                        }
                    }
                    else
                       ++error;
                }
                ++count;
            }
            matrix[x, y] = 'R';
            Print(matrix);
            Console.WriteLine($"\nQ-ty moves:\t{way}\nLeft-turn:\t{left}\nRight-turn:\t{right}\nError(s):\t{error}\n");
        }
    }
}

/*
 while (count<moves.Length)
            {
                if (int.TryParse(moves[count], out int go))
                {
                    way += go;
                    if (turn)
                    {
                        if (y + go<matrix.GetLength(1))
                            for (int i = 0; i<go; i++)
                                matrix[x += xc, y += yc] = 'O';
                        else
                            ++error;
                    }
                    else
                    {
                        if (x + go<matrix.GetLength(0))
                            for (int i = 0; i<go; i++)
                                matrix[x += xc, y] = 'O';
                        else
                            ++error;
                    }
                }
                else
                {
                    if (moves[count] == "L")
                    {
                        //yc = -yc;
                        ++right;
                        turn = true;
                    }
                    else
                         if (moves[count] == "R")
                    {
                        //xc = -xc;
                        ++left;
                        turn = false;
                    }
                    else
                        ++error;
                }
                ++count;
                //SetRobot(matrix, x, y);
                //Print(matrix);
            }
*/