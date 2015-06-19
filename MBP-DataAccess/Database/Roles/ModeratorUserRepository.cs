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
            return 0;
        }
    }
}
