using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Panoply.Library.FilterGraph
{
    public class Graph
    {
        List<Filter> _filters = new List<Filter>();
        internal Graph()
        {
        }

        internal void AddFilter(Filter filter)
        {
            _filters.Add(filter);
        }

        public IList<Filter> Filters { get { return _filters.AsReadOnly(); } }

        /// <summary>
        /// Finds all the filters that have no input pins, and thus form roots of the graph
        /// There's usually only one such filter
        /// </summary>
        /// <returns></returns>
        public IList<Filter> GetRootFilters()
        {
            List<Filter> rootFilters = new List<Filter>();

            foreach (Filter filter in _filters)
            {
                if (filter.GetPins(DirectShowLib.PinDirection.Input).Count == 0)
                {
                    rootFilters.Add(filter);
                }
            }

            return rootFilters;
        }

        public void RenderAsDotFile(TextWriter writer)
        {
            RenderDotFileHead(writer);

            foreach (Filter filter in _filters)
            {
                RenderDotFileFilter(writer, filter);
            }
            foreach (Filter filter in _filters)
            {
                RenderDotFileFilterEdges(writer, filter);
            }

            RenderDotFileFoot(writer);
        }

        internal Pin FindPinForIPin(DirectShowLib.IPin ipin)
        {
            foreach (Filter filter in _filters)
            {
                foreach (Pin pin in filter.Pins)
                {
                    if (pin.IPin == ipin)
                    {
                        return pin;
                    }
                }
            }

            return null;
        }

        private void RenderDotFileHead(TextWriter writer)
        {
            writer.WriteLine("digraph g {");
            writer.WriteLine("  rankdir=LR;");
            writer.WriteLine(@"  label=< 
  <table border='0' cellpadding='1' bgcolor='lightgray'> 
    <tr> 
        <td colspan='2' align='center'>LEGEND</td> 
    </tr>
    <tr>
        <td bgcolor='green'>  </td>
        <td>DMO filter</td>
    </tr>

    <tr>
        <td bgcolor='red'>  </td>
        <td>Some other filter</td>
    </tr>

    <tr>
        <td bgcolor='black'>  </td>
        <td>Normal filter</td>
    </tr>
  </table>
  >;
  labelloc=t;
        ");
        }

        private void RenderDotFileFoot(TextWriter writer)
        {
            writer.Write("}");
        }

        private void RenderDotFileFilter(TextWriter writer, Filter filter)
        {
            String filterNodeName = GetNodeNameForFilter(filter);

            IList<Pin> inPins = filter.GetPins(DirectShowLib.PinDirection.Input);
            IList<Pin> outPins = filter.GetPins(DirectShowLib.PinDirection.Output);

            int numPinRows = inPins.Count;
            if (outPins.Count > numPinRows)
            {
                numPinRows = outPins.Count;
            }

            writer.WriteLine("/* Filter '{0}' */",
                filter.DisplayName);

            writer.WriteLine("  {0} [", filterNodeName);
            writer.WriteLine("    shape=plaintext,");
            writer.Write("    label=<");

            writer.WriteLine("<table border=\"2\" cellpadding=\"0\">");

            for (int pinRow = 0; pinRow < numPinRows; pinRow++)
            {
                writer.WriteLine("      <tr>");
                if (pinRow < inPins.Count)
                {
                    //Create a cell for one of the input pins
                    writer.WriteLine("        <td port=\"pin{0}\" align=\"left\" cellpadding=\"2\"><font point-size=\"8\" face=\"Courier\">{1}</font></td>", 
                        inPins[pinRow].Ordinal,
                        inPins[pinRow].Name);
                }
                else
                {
                    //No more input pins; render an empty cell
                    writer.WriteLine("        <td border=\"0\"></td>");
                }

                if (pinRow < outPins.Count)
                {
                    //Create a cell for one of the output pins
                    writer.WriteLine("        <td port=\"pin{0}\" align=\"right\" cellpadding=\"2\"><font point-size=\"8\" face=\"Courier\">{1}</font></td>",
                        outPins[pinRow].Ordinal,
                        outPins[pinRow].Name);
                }
                else
                {
                    //No more output pins; render an empty cell
                    writer.WriteLine("        <td border=\"0\"></td>");
                }
                writer.WriteLine("      </tr>");
            }

            writer.WriteLine("      <tr>");
            writer.WriteLine("        <td border=\"0\" colspan=\"2\"><font point-size=\"12\" face=\"Arial Bold\">{0}</font></td>",
                filter.DisplayName);
            writer.WriteLine("      </tr>");
            writer.WriteLine("    </table>");
            writer.WriteLine("  >]");
            writer.WriteLine();
        }

        private void RenderDotFileFilterEdges(TextWriter writer, Filter filter)
        {
            //Each pin that's connected to another pin makes up an edge.
            //Create an edge from the port on the source filter corresponding to the output pin
            //with the port on the destinaion filter corresponding to the input pin
            foreach (Pin pin in filter.Pins)
            {
                if (pin.GetDirection() != DirectShowLib.PinDirection.Output)
                {
                    continue;
                }

                Pin connectedToPin = pin.GetConnectedPin();
                if (connectedToPin == null)
                {
                    continue;
                }

                String edgeSource = String.Format("{0}:pin{1}",
                    GetNodeNameForFilter(filter),
                    pin.Ordinal);
                String edgeDestination = String.Format("{0}:pin{1}",
                    GetNodeNameForFilter(connectedToPin.Filter),
                    connectedToPin.Ordinal);

                writer.WriteLine("/* Edge between Filter '{0}' pin '{1} and filter '{2}' pin '{3}' */",
                    filter.DisplayName,
                    pin.Name,
                    connectedToPin.Filter.DisplayName,
                    connectedToPin.Name);

                writer.WriteLine("  {0} -> {1}", edgeSource, edgeDestination);
                writer.WriteLine();
            }
        }

        private string GetNodeNameForFilter(Filter filter)
        {
            String name = filter.DisplayName;

            Regex re = new Regex(@"[^\w]+");

            return re.Replace(name, "_");
        }
    }
}
