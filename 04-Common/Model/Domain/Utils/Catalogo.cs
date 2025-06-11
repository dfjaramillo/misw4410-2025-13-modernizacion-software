using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.CustomFilters;
using Model.Helper;

namespace Model.Domain.Utils
{
    [Table("Catalogo")]
    public class Catalogo : AuditEntity, ISoftDeleted
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdCatalogo { get; set; }
        public string Nombre { get; set; }        
        public bool Deleted { get; set; }                   
    }
}
