using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class MainForm : Form
    {
        private List<Product> products;
        private int nextId;

        public MainForm()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            products = new List<Product>();
            nextId = 1;

            products.Add(new Product { Id = nextId++, Name = "Ноутбук Lenovo", Quantity = 15, Price = 45000, Category = "Электроника" });
            products.Add(new Product { Id = nextId++, Name = "Мышь Logitech", Quantity = 50, Price = 1200, Category = "Периферия" });
            products.Add(new Product { Id = nextId++, Name = "Клавиатура механическая", Quantity = 25, Price = 3500, Category = "Периферия" });
            products.Add(new Product { Id = nextId++, Name = "Монитор 24\"", Quantity = 10, Price = 12000, Category = "Электроника" });

            RefreshProductGrid();
            UpdateStatistics();
        }

        private void RefreshProductGrid()
        {
            dataGridViewProducts.DataSource = null;
            dataGridViewProducts.DataSource = products;
            dataGridViewProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewProducts.Columns["Id"].HeaderText = "ID";
            dataGridViewProducts.Columns["Name"].HeaderText = "Наименование";
            dataGridViewProducts.Columns["Quantity"].HeaderText = "Количество";
            dataGridViewProducts.Columns["Price"].HeaderText = "Цена (руб)";
            dataGridViewProducts.Columns["Category"].HeaderText = "Категория";

            dataGridViewProducts.ClearSelection();
        }

        private void UpdateStatistics()
        {
            int totalItems = products.Sum(p => p.Quantity);
            int totalProducts = products.Count;
            decimal totalValue = products.Sum(p => p.Quantity * p.Price);
            int lowStockCount = products.Count(p => p.Quantity < 10);

            lblTotalItems.Text = $"Всего единиц: {totalItems}";
            lblTotalProducts.Text = $"Всего товаров: {totalProducts}";
            lblTotalValue.Text = $"Общая стоимость: {totalValue:N0} руб.";
            lblLowStock.Text = $"Товаров с малым остатком (<10): {lowStockCount}";

            if (lowStockCount > 0)
            {
                lblLowStock.ForeColor = Color.Red;
                lblLowStock.Font = new Font(lblLowStock.Font, FontStyle.Bold);
            }
            else
            {
                lblLowStock.ForeColor = Color.Green;
                lblLowStock.Font = new Font(lblLowStock.Font, FontStyle.Regular);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addForm = new ProductForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                var newProduct = addForm.Product;
                newProduct.Id = nextId++;
                products.Add(newProduct);
                RefreshProductGrid();
                UpdateStatistics();
                MessageBox.Show("Товар успешно добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите товар для редактирования", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedProduct = (Product)dataGridViewProducts.SelectedRows[0].DataBoundItem;
            var editForm = new ProductForm(selectedProduct);

            if (editForm.ShowDialog() == DialogResult.OK)
            {
                var updatedProduct = editForm.Product;
                selectedProduct.Name = updatedProduct.Name;
                selectedProduct.Quantity = updatedProduct.Quantity;
                selectedProduct.Price = updatedProduct.Price;
                selectedProduct.Category = updatedProduct.Category;

                RefreshProductGrid();
                UpdateStatistics();
                MessageBox.Show("Товар успешно обновлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите товар для удаления", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedProduct = (Product)dataGridViewProducts.SelectedRows[0].DataBoundItem;

            if (MessageBox.Show($"Вы уверены, что хотите удалить товар \"{selectedProduct.Name}\"?",
                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                products.Remove(selectedProduct);
                RefreshProductGrid();
                UpdateStatistics();
                MessageBox.Show("Товар удален!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                RefreshProductGrid();
                return;
            }

            var filteredProducts = products.Where(p =>
                p.Name.ToLower().Contains(searchText) ||
                p.Category.ToLower().Contains(searchText)).ToList();

            dataGridViewProducts.DataSource = null;
            dataGridViewProducts.DataSource = filteredProducts;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            RefreshProductGrid();
        }

        private void btnIncoming_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите товар", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedProduct = (Product)dataGridViewProducts.SelectedRows[0].DataBoundItem;
            var quantityForm = new QuantityForm("Поступление", selectedProduct.Name);

            if (quantityForm.ShowDialog() == DialogResult.OK)
            {
                selectedProduct.Quantity += quantityForm.Quantity;
                RefreshProductGrid();
                UpdateStatistics();
                MessageBox.Show($"Товар \"{selectedProduct.Name}\" пополнен на {quantityForm.Quantity} шт.",
                    "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnOutgoing_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите товар", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedProduct = (Product)dataGridViewProducts.SelectedRows[0].DataBoundItem;
            var quantityForm = new QuantityForm("Списание", selectedProduct.Name);

            if (quantityForm.ShowDialog() == DialogResult.OK)
            {
                if (selectedProduct.Quantity >= quantityForm.Quantity)
                {
                    selectedProduct.Quantity -= quantityForm.Quantity;
                    RefreshProductGrid();
                    UpdateStatistics();
                    MessageBox.Show($"С товара \"{selectedProduct.Name}\" списано {quantityForm.Quantity} шт.",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Недостаточно товара на складе! Доступно: {selectedProduct.Quantity} шт.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridViewProducts_SelectionChanged(object sender, EventArgs e)
        {

        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }
}
