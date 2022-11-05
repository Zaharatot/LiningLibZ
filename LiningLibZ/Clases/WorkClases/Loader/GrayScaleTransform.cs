using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiningLibZ.Clases.WorkClases.Loader
{
    /// <summary>
    /// Класс конвертации изображения в градации серого
    /// </summary>
    internal class GrayScaleTransform
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public GrayScaleTransform()
        {

        }

        /// <summary>
        /// Возвращаем цвет пикселя, переведённый в градации серого
        /// </summary>
        /// <param name="red">Значение Красного канала</param>
        /// <param name="green">Значение зелёного канала</param>
        /// <param name="blue">Значение синего канала</param>
        /// <returns>Яркость пикселя в градациях серого</returns>
        private byte GetGrayScalePixel(byte red, byte green, byte blue) =>
            (byte)(0.299 * red + 0.587 * green + 0.114 * blue);




        /// <summary>
        /// Переводим картинку в режим градаций серого
        /// </summary>
        /// <param name="channels">Массив каналов изображения</param>
        /// <returns>Массив пикселей изображения</returns>
        public byte[] ToGrayScale(byte[] channels)
        {
            //Получаем итоговый размер выходного массива
            int size = channels.Length / 4;
            //Инициализируем выходной массив
            byte[] pixels = new byte[size];
            //Проходимся по каналам
            for (int i = 0, j = 0; i < channels.Length; i += 4, j++)
                //Проставляем пиксели
                //Альфа-канал мы игнорируем           
                pixels[j] = GetGrayScalePixel(
                    //Красный канал
                    channels[i + 1],
                    //Синий канал
                    channels[i + 2],
                    //Зелёный канал
                    channels[i + 3]
                );
            //Возвращаем массив пикселей
            return pixels;
        }

        /// <summary>
        /// Получаем массив каналов из массива пикселей
        /// </summary>
        /// <param name="pixels">Массив пикселей изображения</param>
        /// <returns>Массив каналогв</returns>
        public byte[] FromGrayScale(byte[] pixels)
        {
            //Получаем итоговый размер выходного массива
            int size = pixels.Length * 4;
            //Инициализируем выходной массив
            byte[] channels = new byte[size];
            //Проходимся по каналам
            for (int i = 0, j = 0; i < channels.Length; i += 4, j++)
            {
                //Гидрадоминатус!
                channels[i] = pixels[j];
                //Красный канал
                channels[i + 1] = pixels[j];
                //Синий канал
                channels[i + 2] = pixels[j];
                //Зелёный канал
                channels[i + 3] = 255;
            }
            //Возвращаем массив каналов
            return channels;
        }
    }
}
