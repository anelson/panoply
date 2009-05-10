using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml;

using Panoply.Library.Filters;

namespace Panoply.Library.Presentation
{
    public class FilterCategoryTreeNode : FilterDeviceTreeNode
    {
        /// <summary>
        /// Comparer that sorts categories by their friendly name
        /// </summary>
        private class FilterCategoryTreeNodeComparision : FilterDeviceTreeNodeComparision, IComparer<FilterCategoryTreeNode>
        {
            public int Compare(FilterCategoryTreeNode x, FilterCategoryTreeNode y)
            {
                return base.Compare(x, y);
            }
        }
        /// <summary>
        /// Comparer that sorts filters by their friendly name
        /// </summary>
        private class FilterTreeNodeComparision : FilterDeviceTreeNodeComparision, IComparer<FilterTreeNode>
        {
            public int Compare(FilterTreeNode x, FilterTreeNode y)
            {
                return base.Compare(x, y);
            }
        }
        /// <summary>
        /// Comparer that sorts filter device nodes by their friendly name
        /// </summary>
        private class FilterDeviceTreeNodeComparision : IComparer<FilterDeviceTreeNode>
        {
            public int Compare(FilterDeviceTreeNode x, FilterDeviceTreeNode y)
            {
                return String.Compare(x.FriendlyName, y.FriendlyName);
            }
        }

        FilterCategory _category;
        List<FilterTreeNode> _filters;

        public FilterCategoryTreeNode(FilterCategory category)
            : base(category)
        {
            _category = category;
        }

        public static List<FilterCategoryTreeNode> EnumerateFilterCategories()
        {
            List<FilterCategoryTreeNode> categories = new List<FilterCategoryTreeNode>();

            foreach (FilterCategory category in FilterCategory.EnumerateFilterCategories())
            {
                FilterCategoryTreeNode categoryNode = new FilterCategoryTreeNode(category);

                //Don't include categories with no filters
                //Note that the Filters property is null if there's an exception querying it; those should 
                //still be included so the user can see it.
                if (categoryNode.Filters != null && categoryNode.Filters.Count == 0)
                {
                    continue;
                }
                categories.Add(categoryNode);
            }

            //Sort alphabetically by friendly name
            categories.Sort(new FilterCategoryTreeNodeComparision());

            return categories;
        }

        public static void WriteFilterCategoriesToXml(Stream stream, IEnumerable<FilterCategoryTreeNode> categories) {
            //Write an XML file describing the filter categories tree
            using (XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8)) {
                writer.WriteStartElement("categories");
                foreach (FilterCategoryTreeNode category in categories)
                {
                    category.WriteToXml(writer);
                }
                writer.WriteEndElement();
            }
        }

        public IList<FilterTreeNode> Filters
        {
            get
            {
                try
                {
                    if (_filters == null)
                    {
                        _filters = EnumerateFilters();
                    }

                    return _filters.AsReadOnly();
                } catch (Exception e) {
                    ReportPropertyException("Filters", e);
                    return null;
                }
            }
        }

        public Exception FiltersException
        {
            get { return GetPropertyException("Filters"); }
        }

        internal override void WriteToXml(XmlWriter writer)
        {
            writer.WriteStartElement("category");
            {
                WriteXmlAttributeIfNonNull(writer, "friendlyName", FriendlyName, FriendlyNameException);

                WriteXmlAttributeIfNonNull(writer, "devicePath", DevicePath, DevicePathException);

                WriteXmlAttributeIfNonNull(writer, "merit", Merit, MeritException);

                WriteXmlAttributeIfNonNull(writer, "clsid", Clsid, ClsidException);

                writer.WriteStartElement("filters");
                if (FiltersException != null)
                {
                    writer.WriteAttributeString("filtersException", FiltersException.ToString());
                }
                else
                {
                    foreach (FilterTreeNode filter in Filters)
                    {
                        filter.WriteToXml(writer);
                    }
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        private List<FilterTreeNode> EnumerateFilters()
        {
            List<FilterTreeNode> filters = new List<FilterTreeNode>();
            foreach (Filter filter in _category.Filters)
            {
                filters.Add(new FilterTreeNode(filter));
            }

            //sort alphabetically by name
            filters.Sort(new FilterTreeNodeComparision());

            return filters;
        }
    }
}
