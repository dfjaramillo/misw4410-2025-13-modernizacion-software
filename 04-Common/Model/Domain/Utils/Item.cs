using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.CustomFilters;
using Model.Helper;

namespace Model.Domain.Utils
{
    [Table("Item")]
    public class Item : AuditEntity, ISoftDeleted
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdItem { get; set; }
        public int IdCatalogo { get; set; }
        public string ItemName { get; set; }        
        public bool Deleted { get; set; }
    }
}
