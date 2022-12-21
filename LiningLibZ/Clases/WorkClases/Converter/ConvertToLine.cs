using LiningLibZ.Clases.DataClases;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
        /// <param name="id">Идентификатор позиции текущего пикселя</param>
        /// <param name="size">Размер области для обработки</param>
        /// <returns>Значение цвета пикселя после лайнинга</returns>
        private byte LinePixel(ByteImageInfo image, int id, int size, double coeff)
        {
            //Получаем среднее значение цвета из области
            byte average = image.GetAverageArea(id, size);
            //Возвращаем цвет пикселя исходя из условия
            return (byte)((image.Pixels[id] < average * coeff) ? 0 : 255);
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
            //Проходимся по пикселям изображения в многопоточном режиме
            Parallel.For(0, image.Pixels.Length, i => {
                //Лайним пиксель входного изображения и вставляем в выходное
                output.Pixels[i] = LinePixel(image, i, size, coeff);
            });
            //Возвращаем выходное изображение
            return output;
        }
    }
}
