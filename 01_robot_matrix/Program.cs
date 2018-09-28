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
        // додавання елементу в масив помилок
        static void AddErr(ref string[] arr, string element)
        {
            Array.Resize(ref arr, arr.Length + 1);
            arr[arr.Length - 1] = element;
        }
        
        // заповнення масиву крапочками для візуального відображення поля
        static void Fill(char[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    arr[i, j] = '.';
                }
        }

        // вивід матриці
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
            string [] err = new string [0]; // сюди збергіатимемо всі меседжи з помилками
            char[,] matrix = new char[20, 50]; // матриця по якій рухається робот
            Fill(matrix);

            Console.WriteLine($"Enter first position of (R)obot:"); 
            int x, y; // сюди зберігатимемо кординати кожного ходу робота
            int left = 0, right = 0, error = 0, way = 0; // змінні для моніторингу статистики

            Console.Write($"x < {matrix.GetLength(0)}: "); // питаємо початкові позиції в користувача
            string tmp = Console.ReadLine();
            if (!int.TryParse(tmp, out x)) // якщо щось некоретне приходить від користувача, по замовчуванню 0
               x = 0;

            Console.Write($"y < {matrix.GetLength(1)}: "); // кордината н аналігочіно іксу
            tmp = Console.ReadLine();
            if (!int.TryParse(tmp, out y)) 
                y = 0;

            matrix[x, y] = 'r'; // малюємо стартову точку робота
            // команда для робота
            string command = "5 L 4 R 3 R 2 L 10 R 3 L 2 R 1";
            //string command = "5 L 4 L 3 L 2 R 5 R 4 R 3 R 2";
            //string command = "5 L 4 R 3 L 2 R 10 L 3 R 2 L 1";
            string[] moves; // масив куди спарсимо команди
            moves = command.Split(" .!:;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            Console.Write($"\nSize of matrix: {matrix.GetLength(0)} x {matrix.GetLength(1)}. First position for robot [{x}, {y}]. Commands for robot:\t");
            
            foreach (string d in moves)
            {
                Console.Write($"{d} ");
            }
            Console.WriteLine("\nr (start point) --> R (finish point)");
            int count = 0; //кількість ходів
            int xc = 1, yc = 1; // крок на який переміщається робот
            bool axisY = true; // прапорець на якій вісі ми знаходимось, стартуємо з 'y' і далі по руху робота визначаємо ліво право
            int l = 0, r = 0; // прапорці для визначення чи робот повертає кілька разів в одну сторону (наприклда тричі підряд вліво)
            while (count < moves.Length)
            {
                if (int.TryParse(moves[count], out int go)) //якщо елемент парситься в число, то рухаємо на таку кількість ходів вперед залежно від параметрів
                {
                   
                    if (axisY)
                    {
                        if (y + go * yc < matrix.GetLength(1) && y + go * yc > 0)
                        {
                            for (int i = 0; i < go; i++)
                                matrix[x, y += yc] = 'O';
                            way += go;
                        }
                        else
                        {
                            AddErr(ref err, $"Move '{moves[count - 1]} {moves[count]}' is impossible");
                            ++error;
                        }
                        axisY = !axisY;
                    }
                    else
                    {
                        if (x + go * xc < matrix.GetLength(0) && x + go * xc > 0)
                        {
                            for (int i = 0; i < go; i++)
                                matrix[x += xc, y] = 'O';
                            way += go;
                        }
                        else
                        {
                            AddErr(ref err, $"Move '{moves[count - 1]} {moves[count]}' is impossible");
                            ++error;
                        }
                        axisY = !axisY;
                    }
                }
                else
                {// якщо не парситься, то зчитуємо параметри повороту
                    {
                        if (moves[count] == "R")
                        {
                            ++r;
                            l = 0;
                            if (r % 2 == 0) 
                                yc = -yc;
                            else
                                xc = -xc;
                            ++right;
                        }
                       
                        if (moves[count] == "L")
                        {
                            ++l;
                            r = 0;
                            if (l % 2 == 1)
                                xc = -xc;
                            else
                                yc = -yc;
                            ++left;
                        }
                   
                        //всі інші команди не R чи L просто ігноритимуться
                    }
                }
                ++count;
            }
            matrix[x, y] = 'R'; //фінальна точка робота
            Print(matrix); // виводимо матрицю з ходами
            //статистика
            Console.WriteLine($"\nQ-ty moves:\t{way}\nLeft-turn:\t{left}\nRight-turn:\t{right}\nError(s):\t{error}\n");
            foreach (string e in err)
            {
                Console.WriteLine($"{e} ");
            }

            Console.ReadKey();
        }
    }
}

