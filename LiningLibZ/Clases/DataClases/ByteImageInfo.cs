using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiningLibZ.Clases.DataClases
{
    /// <summary>
    /// Класс, хранящий данные об изображении
    /// </summary>
    internal class ByteImageInfo
    {
        /// <summary>
        /// Пиксели изображения
        /// </summary>
        public byte[] Pixels { get; set; }
        /// <summary>
        /// Размер изображения
        /// </summary>
        public Size ImageSize { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public ByteImageInfo()
        {
            //Проставляем дефолтные значения
            Pixels = null;
            ImageSize = new Size();
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="size">Размер создаваемого изображения</param>
        public ByteImageInfo(Size size)
        {
            //Проставляем переданные значения
            ImageSize = size;
            //Проставляем дефолтные значения
            Pixels = new byte[size.Width * size.Height];
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="pixels">Массив пикселей изображения</param>
        /// <param name="size">Размер создаваемого изображения</param>
        public ByteImageInfo(Size size, byte[] pixels)
        {
            //Проставляем переданные значения
            ImageSize = size;
            Pixels = pixels;
        }

        /// <summary>
        /// Выполняем обрезку значения
        /// </summary>
        /// <param name="val">Значение для обрезки</param>
        /// <param name="min">Минимальное значение</param>
        /// <param name="max">Максимальное значение</param>
        /// <returns>Обрезанное значение</returns>
        private int Clamp(int val, int min, int max) =>
            Math.Max(Math.Min(val, max), min);


        /// <summary>
        /// Получаем область вокруг пикселя
        /// </summary>
        /// <param name="id">Идентификатор позиции текущего пикселя</param>
        /// <param name="size">Размер области</param>
        /// <returns>Байты области</returns>
        public List<byte> GetPixelArea(int id, int size)
        {
            //Массив пикселей области
            List<byte> bytes = new List<byte>();
            //Переменные для обрезанных позиций по оси X
            int min, max;
            //Получаем крайние значения позиций по оси X
            int minX = id - size;
            int maxX = id + size;
            //Получаем в локальные переменные значения свойств, чтобы их попусту не дёргать
            int len = Pixels.Length - 1;
            int width = ImageSize.Width;
            //Получаем значение сдвига для перехода по оси Y
            int minShift = width * size;
            //Получаем обрезанные значения позиций по оси Y
            int minY = Math.Max(-minShift, 0);
            int maxY = Math.Min(minShift, len);
            //Проходимся по оси Y
            for (int y = minY; y <= maxY; y += width)
            {
                //Получаем обрезанные значения для позиций по оси X
                min = Clamp(minX + y, 0, len);
                max = Clamp(maxX + y, 0, len);
                //Проходимся по оси X для текущего Y
                for (int x = min; x <= max; x++)
                    //Добавляем пиксели в выходной массив
                    bytes.Add(Pixels[x]);
            }
            //Возвращаем список найденных пикселей
            return bytes;
        }

        /// <summary>
        /// Получаем среднее значение цвета из области
        /// </summary>
        /// <param name="id">Идентификатор позиции текущего пикселя</param>
        /// <param name="size">Размер области</param>
        /// <returns>Среднее значение цвета из области</returns>
        public byte GetAverageArea(int id, int size) =>
            (byte)GetPixelArea(id, size).Average(px => px);
    }
}
