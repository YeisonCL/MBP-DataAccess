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
    
    public partial class MOD_USER
    {
        public MOD_USER()
        {
            this.LIVE_GAME = new HashSet<LIVE_GAME>();
        }
    
        public int userID { get; set; }
        public int nickAndPassID { get; set; }
        public string name { get; set; }
        public string secondName { get; set; }
        public string country { get; set; }
        public string email { get; set; }
        public string genre { get; set; }
        public string business { get; set; }
        public System.DateTime regDate { get; set; }
        public Nullable<System.DateTime> birthdate { get; set; }
        public int userPhotoID { get; set; }
    
        public virtual ICollection<LIVE_GAME> LIVE_GAME { get; set; }
        public virtual USER_NICK_PASS USER_NICK_PASS { get; set; }
        public virtual USER_PHOTO USER_PHOTO { get; set; }
    }
}
