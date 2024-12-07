using System.ServiceModel;

namespace wcf_chat
{
    /// <summary>
    /// Класс, представляющий пользователя, подключенного к серверу чата.
    /// </summary>
    public class ServerUser
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Контекст операции WCF для текущего пользователя.
        /// Используется для отправки сообщений и взаимодействия с клиентом.
        /// </summary>
        public OperationContext operationContext { get; set; }
    }
}
