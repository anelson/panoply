using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Panoply.Library;
using Filters = Panoply.Library.Filters;
using Presentation = Panoply.Library.Presentation;
using MediaInfo = Panoply.Library.MediaInfo;
using FilterGraph = Panoply.Library.FilterGraph;

namespace Panoply.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            String cmd = "all";
            if (args.Length >= 1)
            {
                cmd = args[0].ToLower();
            }

            if (cmd == "all" || cmd == "dumpfilters")
            {
                DumpFilters();
            }

            if (cmd == "all" || cmd == "dumpfilterspresentation")
            {
                DumpFiltersPresentation();
            }

            if (cmd == "all" || cmd == "dumpfiltersxml")
            {
                DumpFiltersXml();
            }

            if (cmd == "showfiltergraph" && args.Length > 1)
            {
                List<String> fileNames = new List<string>(args);
                fileNames.RemoveAt(0);

                ShowFilterGraph(fileNames.ToArray());
            }

            if (cmd == "renderfiltergraph" && args.Length > 2)
            {
                List<String> fileNames = new List<string>(args);
                fileNames.RemoveAt(0);
                fileNames.RemoveAt(fileNames.Count - 1);

                RenderFilterGraph(fileNames.ToArray(), args[args.Length-1]);
            }

            if (cmd == "mediainfo" && args.Length > 1)
            {
                DumpMediaInfo(args[1]);
            }
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

        private static void DumpFiltersPresentation()
        {
            Console.WriteLine("Filter categories:");
            foreach (Presentation.FilterCategoryTreeNode filterCategory in Presentation.FilterCategoryTreeNode.EnumerateFilterCategories())
            {
                DumpFilterCategoryPresentation(filterCategory);
            }
        }

        private static void DumpFilterCategoryPresentation(Panoply.Library.Presentation.FilterCategoryTreeNode filterCategory)
        {
            Console.Write("* {0}", filterCategory.FriendlyName);
            Console.WriteLine(" [{0}]", filterCategory.FriendlyNameException);

            Console.Write("    Device Path: {0}", filterCategory.DevicePath);
            Console.WriteLine(" [{0}]", filterCategory.DevicePathException);

            Console.Write("    Merit: {0}", filterCategory.Merit);
            Console.WriteLine(" [{0}]", filterCategory.MeritException);

            Console.Write("    CLSID: {0}", filterCategory.Clsid);
            Console.WriteLine(" [{0}]", filterCategory.ClsidException);

            Console.WriteLine("    Filters:");

            DumpFiltersPresentation(filterCategory.Filters);

            Console.WriteLine();
        }

        private static void DumpFiltersPresentation(IList<Panoply.Library.Presentation.FilterTreeNode> filters)
        {
            foreach (Presentation.FilterTreeNode filter in filters)
            {
                DumpFilterPresentation(filter);
            }
        }

        private static void DumpFilterPresentation(Panoply.Library.Presentation.FilterTreeNode filter)
        {
            Console.Write("      * {0}", filter.FriendlyName);
            Console.WriteLine(" [{0}]", filter.FriendlyNameException);

            Console.Write("        Device Path: {0}", filter.DevicePath);
            Console.WriteLine(" [{0}]", filter.DevicePathException);

            Console.Write("        CLSID: {0}", filter.Clsid);
            Console.WriteLine(" [{0}]", filter.ClsidException);

            Console.Write("        Merit: {0}", filter.Merit);
            Console.WriteLine(" [{0}]", filter.MeritException);

            Console.Write("        Version: {0}", filter.Version);
            Console.WriteLine(" [{0}]", filter.VersionException);

            Console.Write("        FilePath: {0}", filter.FilePath);
            Console.WriteLine(" [{0}]", filter.FilePathException);

            Console.Write("        RawFilePath: {0}", filter.RawFilePath);
            Console.WriteLine(" [{0}]", filter.RawFilePathException);

            Console.Write("        FileVersion: {0}", filter.FileVersion);
            Console.WriteLine(" [{0}]", filter.FileVersionException);

            Console.WriteLine();
        }

        private static void DumpFiltersXml()
        {
            Console.WriteLine("Filter tree as XML:");
            Console.WriteLine();

            Presentation.FilterCategoryTreeNode.WriteFilterCategoriesToXml(
                System.Console.OpenStandardOutput(),
                Presentation.FilterCategoryTreeNode.EnumerateFilterCategories());

            Console.WriteLine();
        }

        private static void DumpMediaInfo(string path)
        {
            Console.WriteLine("Media info for '{0}'", path);
            Console.WriteLine("MediaInfoLib version {0}", MediaInfo.MediaInfo.LibraryVersion);

            using (MediaInfo.MediaInfo mi = MediaInfo.MediaInfo.Open(path))
            {
                Console.WriteLine(mi.Inform());

                Console.WriteLine();
                Console.WriteLine("Media info tree: ");

                foreach (MediaInfo.StreamType type in Enum.GetValues(typeof(MediaInfo.StreamType)))
                {
                    DumpMediaInfoStreams(mi, type);
                }
            }
        }

        private static void DumpMediaInfoStreams(Panoply.Library.MediaInfo.MediaInfo mi, Panoply.Library.MediaInfo.StreamType type)
        {
            Console.WriteLine("Stream Type {0}:", type);

            foreach (MediaInfo.Stream stream in mi.GetStreams(type))
            {
                DumpMediaInfoStream(stream);
                Console.WriteLine();
            }
        }

        private static void DumpMediaInfoStream(Panoply.Library.MediaInfo.Stream stream)
        {
            Console.WriteLine("  * Stream Number {0}:", stream.Number + 1);
            Console.WriteLine("    Inform: {0}", stream.Inform());
            Console.WriteLine();
            Console.WriteLine("    Parameters:");
            foreach (MediaInfo.Parameter param in stream.Parameters)
            {
                if (String.IsNullOrEmpty(param.Value))
                {
                    continue;
                }

                Console.Write("    * {0}", param.Name);
                if (!String.IsNullOrEmpty(param.LocalizedName) &&
                    param.Name != param.LocalizedName)
                {
                    Console.Write(" ({0})", param.LocalizedName);
                }

                Console.Write(" = '{0}'", param.Value);
                if (!String.IsNullOrEmpty(param.Units))
                {
                    Console.Write(" {0}", param.Units);
                    if (!String.IsNullOrEmpty(param.LocalizedUnits) &&
                        param.Units != param.LocalizedUnits)
                    {
                        Console.Write(" ({0})", param.LocalizedUnits);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static void ShowFilterGraph(String[] fileNames)
        {
            foreach (String fileName in fileNames)
            {
                Console.WriteLine("Rendering '{0}'", fileName);
            }

            FilterGraph.Graph graph = FilterGraph.GraphBuilder.BuildFilterGraphForFiles(fileNames);

            Console.WriteLine("Filter graph:");
            foreach (FilterGraph.Filter filter in graph.Filters)
            {
                ShowFilterGraphFilter(filter);
            }
        }

        private static void ShowFilterGraphFilter(Panoply.Library.FilterGraph.Filter filter)
        {
            Console.WriteLine(" * {0}", filter.Name);
            Console.WriteLine("   Input pins:");
            ShowFilterGraphFilterPins(filter, DirectShowLib.PinDirection.Input);
            Console.WriteLine("   Output pins:");
            ShowFilterGraphFilterPins(filter, DirectShowLib.PinDirection.Output);
            Console.WriteLine();
        }

        private static void ShowFilterGraphFilterPins(Panoply.Library.FilterGraph.Filter filter, DirectShowLib.PinDirection direction)
        {
            foreach (FilterGraph.Pin pin in filter.GetPins(direction))
            {
                Console.Write("     * {0}",
                    pin.Name);

                FilterGraph.Pin connectedPin = pin.GetConnectedPin();
                if (connectedPin == null)
                {
                    Console.WriteLine(" (Not connected)");
                }
                else
                {
                    Console.WriteLine(" (Connected to filter '{0}' pin '{1}')",
                        connectedPin.Filter.Name,
                        connectedPin.Name);
                }
            }
        }

        private static void RenderFilterGraph(String[] fileNames, string outputFilePath)
        {
            foreach (String filePath in fileNames)
            {
                Console.WriteLine("Rendering '{0}'", filePath);
            }

            FilterGraph.Graph graph = FilterGraph.GraphBuilder.BuildFilterGraphForFiles(fileNames);

            using (TextWriter writer = File.CreateText(outputFilePath)) {
                graph.RenderAsDotFile(writer);
            }

            Console.WriteLine("Rendering filter graph to '{0}'",
                outputFilePath);
        }
    }
}
