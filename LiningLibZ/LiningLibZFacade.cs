using LiningLibZ.Clases.DataClases;
using LiningLibZ.Clases.WorkClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiningLibZ
{
    /// <summary>
    /// Фасадный класс библиотеки лайнига изображений
    /// </summary>
    public class LiningLibZFacade
    {
        /// <summary>
        /// Основной рабочий класс приложения
        /// </summary>
        private MainWork _mainWork;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public LiningLibZFacade()
        {
            Init();
        }

        private void Init()
        {
            //Инициализируем испольуземые классы
            _mainWork = new MainWork();
        }





        /// <summary>
        /// Метод выполнения лайнинга изображения
        /// </summary>
        /// <param name="pixels">Изображение в виде одномерного массива пикселей в режиме градаций серого</param>
        /// <param name="heidht">Ширина оригинального изображения</param>
        /// <param name="width">Высота оригинального изображения</param>
        /// <param name="size">Размер области для обработки</param>
        /// <returns>Массив пикселей отлайненного изображения</returns>
        public byte[] LineImage(byte[] pixels, int width, int heidht, int size, double coeff) =>
            //Вызываем внутренний метод
            _mainWork.LineImage(pixels, width, heidht, size, coeff);

        /// <summary>
        /// Метод выполнения лайнинга изображения
        /// </summary>
        /// <param name="loadPath">Путь для загрузки оригинального изображения</param>
        /// <param name="savePath">Путь для сохранения обработанного изображения</param>
        /// <param name="size">Размер области для обработки</param>
        public void LineImage(string loadPath, string savePath, int size, double coeff) =>
            //Вызываем внутренний метод
            _mainWork.LineImage(loadPath, savePath, size, coeff);

        /// <summary>
        /// Метод выполнения лайнинга изображения
        /// </summary>
        /// <param name="loadPath">Путь для загрузки оригинального изображения</param>
        /// <param name="size">Размер области для обработки</param>
        /// <returns>Массив пикселей отлайненного изображения</returns>
        public byte[] LineImage(string loadPath, int size, double coeff) =>
            //Вызываем внутренний метод
            _mainWork.LineImage(loadPath, size, coeff);
    }
}
