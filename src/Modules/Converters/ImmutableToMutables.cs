using System.Collections.Generic;
using System;

namespace Cycliq.Converters
{
    public static class Converter
    {
        public static List<Attribute> GetMutableAttributesList(IReadOnlyList<Attribute> attrList)
        {
            return new List<Attribute>(attrList);
        }


    }
}