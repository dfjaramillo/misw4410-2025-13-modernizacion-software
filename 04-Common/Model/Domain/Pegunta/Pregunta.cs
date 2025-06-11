using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.CustomFilters;
using Model.Helper;

namespace Model.Domain.Pegunta
{
    [Table("Pregunta")]
    public class Pregunta : AuditEntity, ISoftDeleted
    {
        [Key]
        public int IdPregunta { get; set; }
        public int IdFormulario { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }       
        public bool Deleted { get; set; }
        public List<PreguntaDetalle> PreguntaDetalles { get; set; }

    }
}
