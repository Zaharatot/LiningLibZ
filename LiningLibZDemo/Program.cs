using LiningLibZ;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiningLibZDemo
{
    internal class Program
    {

        /// <summary>
        /// Основной метод приложения
        /// </summary>
        static void Main(string[] args)
        {
            //Инициализируем основной класс библиотеки лайнинга
            LiningLibZFacade liningLibZ = new LiningLibZFacade();

            //Формируем путь для сохранения
            string saveFolder = $"{Environment.CurrentDirectory}\\Completed\\";
            //Создаём путь для сохранения, если его до этого не существовало
            Directory.CreateDirectory(saveFolder);

            do
            {
                try
                {
                    //Выводим подсказку
                    Console.Write("Enter path to image: ");                    
                    //Считываем путь
                    string path = Console.ReadLine();
                    //Формируем путь для сохранения
                    string savePath = Path.Combine(saveFolder, Path.GetFileName(path));
                    //Выполняем лайнинг и сохранение изображения
                    liningLibZ.LineImage(path, savePath, 4, 0.9);
                    //Выводим сообщение о завершении работы
                    Console.WriteLine("Complete!");
                    //Выводим пустую строку
                    Console.WriteLine();
                }
                //В случае ошибки
                catch (Exception e)
                {
                    //Выводим её в консоль
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            while (true);
        }
    }
}
