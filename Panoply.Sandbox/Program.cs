using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Panoply.Library;
using Filters = Panoply.Library.Filters;

namespace Panoply.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            DumpFilters();
        }

        private static void DumpFilters()
        {
            Console.WriteLine("Filter categories:");
            foreach (Filters.FilterCategory filterCategory in Filters.FilterCategory.EnumerateFilterCategories())
            {
                DumpFilterCategory(filterCategory);
            }
        }

        private static void DumpFilterCategory(Filters.FilterCategory filterCategory)
        {
            Console.WriteLine("* {0}", filterCategory.FriendlyName);
            Console.WriteLine("    Device Path: {0}",
                filterCategory.DevicePath);
            Console.WriteLine("    Merit: {0}",
                GetMerit(filterCategory));
            Console.WriteLine("    CLSID: {0}",
                filterCategory.Clsid);
            Console.WriteLine("    Filters:");

            DumpFilters(filterCategory.Filters);

            Console.WriteLine();
        }

        private static String GetMerit(Filters.FilterDevice device)
        {
            try
            {
                return device.Merit.ToString();
            }
            catch (Exception e)
            {
                return String.Format("Exception: {0}", e.Message);
            }
        }

        private static void DumpFilters(IList<Filters.Filter> filters)
        {
            foreach (Filters.Filter filter in filters) {
                DumpFilter(filter);
            }
        }

        private static void DumpFilter(Filters.Filter filter)
        {
            Console.WriteLine("      * {0}",
                filter.FriendlyName);
            Console.WriteLine("        Device Path: {0}",
                filter.DevicePath);
            Console.WriteLine("        CLSID: {0}",
                filter.Clsid);
            Console.WriteLine("        Merit: {0}",
                GetMerit(filter));
            Console.WriteLine("        Version: {0}",
                filter.Version);
            Console.WriteLine("        FilePath: {0}",
                filter.FilePath);
            Console.WriteLine("        RawFilePath: {0}",
                filter.RawFilePath);
            Console.WriteLine("        FileVersion: {0}",
                filter.FileVersion);
            Console.WriteLine();

            try
            {
                if (String.Compare(filter.Clsid.ToString(), "04FE9017-F873-410E-871E-AB91661A4EF7", true) == 0)
                {
                    filter.ShowPropertyPage(IntPtr.Zero);
                }
            } catch {
            }
        }
    }
}
