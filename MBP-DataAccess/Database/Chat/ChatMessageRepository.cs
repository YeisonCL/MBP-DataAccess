using MBP_Cross.DTO.DatabaseDTO;
using MBP_DataAccess.EntityData;
using MBP_Logic.Interface.RepositoryInterface.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBP_DataAccess.Database.Chat
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        /// <summary>
        /// Devuelve los mensajes de chat guardados en la tabla CHAT_MESSAGE asociado a un nickname
        /// </summary>
        /// <remarks>
        /// Sugerencia: Use la vista "VW_CHAT_MESSAGE_EXT" 
        /// </remarks>
        /// <param name="pNickname">pNickname asociado con los mensajes de chat</param>
        /// <returns>Una lista con todos los mensajes asociados ordenados de primero en ser insertado a ultimo</returns>
        public IList<string> getChatMessages(string pNickname)
        {
            IList<string> chatList = new List<string>();
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_CHAT_MESSAGE_EXT
                            where b.nickname.Equals(pNickname)
                            select b;
                foreach (var item in query)
                {
                    chatList.Add(item.message.ToString());
                }
            }
            return chatList;
        }

        /// <summary>
        /// Borra todos los mensajes de chat asociados a un nickname guardados en la tabla CHAT_MESSAGE
        /// </summary>
        /// <param name="pNickname">pPlayerID del cual se borraran los mensajes de chat</param>
        public void deleteMessages(int pPlayerID)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.CHAT_MESSAGE
                            where b.playerID.Equals(pPlayerID)
                            select b;
                foreach (var item in query)
                {
                    db.CHAT_MESSAGE.Remove(item);
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Agrega una nueva fila a la tabla CHAT_MESSAGE segun los datos dados
        /// </summary>
        /// <param name="pMessage">Dados a agregar</param>
        public void addMessage(ChatMessageDTO pMessage)
        {
            using (var db = new MBP_Data_Entities())
            {
                var nuevoChat = new CHAT_MESSAGE()
                {
                    message = pMessage.getMessage(),
                    playerID = pMessage.getPlayerID(),
                    gameID = pMessage.getGameID()
                };

                db.CHAT_MESSAGE.Add(nuevoChat);
                db.SaveChanges();
            }
        }
    }
}
