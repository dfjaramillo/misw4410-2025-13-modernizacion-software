using System.ComponentModel.DataAnnotations;
using Common.CustomFilters;
using Model.Helper;

namespace Model.Domain.Evaluacion
{
    public class EvaluacionUsuario: AuditEntity, ISoftDeleted
    {
        [Key]        
        public int IdEvaluacionUsuario { get; set; }
        public int IdEvaluacion { get; set; }
        public string IdEvaluador { get; set; }
        public string IdEvaluado { get; set; }
        public bool Deleted { get; set; }
    }
}
