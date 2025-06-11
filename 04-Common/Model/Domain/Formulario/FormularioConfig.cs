using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.CustomFilters;
using Model.Helper;

namespace Model.Domain.Formulario
{
    [Table("FormularioConfig")]
    public class FormularioConfig : AuditEntity, ISoftDeleted    
    {
        [Key]
        public int IdFormularioConfig { get; set; }
        public int IdFormulario { get; set; }
        public int IdPregunta { get; set; }        
        public bool Deleted { get; set; }
        
    }
}
