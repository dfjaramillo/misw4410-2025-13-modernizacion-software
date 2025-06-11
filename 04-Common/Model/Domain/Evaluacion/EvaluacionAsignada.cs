using System.ComponentModel.DataAnnotations;
using Common.CustomFilters;
using Model.Helper;

namespace Model.Domain.Evaluacion
{
    public class EvaluacionAsignada : AuditEntity, ISoftDeleted
    {
        [Key]
        public int IdEvaluacionAsignada { get; set; }
        public int IdEvaluacion { get; set; }
        public string IdEvaluador { get; set; }
        
        public bool Deleted { get; set; }
    }
}
