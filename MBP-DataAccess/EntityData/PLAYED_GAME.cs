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
    
    public partial class PLAYED_GAME
    {
        public int playedGameID { get; set; }
        public int winnerID { get; set; }
        public int loserID { get; set; }
        public int winnerScore { get; set; }
        public int looserScore { get; set; }
        public System.DateTime gameDate { get; set; }
    
        public virtual GAME_USER GAME_USER { get; set; }
        public virtual GAME_USER GAME_USER1 { get; set; }
    }
}
