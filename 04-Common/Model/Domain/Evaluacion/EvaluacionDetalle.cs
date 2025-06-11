using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.CustomFilters;
using Model.Helper;

namespace Model.Domain.Evaluacion
{
    [Table("EvaluacionDetalle")]
    public class EvaluacionDetalle : AuditEntity, ISoftDeleted
    {
        [Key]
        public int IdEvaluacionDetalle { get; set; }
        public int IdEvaluacion { get; set; }
        public int EvaluacionDetalleItem { get; set; }
        public string Valor { get; set; }        
        public bool Deleted { get; set; }
    }
}
