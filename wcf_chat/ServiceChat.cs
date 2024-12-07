    using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace wcf_chat
{
    /// <summary>
    /// Реализация WCF сервиса для чата.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        /// <summary>
        /// Список подключённых пользователей.
        /// </summary>
        private List<ServerUser> users = new List<ServerUser>();

        /// <summary>
        /// Следующий идентификатор для пользователя.
        /// </summary>
        private int nextId = 1;

        /// <summary>
        /// Метод для подключения пользователя к чату.
        /// </summary>
        /// <param name="name">Имя пользователя, подключающегося к чату.</param>
        /// <returns>Идентификатор подключившегося пользователя.</returns>
        public int Connect(string name)
        {
            ServerUser user = new ServerUser()
            {
                ID = nextId,
                Name = name,
                operationContext = OperationContext.Current
            };
            nextId++;

            // Уведомляем всех пользователей о подключении нового участника
            SendMsg(": " + user.Name + " вошел!", 0);
            users.Add(user);
            return user.ID;
        }

        /// <summary>
        /// Метод для отключения пользователя из чата.
        /// </summary>
        /// <param name="id">Идентификатор пользователя, который отключается.</param>
        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);
            if (user != null)
            {
                users.Remove(user);
                // Уведомляем всех пользователей о выходе участника
                SendMsg(": " + user.Name + " покинул чат!", 0);
            }
        }

        /// <summary>
        /// Метод для отправки сообщения в чат.
        /// </summary>
        /// <param name="msg">Сообщение, отправляемое пользователем.</param>
        /// <param name="id">Идентификатор пользователя, отправляющего сообщение.</param>
        public void SendMsg(string msg, int id)
        {
            foreach (var item in users)
            {
                string answer = DateTime.Now.ToShortTimeString();

                var user = users.FirstOrDefault(i => i.ID == id);
                if (user != null)
                {
                    answer += ": " + user.Name + ": ";
                }
                answer += msg;

                // Отправка сообщения всем подключенным пользователям
                item.operationContext.GetCallbackChannel<IServerChatCallback>().MsgCallback(answer);
            }
        }
    }
}
