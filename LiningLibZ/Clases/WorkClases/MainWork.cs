using LiningLibZ.Clases.DataClases;
using LiningLibZ.Clases.WorkClases.Converter;
using LiningLibZ.Clases.WorkClases.Loader;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiningLibZ.Clases.WorkClases
{
    /// <summary>
    /// Основной рабочий класс приложения
    /// </summary>
    internal class MainWork
    {
        /// <summary>
        /// Класс конвертации изображения в лайн
        /// </summary>
        private ConvertToLine _convertToLine;
        /// <summary>
        /// Класс загрузки и сохраненяи изображений
        /// </summary>
        private ImageLoader _imageLoader;


        /// <summary>
        /// Конструктор класса
        /// </summary>
        public MainWork()
        {
            Init();
        }

        /// <summary>
        /// Инициализатор класса
        /// </summary>
        private void Init()
        {
            //Инициализируем испольуземые классы
            _convertToLine = new ConvertToLine();
            _imageLoader = new ImageLoader();
        }


        /// <summary>
        /// Метод выполнения лайнинга изображения
        /// </summary>
        /// <param name="pixels">Изображение в виде одномерного массива пикселей в режиме градаций серого</param>
        /// <param name="heidht">Ширина оригинального изображения</param>
        /// <param name="width">Высота оригинального изображения</param>
        /// <param name="size">Размер области для обработки</param>
        /// <param name="coeff">Значение коэффициента для трансформации</param>
        /// <returns>Массив пикселей отлайненного изображения</returns>
        public byte[] LineImage(byte[] pixels, int width, int heidht, int size, double coeff)
        {
            //Инициализируем класс информации об изображении
            ByteImageInfo image = new ByteImageInfo(new Size(width, heidht), pixels);
            //Лайним изображение и получаем обработанное изображение
            ByteImageInfo output = _convertToLine.Convert(image, size, coeff);
            //Возвращаем массив пикселей обработанного изображения
            return output.Pixels;
        }


        /// <summary>
        /// Метод выполнения лайнинга изображения
        /// </summary>
        /// <param name="loadPath">Путь для загрузки оригинального изображения</param>
        /// <param name="savePath">Путь для сохранения обработанного изображения</param>
        /// <param name="size">Размер области для обработки</param>
        /// <param name="coeff">Значение коэффициента для трансформации</param>
        public void LineImage(string loadPath, string savePath, int size, double coeff)
        {
            //ВЫполняем загрузку изображения
            ByteImageInfo image = _imageLoader.LoadImage(loadPath);
            //Лайним изображение и получаем обработанное изображение
            ByteImageInfo output = _convertToLine.Convert(image, size, coeff);
            //Выполняем сохранение обработанного изображения
            _imageLoader.SaveImage(output, savePath);
        }


        /// <summary>
        /// Метод выполнения лайнинга изображения
        /// </summary>
        /// <param name="loadPath">Путь для загрузки оригинального изображения</param>
        /// <param name="size">Размер области для обработки</param>
        /// <param name="coeff">Значение коэффициента для трансформации</param>
        /// <returns>Массив пикселей отлайненного изображения</returns>
        public byte[] LineImage(string loadPath, int size, double coeff)
        {
            //ВЫполняем загрузку изображения
            ByteImageInfo image = _imageLoader.LoadImage(loadPath);
            //Лайним изображение и получаем обработанное изображение
            ByteImageInfo output = _convertToLine.Convert(image, size, coeff);
            //Возвращаем массив пикселей обработанного изображения
            return output.Pixels;
        }
    }
}
