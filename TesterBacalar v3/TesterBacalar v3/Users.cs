//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TesterBacalar_v3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            this.Rezult = new HashSet<Rezult>();
        }
    
        public int user_id_ { get; set; }

        [Required(ErrorMessage = "Введіть логін!")]
        public string login { get; set; }

        [Required(ErrorMessage = "Введіть пароль!")]
        public string password { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        [Required(ErrorMessage = "Встановіть значення!")]
        public Nullable<bool> is_admin { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rezult> Rezult { get; set; }
    }
}
