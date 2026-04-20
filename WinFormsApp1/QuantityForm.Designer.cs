namespace WinFormsApp1
{
    partial class QuantityForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblOperation;
        private Label lblProductName;
        private TextBox txtQuantity;
        private Button btnOk;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblOperation = new Label();
            lblProductName = new Label();
            txtQuantity = new TextBox();
            btnOk = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // lblOperation
            // 
            lblOperation.AutoSize = true;
            lblOperation.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            lblOperation.ForeColor = SystemColors.ActiveCaptionText;
            lblOperation.Location = new Point(30, 25);
            lblOperation.Name = "lblOperation";
            lblOperation.Size = new Size(0, 20);
            lblOperation.TabIndex = 0;
            // 
            // lblProductName
            // 
            lblProductName.AutoSize = true;
            lblProductName.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblProductName.ForeColor = SystemColors.ActiveCaptionText;
            lblProductName.Location = new Point(30, 55);
            lblProductName.Name = "lblProductName";
            lblProductName.Size = new Size(0, 21);
            lblProductName.TabIndex = 1;
            // 
            // txtQuantity
            // 
            txtQuantity.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            txtQuantity.Location = new Point(44, 95);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(225, 32);
            txtQuantity.TabIndex = 2;
            txtQuantity.TextAlign = HorizontalAlignment.Center;
            // 
            // btnOk
            // 
            btnOk.BackColor = Color.Transparent;
            btnOk.FlatStyle = FlatStyle.Flat;
            btnOk.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnOk.ForeColor = Color.Black;
            btnOk.Location = new Point(44, 150);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(100, 40);
            btnOk.TabIndex = 3;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = false;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Transparent;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnCancel.ForeColor = Color.Black;
            btnCancel.Location = new Point(169, 150);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 40);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // QuantityForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(314, 221);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(txtQuantity);
            Controls.Add(lblProductName);
            Controls.Add(lblOperation);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "QuantityForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Изменение количества";
            Load += QuantityForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
