using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.CustomFilters;
using Model.Helper;

namespace Model.Domain.Respuesta
{
    [Table("Respuestas")]
    public class Respuesta : AuditEntity, ISoftDeleted
    {
        [Key]
        public int IdRespuesta { get; set; }
        public string IdEvaluador { get; set; }
        public string IdEvaluado { get; set; }
        public int IdEvaluacion { get; set; }
        public bool Deleted { get; set; }
    }
}
