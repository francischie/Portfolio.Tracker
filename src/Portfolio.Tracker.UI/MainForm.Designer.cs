namespace Portfolio.Tracker.UI
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
            this.label1 = new System.Windows.Forms.Label();
            this.tradeGridView = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.loadButton = new System.Windows.Forms.Button();
            this.profitLossGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.tradeGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.profitLossGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Trades in Portfolio";
            // 
            // tradeGridView
            // 
            this.tradeGridView.AllowUserToAddRows = false;
            this.tradeGridView.AllowUserToDeleteRows = false;
            this.tradeGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tradeGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tradeGridView.Location = new System.Drawing.Point(10, 44);
            this.tradeGridView.Name = "tradeGridView";
            this.tradeGridView.ReadOnly = true;
            this.tradeGridView.Size = new System.Drawing.Size(1170, 168);
            this.tradeGridView.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 238);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "Profit and Loss Report";
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(540, 703);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(123, 34);
            this.loadButton.TabIndex = 2;
            this.loadButton.Text = "Refresh";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // profitLossGridView
            // 
            this.profitLossGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.profitLossGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.profitLossGridView.Location = new System.Drawing.Point(10, 255);
            this.profitLossGridView.Name = "profitLossGridView";
            this.profitLossGridView.Size = new System.Drawing.Size(1170, 429);
            this.profitLossGridView.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 749);
            this.Controls.Add(this.profitLossGridView);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.tradeGridView);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Portfolio Tech Stocks";
            ((System.ComponentModel.ISupportInitialize)(this.tradeGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.profitLossGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.DataGridView tradeGridView;
        private System.Windows.Forms.DataGridView profitLossGridView;
    }
}