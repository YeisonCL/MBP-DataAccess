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
    
    public partial class GAME_SHIP_CATALOG
    {
        public int gameID { get; set; }
        public int shipID { get; set; }
        public int shipCount { get; set; }
    
        public virtual GAME GAME { get; set; }
        public virtual SHIP_CATALOG SHIP_CATALOG { get; set; }
    }
}
