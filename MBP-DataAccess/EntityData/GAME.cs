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
    
    public partial class GAME
    {
        public GAME()
        {
            this.CHAT_MESSAGE = new HashSet<CHAT_MESSAGE>();
            this.LIVE_GAME = new HashSet<LIVE_GAME>();
            this.GAME_AND_PLAYER = new HashSet<GAME_AND_PLAYER>();
            this.GAME_SHIP_CATALOG = new HashSet<GAME_SHIP_CATALOG>();
        }
    
        public int gameID { get; set; }
        public Nullable<System.DateTime> turnChangeTime { get; set; }
        public int shotsTurn { get; set; }
        public int boardSize { get; set; }
        public bool profileAccess { get; set; }
        public bool feed { get; set; }
        public bool @lock { get; set; }
        public bool joined { get; set; }
    
        public virtual ICollection<CHAT_MESSAGE> CHAT_MESSAGE { get; set; }
        public virtual ICollection<LIVE_GAME> LIVE_GAME { get; set; }
        public virtual ICollection<GAME_AND_PLAYER> GAME_AND_PLAYER { get; set; }
        public virtual ICollection<GAME_SHIP_CATALOG> GAME_SHIP_CATALOG { get; set; }
    }
}