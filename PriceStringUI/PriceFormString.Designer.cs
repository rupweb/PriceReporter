namespace PriceUI
{
    partial class PriceFormString
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
            this.instrument = new System.Windows.Forms.Label();
            this.price = new System.Windows.Forms.Label();
            this.direction = new System.Windows.Forms.Label();
            this.averageLabel = new System.Windows.Forms.Label();
            this.average = new System.Windows.Forms.Label();
            this.historyLabel = new System.Windows.Forms.Label();
            this.pricePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.averagePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.historyPanel = new System.Windows.Forms.Panel();
            this.history10 = new System.Windows.Forms.Label();
            this.history9 = new System.Windows.Forms.Label();
            this.history8 = new System.Windows.Forms.Label();
            this.history7 = new System.Windows.Forms.Label();
            this.history6 = new System.Windows.Forms.Label();
            this.history5 = new System.Windows.Forms.Label();
            this.history4 = new System.Windows.Forms.Label();
            this.history3 = new System.Windows.Forms.Label();
            this.history2 = new System.Windows.Forms.Label();
            this.history1 = new System.Windows.Forms.Label();
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
            this.historyLabel.Location = new System.Drawing.Point(0, 0);
            this.historyLabel.Name = "historyLabel";
            this.historyLabel.Size = new System.Drawing.Size(94, 40);
            this.historyLabel.TabIndex = 6;
            this.historyLabel.Text = "History";
            this.historyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.historyPanel.Controls.Add(this.history10);
            this.historyPanel.Controls.Add(this.history9);
            this.historyPanel.Controls.Add(this.history8);
            this.historyPanel.Controls.Add(this.history7);
            this.historyPanel.Controls.Add(this.history6);
            this.historyPanel.Controls.Add(this.history5);
            this.historyPanel.Controls.Add(this.history4);
            this.historyPanel.Controls.Add(this.history3);
            this.historyPanel.Controls.Add(this.history2);
            this.historyPanel.Controls.Add(this.history1);
            this.historyPanel.Controls.Add(this.historyLabel);
            this.historyPanel.Location = new System.Drawing.Point(12, 204);
            this.historyPanel.Name = "historyPanel";
            this.historyPanel.Size = new System.Drawing.Size(295, 210);
            this.historyPanel.TabIndex = 12;
            // 
            // history10
            // 
            this.history10.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.history10.Location = new System.Drawing.Point(89, 180);
            this.history10.Name = "history10";
            this.history10.Size = new System.Drawing.Size(102, 20);
            this.history10.TabIndex = 16;
            this.history10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // history9
            // 
            this.history9.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.history9.Location = new System.Drawing.Point(89, 160);
            this.history9.Name = "history9";
            this.history9.Size = new System.Drawing.Size(102, 20);
            this.history9.TabIndex = 15;
            this.history9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // history8
            // 
            this.history8.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.history8.Location = new System.Drawing.Point(89, 140);
            this.history8.Name = "history8";
            this.history8.Size = new System.Drawing.Size(102, 20);
            this.history8.TabIndex = 14;
            this.history8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // history7
            // 
            this.history7.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.history7.Location = new System.Drawing.Point(89, 120);
            this.history7.Name = "history7";
            this.history7.Size = new System.Drawing.Size(102, 20);
            this.history7.TabIndex = 13;
            this.history7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // history6
            // 
            this.history6.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.history6.Location = new System.Drawing.Point(89, 100);
            this.history6.Name = "history6";
            this.history6.Size = new System.Drawing.Size(102, 20);
            this.history6.TabIndex = 12;
            this.history6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // history5
            // 
            this.history5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.history5.Location = new System.Drawing.Point(89, 80);
            this.history5.Name = "history5";
            this.history5.Size = new System.Drawing.Size(102, 20);
            this.history5.TabIndex = 11;
            this.history5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // history4
            // 
            this.history4.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.history4.Location = new System.Drawing.Point(89, 60);
            this.history4.Name = "history4";
            this.history4.Size = new System.Drawing.Size(102, 20);
            this.history4.TabIndex = 10;
            this.history4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // history3
            // 
            this.history3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.history3.Location = new System.Drawing.Point(89, 40);
            this.history3.Name = "history3";
            this.history3.Size = new System.Drawing.Size(102, 20);
            this.history3.TabIndex = 9;
            this.history3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // history2
            // 
            this.history2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.history2.Location = new System.Drawing.Point(89, 20);
            this.history2.Name = "history2";
            this.history2.Size = new System.Drawing.Size(102, 20);
            this.history2.TabIndex = 8;
            this.history2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // history1
            // 
            this.history1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.history1.Location = new System.Drawing.Point(89, 0);
            this.history1.Name = "history1";
            this.history1.Size = new System.Drawing.Size(102, 20);
            this.history1.TabIndex = 7;
            this.history1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PriceFormString
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
            this.Name = "PriceFormString";
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
        public System.Windows.Forms.FlowLayoutPanel pricePanel;
        public System.Windows.Forms.FlowLayoutPanel averagePanel;
        public System.Windows.Forms.Panel historyPanel;
        public System.Windows.Forms.Label history10;
        public System.Windows.Forms.Label history9;
        public System.Windows.Forms.Label history8;
        public System.Windows.Forms.Label history7;
        public System.Windows.Forms.Label history6;
        public System.Windows.Forms.Label history5;
        public System.Windows.Forms.Label history4;
        public System.Windows.Forms.Label history3;
        public System.Windows.Forms.Label history2;
        public System.Windows.Forms.Label history1;
    }
}

