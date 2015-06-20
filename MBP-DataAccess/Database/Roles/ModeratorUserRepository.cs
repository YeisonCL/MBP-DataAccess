using MBP_Cross.DTO.DatabaseDTO;
using MBP_DataAccess.EntityData;
using MBP_Logic.Interface.RepositoryInterface.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBP_DataAccess.Database.Roles
{
    public class ModeratorUserRepository : IModeratorUserRepository
    {
        /// <summary>
        /// Devuelve el valor de la columna userID de la tabla MOD_USER para un pNickname dado
        /// </summary>
        /// <remarks>
        /// Sugerencia: Use la vista "VW_MOD_USER_EXT"
        /// </remarks>
        /// <param name="pNickname">pNickname a buscar</param>
        /// <returns>Valor de la columna</returns>
        public int getModUserID(string pNickname)
        {
            int userid = 0;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_MOD_USER_EXT
                            where b.nickname.Equals(pNickname)
                            select b;

                foreach (var item in query)
                {
                    userid = item.userID;
                }
            }
            return userid;
        }

        /// <summary>
        /// Agrega una nueva columna a la tabla MOD_USER con los datos dados
        /// </summary>
        /// <param name="pUserData">Datos a agregar</param>
        public void addModeratorUser(ModeratorUserDTO pUserData)
        {
            throw new NotImplementedException();
        }
    }
}
