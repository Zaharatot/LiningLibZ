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
        /// Получаем позицию в массиве пикселей по координатам
        /// </summary>
        /// <param name="x">Позиция по оси X</param>
        /// <param name="y">Позиция по оси Y</param>
        /// <returns>Позиция в одномерном массиве</returns>
        private int GetPosition(int x, int y) =>
            y * ImageSize.Width + x;



        /// <summary>
        /// Метод получения пикселя по координатам
        /// </summary>
        /// <param name="x">Позиция пикселя по оси X</param>
        /// <param name="y">Позиция пикселя по оси Y</param>
        /// <returns>Значение пикселя</returns>
        public byte GetPixel(int x, int y) =>
            //Конвертим позицию в пикселях в позицию одномерного
            //массива, и возвращаем пиксель по этой позиции
            Pixels[GetPosition(x, y)];

        /// <summary>
        /// Метод установки пикселя по координатам
        /// </summary>
        /// <param name="val">Значение для установки</param>
        /// <param name="x">Позиция пикселя по оси X</param>
        /// <param name="y">Позиция пикселя по оси Y</param>
        public void SetPixel(int x, int y, byte val) =>
            //Конвертим позицию в пикселях в позицию одномерного
            //массива, и ставим пиксель по этой позиции
            Pixels[GetPosition(x, y)] = val;

        /// <summary>
        /// Получаем область вокруг пикселя
        /// </summary>
        /// <param name="x">Позиция пикселя по оси X</param>
        /// <param name="y">Позиция пикселя по оси Y</param>
        /// <param name="size">Размер области</param>
        /// <returns>Байты области</returns>
        public List<byte> GetPixelArea(int x, int y, int size)
        {
            //Массив пикселей области
            List<byte> bytes = new List<byte>();
            //Получаем значение сдвига в одну сторону
            int halfSize = size / 2;
            //Получаем обрезанное значение сдвигов по оси X
            int minX = Clamp(x - halfSize, 0, ImageSize.Width);
            int maxX = Clamp(x + halfSize, 0, ImageSize.Width);
            //Получаем обрезанное значение сдвигов по оси Y
            int minY = Clamp(y - halfSize, 0, ImageSize.Height);
            int maxY = Clamp(y + halfSize, 0, ImageSize.Height);
            //Проходимся по пикселям
            for (int x1 = minX; x1 < maxX; x1++)
                for (int y1 = minY; y1 < maxY; y1++)
                    //Добавляем в список пиксель с указанной позиции
                    bytes.Add(GetPixel(x1, y1));
            //Возвращаем список найденных пикселей
            return bytes;
        }

        /// <summary>
        /// Получаем среднее значение цвета из области
        /// </summary>
        /// <param name="x">Позиция пикселя по оси X</param>
        /// <param name="y">Позиция пикселя по оси Y</param>
        /// <param name="size">Размер области</param>
        /// <returns>Среднее значение цвета из области</returns>
        public byte GetAverageArea(int x, int y, int size) =>
            (byte)GetPixelArea(x, y, size).Average(px => px);
    }
}
