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
    [Table("RespuestaDetalles")]
    public class RespuestaDetalle : AuditEntity, ISoftDeleted
    {
        [Key]
        public int IdRespuestaDetalle { get; set; }
        public int IdREspuesta { get; set; }
        public int IdPreguntaDetalle { get; set; }
        public string ValorRespuesta { get; set; }
        public bool Deleted { get; set; }
    }
}
