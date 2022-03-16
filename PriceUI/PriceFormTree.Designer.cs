namespace PriceUI
{
    partial class PriceFormTree
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
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("");
            this.instrument = new System.Windows.Forms.Label();
            this.price = new System.Windows.Forms.Label();
            this.direction = new System.Windows.Forms.Label();
            this.averageLabel = new System.Windows.Forms.Label();
            this.average = new System.Windows.Forms.Label();
            this.historyLabel = new System.Windows.Forms.Label();
            this.historyTree = new BufferedTreeView();
            this.pricePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.averagePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.historyPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.pricePanel.SuspendLayout();
            this.averagePanel.SuspendLayout();
            this.historyPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // instrument
            // 
            this.instrument.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.instrument.Location = new System.Drawing.Point(3, 0);
            this.instrument.Name = "instrument";
            this.instrument.Size = new System.Drawing.Size(80, 40);
            this.instrument.TabIndex = 0;
            this.instrument.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // price
            // 
            this.price.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.price.Location = new System.Drawing.Point(89, 0);
            this.price.Name = "price";
            this.price.Size = new System.Drawing.Size(102, 40);
            this.price.TabIndex = 1;
            this.price.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // direction
            // 
            this.direction.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.direction.Location = new System.Drawing.Point(197, 0);
            this.direction.Name = "direction";
            this.direction.Size = new System.Drawing.Size(80, 40);
            this.direction.TabIndex = 2;
            this.direction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // averageLabel
            // 
            this.averageLabel.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.averageLabel.Location = new System.Drawing.Point(3, 0);
            this.averageLabel.Name = "averageLabel";
            this.averageLabel.Size = new System.Drawing.Size(94, 40);
            this.averageLabel.TabIndex = 4;
            this.averageLabel.Text = "Average";
            this.averageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // average
            // 
            this.average.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.average.Location = new System.Drawing.Point(103, 0);
            this.average.Name = "average";
            this.average.Size = new System.Drawing.Size(174, 40);
            this.average.TabIndex = 5;
            this.average.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // historyLabel
            // 
            this.historyLabel.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.historyLabel.Location = new System.Drawing.Point(3, 0);
            this.historyLabel.Name = "historyLabel";
            this.historyLabel.Size = new System.Drawing.Size(94, 40);
            this.historyLabel.TabIndex = 6;
            this.historyLabel.Text = "History";
            this.historyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // historyTree
            // 
            this.historyTree.Location = new System.Drawing.Point(103, 3);
            this.historyTree.Name = "historyTree";
            treeNode3.Name = "Node0";
            treeNode3.Text = "";
            this.historyTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.historyTree.Size = new System.Drawing.Size(121, 197);
            this.historyTree.TabIndex = 7;
            // 
            // pricePanel
            // 
            this.pricePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pricePanel.Controls.Add(this.instrument);
            this.pricePanel.Controls.Add(this.price);
            this.pricePanel.Controls.Add(this.direction);
            this.pricePanel.Location = new System.Drawing.Point(12, 12);
            this.pricePanel.Name = "pricePanel";
            this.pricePanel.Size = new System.Drawing.Size(295, 79);
            this.pricePanel.TabIndex = 10;
            // 
            // averagePanel
            // 
            this.averagePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.averagePanel.Controls.Add(this.averageLabel);
            this.averagePanel.Controls.Add(this.average);
            this.averagePanel.Location = new System.Drawing.Point(12, 98);
            this.averagePanel.Name = "averagePanel";
            this.averagePanel.Size = new System.Drawing.Size(295, 100);
            this.averagePanel.TabIndex = 11;
            // 
            // historyPanel
            // 
            this.historyPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.historyPanel.Controls.Add(this.historyLabel);
            this.historyPanel.Controls.Add(this.historyTree);
            this.historyPanel.Location = new System.Drawing.Point(12, 205);
            this.historyPanel.Name = "historyPanel";
            this.historyPanel.Size = new System.Drawing.Size(295, 210);
            this.historyPanel.TabIndex = 12;
            // 
            // PriceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 426);
            this.Controls.Add(this.historyPanel);
            this.Controls.Add(this.averagePanel);
            this.Controls.Add(this.pricePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PriceForm";
            this.Text = "Price UI";
            this.pricePanel.ResumeLayout(false);
            this.averagePanel.ResumeLayout(false);
            this.historyPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label instrument;
        public System.Windows.Forms.Label price;
        public System.Windows.Forms.Label direction;
        public System.Windows.Forms.Label averageLabel;
        public System.Windows.Forms.Label average;
        public System.Windows.Forms.Label historyLabel;
        public BufferedTreeView historyTree;
        private System.Windows.Forms.FlowLayoutPanel pricePanel;
        private System.Windows.Forms.FlowLayoutPanel averagePanel;
        public System.Windows.Forms.FlowLayoutPanel historyPanel;
    }
}

