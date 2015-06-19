using MBP_Cross.DTO.DatabaseDTO;
using MBP_DataAccess.EntityData;
using MBP_Logic.Interface.RepositoryInterface.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBP_DataAccess.Database.SystemMessage
{
    public class SystemMessageRepository : ISystemMessageRepository
    {
        /// <summary>
        /// Devuelve el mensage asignado a un código de mensaje dado
        /// </summary>
        /// <param name="pMessageCode">Código de mensaje asociado</param>
        /// <returns>Mensaje asociado</returns>
        public string getSystemMessage(int pMessageCode)
        {
            string systemMessage = null;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.SYSTEM_MESSAGE
                            where b.codeMessage.Equals(pMessageCode)
                            select b;

                foreach (var item in query)
                {
                    systemMessage = item.textMessage;
                }
            }
            return systemMessage;
        }
    }
}
