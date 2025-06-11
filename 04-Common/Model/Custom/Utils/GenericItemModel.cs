using System.Collections.Generic;

namespace Model.Custom.Utils
{
    public class GenericItemModel
    {
        public int ItemId { get; set; }

        public CatalogModel ItemType { get; set; }

        public string ItemName { get; set; }
        public bool Habilitado { get; set; }
           
        public List<GenericItemInfoModel> ItemInfo { get; set; }

        public GenericItemModel ParentItem { get; set; }

    }
}
