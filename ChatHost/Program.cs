using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ChatHost
{
    /// <summary>
    /// Основной класс для запуска хоста WCF-сервиса чата.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Главная точка входа в приложение.
        /// </summary>
        /// <param name="args">Аргументы командной строки (не используются).</param>
        static void Main(string[] args)
        {
            // Создание экземпляра хоста для сервиса ServiceChat
            using (var host = new ServiceHost(typeof(wcf_chat.ServiceChat)))
            {
                // Открытие хоста для обработки запросов
                host.Open();

                // Уведомление о том, что хост успешно запущен
                Console.WriteLine("Хост стартовал!");

                // Ожидание завершения работы хоста
                Console.ReadLine();
            }
        }
    }
}
