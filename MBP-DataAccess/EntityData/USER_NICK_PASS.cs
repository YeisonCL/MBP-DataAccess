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
    
    public partial class USER_NICK_PASS
    {
        public USER_NICK_PASS()
        {
            this.ADMIN_USER = new HashSet<ADMIN_USER>();
            this.GAME_USER = new HashSet<GAME_USER>();
            this.MOD_USER = new HashSet<MOD_USER>();
        }
    
        public int userID { get; set; }
        public string nickname { get; set; }
        public string password { get; set; }
        public string type { get; set; }
    
        public virtual ICollection<ADMIN_USER> ADMIN_USER { get; set; }
        public virtual ICollection<GAME_USER> GAME_USER { get; set; }
        public virtual ICollection<MOD_USER> MOD_USER { get; set; }
    }
}
