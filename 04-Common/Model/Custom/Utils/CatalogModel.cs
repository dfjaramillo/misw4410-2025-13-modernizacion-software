using Model.Custom.UsuarioGridView;
using System;

namespace Model.Custom.Utils
{
    public class CatalogModel
    {
        public int CatalogId { get; set; }

        public string CatalogName { get; set; }

        public bool CatalogEnable { get; set; }

        public Int64 ItemId { get; set; }
        public string ItemIdString { get; set; }

        public string ItemName { get; set; }

        public bool ItemEnable { get; set; }

        public string GroupKey { get; set; }
       
    }
}
