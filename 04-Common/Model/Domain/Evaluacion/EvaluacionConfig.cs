using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.CustomFilters;
using Model.Helper;

namespace Model.Domain.Evaluacion
{
    [Table("EvaluacionConfig")]
    public class EvaluacionConfig : AuditEntity, ISoftDeleted
    {
        [Key]
        public int IdEvaluacionConfig { get; set; }
        public int IdEvaluacion { get; set; }
        public int IdFormulario { get; set; }        
        public bool Deleted { get; set; }
    }
}
