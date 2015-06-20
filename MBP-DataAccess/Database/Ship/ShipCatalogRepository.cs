using MBP_Cross.DTO;
using MBP_Cross.DTO.DatabaseDTO;
using MBP_Cross.DTO.ProtocolDTO;
using MBP_DataAccess.EntityData;
using MBP_Logic.Interface.RepositoryInterface.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MBP_DataAccess.Database.Ship
{
    public class ShipCatalogRepository : IShipCatalogRepository
    {
        /// <summary>
        /// Devuelve todas las naves que se encuentra disponibles dentro del catálogo de naves para crear juegos
        /// </summary>
        /// <returns>Devuelve un objeto que contiene el catálogo de naves</returns>
        public IList<ShipDTO> getAllShipCatalog()
        {
            IList<ShipDTO> Ship_catalog = new List<ShipDTO>();
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.SHIP_CATALOG
                            select b;
                foreach (var item in query)
                {
                    ShipDTO shipDto_temp = new ShipDTO();
                    shipDto_temp.setWidth(item.width);
                    shipDto_temp.setHeight(item.height);
                    shipDto_temp.setName(item.name);
                    shipDto_temp.setPoints(item.points);
                    shipDto_temp.setVersion(Convert.ToInt32(item.shipVersion));
                    shipDto_temp.setPhoto(item.photo);
                    Ship_catalog.Add(shipDto_temp);
                }
            }
            return Ship_catalog;
        }

        /// <summary>
        /// Devuelve el shipID de una nave de la tabla SHIP_CATALOG buscandola por su nombre y version
        /// </summary>
        /// <param name="pName">Nombre de la nave</param>
        /// <param name="pVersion">Version de la nave</param>
        /// <returns>shipID de la nave</returns>
        public int getShipID(string pName, int pVersion)
        {
            int shipId_temp = 0;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.SHIP_CATALOG
                            where b.name.Equals(pName) && b.shipVersion.Equals(pVersion)
                            select b;
                if (query.FirstOrDefault() != null)
                {
                    shipId_temp = query.FirstOrDefault().shipID;
                }
            }
            return shipId_temp;
        }

        /// <summary>
        /// Devuelve una nave completa dado su pShipID de nave
        /// </summary>
        /// <param name="pShipID">pShipID a buscar</param>
        /// <returns>La nave con todos sus datos</returns>
        public ShipCatalogDTO getShipCatalog(int pShipID)
        {
            ShipCatalogDTO ship = new ShipCatalogDTO();
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.SHIP_CATALOG
                            where b.shipID.Equals(pShipID)
                            select b;

                foreach (var item in query)
                {
                    ship._height = item.height;
                    ship._name = item.name;
                    ship._photo = item.photo.ToString();
                    ship._points = item.points;
                    ship._active = item.active;
                    ship._version = (int)item.shipVersion;
                    ship._width = item.width;
                }

            }
            return ship;
        }

        /// <summary>
        /// Devuelve el valor de la columna points de la tabla SHIP_CATALOG para un pShipID dado
        /// </summary>
        /// <param name="pShipID">pShiID a buscar</param>
        /// <returns>Valor de la columna</returns>
        public int getPoints(int pShipID)
        {
            int points = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.SHIP_CATALOG
                            where b.shipID.Equals(pShipID)
                            select b;

                foreach (var item in query)
                {
                    points = item.points;
                }
            }
            return points;
        }

        /// <summary>
        /// Devuelve el valor de la columna height de la tabla SHIP_CATALOG para un pShipID dado
        /// </summary>
        /// <param name="pShipID">pShiID a buscar</param>
        /// <returns>Valor de la columna</returns>
        public int getHeight(int pShipID)
        {
            int height = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = db.SHIP_CATALOG.Where(b => b.shipID.Equals(pShipID)).Select(b => b.height).FirstOrDefault();
                height = query;
            }
            return height;
        }

        /// <summary>
        /// Devuelve el valor de la columna width de la tabla SHIP_CATALOG para un pShipID dado
        /// </summary>
        /// <param name="pShipID">pShiID a buscar</param>
        /// <returns>Valor de la columna</returns>
        public int getWidth(int pShipID)
        {
            int width = -1;
            using (var db = new MBP_Data_Entities())
            {
                var query = db.SHIP_CATALOG.Where(b => b.shipID.Equals(pShipID)).Select(b => b.width).FirstOrDefault();
                width = query;
            }
            return width;
        }

        /// <summary>
        /// Devuelve el valor de la columna photo de la tabla SHIP_CATALOG para un pShipID dado
        /// </summary>
        /// <param name="pShipID">pShiID a buscar</param>
        /// <returns>Valor de la columna</returns>
        public string getPhoto(int pShipID)
        {
            string photo = "";
            using (var db = new MBP_Data_Entities())
            {
                var query = from b in db.SHIP_CATALOG
                            where b.shipID.Equals(pShipID)
                            select b;
                foreach (var item in query)
                {
                    photo = item.photo;
                }
            }
            return photo;
        }
    }
}
