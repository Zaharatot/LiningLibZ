using LiningLibZ.Clases.DataClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiningLibZ.Clases.WorkClases.Converter
{
    /// <summary>
    /// Класс конвертации изображения в лайновую версию
    /// </summary>
    internal class ConvertToLine
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public ConvertToLine()
        {

        }

        /// <summary>
        /// Выполняем лайнинг пикселя и возвращаем результат
        /// </summary>
        /// <param name="image">Изображение для получения пикселя</param>
        /// <param name="x">Позиция по оси X</param>
        /// <param name="y">Позиция по оси Y</param>
        /// <param name="size">Размер области для обработки</param>
        /// <returns>Значение цвета пикселя после лайнинга</returns>
        private byte LinePixel(ByteImageInfo image, int x, int y, int size, double coeff)
        {
            //Получаем среднее значение цвета из области
            byte average = image.GetAverageArea(x, y, size);
            //Получаем текущий пиксель
            byte current = image.GetPixel(x, y);
            //Возвращаем цвет пикселя исходя из условия
            return (byte)((current < average * coeff) ? 0 : 255);
        }



        /// <summary>
        /// Метод конвертации изображения в лайновую версию
        /// </summary>
        /// <param name="image">Изображение для конвертации</param>
        /// <param name="size">Размер области для обработки</param>
        /// <param name="coeff">Значение коэффициента для трансформации</param>
        /// <returns>Сконвертированное изображение</returns>
        public ByteImageInfo Convert(ByteImageInfo image, int size, double coeff)
        {
            //Инициализируем выходное изображение
            ByteImageInfo output = new ByteImageInfo(image.ImageSize);
            //Проходимся по пикселям изображения
            for (int y = 0; y < image.ImageSize.Height; y++)
                for (int x = 0; x < image.ImageSize.Width; x++)
                    //Лайним пиксель входного изображения и вставляем в выходное
                    output.SetPixel(x, y, LinePixel(image, x, y, size, coeff));
            //Возвращаем выходное изображение
            return output;
        }
    }
}
