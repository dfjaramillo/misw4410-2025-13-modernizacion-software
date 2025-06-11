using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.CustomFilters;
using Model.Helper;

namespace Model.Domain.Formulario
{
    [Table("Formulario")]
    public class Formulario : AuditEntity, ISoftDeleted
    {
        [Key]
        public int IdFormulario { get; set; }
        public string Nombre { get; set; }        
        public bool Deleted { get; set; }
        
    }
}
