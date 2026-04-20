namespace WinFormsApp1
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridViewProducts;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnSearch;
        private TextBox txtSearch;
        private Label lblSearch;
        private GroupBox groupBoxActions;
        private GroupBox groupBoxStatistics;
        private Label lblTotalItems;
        private Label lblTotalProducts;
        private Label lblTotalValue;
        private Label lblLowStock;
        private Button btnRefresh;
        private Button btnIncoming;
        private Button btnOutgoing;

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
            dataGridViewProducts = new DataGridView();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnSearch = new Button();
            txtSearch = new TextBox();
            lblSearch = new Label();
            groupBoxActions = new GroupBox();
            btnOutgoing = new Button();
            btnIncoming = new Button();
            btnRefresh = new Button();
            groupBoxStatistics = new GroupBox();
            lblLowStock = new Label();
            lblTotalValue = new Label();
            lblTotalProducts = new Label();
            lblTotalItems = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewProducts).BeginInit();
            groupBoxActions.SuspendLayout();
            groupBoxStatistics.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewProducts
            // 
            dataGridViewProducts.AllowUserToAddRows = false;
            dataGridViewProducts.AllowUserToDeleteRows = false;
            dataGridViewProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewProducts.BackgroundColor = Color.White;
            dataGridViewProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewProducts.Location = new Point(12, 12);
            dataGridViewProducts.MultiSelect = false;
            dataGridViewProducts.Name = "dataGridViewProducts";
            dataGridViewProducts.ReadOnly = true;
            dataGridViewProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewProducts.Size = new Size(803, 400);
            dataGridViewProducts.TabIndex = 0;
            dataGridViewProducts.SelectionChanged += dataGridViewProducts_SelectionChanged;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.Transparent;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Microsoft PhagsPa", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnAdd.Location = new Point(12, 50);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(100, 40);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.Transparent;
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.Font = new Font("Microsoft PhagsPa", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnEdit.Location = new Point(118, 50);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(125, 40);
            btnEdit.TabIndex = 2;
            btnEdit.Text = "Редактировать";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.Transparent;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnDelete.Location = new Point(249, 50);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 40);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.LightGray;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnSearch.Location = new Point(358, 55);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(80, 30);
            btnSearch.TabIndex = 4;
            btnSearch.Text = "Найти";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtSearch.Location = new Point(499, 34);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(200, 23);
            txtSearch.TabIndex = 5;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblSearch.Location = new Point(496, 62);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(285, 17);
            lblSearch.TabIndex = 6;
            lblSearch.Text = "(поиск по названию или категории, Enter)";
            // 
            // groupBoxActions
            // 
            groupBoxActions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxActions.Controls.Add(btnOutgoing);
            groupBoxActions.Controls.Add(btnIncoming);
            groupBoxActions.Controls.Add(btnRefresh);
            groupBoxActions.Controls.Add(btnAdd);
            groupBoxActions.Controls.Add(btnEdit);
            groupBoxActions.Controls.Add(btnDelete);
            groupBoxActions.Controls.Add(btnSearch);
            groupBoxActions.Controls.Add(txtSearch);
            groupBoxActions.Controls.Add(lblSearch);
            groupBoxActions.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxActions.Location = new Point(12, 418);
            groupBoxActions.Name = "groupBoxActions";
            groupBoxActions.Size = new Size(803, 125);
            groupBoxActions.TabIndex = 7;
            groupBoxActions.TabStop = false;
            groupBoxActions.Text = "Управление товарами";
            // 
            // btnOutgoing
            // 
            btnOutgoing.BackColor = Color.Transparent;
            btnOutgoing.FlatStyle = FlatStyle.Flat;
            btnOutgoing.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnOutgoing.Location = new Point(634, 89);
            btnOutgoing.Name = "btnOutgoing";
            btnOutgoing.Size = new Size(120, 30);
            btnOutgoing.TabIndex = 9;
            btnOutgoing.Text = "Списание (-)";
            btnOutgoing.UseVisualStyleBackColor = false;
            btnOutgoing.Click += btnOutgoing_Click;
            // 
            // btnIncoming
            // 
            btnIncoming.BackColor = Color.Transparent;
            btnIncoming.FlatStyle = FlatStyle.Flat;
            btnIncoming.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnIncoming.Location = new Point(499, 89);
            btnIncoming.Name = "btnIncoming";
            btnIncoming.Size = new Size(120, 30);
            btnIncoming.TabIndex = 8;
            btnIncoming.Text = "Поступление (+)";
            btnIncoming.UseVisualStyleBackColor = false;
            btnIncoming.Click += btnIncoming_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.LightGray;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnRefresh.Location = new Point(708, 33);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(76, 26);
            btnRefresh.TabIndex = 7;
            btnRefresh.Text = "Обновить";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // groupBoxStatistics
            // 
            groupBoxStatistics.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            groupBoxStatistics.Controls.Add(lblLowStock);
            groupBoxStatistics.Controls.Add(lblTotalValue);
            groupBoxStatistics.Controls.Add(lblTotalProducts);
            groupBoxStatistics.Controls.Add(lblTotalItems);
            groupBoxStatistics.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxStatistics.Location = new Point(12, 546);
            groupBoxStatistics.Name = "groupBoxStatistics";
            groupBoxStatistics.Size = new Size(803, 112);
            groupBoxStatistics.TabIndex = 8;
            groupBoxStatistics.TabStop = false;
            groupBoxStatistics.Text = "Статистика склада";
            // 
            // lblLowStock
            // 
            lblLowStock.AutoSize = true;
            lblLowStock.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblLowStock.Location = new Point(12, 65);
            lblLowStock.Name = "lblLowStock";
            lblLowStock.Size = new Size(191, 17);
            lblLowStock.TabIndex = 3;
            lblLowStock.Text = "Товаров с малым остатком:";
            // 
            // lblTotalValue
            // 
            lblTotalValue.AutoSize = true;
            lblTotalValue.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblTotalValue.Location = new Point(12, 48);
            lblTotalValue.Name = "lblTotalValue";
            lblTotalValue.Size = new Size(130, 17);
            lblTotalValue.TabIndex = 2;
            lblTotalValue.Text = "Общая стоимость:";
            // 
            // lblTotalProducts
            // 
            lblTotalProducts.AutoSize = true;
            lblTotalProducts.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblTotalProducts.Location = new Point(12, 31);
            lblTotalProducts.Name = "lblTotalProducts";
            lblTotalProducts.Size = new Size(106, 17);
            lblTotalProducts.TabIndex = 1;
            lblTotalProducts.Text = "Всего товаров:";
            // 
            // lblTotalItems
            // 
            lblTotalItems.AutoSize = true;
            lblTotalItems.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblTotalItems.Location = new Point(12, 14);
            lblTotalItems.Name = "lblTotalItems";
            lblTotalItems.Size = new Size(101, 17);
            lblTotalItems.TabIndex = 0;
            lblTotalItems.Text = "Всего единиц:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(827, 667);
            Controls.Add(groupBoxStatistics);
            Controls.Add(groupBoxActions);
            Controls.Add(dataGridViewProducts);
            MinimumSize = new Size(700, 675);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Система управления складскими остатками";
            ((System.ComponentModel.ISupportInitialize)dataGridViewProducts).EndInit();
            groupBoxActions.ResumeLayout(false);
            groupBoxActions.PerformLayout();
            groupBoxStatistics.ResumeLayout(false);
            groupBoxStatistics.PerformLayout();
            ResumeLayout(false);
        }
    }
}