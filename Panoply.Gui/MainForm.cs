using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Panoply.Library.Presentation;
using MediaInfo = Panoply.Library.MediaInfo;

namespace Panoply.Gui
{
    public partial class MainForm : Form
    {
        String _filePath; //The path of the file to attempt to access

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshFilters();
            ClearFilterDetails();
        }

        private void filtersTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ClearFilterDetails();

            TreeNode selectedNode = e.Node;
            if (selectedNode.Tag is FilterCategoryTreeNode)
            {
                PopulateFilterDetails((FilterCategoryTreeNode)selectedNode.Tag);
            }
            else if (selectedNode.Tag is FilterTreeNode)
            {
                PopulateFilterDetails((FilterTreeNode)selectedNode.Tag);
            }
        }

        private void ClearFilterDetails()
        {
            filterDetailsListView.Items.Clear();

            showPropertyPageButton.Enabled =
                unregisterButton.Enabled =
                changeMeritButton.Enabled = false;
        }

        private void PopulateFilterDetails(FilterDeviceTreeNode device)
        {
            AddFilterProperty("Name", device.FriendlyName, device.FriendlyNameException);
            AddFilterProperty("Device Path", device.DevicePath, device.DevicePathException);
            AddFilterProperty("CLSID", device.Clsid, device.ClsidException);
            AddFilterProperty("Merit", device.Merit, device.MeritException);
        }

        private void PopulateFilterDetails(FilterCategoryTreeNode category)
        {
            PopulateFilterDetails((FilterDeviceTreeNode)category);
        }

        private void PopulateFilterDetails(FilterTreeNode filter)
        {
            PopulateFilterDetails((FilterDeviceTreeNode)filter);
            AddFilterProperty("File Path", filter.FilePath, filter.FilePathException);
            AddFilterProperty("Version", filter.Version, filter.VersionException);
            AddFilterProperty("File Version", filter.FileVersion, filter.FileVersionException);

            //The buttons are only valid for filters, not filter categories
            showPropertyPageButton.Enabled =
                unregisterButton.Enabled =
                changeMeritButton.Enabled = true;
        }

        private void AddFilterProperty(string name, object value, Exception exception)
        {
            //If this property has a value, show that, otherwise show the exception
            ListViewItem item = new ListViewItem();

            item.Text = name;

            ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem();
            if (exception != null)
            {
                subItem.Text = String.Format("ERROR: {0}", exception.Message);
            }
            else if (value != null)
            {
                subItem.Text = value.ToString();
            }
            else
            {
                subItem.Text = "[No value]";
            }

            item.SubItems.Add(subItem);

            filterDetailsListView.Items.Add(item);
        }

        private void showPropertyPageButton_Click(object sender, EventArgs e)
        {
            TreeNode node = filtersTreeView.SelectedNode;
            if (node == null) { return; }

            FilterTreeNode filter = node.Tag as FilterTreeNode;
            if (filter == null) { return; }

            try
            {
                filter.ShowPropertyPage(this.Handle);
            }
            catch (NotSupportedException)
            {
                MessageBox.Show(this,
                    "This filter does not have any property pages",
                    "Not Supported");
            }
            catch (Exception err)
            {
                MessageBox.Show(this,
                    string.Format("Error showing property page: {0}", err.Message),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void unregisterButton_Click(object sender, EventArgs e)
        {
            TreeNode node = filtersTreeView.SelectedNode;
            if (node == null) { return; }

            FilterTreeNode filter = node.Tag as FilterTreeNode;
            if (filter == null) { return; }

            if (MessageBox.Show(this,
                "Are you sure you want to unregister this filter?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    filter.Unregister();
                    RefreshFilters();
                }
                catch (Exception err)
                {
                    if (MessageBox.Show(this,
                        String.Format("Error unregistering DLL '{0}': {1}\r\n\r\nDo you want to manually remove this filter from the registry?",
                            filter.FilePath,
                            err.Message),
                        "Confirm Manual Removal",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Error) == DialogResult.Yes) {
                        try
                        {
                            filter.RemoveFromRegistry();
                            RefreshFilters();
                        }
                        catch (Exception err2)
                        {
                            MessageBox.Show(this,
                                string.Format("Error manually unregistering DLL: {0}", err2.Message),
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void changeMeritButton_Click(object sender, EventArgs e)
        {
            TreeNode node = filtersTreeView.SelectedNode;
            if (node == null) { return; }

            FilterTreeNode filter = node.Tag as FilterTreeNode;
            if (filter == null) { return; }

            using (SetMeritForm form = new SetMeritForm(filter))
            {
                form.ShowDialog(this);
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(String.Format("AllowEffect: {0}", e.AllowedEffect));

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                String[] filePaths = (String[])(e.Data.GetData(DataFormats.FileDrop));
                if (filePaths.Length == 1)
                {
                    if (tabControl.SelectedTab == filtersTabPage)
                    {
                        //Switch to the media info tab page
                        tabControl.SelectedTab = mediaInfoTabPage;
                    }
                    OpenFile(filePaths[0]);
                }
            }
        }

        private void mediaInfoBrowseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.CheckFileExists = true;
                dlg.CheckPathExists = true;
                dlg.Filter = "All Files (*.*)|*.*";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    OpenFile(dlg.FileName);
                }
            }
        }

        private void OpenFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show(this,
                    String.Format("Path '{0}' does not exist", filePath),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            _filePath = filePath;
            RefreshActiveTab();
        }

        private void RefreshActiveTab()
        {
            if (tabControl.SelectedTab == mediaInfoTabPage)
            {
                RefreshMediaInfo();
            }
            else if (tabControl.SelectedTab == graphTabPage)
            {
                RefreshGraph();
            }
            else if (tabControl.SelectedTab == filtersTabPage)
            {
                RefreshFilters();
            }
        }

        private void RefreshMediaInfo()
        {
            mediaInfoSummaryTextBox.Text = String.Empty;
            mediaInfoDetailsTreeView.Nodes.Clear();

            if (String.IsNullOrEmpty(_filePath))
            {
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                using (MediaInfo.MediaInfo mi = MediaInfo.MediaInfo.Open(_filePath))
                {
                    mediaInfoSummaryTextBox.Text = mi.Inform();

                    PopulateMediaInfoTree(mi);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(this,
                    e.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void RefreshGraph()
        {
            throw new NotImplementedException();
        }

        private void RefreshFilters()
        {
            filtersTreeView.Nodes.Clear();

            try
            {
                List<FilterCategoryTreeNode> categories = FilterCategoryTreeNode.EnumerateFilterCategories();
                foreach (FilterCategoryTreeNode category in categories)
                {
                    PopulateFilterTreeWithCategory(category);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(this,
                    String.Format("Error enumerating DirectShow filter categories: {0}", e.Message),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void PopulateFilterTreeWithCategory(FilterCategoryTreeNode category)
        {
            TreeNode node = new TreeNode();
            node.Text = category.FriendlyName;
            if (node.Text == null)
            {
                node.Text = String.Format("!! {0}", category.FriendlyNameException);
            }
            node.Tag = category;
            filtersTreeView.Nodes.Add(node);

            if (category.Filters == null)
            {
                //There's an error getting the filters for this category.  Add a node for the error
                TreeNode errorNode = new TreeNode();
                errorNode.Text = String.Format("Error getting filters: {0}", category.FiltersException);
                node.Nodes.Add(errorNode);
            }
            else
            {
                foreach (FilterTreeNode filter in category.Filters)
                {
                    PopulateFiltersTreeWithFilter(node, filter);
                }
            }
        }

        private void PopulateFiltersTreeWithFilter(TreeNode parentNode, FilterTreeNode filter)
        {
            TreeNode node = new TreeNode();
            node.Text = filter.FriendlyName;
            if (node.Text == null)
            {
                node.Text = String.Format("!! {0}", filter.FriendlyNameException);
            }
            node.Tag = filter;

            parentNode.Nodes.Add(node);
        }

        private void PopulateMediaInfoTree(MediaInfo.MediaInfo mi)
        {
            foreach (MediaInfo.StreamType type in Enum.GetValues(typeof(MediaInfo.StreamType)))
            {
                IList<MediaInfo.Stream> streams = mi.GetStreams(type);

                if (streams.Count == 0)
                {
                    //Skip this type
                    continue;
                }

                PopulateMediaInfoTreeWithStreamType(type, streams);
            }
        }

        private void PopulateMediaInfoTreeWithStreamType(Panoply.Library.MediaInfo.StreamType type, IList<Panoply.Library.MediaInfo.Stream> streams)
        {
            TreeNode typeNode = new TreeNode();
            typeNode.Text = String.Format("Stream Type {0}", type);
            mediaInfoDetailsTreeView.Nodes.Add(typeNode);

            foreach (MediaInfo.Stream stream in streams)
            {
                PopulateMediaInfoTreeWithStream(typeNode, stream);
            }
        }

        private void PopulateMediaInfoTreeWithStream(TreeNode typeNode, Panoply.Library.MediaInfo.Stream stream)
        {
            TreeNode streamNode = new TreeNode();
            streamNode.Text = String.Format("Stream {0}", stream.Number);

            typeNode.Nodes.Add(streamNode);

            foreach (MediaInfo.Parameter parameter in stream.Parameters)
            {
                PopulateMediaInfoTreeWithParameter(streamNode, parameter);
            }
        }

        private void PopulateMediaInfoTreeWithParameter(TreeNode streamNode, Panoply.Library.MediaInfo.Parameter parameter)
        {
            TreeNode paramNode = new TreeNode();
            paramNode.Text = parameter.Name;
            streamNode.Nodes.Add(paramNode);

            TreeNode valueNode = new TreeNode();
            valueNode.Text = string.Format("Value: {0}", parameter.Value);
            paramNode.Nodes.Add(valueNode);

            if (!String.IsNullOrEmpty(parameter.Units))
            {
                TreeNode unitsNode = new TreeNode();
                unitsNode.Text = string.Format("Units: {0}", parameter.Units);
                paramNode.Nodes.Add(unitsNode);
            }

            if (!String.IsNullOrEmpty(parameter.LocalizedName))
            {
                TreeNode lnameNode = new TreeNode();
                lnameNode.Text = string.Format("Localized Name: {0}", parameter.LocalizedName);
                paramNode.Nodes.Add(lnameNode);
            }

            if (!String.IsNullOrEmpty(parameter.LocalizedUnits))
            {
                TreeNode lunitsNode = new TreeNode();
                lunitsNode.Text = string.Format("Localized Units: {0}", parameter.LocalizedUnits);
                paramNode.Nodes.Add(lunitsNode);
            }
        }
    }
}
