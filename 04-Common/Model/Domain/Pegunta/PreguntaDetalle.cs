using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.CustomFilters;
using Model.Helper;

namespace Model.Domain.Pegunta
{
    [Table("PreguntaDetalle")]
    public class PreguntaDetalle : AuditEntity, ISoftDeleted
    {
        [Key]
        public int IdPreguntaDetalle { get; set; }
        public int IdPregunta { get; set; }
        public int DetalleItem { get; set; }
        public string Valor { get; set; }
        public string ValorLargo { get; set; }             
        public bool Deleted { get; set; }        
    }
}
