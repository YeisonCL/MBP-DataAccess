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
    
    public partial class GAME_USER
    {
        public GAME_USER()
        {
            this.GAME_PLAYER = new HashSet<GAME_PLAYER>();
            this.GAME_USER_ABILITY = new HashSet<GAME_USER_ABILITY>();
            this.PLAYED_GAME = new HashSet<PLAYED_GAME>();
            this.PLAYED_GAME1 = new HashSet<PLAYED_GAME>();
        }
    
        public int userID { get; set; }
        public int nickAndPassID { get; set; }
        public string name { get; set; }
        public string secondName { get; set; }
        public string country { get; set; }
        public string email { get; set; }
        public string genre { get; set; }
        public System.DateTime regDate { get; set; }
        public int wins { get; set; }
        public int defeats { get; set; }
        public int hits { get; set; }
        public int misses { get; set; }
        public int totalPoints { get; set; }
        public int points { get; set; }
        public Nullable<System.DateTime> birthdate { get; set; }
        public string personalDescription { get; set; }
    
        public virtual ICollection<GAME_PLAYER> GAME_PLAYER { get; set; }
        public virtual ICollection<GAME_USER_ABILITY> GAME_USER_ABILITY { get; set; }
        public virtual USER_NICK_PASS USER_NICK_PASS { get; set; }
        public virtual ICollection<PLAYED_GAME> PLAYED_GAME { get; set; }
        public virtual USER_PHOTO USER_PHOTO { get; set; }
        public virtual ICollection<PLAYED_GAME> PLAYED_GAME1 { get; set; }
    }
}
