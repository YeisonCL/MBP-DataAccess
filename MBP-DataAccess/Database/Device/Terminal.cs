using MBPL_Logic.Interface.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBP_DataAccess.Database.Device
{
    public class Terminal : ILiveTerminal
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
    }
}
