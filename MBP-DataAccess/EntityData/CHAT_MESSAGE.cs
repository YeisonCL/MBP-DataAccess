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
    
    public partial class CHAT_MESSAGE
    {
        public int chatMessageID { get; set; }
        public Nullable<int> gameID { get; set; }
        public Nullable<int> playerID { get; set; }
        public string message { get; set; }
    
        public virtual GAME_PLAYER GAME_PLAYER { get; set; }
        public virtual GAME GAME { get; set; }
    }
}