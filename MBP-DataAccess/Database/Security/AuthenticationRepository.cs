using MBP_DataAccess.EntityData;
using MBP_Logic.Interface.RepositoryInterface.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBP_DataAccess.Database.Security
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        /// <summary>
        /// Devuelve una tupla con dos valores si el usuario dado y el password dado hacen match con alguna fila de la tabla USER_NICK_PASS,
        /// si hacen match en el item 1 de la tupla (bool) se enviara "true" y en el item dos (string) el valor de la columna type para esos datos
        /// dados, de no ser así y no hacer match con ninguna fila, entonces en el item 1 (bool) se enviara "false" y en el item 2 (string) se
        /// enviara "NONE"
        /// </summary>
        /// <remarks>
        /// Sugerencia: Para devolver una tupla se hace "return new Tuple<bool,string>(valor1, valor2);"
        /// </remarks>
        /// <param name="pNickname">Nickname del usuario</param>
        /// <param name="pPassword">Contraseña de usuario</param>
        /// <returns>La tupla</returns>
        public Tuple<bool, string> checkUser(string pNickname, string pPassword)
        {
            Tuple<bool, string> checkuser = null;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.USER_NICK_PASS
                            where b.nickname.Equals(pNickname) & b.password.Equals(pPassword)
                            select b;

                foreach (var item in query)
                {
                    if (item != null)
                    {
                        checkuser = new Tuple<bool, string>(true, item.type);
                    }
                }
            }
            return checkuser;
        }
    }
}
