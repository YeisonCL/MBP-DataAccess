using MBP_Cross.DTO;
using MBP_Cross.DTO.DatabaseDTO;
using MBP_Cross.DTO.ProtocolDTO;
using MBP_DataAccess.EntityData;
using MBP_Logic.Interface.RepositoryInterface.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MBP_DataAccess.Database.Game
{
    public class GameRepository : IGameRepository
    {
        /// <summary>
        /// Crea una nueva fila en la tabla GAME de la base de datos según los valores datos
        /// </summary>
        /// <param name="pGameData">Valores para la nueva fila a agregar a la tabla</param>
        /// <returns>El GameID de la nueva fila agregada</returns>
        public int addGame(GameDTO pGameData)
        {
            int gameID_temp;
            using (var db = new MBP_Data_Entities())
            {
                var newgame = new GAME()
                {
                    @lock = pGameData.getLock(),
                    boardSize = pGameData.getBoardSize(),
                    feed = pGameData.getFeed(),
                    joined = pGameData.getJoined(),
                    profileAccess = pGameData.getProfileAccess(),
                    shotsTurn = pGameData.getShotsTurn(),
                    turnChangeTime = pGameData.getTurnChangeTime()
                };
                var gameId = db.GAME.Add(newgame).gameID;
                db.SaveChanges();
                gameID_temp = gameId;
            }
            return gameID_temp;
        }

        /// <summary>
        /// Crea una nueva fila en la tabla GAME_AND_PLAYER con los datos dados
        /// </summary>
        /// <param name="pPlayerAndGameData">Datos para crear la nueva fila</param>
        public void addGameAndPlayer(GameAndPlayerDTO pGameAndPlayerData)
        {
            using (var db = new MBP_Data_Entities())
            {
                var newGameAndPlayer = new GAME_AND_PLAYER()
                { 
                    gameID = pGameAndPlayerData.getGameID(),
                    playerID = pGameAndPlayerData.getPlayerID(),
                };
                db.GAME_AND_PLAYER.Add(newGameAndPlayer);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Crea una nueva fila en la tabla GAME_SHIP_CATALOG segu los datos dados
        /// </summary>
        /// <param name="pGameShipCatalog">Datos de la nueva fila</param>
        public void addGameShipCatalog(GameShipCatalogDTO pGameShipCatalog)
        {
            using (var db = new MBP_Data_Entities())
            {
                var newGameShipCatalog = new GAME_SHIP_CATALOG()
                {
                    shipCount = pGameShipCatalog.getCount(),
                    shipID = pGameShipCatalog.getShipID(),
                    gameID = pGameShipCatalog.getGameID()
                };
                db.GAME_SHIP_CATALOG.Add(newGameShipCatalog);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Devuelve el gameID de un juego dado el nickname de un jugador
        /// </summary>
        /// <remarks>
        /// Sugerencia: Usar la vista "VW_WAITING_GAMES_INFO"
        /// </remarks>
        /// <param name="pPlayerID">Nickname a buscar</param>
        /// <returns></returns>
        public int getGameID(string pNickname)
        {
            int gameID_temp;
            using (var db = new MBP_Data_Entities())
            {
                var playerid = db.USER_NICK_PASS.Where(b => b.nickname.Equals(pNickname)).Select(b => b.userID).FirstOrDefault();
                var query = db.VW_GAME_AND_PLAYER_INFO.Where(c => c.playerID.Equals(playerid)).Select(c => c.gameID).FirstOrDefault();

                gameID_temp = query;
            }
            return gameID_temp;
        }

        /// <summary>
        /// Devuelve el valor de la columna feed de la tabla GAME dado el gameID del juego
        /// </summary>
        /// <param name="pGameID">GameID a consultar</param>
        /// <returns>Valor de la columna como un boolenano</returns>
        public bool getFeed(int pGameID)
        {
            bool feed_temp = false;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME
                            where b.gameID.Equals(pGameID)
                            select b;
                foreach (var item in query)
                {
                    feed_temp = item.feed;
                }
            }
            return feed_temp;
        }

        /// <summary>
        /// Devuelve una lista con todos los juegos que se encuentran en espera para ser aceptados
        /// <remarks>
        /// Sugerencia: Use la vista ""WAITING GAMES"
        /// </remarks>
        /// </summary>
        /// <returns>Juegos en espera</returns>
        public IList<WaitingGameDTO> getAllWaitingGames()
        {
            IList<WaitingGameDTO> allWaitingGames = null;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_GAME_AND_PLAYER_INFO
                            select b;
                foreach (var item in query)
                {
                    WaitingGameDTO waitingGameDto = new WaitingGameDTO();
                    waitingGameDto.setBoardSize(item.boardSize);
                    waitingGameDto.setCreatorNickname(item.nickname);
                    waitingGameDto.setProfileAccess(item.profileAccess);
                    waitingGameDto.setTotalShips(Convert.ToInt32(item.ships));
                    waitingGameDto.setTurnShots(item.shotsTurn);
                    allWaitingGames.Add(waitingGameDto);
                }
            }
            return allWaitingGames;
        }

        /// <summary>
        /// Devuelve un objeto que contiene todas las naves de la tabla GAME_SHIP_CATALOG asociadas a un gameID
        /// </summary>
        /// <remarks>
        /// Sugerencia: Use la vista "VW_GAME_SHIP_CATALOG"
        /// </remarks>
        /// <param name="gameID">gameID a utilizar para la busqueda</param>
        /// <returns>Objeto con las naves</returns>
        public IList<ShipDTO> getGameShipCatalog(int pGameID)
        {
            IList<ShipDTO> gameShipCatalog_temp = null;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_GAME_SHIP_CATALOG
                            where b.gameID.Equals(pGameID)
                            select b;
                foreach (var item in query)
                {
                    ShipDTO ship = new ShipDTO();
                    ship.setQuantity(item.shipCount);
                    ship.setHeight(item.height);
                    ship.setName(item.name);
                    ship.setPoints(item.points);
                    if (item.shipVersion != null) ship.setVersion(item.shipVersion.Value);
                    ship.setWidth(item.width);
                    gameShipCatalog_temp.Add(ship);
                }
            }
            return gameShipCatalog_temp;
        }

        /// <summary>
        /// Devuelve el valor de la columna shotsTurn asociada a un gameID dado
        /// </summary>
        /// <param name="pGameID">gameID al cual se realizará la busqueda</param>
        /// <returns>Valor de la columna shotsTurn</returns>
        public int getShotsTurn(int pGameID)
        {
            int shotsTurn = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME
                            where b.gameID.Equals(pGameID)
                            select b;

                foreach (var item in query)
                {
                    shotsTurn = item.shotsTurn;
                }
            }
            return shotsTurn;
        }

        /// <summary>
        /// Devuelve el valor de la columna boardSize asociada a un pGameID dado
        /// </summary>
        /// <param name="pGameID">gameID al cual se realizará la busqueda</param>
        /// <returns>Valor de la columna boardSize</returns>
        public int getBoardSize(int pGameID)
        {
            int boardSize_temp = 0;
            using (var db = new MBP_Data_Entities())
            {
                var query = db.GAME.Where(c => c.gameID.Equals(pGameID)).Select(c => c.boardSize).FirstOrDefault();
                boardSize_temp = query;
            }
            return boardSize_temp;
        }

        /// <summary>
        /// Devuelve el valor de la columna joined de la tabla GAME para un pGameID dado
        /// </summary>
        /// <param name="pGameID">gameID al cual se realizará la busqueda</param>
        /// <returns>Valor de la columna joined</returns>
        public bool isBlockGame(int pGameID)
        {
            bool isBlocked = false;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME
                            where b.gameID.Equals(pGameID)
                            select b;
                foreach (var item in query)
                {
                    isBlocked = item.joined;
                }
            }
            return isBlocked;
        }

        /// <summary>
        /// Este metodo es de tipo transaccional, es decir, todo lo que se realiza en el DEBE ir dentro de una transacción, es importante que el
        /// isolation level sea de tipo "RepeatableRead", cuando se llama a este metodo se cambia el estado de la columna lock de la tabla GAME
        /// a true SIEMPRE, sin importar si la columna ya tiene valor de true en ella o no, de esta forma automáticamente esa fila queda bloqueada por
        /// el tipo de isolation usado, bloqueando de esta forma a otras transacciones que esten usando este mismo método tratando de cambiar el valor
        /// de esta columna para un mismo pGameID dado, una vez que se cambia el valor a true se pregunta si el valor de la columna joined de la tabla GAME
        /// es false, si es así, se cambia a true y el valor retornado será "MBP_Logic.Game.OnlineGame.GAME_SUCCESSFULLY_LOCKED", de no ser así, se deja
        /// el valor de la columna en false tal como está y se retorna "MBP_Logic.Game.OnlineGame.GAME_UNSUCCESSFULLY_LOCKED".
        /// </summary>
        /// <remarks>
        /// Sugerencia: Usar este enlace como guía "http://www.c-sharpcorner.com/UploadFile/ff2f08/working-with-transaction-in-entity-framework-6-0/"
        /// </remarks>
        /// <param name="pGameID">Valor al cual se aplicará lo explicado arriba</param>
        /// <returns>Valor explicado arriba</returns>
        public int blockGame(int pGameID)
        {
            int blocked = -1;
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
                        var query = from b in db.GAME
                                    where b.gameID.Equals(pGameID)
                                    select b;

                        foreach (var item in query)
                        {
                            item.@lock = true;
                            if (item.joined.Equals(true))
                            {
                                item.joined = true;
                                blocked = MBP_Logic.Game.OnlineGame.GAME_SUCCESSFULLY_LOCKED;
                            }
                            else
                            {
                                blocked = MBP_Logic.Game.OnlineGame.GAME_UNSUCCESSFULLY_LOCKED;
                            }

                        }
                        scope.Complete();
                    }
                    catch (Exception)
                    {
                        scope.Dispose();
                        throw;
                    }
                }
            }
            return blocked;
        }

        /// <summary>
        /// Cambia el valor de la columna feed de la tabla GAME a un valor de TRUE
        /// </summary>
        /// <param name="pGameID">pGameID al cual se la hará el cambio</param>
        public void putGameToRun(int pGameID)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME
                            where b.gameID.Equals(pGameID)
                            select b;

                foreach (var item in query)
                {
                    item.feed = true;
                }

                db.SaveChanges();
            }
        }

        /// <summary>
        /// Devuelve el valor de la columna turnChangeTime de la tabla GAME dado un pGameID
        /// </summary>
        /// <param name="pGameID">pGameID a extraer el turnChangeTime</param>
        /// <returns>Valor de la columna</returns>
        public DateTime getTurnChangeTime(int pGameID)
        {
            DateTime datetime = DateTime.Now;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME
                            where b.gameID.Equals(pGameID)
                            select b;
                foreach (var item in query)
                {
                    if (item.turnChangeTime != null) datetime = item.turnChangeTime.Value;
                }
            }
            return datetime;
        }

        /// <summary>
        /// Actualiza la columna turnChangeTime de la tabla GAME a el valor dado
        /// </summary>
        /// <param name="pGameID">pGameID al cual se actualizara el turnChangeTime</param>
        /// <param name="pTime">Valor de actualizacion</param>
        public void updateTurnChangeTime(int pGameID, DateTime pTime)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME
                            where b.gameID.Equals(pGameID)
                            select b;
                foreach (var item in query)
                {
                    item.turnChangeTime = pTime;
                }
            }
        }

        /// <summary>
        /// Setea el valor de la columna feed de la tabla GAME al valor dado de pValue
        /// </summary>
        /// <param name="pGameID">pGameID a setear</param>
        /// <param name="pValue">Valor de seteo</param>
        public void setFeed(int pGameID, bool pValue)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME
                            where b.gameID.Equals(pGameID)
                            select b;

                foreach (var item in query)
                {
                    item.feed = pValue;
                }

                db.SaveChanges();
            }
        }

        /// <summary>
        /// Borra una fila de la tabla GAME para un pGameID dado
        /// </summary>
        /// <param name="pGameID">pGameID a borrar</param>
        public void deleteGame(int pGameID)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME
                            where b.gameID.Equals(pGameID)
                            select b;
                foreach (var item in query)
                {
                    db.GAME.Remove(item);
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Borra una fila de la tabla GAME_AND_PLAYER
        /// </summary>
        /// <param name="pPlayerID">pPlayerID a borrar</param>
        public void deleteGameAndPlayer(int pPlayerID)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME_AND_PLAYER
                            where b.playerID.Equals(pPlayerID)
                            select b;
                foreach (var item in query)
                {
                    db.GAME_AND_PLAYER.Remove(item);
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Borra uan fila de la tabla GAME_SHIP_CATALOG que haga match con el pGameID dado
        /// </summary>
        /// <param name="pGameID">pGameID a borrar</param>
        public void deleteGameShipCatalog(int pGameID)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME_SHIP_CATALOG
                            where b.gameID.Equals(pGameID)
                            select b;
                foreach (var item in query)
                {
                    db.GAME_SHIP_CATALOG.Remove(item);
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Devuelve el valor de la columna profileAccess de la tabla GAME dado un pGameID
        /// </summary>
        /// <param name="pGameID">pGameID a extraer el profileAccess</param>
        /// <returns>Valor de la columna</returns>
        public bool getProfileAccess(int pGameID)
        {
            bool profileaccess = false;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME
                            where b.gameID.Equals(pGameID)
                            select b;
                foreach (var item in query)
                {
                    profileaccess = item.profileAccess;
                }
            }
            return profileaccess;
        }
    }
}
