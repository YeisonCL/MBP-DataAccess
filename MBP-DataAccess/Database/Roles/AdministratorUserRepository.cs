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
    public class AdministratorUserRepository : IAdministratorUserRepository
    {
        /// <summary>
        /// Agrega una nueva fila a la tabla ADMIN_USER con los datos dados
        /// </summary>
        /// <param name="pUserData">Nuevos datos</param>
        public void addAdminUser(AdminUserDTO pUserData)
        {
            using (var db = new MBP_Data_Entities())
            {
                USER_NICK_PASS userNickPass = new USER_NICK_PASS()
                {
                    nickname = pUserData.getNickname(),
                    password = pUserData.getPassword(),
                    type = pUserData.getType(),
                };
                db.USER_NICK_PASS.Add(userNickPass);
                db.SaveChanges();

                ADMIN_USER adminUser = new ADMIN_USER()
                {
                    email = pUserData._email,
                    name = pUserData.getName(),
                    secondName = pUserData.getSecondName(),
                    regDate = pUserData.getRegDate(),
                    nickAndPassID = userNickPass.userID,
                };
                db.ADMIN_USER.Add(adminUser);
                db.SaveChanges();
            }
        }
    }
}
