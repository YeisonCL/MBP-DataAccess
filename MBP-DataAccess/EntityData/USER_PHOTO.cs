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
    
    public partial class USER_PHOTO
    {
        public System.Guid uniqueID { get; set; }
        public int userID { get; set; }
        public string photo { get; set; }
    
        public virtual GAME_USER GAME_USER { get; set; }
    }
}
