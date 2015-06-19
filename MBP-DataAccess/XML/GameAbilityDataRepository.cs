using MBP_Logic.Interface.RepositoryInterface.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBP_DataAccess.XML
{
    public class GameAbilityDataRepository : IGameAbilityDataRepository
    {
        /// <summary>
        /// Devuelve la cantidad disponible para uso de cada habilidad al iniciar una partida
        /// </summary>
        /// <param name="pAbilityCode">Habilidad a obtener</param>
        /// <returns>Cantidad disponible para uso</returns>
        public int getAvailableCount(int pAbilityCode)
        {
            return 5;
        }
    }
}
