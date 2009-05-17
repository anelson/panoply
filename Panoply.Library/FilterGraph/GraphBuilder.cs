using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using DirectShowLib;

namespace Panoply.Library.FilterGraph
{
    public class GraphBuilder
    {
        public static Graph BuildFilterGraphForFile(String fileName)
        {
            return BuildFilterGraphForFiles(new String[] { fileName });
        }

        public static Graph BuildFilterGraphForFiles(String[] fileNames)
        {
            IGraphBuilder builder = CreateGraphBuilder();

            int hr;
            foreach (String fileName in fileNames)
            {
                hr = builder.RenderFile(fileName, null);
                DsError.ThrowExceptionForHR(hr);
            }

            IEnumFilters filtersEnum;
            hr = builder.EnumFilters(out filtersEnum);
            DsError.ThrowExceptionForHR(hr);

            IBaseFilter[] filters = new IBaseFilter[1];
            IntPtr count = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(ulong)));

            try
            {
                Graph graph = new Graph();

                while ((hr = filtersEnum.Next(1, filters, count)) == 0)
                {
                    IBaseFilter filter = filters[0];

                    graph.AddFilter(new Filter(graph, filter));
                }

                return graph;
            }
            finally
            {
                Marshal.FreeCoTaskMem(count);
                count = IntPtr.Zero;
            }
        }

        private static IGraphBuilder CreateGraphBuilder()
        {
            DirectShowLib.FilterGraph filterManager = new DirectShowLib.FilterGraph();
            IGraphBuilder builder = (IGraphBuilder)filterManager;

            return builder;
        }
    }
}
