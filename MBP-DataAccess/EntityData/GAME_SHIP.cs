//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MBP_DataAccess.EntityData
{
    using System;
    using System.Collections.Generic;
    
    public partial class GAME_SHIP
    {
        public GAME_SHIP()
        {
            this.BOARD_SHIP = new HashSet<BOARD_SHIP>();
            this.MBP_LIVE_SHIPS = new HashSet<MBP_LIVE_SHIPS>();
            this.SHIELD = new HashSet<SHIELD>();
        }
    
        public int gameShipID { get; set; }
        public int shipID { get; set; }
        public int playerID { get; set; }
        public int integrity { get; set; }
        public int posX { get; set; }
        public int posY { get; set; }
    
        public virtual ICollection<BOARD_SHIP> BOARD_SHIP { get; set; }
        public virtual GAME_PLAYER GAME_PLAYER { get; set; }
        public virtual ICollection<MBP_LIVE_SHIPS> MBP_LIVE_SHIPS { get; set; }
        public virtual ICollection<SHIELD> SHIELD { get; set; }
        public virtual SHIP_CATALOG SHIP_CATALOG { get; set; }
    }
}
