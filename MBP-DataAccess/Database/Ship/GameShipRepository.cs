using MBP_Cross.DTO.DatabaseDTO;
using MBP_DataAccess.EntityData;
using MBP_Logic.Interface.RepositoryInterface.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBP_DataAccess.Database.Ship
{
    public class GameShipRepository : IGameShipRepository
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
                var query = from b in db.GAME_SHIP
                            orderby b.gameShipID
                            select b.gameShipID;
                gameShipID_temp = query.Last();
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
                            select b.integrity;
                foreach (var item in query)
                {
                    integrity_temp = item;
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
                var query = db.GAME_SHIP.Where(b => b.gameShipID.Equals(pGameShipID)).Select(b => b.shipID).FirstOrDefault();
                shipid = (int)query;
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
            
        }
    }
}
