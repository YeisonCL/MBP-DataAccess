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
    
    public partial class GAME_USER_ABILITY
    {
        public int gameUserAbilityID { get; set; }
        public int gameUserID { get; set; }
        public int abilityID { get; set; }
    
        public virtual ABILITY_CATALOG ABILITY_CATALOG { get; set; }
        public virtual GAME_USER GAME_USER { get; set; }
    }
}
