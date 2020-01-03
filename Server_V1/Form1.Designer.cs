namespace Server_V1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("监控点1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("监控点2");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("监控点3");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("监控点4");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("区域1", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("区域2");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("监控系统", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6});
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnSystem = new System.Windows.Forms.Button();
            this.btnStatistics = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnManagement = new System.Windows.Forms.Button();
            this.btnIndex = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.treeViewMain = new System.Windows.Forms.TreeView();
            this.listBoxMessage = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnSystem);
            this.splitContainer1.Panel1.Controls.Add(this.btnStatistics);
            this.splitContainer1.Panel1.Controls.Add(this.btnSearch);
            this.splitContainer1.Panel1.Controls.Add(this.btnManagement);
            this.splitContainer1.Panel1.Controls.Add(this.btnIndex);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1902, 1033);
            this.splitContainer1.SplitterDistance = 55;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnSystem
            // 
            this.btnSystem.Location = new System.Drawing.Point(429, 10);
            this.btnSystem.Margin = new System.Windows.Forms.Padding(1);
            this.btnSystem.Name = "btnSystem";
            this.btnSystem.Size = new System.Drawing.Size(100, 40);
            this.btnSystem.TabIndex = 0;
            this.btnSystem.Text = "系统";
            this.btnSystem.UseVisualStyleBackColor = true;
            this.btnSystem.Click += new System.EventHandler(this.BtnSystem_Click);
            // 
            // btnStatistics
            // 
            this.btnStatistics.Location = new System.Drawing.Point(322, 10);
            this.btnStatistics.Margin = new System.Windows.Forms.Padding(1);
            this.btnStatistics.Name = "btnStatistics";
            this.btnStatistics.Size = new System.Drawing.Size(100, 40);
            this.btnStatistics.TabIndex = 0;
            this.btnStatistics.Text = "统计";
            this.btnStatistics.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(215, 10);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 40);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnManagement
            // 
            this.btnManagement.Location = new System.Drawing.Point(108, 10);
            this.btnManagement.Margin = new System.Windows.Forms.Padding(1);
            this.btnManagement.Name = "btnManagement";
            this.btnManagement.Size = new System.Drawing.Size(100, 40);
            this.btnManagement.TabIndex = 0;
            this.btnManagement.Text = "管理";
            this.btnManagement.UseVisualStyleBackColor = true;
            // 
            // btnIndex
            // 
            this.btnIndex.Location = new System.Drawing.Point(1, 10);
            this.btnIndex.Margin = new System.Windows.Forms.Padding(1);
            this.btnIndex.Name = "btnIndex";
            this.btnIndex.Size = new System.Drawing.Size(100, 40);
            this.btnIndex.TabIndex = 0;
            this.btnIndex.Text = "首页";
            this.btnIndex.UseVisualStyleBackColor = true;
            this.btnIndex.Click += new System.EventHandler(this.BtnIndex_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.ForeColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Size = new System.Drawing.Size(1902, 973);
            this.splitContainer2.SplitterDistance = 286;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Location = new System.Drawing.Point(-2, -3);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.treeViewMain);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.listBoxMessage);
            this.splitContainer3.Size = new System.Drawing.Size(281, 970);
            this.splitContainer3.SplitterDistance = 482;
            this.splitContainer3.TabIndex = 1;
            // 
            // treeViewMain
            // 
            this.treeViewMain.BackColor = System.Drawing.Color.White;
            this.treeViewMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewMain.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.treeViewMain.LabelEdit = true;
            this.treeViewMain.Location = new System.Drawing.Point(0, 0);
            this.treeViewMain.Margin = new System.Windows.Forms.Padding(4);
            this.treeViewMain.Name = "treeViewMain";
            treeNode1.Name = "Node0-1-1";
            treeNode1.Text = "监控点1";
            treeNode2.Name = "Node0-1-2";
            treeNode2.Text = "监控点2";
            treeNode3.Name = "Node0-1-3";
            treeNode3.Text = "监控点3";
            treeNode4.Name = "Node0-1-4";
            treeNode4.Text = "监控点4";
            treeNode5.Name = "Node0-1";
            treeNode5.NodeFont = new System.Drawing.Font("宋体", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            treeNode5.Text = "区域1";
            treeNode6.Name = "Node0-2";
            treeNode6.NodeFont = new System.Drawing.Font("宋体", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            treeNode6.Text = "区域2";
            treeNode7.Name = "Node0";
            treeNode7.Text = "监控系统";
            this.treeViewMain.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode7});
            this.treeViewMain.Size = new System.Drawing.Size(281, 482);
            this.treeViewMain.TabIndex = 0;
            this.treeViewMain.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeViewMain_NodeMouseDoubleClick);
            // 
            // listBoxMessage
            // 
            this.listBoxMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxMessage.FormattingEnabled = true;
            this.listBoxMessage.ItemHeight = 15;
            this.listBoxMessage.Location = new System.Drawing.Point(3, 1);
            this.listBoxMessage.Name = "listBoxMessage";
            this.listBoxMessage.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxMessage.Size = new System.Drawing.Size(259, 484);
            this.listBoxMessage.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1902, 1033);
            this.Controls.Add(this.splitContainer1);
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnIndex;
        private System.Windows.Forms.Button btnSystem;
        private System.Windows.Forms.Button btnStatistics;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnManagement;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeViewMain;
        private System.Windows.Forms.SplitContainer splitContainer3;
        public System.Windows.Forms.ListBox listBoxMessage;
    }
}

