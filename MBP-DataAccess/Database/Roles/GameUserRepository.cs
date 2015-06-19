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
    public class GameUserRepository : IGameUserRepository
    {
        /// <summary>
        /// Obtiene el userID de un jugador a partir de su nickname
        /// </summary>
        /// <param name="pNickname">Nickname del jugador</param>
        /// <returns>El userID del jugador</returns>
        public int getGameUserID(string pNickname)
        {
            int gameuserid = 0;
            using (var db = new MBP_Data_Entities())
            {
                var query = db.VW_GAME_USER_EXT.Where(c => c.nickname.Equals(pNickname)).Select(c => c.userID).FirstOrDefault();
                gameuserid = query;
            }
            return gameuserid;
        }

        /// <summary>
        /// Retorna las habilidades que dispone un jugador segun su GameUserID usando la tabla GAME_USER_ABILITY
        /// </summary>
        /// <remarks>
        /// Recomendacion: Usar la vista VW_GAME_USER_ABILITY_EXT
        /// </remarks>
        /// <param name="pGameUserID">Jugador del cual se retornaran las habilidades</param>
        /// <returns>Devuelve una lista ORDENADA de menor a mayor con todos los abilityID extraidos del jugador</returns>
        public IList<int> getGameUserAbilities(int pGameUserID)
        {
            IList<int> gameuserabilityList = null;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_GAME_USER_ABILITY_EXT
                            where b.userID.Equals(pGameUserID)
                            orderby b.abilityID
                            select b;
                foreach (var item in query)
                {
                    gameuserabilityList.Add(item.abilityID);
                }
            }
            return gameuserabilityList;
        }

        /// <summary>
        /// Agrega a la columa totalPoints de la tabla GAME_USER la cantidad de puntos enviados como parametro del userID dado
        /// </summary>
        /// <remarks>
        /// Nota: Notese que lo que está haciendo realmente es tomando los puntos que ya se encuentran en totalPoints y SUMANDOLE los que se envian
        /// como parametro
        /// </remarks>
        /// <param name="pUserID">userID al cual se agregarán los puntos</param>
        /// <param name="pPoints">Puntos a agregar</param>
        public void addToTotalPoints(int pUserID, int pPoints)
        {
            int totalpoints = 0;
            using (var db = new MBP_Data_Entities())
            {
                var query = db.GAME_USER.Where(c => c.userID.Equals(pUserID)).Select(c => c).FirstOrDefault();
                var points = db.GAME_USER.Where(c => c.userID.Equals(pUserID)).Select(c => c.points).FirstOrDefault();
                totalpoints = points + pPoints;
                query.points = totalpoints;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Actualiza el valor de la columna hits de la clase GAME_USER al valor dado en pValue
        /// </summary>
        /// <param name="pUserID">pUserID al cual se debe actualizar el valor de la columna</param>
        /// <param name="pValue">Valor al que se debe actualizar la columna</param>
        public void updateHits(int pUserID, int pValue)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME_USER
                            where b.userID.Equals(pUserID)
                            select b;
                foreach (var item in query)
                {
                    item.hits = pValue;
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Actualiza el valor de la columna miss de la clase GAME_USER al valor dado en pValue
        /// </summary>
        /// <param name="pUserID">pUserID al cual se debe actualizar el valor de la columna</param>
        /// <param name="pValue">Valor al que se debe actualizar la columna</param>
        public void updateMiss(int pUserID, int pValue)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME_USER
                            where b.userID.Equals(pUserID)
                            select b;
                foreach (var item in query)
                {
                    item.misses = pValue;
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Actualiza el valor de la columna points de la clase GAME_USER al valor dado en pValue
        /// </summary>
        /// <param name="pUserID">pUserID al cual se debe actualizar el valor de la columna</param>
        /// <param name="pValue">Valor al que se debe actualizar la columna</param>
        public void updatePoints(int pUserID, int pValue)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME_USER
                            where b.userID.Equals(pUserID)
                            select b;
                foreach (var item in query)
                {
                    item.points = pValue;
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Devuelve el valor de la columna hits de la tabla GAME_USER dado un nickname.
        /// </summary>
        /// <remarks>
        /// Sugerencia: Use la vista "VW_GAME_USER_EXT"
        /// </remarks>
        /// <param name="pNickname">Nickname al cual se extraeran los datos</param>
        /// <returns>El valor de la columna</returns>
        public int getHits(string pNickname)
        {
            int hits = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_GAME_USER_EXT
                            where b.nickname.Equals(pNickname)
                            select b;
                foreach (var item in query)
                {
                    hits = item.hits;
                }
            }
            return hits;
        }

        /// <summary>
        /// Devuelve el valor de la columna miss de la tabla GAME_USER dado un nickname.
        /// </summary>
        /// <remarks>
        /// Sugerencia: Use la vista "VW_GAME_USER_EXT"
        /// </remarks>
        /// <param name="pNickname">Nickname al cual se extraeran los datos</param>
        /// <returns>El valor de la columna</returns>
        public int getMiss(string pNickname)
        {
            int misses = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_GAME_USER_EXT
                            where b.nickname.Equals(pNickname)
                            select b;
                foreach (var item in query)
                {
                    misses = item.misses;
                }
            }
            return misses;
        }

        /// <summary>
        /// Devuelve el valor de la columna points de la tabla GAME_USER dado un nickname.
        /// </summary>
        /// <remarks>
        /// Sugerencia: Use la vista "VW_GAME_USER_EXT"
        /// </remarks>
        /// <param name="pNickname">Nickname al cual se extraeran los datos</param>
        /// <returns>El valor de la columna</returns>
        public int getPoints(string pNickname)
        {
            int points = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_GAME_USER_EXT
                            where b.nickname.Equals(pNickname)
                            select b;
                foreach (var item in query)
                {
                    points = item.points;
                }
            }
            return points;
        }

        /// <summary>
        /// Devuelve el valor de la columna wins de la tabla GAME_USER dado un nickname.
        /// </summary>
        /// <remarks>
        /// Sugerencia: Use la vista "VW_GAME_USER_EXT"
        /// </remarks>
        /// <param name="pNickname">Nickname al cual se extraeran los datos</param>
        /// <returns>El valor de la columna</returns>
        public int getDefeats(string pNickname)
        {
            int defeats = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_GAME_USER_EXT
                            where b.nickname.Equals(pNickname)
                            select b;
                foreach (var item in query)
                {
                    defeats = item.defeats;
                }
            }
            return defeats;
        }

        /// <summary>
        /// Devuelve el valor de la columna defeats de la tabla GAME_USER dado un nickname.
        /// </summary>
        /// <remarks>
        /// Sugerencia: Use la vista "VW_GAME_USER_EXT"
        /// </remarks>
        /// <param name="pNickname">Nickname al cual se extraeran los datos</param>
        /// <returns>El valor de la columna</returns>
        public int getWins(string pNickname)
        {
            int wins = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_GAME_USER_EXT
                            where b.nickname.Equals(pNickname)
                            select b;
                foreach (var item in query)
                {
                    wins = item.wins;
                }
            }
            return wins;
        }

        /// <summary>
        /// Actualiza el valor de la columna defeats de la clase GAME_USER al valor dado en pValue
        /// </summary>
        /// <param name="pUserID">pUserID al cual se debe actualizar el valor de la columna</param>
        /// <param name="pValue">Valor al que se debe actualizar la columna</param>
        public void updateDefeats(int pUserID, int pValue)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME_USER
                            where b.userID.Equals(pUserID)
                            select b;
                foreach (var item in query)
                {
                    item.defeats = pValue;
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Actualiza el valor de la columna wins de la clase GAME_USER al valor dado en pValue
        /// </summary>
        /// <param name="pUserID">pUserID al cual se debe actualizar el valor de la columna</param>
        /// <param name="pValue">Valor al que se debe actualizar la columna</param>
        public void updateWins(int pUserID, int pValue)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME_USER
                            where b.userID.Equals(pUserID)
                            select b;
                foreach (var item in query)
                {
                    item.wins = pValue;
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Agrega una nueva fila a la tabla GAME_USER_ABILITY segun los datos dados
        /// </summary>
        /// <param name="pGameUserAbility">Datos para la nueva fila</param>
        public void addGameUserAbility(GameUserAbilityDTO pGameUserAbility)
        {
            using (var db = new MBP_Data_Entities())
            {
                GAME_USER_ABILITY gsmGameUserAbility = new GAME_USER_ABILITY()
                {
                    abilityID = pGameUserAbility._abilityID,
                    gameUserID = pGameUserAbility._gameUserID
                };
                db.GAME_USER_ABILITY.Add(gsmGameUserAbility);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Borra una fila de la tabla GAME_USER_ABILITY segun los datos dados
        /// </summary>
        /// <param name="pGameUserAbility">Datos de la fila que se debe borrar</param>
        public void deleteGameUserAbility(GameUserAbilityDTO pGameUserAbility)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME_USER_ABILITY
                            where
                                b.abilityID.Equals(pGameUserAbility._abilityID) &
                                b.gameUserID.Equals(pGameUserAbility._gameUserID)
                            select b;

                foreach (var item in query)
                {
                    db.GAME_USER_ABILITY.Remove(item);
                }
            }
        }
    }
}
