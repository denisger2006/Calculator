namespace Math_Calculator
{
    partial class HistoryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistoryForm));
            listView1 = new ListView();
            columnExpression = new ColumnHeader();
            columnResult = new ColumnHeader();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView1.Columns.AddRange(new ColumnHeader[] { columnExpression, columnResult });
            listView1.FullRowSelect = true;
            listView1.Location = new Point(12, 12);
            listView1.Name = "listView1";
            listView1.Size = new Size(762, 417);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.ColumnWidthChanging += listView1_ColumnWidthChanging;
            listView1.KeyDown += listView1_KeyDown;
            // 
            // columnExpression
            // 
            columnExpression.Text = "Выражение";
            columnExpression.Width = 381;
            // 
            // columnResult
            // 
            columnResult.Text = "Результат выражения";
            columnResult.TextAlign = HorizontalAlignment.Center;
            columnResult.Width = 381;
            // 
            // HistoryForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 453);
            Controls.Add(listView1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "HistoryForm";
            Text = "HistoryForm";
            ResumeLayout(false);
        }

        #endregion

        private ListView listView1;
        private ColumnHeader columnExpression;
        private ColumnHeader columnResult;
    }
}