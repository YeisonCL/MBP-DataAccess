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
            int usernickpassId = -1;
            using (var db = new MBP_Data_Entities())
            {
                USER_NICK_PASS userNickPass = new USER_NICK_PASS()
                {
                    nickname = pUserData._nickname,
                    password = pUserData._password,
                    type = pUserData._type
                };
                usernickpassId = db.USER_NICK_PASS.Add(userNickPass).userID;
                db.SaveChanges();
                MOD_USER modUser = new MOD_USER()
                {
                    nickAndPassID = usernickpassId,
                    birthdate = pUserData._birthdate,
                    business = pUserData._business,
                    country = pUserData._country,
                    email = pUserData._email,
                    genre = pUserData._genre,
                    name = pUserData._name,
                    photo = pUserData._photo,
                    regDate = pUserData._regDate,
                    secondName = pUserData._secondName
                };
                db.MOD_USER.Add(modUser);
                db.SaveChanges();
            }
        }
    }
}
