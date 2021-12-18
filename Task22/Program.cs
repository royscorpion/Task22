using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task22
{
    class Program
    {
        // Многопоточное приложение на основе библиотеки TPL
        // Метод генерации массива случайных чисел от 0 до 1000 с размером массива, указанным пользователем
        static int[] RandomArray()
        {
            Console.Write("Укажите размер массива: ");
            int n = InputIntNumber();
            Random random = new Random();
            int[] array = new int[n];
            Console.WriteLine("Сгенерирован массив");
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0,1000);
                Console.Write(array[i] + " ");
            }
            Console.WriteLine();
            return (array);
        }
        // Метод вычисления суммы чисел массива и поиска максимального числа в массиве
        static void SumArray(Task task,object a)
        {
            int[] array = (int[])a;
            int SumArray = 0;
            foreach (int n in array) 
                SumArray += n;
            Console.WriteLine($"Сумма чисел массива равна {SumArray}");
            Console.WriteLine($"Максимальное число в массиве: {array.Max()}");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Массив случайных чисел");
            //Func<int[]> func = new Func<int[]>(RandomArray);
            //Task<int[]> task1 = new Task<int[]>(func);
            Task<int[]> task1 = new Task<int[]>(() => RandomArray()); // использована техника предположения делегата
            task1.Start();

            Action<Task, object> action = new Action<Task, object>(SumArray); 
            Task task2 = task1.ContinueWith(action, task1.Result); // задача-продолжение

            task2.Wait(); //ожидание завершения task2
            Console.ReadKey();
        }
        //Проверка корректности введенных данных (integer)
        static int InputIntNumber()
        {
            int number = 0;
            bool x;
            do
            {
                try
                {
                    number = Convert.ToInt32(Console.ReadLine());
                    x = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка! {0}\nПопробуйте еще раз\n", ex.Message);
                    Console.Write("Введите целое число: ");
                    x = true;
                }
            } while (x);
            return number;
        }

    }

}
