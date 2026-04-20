namespace WinFormsApp1
{
    partial class ProductForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtName;
        private TextBox txtQuantity;
        private TextBox txtPrice;
        private TextBox txtCategory;
        private Label lblName;
        private Label lblQuantity;
        private Label lblPrice;
        private Label lblCategory;
        private Button btnSave;
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
            txtName = new TextBox();
            txtQuantity = new TextBox();
            txtPrice = new TextBox();
            txtCategory = new TextBox();
            lblName = new Label();
            lblQuantity = new Label();
            lblPrice = new Label();
            lblCategory = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtName.Location = new Point(130, 30);
            txtName.Name = "txtName";
            txtName.Size = new Size(250, 25);
            txtName.TabIndex = 0;
            // 
            // txtQuantity
            // 
            txtQuantity.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtQuantity.Location = new Point(130, 70);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(250, 25);
            txtQuantity.TabIndex = 1;
            // 
            // txtPrice
            // 
            txtPrice.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtPrice.Location = new Point(130, 110);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(250, 25);
            txtPrice.TabIndex = 2;
            // 
            // txtCategory
            // 
            txtCategory.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtCategory.Location = new Point(130, 150);
            txtCategory.Name = "txtCategory";
            txtCategory.Size = new Size(250, 25);
            txtCategory.TabIndex = 3;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Microsoft PhagsPa", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblName.Location = new Point(30, 33);
            lblName.Name = "lblName";
            lblName.Size = new Size(80, 16);
            lblName.TabIndex = 4;
            lblName.Text = "Название:*";
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Font = new Font("Microsoft PhagsPa", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblQuantity.Location = new Point(30, 73);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(94, 16);
            lblQuantity.TabIndex = 5;
            lblQuantity.Text = "Количество:*";
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Font = new Font("Microsoft PhagsPa", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblPrice.Location = new Point(30, 113);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(49, 16);
            lblPrice.TabIndex = 6;
            lblPrice.Text = "Цена:*";
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Microsoft PhagsPa", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblCategory.Location = new Point(30, 153);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(86, 16);
            lblCategory.TabIndex = 7;
            lblCategory.Text = "Категория:*";
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.Transparent;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnSave.ForeColor = Color.Black;
            btnSave.Location = new Point(90, 210);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 40);
            btnSave.TabIndex = 8;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Transparent;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnCancel.ForeColor = Color.Black;
            btnCancel.Location = new Point(230, 210);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 40);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // ProductForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(414, 281);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(lblCategory);
            Controls.Add(lblPrice);
            Controls.Add(lblQuantity);
            Controls.Add(lblName);
            Controls.Add(txtCategory);
            Controls.Add(txtPrice);
            Controls.Add(txtQuantity);
            Controls.Add(txtName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ProductForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Товар";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
