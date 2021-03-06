﻿namespace Panoply.Gui
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.filtersTabPage = new System.Windows.Forms.TabPage();
            this.detailsGroupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.filterActionsGroupBox = new System.Windows.Forms.GroupBox();
            this.changeMeritButton = new System.Windows.Forms.Button();
            this.unregisterButton = new System.Windows.Forms.Button();
            this.showPropertyPageButton = new System.Windows.Forms.Button();
            this.filterDetailsListView = new System.Windows.Forms.ListView();
            this.colHeaderProperty = new System.Windows.Forms.ColumnHeader();
            this.colHeaderValue = new System.Windows.Forms.ColumnHeader();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.filterTreeGroupBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.filtersTreeView = new System.Windows.Forms.TreeView();
            this.mediaInfoTabPage = new System.Windows.Forms.TabPage();
            this.graphTabPage = new System.Windows.Forms.TabPage();
            this.summaryMediaInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.mediaInfoDetailsGroupBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mediaInfoBrowseButton = new System.Windows.Forms.Button();
            this.mediaInfoSummaryTextBox = new System.Windows.Forms.TextBox();
            this.mediaInfoDetailsTreeView = new System.Windows.Forms.TreeView();
            this.label4 = new System.Windows.Forms.Label();
            this.graphViewsTabControl = new System.Windows.Forms.TabControl();
            this.treeGraphTabPage = new System.Windows.Forms.TabPage();
            this.graphvizGraphTabPage = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.graphVizPathTextBox = new System.Windows.Forms.TextBox();
            this.graphVizBinaryPathBrowseButton = new System.Windows.Forms.Button();
            this.graphViewGroupBox = new System.Windows.Forms.GroupBox();
            this.graphvizGraphPictureBox = new System.Windows.Forms.PictureBox();
            this.graphvizGraphPanel = new System.Windows.Forms.Panel();
            this.tabControl.SuspendLayout();
            this.filtersTabPage.SuspendLayout();
            this.detailsGroupBox.SuspendLayout();
            this.filterActionsGroupBox.SuspendLayout();
            this.filterTreeGroupBox.SuspendLayout();
            this.mediaInfoTabPage.SuspendLayout();
            this.graphTabPage.SuspendLayout();
            this.summaryMediaInfoGroupBox.SuspendLayout();
            this.mediaInfoDetailsGroupBox.SuspendLayout();
            this.graphViewsTabControl.SuspendLayout();
            this.graphvizGraphTabPage.SuspendLayout();
            this.graphViewGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.graphvizGraphPictureBox)).BeginInit();
            this.graphvizGraphPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.filtersTabPage);
            this.tabControl.Controls.Add(this.mediaInfoTabPage);
            this.tabControl.Controls.Add(this.graphTabPage);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(773, 512);
            this.tabControl.TabIndex = 0;
            // 
            // filtersTabPage
            // 
            this.filtersTabPage.Controls.Add(this.detailsGroupBox);
            this.filtersTabPage.Controls.Add(this.splitter1);
            this.filtersTabPage.Controls.Add(this.filterTreeGroupBox);
            this.filtersTabPage.Location = new System.Drawing.Point(4, 22);
            this.filtersTabPage.Name = "filtersTabPage";
            this.filtersTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.filtersTabPage.Size = new System.Drawing.Size(765, 439);
            this.filtersTabPage.TabIndex = 0;
            this.filtersTabPage.Text = "DirectShow Filters";
            this.filtersTabPage.UseVisualStyleBackColor = true;
            // 
            // detailsGroupBox
            // 
            this.detailsGroupBox.Controls.Add(this.label2);
            this.detailsGroupBox.Controls.Add(this.filterActionsGroupBox);
            this.detailsGroupBox.Controls.Add(this.filterDetailsListView);
            this.detailsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailsGroupBox.Location = new System.Drawing.Point(3, 262);
            this.detailsGroupBox.Name = "detailsGroupBox";
            this.detailsGroupBox.Size = new System.Drawing.Size(759, 174);
            this.detailsGroupBox.TabIndex = 6;
            this.detailsGroupBox.TabStop = false;
            this.detailsGroupBox.Text = "Selected Filter Details:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(420, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(327, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "NB: Changes to the merit of a filter won\'t take effect until you log out";
            // 
            // filterActionsGroupBox
            // 
            this.filterActionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.filterActionsGroupBox.Controls.Add(this.changeMeritButton);
            this.filterActionsGroupBox.Controls.Add(this.unregisterButton);
            this.filterActionsGroupBox.Controls.Add(this.showPropertyPageButton);
            this.filterActionsGroupBox.Location = new System.Drawing.Point(654, 32);
            this.filterActionsGroupBox.Name = "filterActionsGroupBox";
            this.filterActionsGroupBox.Size = new System.Drawing.Size(99, 136);
            this.filterActionsGroupBox.TabIndex = 2;
            this.filterActionsGroupBox.TabStop = false;
            this.filterActionsGroupBox.Text = "Filter Actions";
            // 
            // changeMeritButton
            // 
            this.changeMeritButton.Location = new System.Drawing.Point(8, 78);
            this.changeMeritButton.Name = "changeMeritButton";
            this.changeMeritButton.Size = new System.Drawing.Size(85, 23);
            this.changeMeritButton.TabIndex = 4;
            this.changeMeritButton.Text = "Change Merit";
            this.changeMeritButton.UseVisualStyleBackColor = true;
            this.changeMeritButton.Click += new System.EventHandler(this.changeMeritButton_Click);
            // 
            // unregisterButton
            // 
            this.unregisterButton.Location = new System.Drawing.Point(8, 48);
            this.unregisterButton.Name = "unregisterButton";
            this.unregisterButton.Size = new System.Drawing.Size(85, 23);
            this.unregisterButton.TabIndex = 3;
            this.unregisterButton.Text = "Unregister";
            this.unregisterButton.UseVisualStyleBackColor = true;
            this.unregisterButton.Click += new System.EventHandler(this.unregisterButton_Click);
            // 
            // showPropertyPageButton
            // 
            this.showPropertyPageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.showPropertyPageButton.Location = new System.Drawing.Point(8, 19);
            this.showPropertyPageButton.Name = "showPropertyPageButton";
            this.showPropertyPageButton.Size = new System.Drawing.Size(85, 23);
            this.showPropertyPageButton.TabIndex = 1;
            this.showPropertyPageButton.Text = "Properties";
            this.showPropertyPageButton.UseVisualStyleBackColor = true;
            this.showPropertyPageButton.Click += new System.EventHandler(this.showPropertyPageButton_Click);
            // 
            // filterDetailsListView
            // 
            this.filterDetailsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.filterDetailsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colHeaderProperty,
            this.colHeaderValue});
            this.filterDetailsListView.Location = new System.Drawing.Point(9, 32);
            this.filterDetailsListView.Name = "filterDetailsListView";
            this.filterDetailsListView.Size = new System.Drawing.Size(639, 136);
            this.filterDetailsListView.TabIndex = 0;
            this.filterDetailsListView.UseCompatibleStateImageBehavior = false;
            this.filterDetailsListView.View = System.Windows.Forms.View.Details;
            // 
            // colHeaderProperty
            // 
            this.colHeaderProperty.Text = "Property";
            this.colHeaderProperty.Width = 80;
            // 
            // colHeaderValue
            // 
            this.colHeaderValue.Text = "Value";
            this.colHeaderValue.Width = 300;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(3, 259);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(759, 3);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // filterTreeGroupBox
            // 
            this.filterTreeGroupBox.Controls.Add(this.label1);
            this.filterTreeGroupBox.Controls.Add(this.filtersTreeView);
            this.filterTreeGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.filterTreeGroupBox.Location = new System.Drawing.Point(3, 3);
            this.filterTreeGroupBox.Name = "filterTreeGroupBox";
            this.filterTreeGroupBox.Size = new System.Drawing.Size(759, 256);
            this.filterTreeGroupBox.TabIndex = 4;
            this.filterTreeGroupBox.TabStop = false;
            this.filterTreeGroupBox.Text = "Filter Tree";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(747, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "The tree below lists the DirectX filters installed on your system.  You can use t" +
                "his tree to select a filter and view its properties, modify its merit, or invoke" +
                " its property pages.";
            // 
            // filtersTreeView
            // 
            this.filtersTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.filtersTreeView.Location = new System.Drawing.Point(9, 52);
            this.filtersTreeView.Name = "filtersTreeView";
            this.filtersTreeView.Size = new System.Drawing.Size(744, 198);
            this.filtersTreeView.TabIndex = 1;
            this.filtersTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.filtersTreeView_AfterSelect);
            // 
            // mediaInfoTabPage
            // 
            this.mediaInfoTabPage.Controls.Add(this.mediaInfoDetailsGroupBox);
            this.mediaInfoTabPage.Controls.Add(this.splitter2);
            this.mediaInfoTabPage.Controls.Add(this.summaryMediaInfoGroupBox);
            this.mediaInfoTabPage.Location = new System.Drawing.Point(4, 22);
            this.mediaInfoTabPage.Name = "mediaInfoTabPage";
            this.mediaInfoTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.mediaInfoTabPage.Size = new System.Drawing.Size(765, 486);
            this.mediaInfoTabPage.TabIndex = 1;
            this.mediaInfoTabPage.Text = "Media Info";
            this.mediaInfoTabPage.UseVisualStyleBackColor = true;
            // 
            // graphTabPage
            // 
            this.graphTabPage.Controls.Add(this.graphViewsTabControl);
            this.graphTabPage.Controls.Add(this.label4);
            this.graphTabPage.Location = new System.Drawing.Point(4, 22);
            this.graphTabPage.Name = "graphTabPage";
            this.graphTabPage.Size = new System.Drawing.Size(765, 486);
            this.graphTabPage.TabIndex = 2;
            this.graphTabPage.Text = "Graph";
            this.graphTabPage.UseVisualStyleBackColor = true;
            // 
            // summaryMediaInfoGroupBox
            // 
            this.summaryMediaInfoGroupBox.Controls.Add(this.mediaInfoSummaryTextBox);
            this.summaryMediaInfoGroupBox.Controls.Add(this.mediaInfoBrowseButton);
            this.summaryMediaInfoGroupBox.Controls.Add(this.label3);
            this.summaryMediaInfoGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.summaryMediaInfoGroupBox.Location = new System.Drawing.Point(3, 3);
            this.summaryMediaInfoGroupBox.Name = "summaryMediaInfoGroupBox";
            this.summaryMediaInfoGroupBox.Size = new System.Drawing.Size(759, 228);
            this.summaryMediaInfoGroupBox.TabIndex = 0;
            this.summaryMediaInfoGroupBox.TabStop = false;
            this.summaryMediaInfoGroupBox.Text = "Summary";
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter2.Location = new System.Drawing.Point(3, 231);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(759, 3);
            this.splitter2.TabIndex = 1;
            this.splitter2.TabStop = false;
            // 
            // mediaInfoDetailsGroupBox
            // 
            this.mediaInfoDetailsGroupBox.Controls.Add(this.mediaInfoDetailsTreeView);
            this.mediaInfoDetailsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediaInfoDetailsGroupBox.Location = new System.Drawing.Point(3, 234);
            this.mediaInfoDetailsGroupBox.Name = "mediaInfoDetailsGroupBox";
            this.mediaInfoDetailsGroupBox.Size = new System.Drawing.Size(759, 249);
            this.mediaInfoDetailsGroupBox.TabIndex = 2;
            this.mediaInfoDetailsGroupBox.TabStop = false;
            this.mediaInfoDetailsGroupBox.Text = "Details";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(458, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "To display the media info for a file, drop it on this window or browse for it usi" +
                "ng the button at right";
            // 
            // mediaInfoBrowseButton
            // 
            this.mediaInfoBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mediaInfoBrowseButton.Location = new System.Drawing.Point(681, 11);
            this.mediaInfoBrowseButton.Name = "mediaInfoBrowseButton";
            this.mediaInfoBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.mediaInfoBrowseButton.TabIndex = 1;
            this.mediaInfoBrowseButton.Text = "Browse";
            this.mediaInfoBrowseButton.UseVisualStyleBackColor = true;
            this.mediaInfoBrowseButton.Click += new System.EventHandler(this.mediaInfoBrowseButton_Click);
            // 
            // mediaInfoSummaryTextBox
            // 
            this.mediaInfoSummaryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mediaInfoSummaryTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mediaInfoSummaryTextBox.Location = new System.Drawing.Point(6, 40);
            this.mediaInfoSummaryTextBox.Multiline = true;
            this.mediaInfoSummaryTextBox.Name = "mediaInfoSummaryTextBox";
            this.mediaInfoSummaryTextBox.ReadOnly = true;
            this.mediaInfoSummaryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.mediaInfoSummaryTextBox.Size = new System.Drawing.Size(747, 182);
            this.mediaInfoSummaryTextBox.TabIndex = 2;
            this.mediaInfoSummaryTextBox.WordWrap = false;
            // 
            // mediaInfoDetailsTreeView
            // 
            this.mediaInfoDetailsTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediaInfoDetailsTreeView.Location = new System.Drawing.Point(3, 16);
            this.mediaInfoDetailsTreeView.Name = "mediaInfoDetailsTreeView";
            this.mediaInfoDetailsTreeView.Size = new System.Drawing.Size(753, 230);
            this.mediaInfoDetailsTreeView.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(437, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "To display the graph for a file, drop it on this window or browse for it using th" +
                "e button at right";
            // 
            // graphViewsTabControl
            // 
            this.graphViewsTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.graphViewsTabControl.Controls.Add(this.treeGraphTabPage);
            this.graphViewsTabControl.Controls.Add(this.graphvizGraphTabPage);
            this.graphViewsTabControl.Location = new System.Drawing.Point(3, 16);
            this.graphViewsTabControl.Name = "graphViewsTabControl";
            this.graphViewsTabControl.SelectedIndex = 0;
            this.graphViewsTabControl.Size = new System.Drawing.Size(759, 467);
            this.graphViewsTabControl.TabIndex = 1;
            // 
            // treeGraphTabPage
            // 
            this.treeGraphTabPage.Location = new System.Drawing.Point(4, 22);
            this.treeGraphTabPage.Name = "treeGraphTabPage";
            this.treeGraphTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.treeGraphTabPage.Size = new System.Drawing.Size(751, 441);
            this.treeGraphTabPage.TabIndex = 0;
            this.treeGraphTabPage.Text = "Tree View";
            this.treeGraphTabPage.UseVisualStyleBackColor = true;
            // 
            // graphvizGraphTabPage
            // 
            this.graphvizGraphTabPage.Controls.Add(this.graphViewGroupBox);
            this.graphvizGraphTabPage.Controls.Add(this.graphVizBinaryPathBrowseButton);
            this.graphvizGraphTabPage.Controls.Add(this.graphVizPathTextBox);
            this.graphvizGraphTabPage.Controls.Add(this.label6);
            this.graphvizGraphTabPage.Controls.Add(this.label5);
            this.graphvizGraphTabPage.Location = new System.Drawing.Point(4, 22);
            this.graphvizGraphTabPage.Name = "graphvizGraphTabPage";
            this.graphvizGraphTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.graphvizGraphTabPage.Size = new System.Drawing.Size(751, 441);
            this.graphvizGraphTabPage.TabIndex = 1;
            this.graphvizGraphTabPage.Text = "Graphical View";
            this.graphvizGraphTabPage.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(390, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "To render the filter graph visually you must have GraphViz 2.22 or higher install" +
                "ed.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(146, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "GraphViz dot.exe binary path:";
            // 
            // graphVizPathTextBox
            // 
            this.graphVizPathTextBox.Location = new System.Drawing.Point(161, 34);
            this.graphVizPathTextBox.Name = "graphVizPathTextBox";
            this.graphVizPathTextBox.Size = new System.Drawing.Size(315, 20);
            this.graphVizPathTextBox.TabIndex = 2;
            // 
            // graphVizBinaryPathBrowseButton
            // 
            this.graphVizBinaryPathBrowseButton.Location = new System.Drawing.Point(482, 34);
            this.graphVizBinaryPathBrowseButton.Name = "graphVizBinaryPathBrowseButton";
            this.graphVizBinaryPathBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.graphVizBinaryPathBrowseButton.TabIndex = 3;
            this.graphVizBinaryPathBrowseButton.Text = "Browse";
            this.graphVizBinaryPathBrowseButton.UseVisualStyleBackColor = true;
            // 
            // graphViewGroupBox
            // 
            this.graphViewGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.graphViewGroupBox.Controls.Add(this.graphvizGraphPanel);
            this.graphViewGroupBox.Location = new System.Drawing.Point(12, 60);
            this.graphViewGroupBox.Name = "graphViewGroupBox";
            this.graphViewGroupBox.Size = new System.Drawing.Size(733, 375);
            this.graphViewGroupBox.TabIndex = 4;
            this.graphViewGroupBox.TabStop = false;
            this.graphViewGroupBox.Text = "View";
            // 
            // graphvizGraphPictureBox
            // 
            this.graphvizGraphPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.graphvizGraphPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("graphvizGraphPictureBox.Image")));
            this.graphvizGraphPictureBox.InitialImage = null;
            this.graphvizGraphPictureBox.Location = new System.Drawing.Point(3, 3);
            this.graphvizGraphPictureBox.Name = "graphvizGraphPictureBox";
            this.graphvizGraphPictureBox.Size = new System.Drawing.Size(1469, 568);
            this.graphvizGraphPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.graphvizGraphPictureBox.TabIndex = 0;
            this.graphvizGraphPictureBox.TabStop = false;
            // 
            // graphvizGraphPanel
            // 
            this.graphvizGraphPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.graphvizGraphPanel.AutoScroll = true;
            this.graphvizGraphPanel.Controls.Add(this.graphvizGraphPictureBox);
            this.graphvizGraphPanel.Location = new System.Drawing.Point(6, 19);
            this.graphvizGraphPanel.Name = "graphvizGraphPanel";
            this.graphvizGraphPanel.Size = new System.Drawing.Size(721, 350);
            this.graphvizGraphPanel.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 512);
            this.Controls.Add(this.tabControl);
            this.Name = "MainForm";
            this.Text = "Panoply";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.tabControl.ResumeLayout(false);
            this.filtersTabPage.ResumeLayout(false);
            this.detailsGroupBox.ResumeLayout(false);
            this.detailsGroupBox.PerformLayout();
            this.filterActionsGroupBox.ResumeLayout(false);
            this.filterTreeGroupBox.ResumeLayout(false);
            this.mediaInfoTabPage.ResumeLayout(false);
            this.graphTabPage.ResumeLayout(false);
            this.graphTabPage.PerformLayout();
            this.summaryMediaInfoGroupBox.ResumeLayout(false);
            this.summaryMediaInfoGroupBox.PerformLayout();
            this.mediaInfoDetailsGroupBox.ResumeLayout(false);
            this.graphViewsTabControl.ResumeLayout(false);
            this.graphvizGraphTabPage.ResumeLayout(false);
            this.graphvizGraphTabPage.PerformLayout();
            this.graphViewGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.graphvizGraphPictureBox)).EndInit();
            this.graphvizGraphPanel.ResumeLayout(false);
            this.graphvizGraphPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage filtersTabPage;
        private System.Windows.Forms.TabPage mediaInfoTabPage;
        private System.Windows.Forms.TabPage graphTabPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView filtersTreeView;
        private System.Windows.Forms.GroupBox filterTreeGroupBox;
        private System.Windows.Forms.GroupBox detailsGroupBox;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ListView filterDetailsListView;
        private System.Windows.Forms.ColumnHeader colHeaderProperty;
        private System.Windows.Forms.ColumnHeader colHeaderValue;
        private System.Windows.Forms.Button showPropertyPageButton;
        private System.Windows.Forms.GroupBox filterActionsGroupBox;
        private System.Windows.Forms.Button changeMeritButton;
        private System.Windows.Forms.Button unregisterButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox mediaInfoDetailsGroupBox;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.GroupBox summaryMediaInfoGroupBox;
        private System.Windows.Forms.Button mediaInfoBrowseButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox mediaInfoSummaryTextBox;
        private System.Windows.Forms.TreeView mediaInfoDetailsTreeView;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl graphViewsTabControl;
        private System.Windows.Forms.TabPage treeGraphTabPage;
        private System.Windows.Forms.TabPage graphvizGraphTabPage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox graphViewGroupBox;
        private System.Windows.Forms.Button graphVizBinaryPathBrowseButton;
        private System.Windows.Forms.TextBox graphVizPathTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox graphvizGraphPictureBox;
        private System.Windows.Forms.Panel graphvizGraphPanel;
    }
}