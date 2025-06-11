using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.CustomFilters;
using Model.Helper;

namespace Model.Domain.Formulario
{
    [Table("FormularioDetalle")]
    public class FormularioDetalle : AuditEntity, ISoftDeleted
    {
        [Key]
        public int IdFormularioDetalle { get; set; }
        public int IdFormulario  { get; set; }
        public int FormDetalleItem { get; set; }
        public string Valor { get; set; }        
        public bool Deleted { get; set; }
    }
}
