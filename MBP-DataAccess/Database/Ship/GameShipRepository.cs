﻿using MBP_Cross.DTO.DatabaseDTO;
using MBP_DataAccess.EntityData;
using MBP_Logic.Interface.RepositoryInterface.Database;
using MBPL_Logic.Interface.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBP_DataAccess.Database.Ship
{
    public class GameShipRepository : IGameShipRepository, IMBPLiveShipsRepository
    {
        /// <summary>
        /// Agrega una nueva fila a la tabla GAME_SHIP según los datos dados
        /// </summary>
        /// <param name="pGameShip">Datos para la nueva fila</param>
        /// <returns>gameShipID de la nueva fila creada</returns>
        public int addGameShip(GameShipDTO pGameShip)
        {
            int gameShipID_temp = 0;
            using (var db = new MBP_Data_Entities())
            {
                var gs = new GAME_SHIP()
                {
                    shipID = pGameShip.getShipID(),
                    playerID = pGameShip.getPlayerID(),
                    integrity = pGameShip.getIntegrity(),
                    posX = pGameShip.getPosX(),
                    posY = pGameShip.getPosY()
                };
                db.GAME_SHIP.Add(gs);
                db.SaveChanges();
                gameShipID_temp = gs.gameShipID;
            }
            return gameShipID_temp;
        }

        /// <summary>
        /// Devuelve el valor de la columna integrity de la tabla GAME_SHIP dado un pGameShipID
        /// </summary>
        /// <param name="pGameShipID">pGameShipID a buscar</param>
        /// <returns>Valor de la columna</returns>
        public int getIntegrityGameShip(int pGameShipID)
        {
            int integrity_temp = 0;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME_SHIP
                            where b.gameShipID.Equals(pGameShipID)
                            select b;
                foreach (var item in query)
                {
                    integrity_temp = item.integrity;
                }
            }
            return integrity_temp;
        }

        /// <summary>
        /// Actualiza el valor de la columna integrity de la clase GAME_SHIP al valor dado para un pGameShipID dado
        /// </summary>
        /// <param name="pIntegrity">Valor nuevo</param>
        /// <param name="pGameShipID">pGameShipID a actualizar</param>
        public void updateGameShipIntegrity(int pIntegrity, int pGameShipID)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME_SHIP
                            where b.gameShipID.Equals(pGameShipID)
                            select b;

                foreach (var item in query)
                {
                    item.integrity = pIntegrity;
                }

                db.SaveChanges();
            }
        }

        /// <summary>
        /// Devuelve el valor de la columna shipID de la tabla GAME_SHIP para un pGameShipID dado
        /// </summary>
        /// <param name="pGameShipID">pGameShipID a buscar</param>
        /// <returns>Valor de la columna</returns>
        public int getShipID(int pGameShipID)
        {
            int shipid = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME_SHIP
                            where b.gameShipID.Equals(pGameShipID)
                            select b;
                foreach (var item in query)
                {
                    shipid = item.shipID;
                }
            }
            return shipid;
        }

        /// <summary>
        /// Devuelve el valor de la columna posX de la tabla GAME_SHIP para el pGameShipID dado
        /// </summary>
        /// <param name="pGameShipID">pGameShipID a buscar</param>
        /// <returns>Valor de la columna</returns>
        public int getPosX(int pGameShipID)
        {
            int posx = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = db.GAME_SHIP.Where(b => b.gameShipID.Equals(pGameShipID)).Select(b => b.posX).FirstOrDefault();
                posx = (int)query;
            }
            return posx;
        }

        /// <summary>
        /// Devuelve el valor de la columna posY de la tabla GAME_SHIP para el pGameShipID dado
        /// </summary>
        /// <param name="pGameShipID">pGameShipID a buscar</param>
        /// <returns>Valor de la columna</returns>
        public int getPosY(int pGameShipID)
        {
            int posy = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = db.GAME_SHIP.Where(b => b.gameShipID.Equals(pGameShipID)).Select(b => b.posY).FirstOrDefault();
                posy = (int)query;
            }
            return posy;
        }

        /// <summary>
        /// Borra todas las filas que se encuentran en la tabla GAME_SHIP que hagan match con el pPlayerID dado
        /// </summary>
        /// <param name="pPlayerID">pPlayerID a borrar</param>
        public void deleteGameShip(int pPlayerID)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.GAME_SHIP
                            where b.playerID.Equals(pPlayerID)
                            select b;
                foreach (var item in query)
                {
                    db.GAME_SHIP.Remove(item);
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Agrega una nueva fila a la tabla MBP_LIVE_SHIPS con los datos dados
        /// </summary>
        /// <param name="pPlayerID">Jugador</param>
        /// <param name="pGameShipID">ID a la nave</param>
        /// <param name="pIndexShipID">pIndex del vaso</param>
        /// <param name="pGameID">ID del juego</param>
        public void addMBPLiveShips(int pPlayerID, int pGameShipID, int pIndexShipID, int pGameID)
        {
            using (var db = new MBP_Data_Entities())
            {
                MBP_LIVE_SHIPS mbpLiveShips = new MBP_LIVE_SHIPS()
                {
                    playerID = pPlayerID,
                    gameID = pGameID,
                    gameShipID = pGameShipID,
                    indexShip = pIndexShipID
                };
                db.MBP_LIVE_SHIPS.Add(mbpLiveShips);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Obtiene el valor de la columna gameShipID de la tabla MBP_LIVE_SHIPS para un gameID y un index dado
        /// </summary>
        /// <param name="pGameID">ID del juego</param>
        /// <param name="pIndex">Index de la nave</param>
        /// <returns>Valor de la columna</returns>
        public int getGameShipID(int pGameID, int pIndex)
        {
            int gameshipid = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query =
                    db.MBP_LIVE_SHIPS.Where(b => b.gameID.Equals(pGameID) && b.indexShip.Equals(pIndex))
                        .Select(b => b.gameShipID)
                        .FirstOrDefault();
                gameshipid = query;
            }
            return gameshipid;
        }

        /// <summary>
        /// Obtiene el valor de la columna playerID de la tabla MBP_LIVE_SHIPS para un gameID y un index dado
        /// </summary>
        /// <param name="pGameID">ID del juego</param>
        /// <param name="pIndex">Index de la nave</param>
        /// <returns>Valor de la columna</returns>
        public int getLivePlayerID(int pGameID, int pIndex)
        {
            int playerid = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query =
                    db.MBP_LIVE_SHIPS.Where(b => b.gameID.Equals(pGameID) && b.indexShip.Equals(pIndex))
                        .Select(b => b.playerID)
                        .FirstOrDefault();
                playerid = query;
            }
            return playerid;
        }

        /// <summary>
        /// Borra una fila de la tabla MBP_LIVE_SHIPS para los datos dados
        /// </summary>
        /// <param name="pGameID">ID de juego a borrar</param>
        /// <param name="pIndex">Index de juego a borrar</param>
        public void deleteLiveShip(int pGameID, int pIndex)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.MBP_LIVE_SHIPS
                            where b.gameID.Equals(pGameID) && b.indexShip.Equals(pIndex)
                            select b;
                db.MBP_LIVE_SHIPS.Remove(query.FirstOrDefault());
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Borra todas las filas de la tabla MBP_LIVE_SHIPS que hagan match con el gameID dado
        /// </summary>
        /// <param name="pGameID">pGameID a borrar</param>
        public void deleteAllLiveShips(int pGameID)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.MBP_LIVE_SHIPS
                            where b.gameID.Equals(pGameID)
                            select b;

                foreach (var item in query)
                {
                    db.MBP_LIVE_SHIPS.Remove(item);
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Devuelve una lista con todas las gameShip que hay en la tabla GAME_SHIP para un pNickname dado
        /// </summary>
        /// <remarks>
        /// Sugerencia: Use la vista "VW_GAME_SHIP_EXT" 
        /// </remarks>
        /// <param name="pNickname">pNickname a buscar</param>
        /// <returns>Lista con las naves</returns>
        public IList<GameShipDTO> getGameShips(string pNickname)
        {
            IList<GameShipDTO> gameShipDtosList = new List<GameShipDTO>();
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_GAME_SHIP_EXT
                            where b.nickname.Equals(pNickname)
                            select b;

                foreach (var item in query)
                {
                    GameShipDTO gameShipDto = new GameShipDTO();
                    gameShipDto._gameShipID = item.gameShipID;
                    gameShipDto._integrity = item.integrity;
                    gameShipDto._playerID =  item.playerID;
                    gameShipDto._posX = item.posX;
                    gameShipDto._posY = item.posY;
                    gameShipDto._shipID = item.shipID;
                    gameShipDtosList.Add(gameShipDto);
                }
            }
            return gameShipDtosList;
        }
    }
}
