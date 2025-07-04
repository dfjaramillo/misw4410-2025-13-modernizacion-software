﻿using System;

namespace Model.Custom.Utils
{
    public class GenericItemInfoModel
    {
        public int ItemInfoId { get; set; }

        public CatalogModel ItemInfoType { get; set; }

        public string Value { get; set; }

        public string ValueName { get; set; }

        public string LargeValue { get; set; }

        public bool Enable { get; set; }

        public DateTime LastModify { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
