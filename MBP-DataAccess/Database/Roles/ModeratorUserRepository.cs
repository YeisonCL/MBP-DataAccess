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

                USER_PHOTO userPhoto = new USER_PHOTO()
                {
                    photo = pUserData.getPhoto(),
                };
                db.USER_PHOTO.Add(userPhoto);
                db.SaveChanges();

                MOD_USER modUser = new MOD_USER()
                {
                    nickAndPassID = userNickPass.userID,
                    birthdate = pUserData.getBirthDate(),
                    business = pUserData.getBusiness(),
                    country = pUserData.getCountry(),
                    email = pUserData.getEmail(),
                    genre = pUserData.getGenre(),
                    name = pUserData.getName(),
                    userPhotoID = userPhoto.userID,
                    regDate = pUserData.getRegDate(),
                    secondName = pUserData.getSecondName(),
                };
                db.MOD_USER.Add(modUser);
                db.SaveChanges();
            }
        }


        public ModeratorUserDTO getModeratorUser(string pNickname)
        {
            ModeratorUserDTO moderatorUser = new ModeratorUserDTO();
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_MOD_USER_EXT
                            where b.nickname.Equals(pNickname)
                            select b;

                foreach (var item in query)
                {
                    moderatorUser.setName(item.name);
                    moderatorUser.setSecondName(item.secondName);
                    moderatorUser.setGenre(item.genre);
                    moderatorUser.setBusiness(item.business);
                    moderatorUser.setBirthDate(item.birthdate.Value);
                    moderatorUser.setCountry(item.country);
                    moderatorUser.setEmail(item.email);
                    moderatorUser.setNickname(item.nickname);
                    moderatorUser.setRegDate(item.regDate);
                    var query2 = from b in db.USER_PHOTO
                                 where b.userID.Equals(item.userPhotoID)
                                 select b;
                    foreach (var item2 in query2)
                    {
                        moderatorUser.setPhoto(item2.photo);
                    }
                }
            }
            return moderatorUser;
        }
    }
}
