using Common.CustomFilters;
using Model.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Domain.Usuario
{
    [Table("UsuariosAsignado")]
    public class UsuariosAsignado : AuditEntity, ISoftDeleted
    {
        [Key]
        public int IdUsuarioAsignado { get; set; }
        public int IdEvaluacionAsignada { get; set; }
        public string IdEvaluado { get; set; }
        public bool IsEvaluado { get; set; }
        public bool Deleted { get; set; }
    }
}
