//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp6.AppDate
{
    using System;
    using System.Collections.Generic;
    
    public partial class izbr
    {
        public int id { get; set; }
        public int car_id { get; set; }
        public int user_id { get; set; }
    
        public virtual Carss Carss { get; set; }
        public virtual user user { get; set; }
    }
}
