using MBP_Cross.DTO.DatabaseDTO;
using MBP_Cross.DTO.ProtocolDTO;
using MBP_DataAccess.EntityData;
using MBP_Logic.Interface.RepositoryInterface.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MBP_DataAccess.Database.GamePlayer
{
    public class GamePlayerRepository : IGamePlayerRepository
    {
        /// <summary>
        /// Crea una nueva fila en la tabla GAME_PLAYER de la base de datos.
        /// </summary>
        /// <param name="pGamePlayer">Datos del jugador por agregar</param>
        /// <returns>Un objeto que contiene el playerID y el gameUserID de la nueva fila creada</returns>
        public GameAndPlayerDTO addGamePlayer(GamePlayerDTO pGamePlayer)
        {
            GameAndPlayerDTO gameAndPlayerDto = new GameAndPlayerDTO();
            using (var db = new MBP_Data_Entities())
            {
                GAME_PLAYER gamePlayer = new GAME_PLAYER()
                {
                    availableShots = pGamePlayer.getAvailableShots(),
                    gameUserID = pGamePlayer.getGameUserID(),
                    hitShots = pGamePlayer.getHitShots(),
                    inTurn = pGamePlayer.getInTurn(),
                    lastFeed = pGamePlayer.getLastFeed(),
                    missShots = pGamePlayer.getMissShots(),
                    points = pGamePlayer.getPoints(),
                    @lock = pGamePlayer.getLock(),
                    remainingShips = pGamePlayer.getRemainingShips(),
                    winFeedDelivered = pGamePlayer.getWinFeedDelivered(),

                };
                db.GAME_PLAYER.Add(gamePlayer);
                db.SaveChanges();
                gameAndPlayerDto.setGameUserID(pGamePlayer.getGameUserID());
                gameAndPlayerDto.setPlayerID(gamePlayer.playerID);
            }
            return gameAndPlayerDto;
        }

        /// <summary>
        /// Devuelve el playerID de un jugador dado su nickname
        /// </summary>
        /// <remarks>
        /// Sugerencia: Use la vista "VW_GAME_PLAYER_EXT"
        /// </remarks>
        /// <param name="pNickname">Nickname al cual se realizará la busqueda</param>
        /// <returns>El playerID del jugador</returns>
        public int getPlayerID(string pNickname)
        {
            int playerid = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = db.VW_GAME_PLAYER_EXT.Where(c => c.nickname.Equals(pNickname)).Select(c => c.playerID).FirstOrDefault();
                playerid = query;
            }
            return playerid;
        }

        /// <summary>
        /// Actualiza el valor de la columna lastFeed de la tabla GAME_PLAYER al valor pDateTimeFeed dado un gamePlayerID
        /// </summary>
        /// <param name="pGamePlayerID">gamePlayerID el cual se actualizará</param>
        /// <param name="pDateTimeFeed">Nuevo dato del lastFeed</param>
        public void updateLastFeed(int pGamePlayerID, DateTime pDateTimeFeed)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME_PLAYER
                            where b.playerID.Equals(pGamePlayerID)
                            select b;

                foreach (var item in query)
                {
                    item.lastFeed = pDateTimeFeed;
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Actualiza el valor de la columna inTurn de la tabla GAME_PLAYER al valor pValue dado un gamePlayerID
        /// </summary>
        /// <param name="pGamePlayerID">gamePlayerID el cual se actualizará</param>
        /// <param name="pValue">Nuevo dato del inTurn</param>
        public void setInTurn(int pGamePlayerID, bool pValue)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME_PLAYER
                            where b.playerID.Equals(pGamePlayerID)
                            select b;

                foreach (var item in query)
                {
                    item.inTurn = pValue;
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Devuelve el valor de la columna inTurn de la tabla GAME_PLAYER dado un nickname
        /// </summary>
        /// <remarks>
        /// Sugerencia: Use la vista "VW_GAME_PLAYER_EXT"
        /// </remarks>
        /// <param name="pNickname">nickname sobre el cual se realizará la busqueda</param>
        /// <returns>Valor de la columna</returns>
        public bool getInTurn(string pNickname)
        {
            bool inturn = false;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_GAME_PLAYER_EXT
                            where b.nickname.Equals(pNickname)
                            select b;

                foreach (var item in query)
                {
                    inturn = item.inTurn;
                }
            }
            return inturn;
        }

        /// <summary>
        /// Devuelve el valor de la columna remainingShips de la tabla GAME_PLAYER dado un nicname
        /// </summary>
        /// <remarks>
        /// Sugerencia: Use la vista "VW_GAME_PLAYER_EXT"
        /// </remarks>
        /// <param name="pNickname">nickname a buscar</param>
        /// <returns>Valor de la columna</returns>
        public int getReimainingShips(string pNickname)
        {
            int remainingships = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_GAME_PLAYER_EXT
                            where b.nickname.Equals(pNickname)
                            select b;
                foreach (var item in query)
                {
                    remainingships = item.remainingShips;
                }
            }
            return remainingships;
        }

        /// <summary>
        /// Devuelve el valor de la columna hitShots de la tabla GAME_PLAYER dado un nickname.
        /// </summary>
        /// <remarks>
        /// Sugerencia: Use la vista "VW_GAME_PLAYER_EXT"
        /// </remarks>
        /// <param name="pNickname">Nickname al cual se extraeran los datos</param>
        /// <returns>El valor de la columna</returns>
        public int getHitShots(string pNickname)
        {
            int hitshots = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_GAME_PLAYER_EXT
                            where b.nickname.Equals(pNickname)
                            select b;
                foreach (var item in query)
                {
                    hitshots = item.hitShots;
                }
            }
            return hitshots;
        }

        /// <summary>
        /// Devuelve el valor de la columna missShots de la tabla GAME_PLAYER dado un nickname.
        /// </summary>
        /// <remarks>
        /// Sugerencia: Use la vista "VW_GAME_PLAYER_EXT"
        /// </remarks>
        /// <param name="pNickname">Nickname al cual se extraeran los datos</param>
        /// <returns>El valor de la columna</returns>
        public int getMissShots(string pNickname)
        {
            int missshots = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_GAME_PLAYER_EXT
                            where b.nickname.Equals(pNickname)
                            select b;
                foreach (var item in query)
                {
                    missshots = item.missShots;
                }
            }
            return missshots;
        }

        /// <summary>
        /// Devuelve el valor de la columna availableShots de la tabla GAME_PLAYER dado un nickname.
        /// </summary>
        /// <remarks>
        /// Sugerencia: Use la vista "VW_GAME_PLAYER_EXT"
        /// </remarks>
        /// <param name="pNickname">Nickname al cual se extraeran los datos</param>
        /// <returns>El valor de la columna</returns>
        public int getAvailableShots(string pNickname)
        {
            int availableshots = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_GAME_PLAYER_EXT
                            where b.nickname.Equals(pNickname)
                            select b;
                foreach (var item in query)
                {
                    availableshots = item.availableShots;
                }
            }
            return availableshots;
        }

        /// <summary>
        /// Devuelve el valor de la columna points de la tabla GAME_PLAYER dado un nickname.
        /// </summary>
        /// <remarks>
        /// Sugerencia: Use la vista "VW_GAME_PLAYER_EXT"
        /// </remarks>
        /// <param name="pNickname">Nickname al cual se extraeran los datos</param>
        /// <returns>El valor de la columna</returns>
        public int getPoints(string pNickname)
        {
            int points = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_GAME_PLAYER_EXT
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
        /// Devuelve el playerID de un jugador que se encuentra participando en un juego que tiene un pGameID y ademas es diferente
        /// del pPlayerID dado
        /// </summary>
        /// <remarks>
        /// Sugerencia: Use la tabla GAME_AND_PLAYER
        /// </remarks>
        /// <param name="pGameID">pGameID en el cual se encuentra participando el pPlayerID dado</param>
        /// <param name="pPlayerID">pPlayerID dado del jugador</param>
        /// <returns>El playerID del jugador que esta participando en el juego con el pGameID dado y es diferente del pPlayerID dado</returns>
        public int getOpponentPlayerID(int pGameID, int pPlayerID)
        {
            int playerId = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME_AND_PLAYER
                            where b.gameID.Equals(pGameID)
                            select b;
                foreach (var item in query)
                {
                    if (item.playerID != pPlayerID)
                    {
                        playerId = pPlayerID;
                    }
                }
            }
            return playerId;
        }

        /// <summary>
        /// Devuelve el nickname de un jugador dado su pPlayerID
        /// </summary>
        /// <remarks>
        /// Sugerencia: Use la vista "VW_GAME_PLAYER_EXT"
        /// </remarks>
        /// <param name="pPlayerID">pPlayerID dado para devolver el nickname</param>
        /// <returns>Nickname del jugador</returns>
        public string getNickname(int pPlayerID)
        {
            string nick = "";
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_GAME_PLAYER_EXT
                            where b.playerID.Equals(pPlayerID)
                            select b;
                foreach (var item in query)
                {
                    nick = item.nickname;
                }
            }
            return nick;
        }

        /// <summary>
        /// Devuelve el el valor de la columna lastFeed de la tabla GAME_PLAYER dado un nickname
        /// </summary>
        /// <remarks>
        /// Sugerencia: Use la vista "VW_GAME_PLAYER_EXT"
        /// </remarks>
        /// <param name="pNickname">Nickname a buscar</param>
        /// <returns>Valor de la columna</returns>
        public DateTime getLastFeed(string pNickname)
        {
            DateTime lfeed = DateTime.Now;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_GAME_PLAYER_EXT
                            where b.nickname.Equals(pNickname)
                            select b;
                foreach (var item in query)
                {
                    lfeed = item.lastFeed;
                }
            }
            return lfeed;
        }

        /// <summary>
        /// Actualiza el valor de la columna remainingShips de la clase GAME_PLAYER al valor dado en pValue
        /// </summary>
        /// <param name="pPlayerID">pPlayerID al cual se debe actualizar el valor de la columna</param>
        /// <param name="pValue">Valor al que se debe actualizar la columna</param>
        public void updateRemainingShips(int pPlayerID, int pValue)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME_PLAYER
                            where b.playerID.Equals(pPlayerID)
                            select b;

                foreach (var item in query)
                {
                }
            }
        }

        /// <summary>
        /// Actualiza el valor de la columna availableShots de la clase GAME_PLAYER al valor dado en pValue
        /// </summary>
        /// <param name="pPlayerID">pPlayerID al cual se debe actualizar el valor de la columna</param>
        /// <param name="pValue">Valor al que se debe actualizar la columna</param>
        public void updateAvailableShots(int pPlayerID, int pValue)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME_PLAYER
                            where b.playerID.Equals(pPlayerID)
                            select b;
                foreach (var item in query)
                {
                    item.availableShots = pValue;
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Actualiza el valor de la columna points de la clase GAME_PLAYER al valor dado en pValue
        /// </summary>
        /// <param name="pPlayerID">pPlayerID al cual se debe actualizar el valor de la columna</param>
        /// <param name="pValue">Valor al que se debe actualizar la columna</param>
        public void updatePoints(int pPlayerID, int pValue)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME_PLAYER
                            where b.playerID.Equals(pPlayerID)
                            select b;
                foreach (var item in query)
                {
                    item.points = pValue;
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Actualiza el valor de la columna hitShots de la clase GAME_PLAYER al valor dado en pValue
        /// </summary>
        /// <param name="pPlayerID">pPlayerID al cual se debe actualizar el valor de la columna</param>
        /// <param name="pValue">Valor al que se debe actualizar la columna</param>
        public void updateHitShots(int pPlayerID, int pValue)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME_PLAYER
                            where b.playerID.Equals(pPlayerID)
                            select b;
                foreach (var item in query)
                {
                    item.hitShots = pValue;
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Actualiza el valor de la columna missShots de la clase GAME_PLAYER al valor dado en pValue
        /// </summary>
        /// <param name="pPlayerID">pPlayerID al cual se debe actualizar el valor de la columna</param>
        /// <param name="pValue">Valor al que se debe actualizar la columna</param>
        public void updateMissShots(int pPlayerID, int pValue)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME_PLAYER
                            where b.playerID.Equals(pPlayerID)
                            select b;
                foreach (var item in query)
                {
                    item.missShots = pValue;
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Este metodo es de tipo transaccional, es decir, todo lo que se realiza en el DEBE ir dentro de una transacción, es importante que el
        /// isolation level sea de tipo "RepeatableRead", cuando se llama a este metodo se cambia el estado de la columna lock de la tabla GAME_PLAYER
        /// a true SIEMPRE, sin importar si la columna ya tiene valor de true en ella o no, de esta forma automáticamente esa fila queda bloqueada por
        /// el tipo de isolation usado, bloqueando de esta forma a otras transacciones que esten usando este mismo método tratando de cambiar el valor
        /// de esta columna para un mismo pPlayerID dado, una vez que se cambia el valor a true de la columba lock, se obtiene el valor de la columna 
        /// winFeedDelivered con el pNickname dado y se retorna
        /// </summary>
        /// <remarks>
        /// Sugerencia: Usar este enlace como guía "http://www.c-sharpcorner.com/UploadFile/ff2f08/working-with-transaction-in-entity-framework-6-0/"
        /// </remarks>
        /// <param name="pNickname">Valor para el cual se exttraera lo explicado arriba</param>
        /// <returns>Valor de la columna</returns>
        public bool getWindFeedDelivered(int pPlayerID)
        {
            bool widfeeddelivered = false;
            var scope = new TransactionScope(
                TransactionScopeOption.RequiresNew,
                new TransactionOptions()
                {
                    IsolationLevel = IsolationLevel.RepeatableRead
                }
                );
            using (var db = new MBP_Data_Entities())
            {
                using (scope)
                {
                    try
                    {
                        var query = from b in db.GAME_PLAYER
                                    where b.playerID.Equals(pPlayerID)
                                    select b;

                        foreach (var item in query)
                        {
                            item.@lock = true;
                            widfeeddelivered = item.winFeedDelivered;
                        }
                        db.SaveChanges();
                        scope.Complete();
                    }
                    catch (Exception)
                    {
                        scope.Dispose();
                        throw;
                    }
                }
            }
            return widfeeddelivered;
        }

        /// <summary>
        /// Este metodo es de tipo transaccional, es decir, todo lo que se realiza en el DEBE ir dentro de una transacción, es importante que el
        /// isolation level sea de tipo "RepeatableRead", cuando se llama a este metodo se cambia el estado de la columna lock de la tabla GAME_PLAYER
        /// a true SIEMPRE, sin importar si la columna ya tiene valor de true en ella o no, de esta forma automáticamente esa fila queda bloqueada por
        /// el tipo de isolation usado, bloqueando de esta forma a otras transacciones que esten usando este mismo método tratando de cambiar el valor
        /// de esta columna para un mismo pPlayerID dado, una vez que se cambia el valor a true de la columba lock, se actualiza el valor de la columna
        /// winFeedDelivered al valor dando en la variable pValue despues de esto se suelta el lock
        /// </summary>
        /// <remarks>
        /// Sugerencia: Usar este enlace como guía "http://www.c-sharpcorner.com/UploadFile/ff2f08/working-with-transaction-in-entity-framework-6-0/"
        /// </remarks>
        /// <param name="pPlayerID">Valor al cual se aplicará lo explicado arriba</param>
        public void updateWinFeedDelivered(int pPlayerID, bool pValue)
        {
            var scope = new TransactionScope(
                TransactionScopeOption.RequiresNew,
                new TransactionOptions()
                {
                    IsolationLevel = IsolationLevel.RepeatableRead
                }
                );
            using (var db = new MBP_Data_Entities())
            {
                using (scope)
                {
                    try
                    {
                        var query = from b in db.GAME_PLAYER
                                    where b.playerID.Equals(pPlayerID)
                                    select b;

                        foreach (var item in query)
                        {
                            item.@lock = true;
                            item.winFeedDelivered = pValue;
                        }
                        db.SaveChanges();
                        scope.Complete();
                    }
                    catch (Exception)
                    {
                        scope.Dispose();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Borra una fila de la tabla GAME_PLAYER para un pPlayerID dado
        /// </summary>
        /// <param name="pPlayerID">pPlayerID a borrar</param>
        public void deleteGamePlayer(int pPlayerID)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME_PLAYER
                            where b.playerID.Equals(pPlayerID)
                            select b;
                foreach (var item in query)
                {
                    db.GAME_PLAYER.Remove(item);
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Obtiene la foto de un jugador
        /// </summary>
        /// <param name="pNickname"></param>
        /// <returns></returns>
        public string getPhoto(string pNickname)
        {
            string photo = "";
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_GAME_USER_EXT
                            where b.nickname.Equals(pNickname)
                            select b;
                foreach (var item in query)
                {
                    var query2 = from b in db.USER_PHOTO
                                where b.userID.Equals(item.userID)
                                select b;
                    foreach(var item2 in query2)
                    {
                        photo = item2.photo;
                    }
                }
            }
            return photo;
        }
    }
}
