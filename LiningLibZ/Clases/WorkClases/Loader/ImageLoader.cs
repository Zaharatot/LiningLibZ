using LiningLibZ.Clases.DataClases;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LiningLibZ.Clases.WorkClases.Loader
{
    /// <summary>
    /// Класс выполнения загрузки изображения
    /// </summary>
    internal class ImageLoader
    {
        /// <summary>
        /// Класс конвертации изображения в градации серого
        /// </summary>
        private GrayScaleTransform _grayScaleTransform;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public ImageLoader()
        {
            Init();
        }

        /// <summary>
        /// Инициализатор класса
        /// </summary>
        private void Init()
        {
            //Инициализируем используемые классы
            _grayScaleTransform = new GrayScaleTransform();
        }



        /// <summary>
        /// Получаем пиксели изображения в виде одномерного массива
        /// </summary>
        /// <param name="img">Исходное изображение</param>
        /// <returns>Массив пикселей</returns>
        private byte[] GetImagePixels(Bitmap img)
        {
            byte[] pixels = null;
            try
            {
                //Получаем размер считываемого массива
                int size = img.Width * img.Height * 4;
                //ИНициализируем массив для пикселей
                pixels = new byte[size];
                //Лочим биты картинки
                var bd = img.LockBits(
                    new Rectangle(0, 0, img.Width, img.Height),
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);
                //Считываем пиксели изображения
                Marshal.Copy(bd.Scan0, pixels, 0, size);
                //Разблокируем пиксели изображения
                img.UnlockBits(bd);
            }
            catch { pixels = null; }
            return pixels;
        }

        /// <summary>
        /// Вставляем пиксели в изображение
        /// </summary>
        /// <param name="channels">каналы изображения для вставки</param>
        /// <param name="img">Изображение для сохранения</param>
        /// <returns>Массив пикселей</returns>
        private void SetImagePixels(Bitmap img, byte[] channels)
        {
            try
            {
                //Лочим биты картинки
                var bd = img.LockBits(
                    new Rectangle(0, 0, img.Width, img.Height),
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);
                //Колпируем байты обратно в картинку
                Marshal.Copy(channels, 0, bd.Scan0, channels.Length);
                //Разблокируем пиксели изображения
                img.UnlockBits(bd);
            }
            catch { }
        }




        /// <summary>
        /// Выполняем загрузку изображения
        /// </summary>
        /// <param name="path">Путь к файлу изображения</param>
        /// <returns>Загруженное байтовое изображение</returns>
        public ByteImageInfo LoadImage(string path)
        {
            ByteImageInfo ex;
            //Загружаем изображение по указанному пути
            using (Bitmap original = new Bitmap(path))
            {
                //Получаем каналы изображения
                byte[] channels = GetImagePixels(original);
                //Получаем массив пикселей изображения
                byte[] pixels = _grayScaleTransform.ToGrayScale(channels);
                //Инициализируем класс информации об изображении
                ex = new ByteImageInfo() {
                    ImageSize = original.Size,
                    Pixels = pixels
                };
            }
            //Возвращаем результат
            return ex;
        }

        /// <summary>
        /// ВЫполняем сохранение изображения
        /// </summary>
        /// <param name="image">Изображение для сохранения</param>
        /// <param name="path">Путь для сохранения</param>
        public void SaveImage(ByteImageInfo image, string path)
        {
            //Инициализируем изображение для сохранения по указанным размерам
            using(Bitmap original = new Bitmap(image.ImageSize.Width, image.ImageSize.Height))
            {
                //Получаем каналы изображения из пикселей
                byte[] channels = _grayScaleTransform.FromGrayScale(image.Pixels);
                //Вставляем массив каналов в изображение
                SetImagePixels(original, channels);
                //Сохраняем изображение в файл
                original.Save(path, ImageFormat.Png);
            }
        }
    }
}
