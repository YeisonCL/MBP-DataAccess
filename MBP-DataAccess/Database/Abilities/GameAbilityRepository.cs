using MBP_Cross.DTO.DatabaseDTO;
using MBP_Cross.DTO.ProtocolDTO;
using MBP_DataAccess.EntityData;
using MBP_Logic.Interface.RepositoryInterface.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBP_DataAccess.Database.Abilities
{
    public class GameAbilityRepository : IGameAbilityRepository
    {
        /// <summary>
        /// Agrega una nueva fila a la tabla GAME_ABILITY segun los datos dados
        /// </summary>
        /// <param name="pGameAbility">Dados para agregar la nueva fila</param>
        public void addGameAbility(GameAbilityDTO pGameAbility)
        {
            using (var db = new MBP_Data_Entities())
            {
                var gameAbility_temp = new GAME_ABILITY()
                {
                    playerID = pGameAbility.getPlayerID(),
                    active = pGameAbility.getActive(),
                    availableCount = pGameAbility.getAvailableCount(),
                    abilityID = pGameAbility.getAbilityID()
                };
                db.GAME_ABILITY.Add(gameAbility_temp);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Devuelve una lista de las habilidades que tiene el jugador las cuales se encuentran en la tabla GAME_ABILITY, devolviendo el nombre 
        /// de la habilidad junto con su valor de availableCount en la tabla
        /// </summary>
        /// <param name="pNickname">Nickname al cual se le extraeran las habilidades</param>
        /// <returns>Lista con los feed de las habilidades</returns>
        public IList<GameAbilityFeedDTO> getGameAbilityFeed(string pNickname)
        {
            IList<GameAbilityFeedDTO> gameAbility_List = new List<GameAbilityFeedDTO>();
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_GAME_ABILITY_EXT
                            where b.nickname.Equals(pNickname)
                            select b;
                foreach(var item in query)
                {
                    GameAbilityFeedDTO gaf_temp = new GameAbilityFeedDTO();
                    var query2 = from b in db.ABILITY_CATALOG
                            where b.abilityID.Equals(item.abilityID)
                            select b;
                    foreach(var item2 in query2)
                    {
                        gaf_temp.setName(item2.name);
                        gaf_temp.setAvailable(item.availableCount);
                        gameAbility_List.Add(gaf_temp);
                    }
                }
            }
            return gameAbility_List;
        }

        /// <summary>
        /// Devuelve el valor de la columna active de la tabla GAME_ABILITY para un pNickname y un pAbilityID dado, en dado caso
        /// que NO exista ninguna fila que haga match con el valor del pNickname y el pAbilityID dado entonces se devolvera false
        /// de igual forma
        /// </summary>
        /// <remarks>
        /// Sugerencia: Usar la vista "VW_GAME_ABILITY_EXT"
        /// </remarks>
        /// <param name="pNickname">pNickname a buscar</param>
        /// <param name="pAbilityID">pAbilityID a buscar</param>
        /// <returns>Valor de la columna</returns>
        public bool isAbilityActive(string pNickname, int pAbilityID)
        {
            bool isAbil_Active = false;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_GAME_ABILITY_EXT
                            where b.nickname.Equals(pNickname)
                                  & b.abilityID.Equals(pAbilityID)
                            select b;
                if (query.FirstOrDefault() != null)
                {
                    isAbil_Active = query.First().active;
                }
            }
            return isAbil_Active;
        }

        /// <summary>
        /// Devuelve true si existe una fila en la tabla SHIELD que haga match con los datos dados, de no ser asi devuelve false
        /// </summary>
        /// <param name="pNickname">pNickname del jugador</param>
        /// <param name="pGameShipID">pGameShipID de la nave que tiene el escudo</param>
        /// <param name="pAbilityID">Apuntador al tipo de habilidad</param>
        /// <returns>True si esta la fila, false sino</returns>
        public bool isShieldActive(string pNickname, int pGameShipID, int pAbilityID)
        {
            bool isShieldActive_temp = false;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.SHIELD
                            where b.abilityID.Equals(pAbilityID) & b.gameShipID.Equals(pGameShipID)
                            select b;
                if (query.FirstOrDefault() != null)
                {
                    isShieldActive_temp = true;
                }
            }
            return isShieldActive_temp;
        }

        /// <summary>
        /// Devuelve el valor de la columna direction de la tabla ONE_PLUS_ABILITY para los datos dados
        /// </summary>
        /// <remarks>
        /// Sugerencia: Usar la vista "VW_GAME_ABILITY_EXT"
        /// </remarks>
        /// <param name="pNickname">pNickname del jugador que tiene la habilidad</param>
        /// <param name="pAbilityID">Codigo de la habilidad</param>
        /// <returns>Valor de la columna</returns>
        public string getOnePlusDirection(string pNickname, int pAbilityID)
        {
            string direction = "";
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_GAME_ABILITY_EXT
                            where b.nickname.Equals(pNickname)
                                  & b.abilityID.Equals(pAbilityID)
                            select b;
                foreach (var item in query)
                {
                    direction = item.direction;
                }
            }
            return direction;
        }


        /// <summary>
        /// Actualiza el valor de la columna actve de la tabla GAME_ABLITY a el valor dado en pValue
        /// </summary>
        /// <param name="pPlayerID">Usuario que sera actualizado</param>
        /// <param name="pAbilityID">Habilidad que sera actualizada</param>
        /// <param name="pValue">Valor a la cual se actualizara la columna</param>
        public void updateGameAbilityActive(int pPlayerID, int pAbilityID, bool pValue)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME_ABILITY
                            where b.playerID.Equals(pPlayerID) & b.abilityID.Equals(pAbilityID)
                            select b;
                foreach (var item in query)
                {
                    item.active = pValue;

                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Devuelve el valor de la columna availableCount de la tabla GAME_ABILITY para la fila que haga match con los datos dados, en caso que
        /// ninguna fila haga match se devolvera -1
        /// </summary>
        /// <remarks>
        /// Sugerencia: Usar la vista "VW_GAME_ABILITY_EXT"
        /// </remarks>
        /// <param name="pNickname">pNickname del jugador que tiene la habilidad</param>
        /// <param name="pAbilityID">Codigo de la habilidad</param>
        /// <returns>Valor de la columna</returns>
        public int getAvailableCount(string pNickname, int pAbilityID)
        {
            int availablecounts = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_GAME_ABILITY_EXT
                            where b.nickname.Equals(pNickname)
                            select b.playerID;
                var query2 =
                    db.GAME_ABILITY.Where(c => c.playerID.Equals(query) & c.abilityID.Equals(pAbilityID))
                        .Select(c => c.availableCount)
                        .FirstOrDefault();
                availablecounts = query2;
            }
            return availablecounts;
        }


        /// <summary>
        /// Agrega una nueva fila a la tabla ONE_PLUS_ABILITY segun los datos dados
        /// </summary>
        /// <param name="pGameAbility">Dados para agregar la nueva fila</param>
        public void addOnePlusAbility(GameAbilityDTO pGameAbility)
        {
            using (var db = new MBP_Data_Entities())
            {
                ONE_PLUS_ABILITY oneplusabiliti = new ONE_PLUS_ABILITY()
                {
                    playerID = pGameAbility._playerID,
                    abilityID = pGameAbility._abilityID,
                    direction = pGameAbility._direction,
                };
                db.ONE_PLUS_ABILITY.Add(oneplusabiliti);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Borra una fila de la tabla ONE_PLUS_ABILITY que haga match con los datos dados
        /// </summary>
        /// <param name="pPlayerID">pPlayerID del jugador</param>
        /// <param name="pAbilityID">pAbilityID de la habilidad</param>
        public void deleteOnePlusAbility(int pPlayerID, int pAbilityID)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.ONE_PLUS_ABILITY
                            where b.playerID.Equals(pPlayerID) & b.abilityID.Equals(pAbilityID)
                            select b;
                foreach (var item in query)
                {
                    db.ONE_PLUS_ABILITY.Remove(item);
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Agrega una nueva fila a la tabla SHIELD segun los datos dados
        /// </summary>
        /// <param name="pPlayerID">Jugador que tendra el escudo</param>
        /// <param name="pAbilityID">Identificador de la habilidad</param>
        /// <param name="pGameShipID">Nave que tiene el escudo</param>
        public void addShieldAbility(int pPlayerID, int pAbilityID, int pGameShipID)
        {
            using (var db = new MBP_Data_Entities())
            {
                SHIELD shield = new SHIELD()
                {
                    playerID = pPlayerID,
                    gameShipID = pGameShipID,
                    abilityID = pAbilityID,
                };
                db.SHIELD.Add(shield);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Borra una fila de la tabla SHIELD segun los datos dados
        /// </summary>
        /// <param name="pPlayerID">Jugador que tiene el escudo</param>
        /// <param name="pAbilityID">Identificador de la habilidad</param>
        /// <param name="pGameShipID">Nave que tiene el escudo</param>
        public void deleteShieldAbility(int pPlayerID, int pAbilityID, int pGameShipID)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.SHIELD
                            where b.playerID.Equals(pPlayerID) & b.abilityID.Equals(pAbilityID) & b.gameShipID.Equals(pGameShipID)
                            select b;
                foreach (var item in query)
                {
                    db.SHIELD.Remove(item);
                }
            }
        }

        /// <summary>
        /// Modifica el valor de la columna availableCount de la tabla GAME_ABILITY al valor de pValue para los datos dados
        /// </summary>
        /// <param name="pPlayerID">Jugador que tiene la habilidad</param>
        /// <param name="pAbilityID">Codigo de la habilidad</param>
        /// <param name="pValue">Valor al cual debe ser modificada la columna</param>
        public void updateAvailableCount(int pPlayerID, int pAbilityID, int pValue)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME_ABILITY
                            where b.playerID.Equals(pPlayerID) & b.abilityID.Equals(pAbilityID)
                            select b;

                foreach (var item in query)
                {
                    item.availableCount = pValue;
                }
            }   
        }

        /// <summary>
        /// Borra todas las filas de la tabla GAME_ABILITY que haga match con el pPlayerID dado
        /// </summary>
        /// <param name="pPlayerID">pPlayerID a borrar</param>
        public void deleteGameAbility(int pPlayerID)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME_ABILITY
                            where b.playerID.Equals(pPlayerID)
                            select b;
                foreach (var item in query)
                {
                    db.GAME_ABILITY.Remove(item);
                }
                db.SaveChanges();
            }
        }
    }
}
