using MBP_Cross.DTO.DatabaseDTO;
using MBP_Cross.DTO.ProtocolDTO;
using MBP_DataAccess.EntityData;
using MBP_Logic.Interface.RepositoryInterface.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBP_DataAccess.Database.GameBoard
{
    public class GameBoardRepository : IGameBoardRepository
    {
        /// <summary>
        /// Agrega una fila a la tabla BOARD_BOX segun los datos dados
        /// </summary>
        /// <param name="pBoardBox">Datos para la nueva fila</param>
        public void addBoardBox(BoardBoxDTO pBoardBox)
        {
            using (var db = new MBP_Data_Entities())
            {
                BOARD_BOX boardbox = new BOARD_BOX()
                {
                    alive = pBoardBox.getAlive(),
                    posX = pBoardBox.getPositionX(),
                    posY = pBoardBox.getPositionY(),
                    playerID = pBoardBox.getPlayerID()
                };
                db.BOARD_BOX.Add(boardbox);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Agrega una nueva fila a la tabla BOARD_SHIP segun los datos dados
        /// </summary>
        /// <param name="pBoardShip">Datos para la nueva fila</param>
        public void addBoardShip(BoardShipDTO pBoardShip)
        {
            using (var db = new MBP_Data_Entities())
            {
                BOARD_SHIP boardShip = new BOARD_SHIP()
                {
                    playerID = pBoardShip.getPlayerID(),
                    gameShipID = pBoardShip.getGameShipID(),
                    posX = pBoardShip.getPositionX(),
                    posY = pBoardShip.getPositionY()

                };
                db.BOARD_SHIP.Add(boardShip);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Devuelve si existe una nave en la posicion dada, para hacer esto revisa si existe una fila en la tabla BOARD_SHIP que haga
        /// match con los valores de pPosx, pPosY y pPlayerID dados, si la fila existe devuelve true de no ser así devuelve false
        /// </summary>
        /// <param name="pPosX">Valor X de la nave</param>
        /// <param name="pPosY">Valor Y de la nave</param>
        /// <param name="pPlayerID">Valor de jugador que tiene la nave</param>
        /// <returns>Si existe o no la fila</returns>
        public bool existShip(int pPosX, int pPosY, int pPlayerID)
        {
            bool existsship = false;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.BOARD_SHIP
                            where b.playerID.Equals(pPlayerID) & b.posX.Equals(pPosX) & b.posY.Equals(pPosY)
                            select b;

                foreach (var item in query)
                {
                    if (item != null)
                    {
                        existsship = true;
                    }
                }
            }
            return existsship;
        }

        /// <summary>
        /// Devuelve el valor de la columa gameShipID de la tabla BOARD_SHIP segun los datos dados
        /// </summary>
        /// <param name="pPosX">Valor X de la nave</param>
        /// <param name="pPosY">Valor Y de la nave</param>
        /// <param name="pPlayerID">Valor del jugador que tiene la nave</param>
        /// <returns>El valor de la columna</returns>
        public int getGameShipID(int pPosX, int pPosY, int pPlayerID)
        {
            int gameshipid = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.BOARD_SHIP
                            where b.playerID.Equals(pPlayerID) & b.posX.Equals(pPosX) & b.posY.Equals(pPosY)
                            select b;


                foreach (var item in query)
                {
                    if (item != null)
                    {
                        gameshipid = (int)item.gameShipID;
                    }
                }
            }
            return gameshipid;
        }

        /// <summary>
        /// Modifica el valor de la columna alive de la tabla BOARD_BOX al valor dado en pValue para los datos dados
        /// </summary>
        /// <param name="pPosX">Valor X del boardBox</param>
        /// <param name="pPosY">Valor Y del boardBox</param>
        /// <param name="pPlayerID">Valor del jugador que tiene el tablero</param>
        /// <param name="pValue">Valor a modificar la columna</param>
        public void setBoardBoxAlive(int pPosX, int pPosY, int pPlayerID, bool pValue)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.BOARD_BOX
                            where b.posX.Equals(pPosX) & b.posY.Equals(pPosY) & b.playerID.Equals(pPlayerID)
                            select b;

                foreach (var item in query)
                {
                    item.alive = pValue;
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Extrae todas las filas de la tabla BOARD_SHIP que haga match con el pGameShipID y el pPlayerID dado, es necesario aclarar que
        /// basta con setear las variables X y Y (Posiciones) de cada shipPositionDTO, las demas variables NO son necesarias
        /// </summary>
        /// <param name="pGameShipID">pGameShipID de la nave</param>
        /// <param name="pPlayerID">pPlayerID del jugador</param>
        /// <returns>Una lista con todos los shotFeedDTO co sus respectivas variables pPosX y pPoxY seteadas UNICAMENTE</returns>
        public IList<ShipPositionDTO> extractAllShipPosition(int pGameShipID, int pPlayerID)
        {
            IList<ShipPositionDTO> allshiPosition = new List<ShipPositionDTO>();
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.BOARD_SHIP
                            where b.gameShipID.Equals(pGameShipID) & b.playerID.Equals(pPlayerID)
                            select b;
                foreach (var item in query)
                {
                    ShipPositionDTO shipposition = new ShipPositionDTO();
                    shipposition.setRawPosition((int)item.posX);
                    shipposition.setColumnPosition((int)item.posY);
                    allshiPosition.Add(shipposition);
                }
            }
            return allshiPosition;
        }

        /// <summary>
        /// Retorna el valor de la columna alive de la tabla BOARD_BOX para los datos dados
        /// </summary>
        /// <param name="pPosX">Posicion X</param>
        /// <param name="pPosY">Posicion Y</param>
        /// <param name="pPlayerID">Jugador</param>
        /// <returns>Valor de la columna</returns>
        public bool getBoardBoxAlive(int pPosX, int pPosY, int pPlayerID)
        {
            bool boarboxalive = false;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.BOARD_BOX
                            where b.posX.Equals(pPosX) & b.posY.Equals(pPosY) & b.playerID.Equals(pPlayerID)
                            select b;

                foreach (var item in query)
                {
                    boarboxalive = item.alive;
                }
            }
            return boarboxalive;
        }

        /// <summary>
        /// Devuelve una lista de enteros de todos los gameShipsID DIFERENTES que se encuentran en la tabla BOARD_SHIP para un pPlayerID
        /// </summary>
        /// <param name="pPlayerID">pPlayerID al cual se le deben sacar los gameShipsID</param>
        /// <returns>Lista de enteros</returns>
        public IList<int> getAllGameShipID(int pPlayerID)
        {
            IList<int> allgameshipID = new List<int>();
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.BOARD_SHIP
                            where b.playerID.Equals(pPlayerID)
                            select b;

                foreach (var item in query)
                {
                    allgameshipID.Add((int)item.gameShipID);
                }
            }
            return allgameshipID;
        }

        /// <summary>
        /// Borra una fila de la tabla BOARD_BOX que haga match con el pPlayerID dado
        /// </summary>
        /// <param name="pPlayerID">pPlayerID a borrar</param>
        public void deleteBoardBox(int pPlayerID)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.BOARD_BOX
                            where b.playerID.Equals(pPlayerID)
                            select b;
                foreach (var item in query)
                {
                    db.BOARD_BOX.Remove(item);
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Borra una fila de la tabla BOARD_SHIP para un pPlayerID dado
        /// </summary>
        /// <param name="pPlayerID">pPlayerID dado</param>
        public void deleteBoardShip(int pPlayerID)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.BOARD_SHIP
                            where b.playerID.Equals(pPlayerID)
                            select b;
                foreach (var item in query)
                {
                    db.BOARD_SHIP.Remove(item);
                }
                db.SaveChanges();
            }
        }
    }
}
