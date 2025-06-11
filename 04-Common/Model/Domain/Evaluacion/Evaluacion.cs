using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.CustomFilters;
using Model.Helper;

namespace Model.Domain.Evaluacion
{
    [Table("Evaluacion")]
    public class Evaluacion : AuditEntity, ISoftDeleted
    {
        [Key]
        public int IdEvaluacion { get; set; }
        public string Nombre { get; set; }        
        public bool Deleted { get; set; }
    }
}
