using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace wcf_chat
{
    /// <summary>
    /// Интерфейс WCF-сервиса для чата, содержащий основные методы взаимодействия.
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IServerChatCallback))]
    public interface IServiceChat
    {
        /// <summary>
        /// Подключает нового пользователя к чату.
        /// </summary>
        /// <param name="name">Имя пользователя, подключающегося к чату.</param>
        /// <returns>Уникальный идентификатор пользователя.</returns>
        [OperationContract]
        int Connect(string name);

        /// <summary>
        /// Отключает пользователя от чата.
        /// </summary>
        /// <param name="id">Уникальный идентификатор пользователя, который отключается.</param>
        [OperationContract]
        void Disconnect(int id);

        /// <summary>
        /// Отправляет сообщение от одного пользователя ко всем подключённым пользователям.
        /// </summary>
        /// <param name="msg">Текст сообщения.</param>
        /// <param name="id">Уникальный идентификатор пользователя, отправляющего сообщение.</param>
        [OperationContract(IsOneWay = true)]
        void SendMsg(string msg, int id);
    }

    /// <summary>
    /// Интерфейс обратного вызова для получения сообщений от сервера.
    /// </summary>
    public interface IServerChatCallback
    {
        /// <summary>
        /// Метод обратного вызова для отправки сообщений от сервера к клиенту.
        /// </summary>
        /// <param name="msg">Сообщение, отправляемое клиенту.</param>
        [OperationContract(IsOneWay = true)]
        void MsgCallback(string msg);
    }
}
