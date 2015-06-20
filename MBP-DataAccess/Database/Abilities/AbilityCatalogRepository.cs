using MBP_DataAccess.EntityData;
using MBP_Logic.Interface.RepositoryInterface.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBP_DataAccess.Database.Abilities
{
    public class AbilityCatalogRepository : IAbilityCatalogRepository
    {
        /// <summary>
        /// Devuelve el valor de la columna defeats de la tabla ABILITY_CATALOG dado un abilityID.
        /// </summary>
        /// <param name="pAbilityID">pAbilityID dado al cual se extraeran los datos</param>
        /// <returns>El valor de la columna</returns>
        public int getAbilityDefeats(int pAbilityID)
        {
            int abilitydefeats = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = db.ABILITY_CATALOG.Where(b => b.abilityID.Equals(pAbilityID)).Select(b => b.abilityID).FirstOrDefault();
                abilitydefeats = query;
            }
            return abilitydefeats;
        }

        /// <summary>
        /// Devuelve el valor de la columna points de la tabla ABILITY_CATALOG dado un abilityID.
        /// </summary>
        /// <param name="pAbilityID">pAbilityID dado al cual se extraeran los datos</param>
        /// <returns>El valor de la columna</returns>
        public int getAbilityPoints(int pAbilityID)
        {
            int points = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = db.ABILITY_CATALOG.Where(b => b.abilityID.Equals(pAbilityID)).Select(b => b.points).FirstOrDefault();
                points = (int)query;
            }
            return points;
        }

        /// <summary>
        /// Devuelve el valor de la columna ranking de la tabla ABILITY_CATALOG dado un abilityID.
        /// </summary>
        /// <param name="pAbilityID">pAbilityID dado al cual se extraeran los datos</param>
        /// <returns>El valor de la columna</returns>
        public int getAbilityRanking(int pAbilityID)
        {
            int ranking = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = db.ABILITY_CATALOG.Where(b => b.abilityID.Equals(pAbilityID)).Select(b => b.ranking).FirstOrDefault();
                ranking = (int)query;
            }
            return ranking;
        }

        /// <summary>
        /// Devuelve el valor de la columna wins de la tabla ABILITY_CATALOG dado un abilityID.
        /// </summary>
        /// <param name="pAbilityID">pAbilityID dado al cual se extraeran los datos</param>
        /// <returns>El valor de la columna</returns>
        public int getAbilityWins(int pAbilityID)
        {
            int wins = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = db.ABILITY_CATALOG.Where(b => b.abilityID.Equals(pAbilityID)).Select(b => b.wins).FirstOrDefault();
                wins = (int)query;
            }
            return wins;
        }

        /// <summary>
        /// Devuelve el valor de la columna experience de la tabla ABILITY_CATALOG dado un abilityID.
        /// </summary>
        /// <param name="pAbilityID">pAbilityID dado al cual se extraeran los datos</param>
        /// <returns>El valor de la columna</returns>
        public int getAbilityExperience(int pAbilityID)
        {
            int experience = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = db.ABILITY_CATALOG.Where(b => b.abilityID.Equals(pAbilityID)).Select(b => b.experience).FirstOrDefault();
                experience = (int)query;
            }
            return experience;
        }


        public string getAbilityName(int pAbilityID)
        {
            string name = "";
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.ABILITY_CATALOG
                            where b.abilityID.Equals(pAbilityID)
                            select b;
                foreach (var item in query)
                {
                    name = item.name;
                }
            }
            return name;
        }
    }
}
