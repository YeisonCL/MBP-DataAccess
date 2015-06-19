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
            
        }

        /// <summary>
        /// Devuelve true si en la tabla LIVE_TERMINAL existe al menos una fila con el pKey dado, de lo contrario devuelve false
        /// </summary>
        /// <param name="pKey">pKey a verificar</param>
        /// <returns>True o false dependiendo</returns>
        public bool getTerminalKey(string pKey)
        {
            return false;
        }

        /// <summary>
        /// Agrega una nueva fila a la tabla LIVE_GAME con los datos dados
        /// </summary>
        /// <param name="pModeratorID">ID del moderador</param>
        /// <param name="pGameID">ID del juego</param>
        /// <param name="pTerminalID">ID del terminal</param>
        public void addLiveGame(int pModeratorID, int pGameID, int pTerminalID)
        {
            
        }

        /// <summary>
        /// Devuelve el valor de la columna idTerminal de la tabla LIVE_TERMINAL para un pkey dado
        /// </summary>
        /// <param name="pKey">pKey a buscar</param>
        /// <returns>Valor de la columna</returns>
        public int getTerminalKeyID(string pKey)
        {
            return 0;
        }

        /// <summary>
        /// Obtiene el valor de la columna gameID de la tabla LIVE_GAME para un terminalID dado
        /// </summary>
        /// <param name="pTerminalID">Valor a buscar</param>
        /// <returns>Valor de la columna</returns>
        public int getGameIDFromLiveGame(int pTerminalID)
        {
            return 0;
        }
    }
}
