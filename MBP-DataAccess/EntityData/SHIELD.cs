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
    
    public partial class SHIELD
    {
        public int shieldID { get; set; }
        public Nullable<int> playerID { get; set; }
        public Nullable<int> abilityID { get; set; }
        public Nullable<int> gameShipID { get; set; }
    
        public virtual GAME_ABILITY GAME_ABILITY { get; set; }
        public virtual GAME_SHIP GAME_SHIP { get; set; }
    }
}