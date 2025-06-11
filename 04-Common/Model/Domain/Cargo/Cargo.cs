using Common.CustomFilters;
using Model.Helper;

namespace Model.Domain.Cargo
{
    public class Cargo : AuditEntity, ISoftDeleted
    {
        public long CargoId { get; set; }
        public string Nombre { get; set; }
        public bool Deleted { get; set; }
    }
}
