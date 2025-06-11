using System;
using System.ComponentModel.DataAnnotations;
using Common.CustomFilters;
using Model.Helper;

namespace Model.Domain.Area
{
    public class Area : AuditEntity, ISoftDeleted
    {
        [Key]
        public Int64 IdArea { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
    }
}
