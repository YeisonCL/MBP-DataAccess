using MBP_DataAccess.EntityData;
using MBPL_Logic.Interface.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBP_DataAccess.Database.Device
{
    public class TerminalRepository : ILiveTerminal, ILiveGame
    {
        /// <summary>
        /// Agrea una nueva fila a la tabla LIVE_TERMINAL con los datos dados
        /// </summary>
        /// <param name="pKey">Datos a agregar</param>
        public void addTerminalKey(string pKey)
        {
            using (var db = new MBP_Data_Entities())
            {
                LIVE_TERMINAL liveTerminal = new LIVE_TERMINAL()
                {
                    keyTerminal = pKey
                };
                db.LIVE_TERMINAL.Add(liveTerminal);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Devuelve true si en la tabla LIVE_TERMINAL existe al menos una fila con el pKey dado, de lo contrario devuelve false
        /// </summary>
        /// <param name="pKey">pKey a verificar</param>
        /// <returns>True o false dependiendo</returns>
        public bool getTerminalKey(string pKey)
        {
            bool existterminalkey = false;
            using (var db = new MBP_Data_Entities())
            {
                var query = db.LIVE_TERMINAL.Where(b => b.keyTerminal.Equals(pKey)).Select(b => b).FirstOrDefault();
                if (query != null)
                {
                    existterminalkey = true;
                }
            }
            return existterminalkey;
        }

        /// <summary>
        /// Agrega una nueva fila a la tabla LIVE_GAME con los datos dados
        /// </summary>
        /// <param name="pModeratorID">ID del moderador</param>
        /// <param name="pGameID">ID del juego</param>
        /// <param name="pTerminalID">ID del terminal</param>
        public void addLiveGame(int pModeratorID, int pGameID, int pTerminalID)
        {
            using (var db = new MBP_Data_Entities())
            {
                LIVE_GAME liveGame = new LIVE_GAME()
                {
                    gameID = pGameID,
                    modID = pModeratorID,
                    idTerminal = pTerminalID
                };
                db.LIVE_GAME.Add(liveGame);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Devuelve el valor de la columna idTerminal de la tabla LIVE_TERMINAL para un pkey dado
        /// </summary>
        /// <param name="pKey">pKey a buscar</param>
        /// <returns>Valor de la columna</returns>
        public int getTerminalKeyID(string pKey)
        {
            int idterminal = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.LIVE_TERMINAL
                            where b.keyTerminal.Equals(pKey)
                            select b;

                foreach (var item in query)
                {
                    idterminal = item.idTerminal;
                }
            }
            return idterminal;
        }

        /// <summary>
        /// Obtiene el valor de la columna gameID de la tabla LIVE_GAME para un terminalID dado
        /// </summary>
        /// <param name="pTerminalID">Valor a buscar</param>
        /// <returns>Valor de la columna</returns>
        public int getGameIDFromLiveGame(int pTerminalID)
        {
            int gameid = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.LIVE_GAME
                            where b.idTerminal.Equals(pTerminalID)
                            select b;

                foreach (var item in query)
                {
                    gameid = item.gameID;
                }
            }
            return gameid;
        }
    }
}
