using MBP_Cross.DTO.ProtocolDTO;
using MBP_DataAccess.EntityData;
using MBP_Logic.Interface.RepositoryInterface.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBP_DataAccess.Database.Shot
{
    public class ShotRepository : IShotRepository
    {
        /// <summary>
        /// Devuelve los shotFeed que se encuentran en la tabla SHOT_FEED para un nickname dado
        /// </summary>
        /// <remarks>
        /// Nota: No hace falta llenar los campos de ID al devolver las filas, pues no son necesarios
        /// Sugerencia: Usar la vista "VW_SHOT_FEED_EXT"
        /// </remarks>
        /// <param name="pNickname">nickname a buscar</param>
        /// <returns>Una lista con todos los shots encontrador en la tabla</returns>
        public IList<ShotFeedDTO> getShotFeed(string pNickname)
        {
            IList<ShotFeedDTO> shotfeedList = null;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.VW_SHOT_FEED_EXT
                            where b.nickname.Equals(pNickname)
                            select b;

                foreach (var item in query)
                {
                    ShotFeedDTO shotFeed = new ShotFeedDTO();
                    shotFeed.setBomb(item.bomb);
                    shotFeed.setPosX(item.posX);
                    shotFeed.setPosY(item.posY);
                    shotFeed.setSuccess(item.success);
                    shotfeedList.Add(shotFeed);
                }
            }
            return shotfeedList;
        }

        /// <summary>
        /// Agrega una nueva fila a la tabla SHOT_FEED segun los valores dados en la variable pShotFeed
        /// </summary>
        /// <param name="pShotFeed">Datos a agregar en la nueva fila</param>
        public void addShotFeed(ShotFeedDTO pShotFeed)
        {
            using (var db = new MBP_Data_Entities())
            {
                SHOT_FEED shotFeed = new SHOT_FEED()
                {
                    bomb = pShotFeed.getBomb(),
                    posX = pShotFeed.getPosX(),
                    destroyedShield = pShotFeed.getDestroyedShield(),
                    posY = pShotFeed.getPosY(),
                    success = pShotFeed.getSuccess(),
                    againstPlayerID = pShotFeed.getAgainstPlayerID()
                };

                db.SHOT_FEED.Add(shotFeed);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Borra todas las filas en la tabla SHOT_FEED que hagan match con el pPlayerID dado
        /// </summary>
        /// <param name="pPlayerID">pPlayerID al cual se le borraran los feeds</param>
        public void deleteShotsFeed(int pPlayerID)
        {
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.SHOT_FEED
                            where b.againstPlayerID.Equals(pPlayerID)
                            select b;

                foreach (var item in query)
                {
                    db.SHOT_FEED.Remove(item);
                }
            }
        }
    }
}
