using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WarehouseManagementSystem
{
    public partial class MainForm : Form
    {
        private DatabaseHelper db;
        private List<Product> products;
        private List<Product> filteredProducts;
        private bool isDataLoaded = false;

        public MainForm()
        {
            InitializeComponent();
            InitializeDatabase();
            SetupContextMenu();
            SetupKeyboardShortcuts();
        }

        private void InitializeDatabase()
        {
            db = new DatabaseHelper();
            LoadDataFromDatabase();
        }

        private void LoadDataFromDatabase()
        {
            try
            {
                // Загружаем данные из БД
                products = db.GetAllProducts();
                filteredProducts = null;
                isDataLoaded = true;
                
                RefreshProductGrid();
                UpdateStatistics();
                
                if (products.Count == 0)
                {
                    // Предложение добавить тестовые данные
                    var result = MessageBox.Show("База данных пуста. Хотите добавить тестовые данные?", 
                        "Нет данных", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        AddSampleData();
                    }
                }
                else
                {
                    UpdateStatusLabel($"Загружено товаров: {products.Count}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", 
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddSampleData()
        {
            // Добавляем тестовые данные в БД
            var sampleProducts = new List<Product>
            {
                new Product { Name = "Ноутбук Lenovo ThinkPad", Quantity = 15, Price = 45000, Category = "Электроника" },
                new Product { Name = "Мышь Logitech MX Master", Quantity = 8, Price = 1200, Category = "Периферия" },
                new Product { Name = "Клавиатура механическая Redragon", Quantity = 25, Price = 3500, Category = "Периферия" },
                new Product { Name = "Монитор Dell 24\"", Quantity = 5, Price = 12000, Category = "Электроника" },
                new Product { Name = "SSD Kingston 500GB", Quantity = 30, Price = 4500, Category = "Комплектующие" },
                new Product { Name = "Оперативная память 16GB", Quantity = 12, Price = 3800, Category = "Комплектующие" }
            };
            
            foreach (var product in sampleProducts)
            {
                db.AddProduct(product);
            }
            
            LoadDataFromDatabase();
            NotifyUser("Тестовые данные добавлены", NotificationType.Success);
        }

        private void SetupContextMenu()
        {
            // Контекстное меню для таблицы
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Редактировать", null, (s, e) => btnEdit_Click(null, null));
            contextMenu.Items.Add("Удалить", null, (s, e) => btnDelete_Click(null, null));
            contextMenu.Items.Add("-"); // Разделитель
            contextMenu.Items.Add("Поступление (+)", null, (s, e) => btnIncoming_Click(null, null));
            contextMenu.Items.Add("Списание (-)", null, (s, e) => btnOutgoing_Click(null, null));
            dataGridViewProducts.ContextMenuStrip = contextMenu;
        }

        private void SetupKeyboardShortcuts()
        {
            // Горячие клавиши
            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                if (e.Control && e.KeyCode == Keys.N) // Ctrl+N - Добавить
                    btnAdd_Click(null, null);
                else if (e.Control && e.KeyCode == Keys.E) // Ctrl+E - Редактировать
                    btnEdit_Click(null, null);
                else if (e.Control && e.KeyCode == Keys.D) // Ctrl+D - Удалить
                    btnDelete_Click(null, null);
                else if (e.Control && e.KeyCode == Keys.F) // Ctrl+F - Поиск
                    txtSearch.Focus();
                else if (e.KeyCode == Keys.F5) // F5 - Обновить
                    btnRefresh_Click(null, null);
                else if (e.Control && e.KeyCode == Keys.I) // Ctrl+I - Поступление
                    btnIncoming_Click(null, null);
                else if (e.Control && e.KeyCode == Keys.O) // Ctrl+O - Списание
                    btnOutgoing_Click(null, null);
                else if (e.Control && e.KeyCode == Keys.B) // Ctrl+B - Резервное копирование
                    BackupDatabase();
            };
        }

        private void RefreshProductGrid()
        {
            var displayList = filteredProducts ?? products;
            
            dataGridViewProducts.DataSource = null;
            dataGridViewProducts.DataSource = displayList;
            
            // Настройка внешнего вида таблицы
            dataGridViewProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewProducts.AutoGenerateColumns = true;
            
            // Настройка заголовков
            if (dataGridViewProducts.Columns["Id"] != null)
                dataGridViewProducts.Columns["Id"].HeaderText = "ID";
            if (dataGridViewProducts.Columns["Name"] != null)
                dataGridViewProducts.Columns["Name"].HeaderText = "Наименование";
            if (dataGridViewProducts.Columns["Quantity"] != null)
                dataGridViewProducts.Columns["Quantity"].HeaderText = "Количество";
            if (dataGridViewProducts.Columns["Price"] != null)
                dataGridViewProducts.Columns["Price"].HeaderText = "Цена (руб)";
            if (dataGridViewProducts.Columns["Category"] != null)
                dataGridViewProducts.Columns["Category"].HeaderText = "Категория";
            
            // Подсветка строк с малым остатком
            foreach (DataGridViewRow row in dataGridViewProducts.Rows)
            {
                if (row.DataBoundItem != null)
                {
                    var product = (Product)row.DataBoundItem;
                    if (product.Quantity < 10 && product.Quantity > 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightSalmon;
                        row.DefaultCellStyle.ForeColor = Color.DarkRed;
                        row.DefaultCellStyle.Font = new Font(dataGridViewProducts.Font, FontStyle.Bold);
                    }
                    else if (product.Quantity == 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                        row.DefaultCellStyle.ForeColor = Color.Gray;
                    }
                }
            }
            
            dataGridViewProducts.ClearSelection();
        }

        private void UpdateStatistics()
        {
            var stats = db.GetStatistics();
            
            lblTotalItems.Text = $"📦 Всего единиц: {stats.totalItems:N0}";
            lblTotalProducts.Text = $"📋 Всего товаров: {stats.totalProducts}";
            lblTotalValue.Text = $"💰 Общая стоимость: {stats.totalValue:N0} руб.";
            lblLowStock.Text = $"⚠️ Товаров с малым остатком (<10): {stats.lowStock}";
            
            // Подсветка при малом остатке
            if (stats.lowStock > 0)
            {
                lblLowStock.ForeColor = Color.OrangeRed;
                lblLowStock.Font = new Font(lblLowStock.Font, FontStyle.Bold);
            }
            else
            {
                lblLowStock.ForeColor = Color.Green;
                lblLowStock.Font = new Font(lblLowStock.Font, FontStyle.Regular);
            }
        }

        private void UpdateStatusLabel(string message)
        {
            // Можно обновлять статусную строку, если добавить StatusStrip
            // statusStrip1.Items[0].Text = message;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addForm = new ProductForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                var newProduct = addForm.Product;
                int newId = db.AddProduct(newProduct);
                
                if (newId > 0)
                {
                    LoadDataFromDatabase(); // Перезагружаем данные из БД
                    NotifyUser($"Товар \"{newProduct.Name}\" успешно добавлен!", NotificationType.Success);
                }
                else
                {
                    NotifyUser("Ошибка при добавлении товара", NotificationType.Error);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 0)
            {
                NotifyUser("Выберите товар для редактирования", NotificationType.Warning);
                return;
            }

            var selectedProduct = (Product)dataGridViewProducts.SelectedRows[0].DataBoundItem;
            var editForm = new ProductForm(selectedProduct);
            
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                var updatedProduct = editForm.Product;
                updatedProduct.Id = selectedProduct.Id;
                
                if (db.UpdateProduct(updatedProduct))
                {
                    LoadDataFromDatabase(); // Перезагружаем данные из БД
                    NotifyUser($"Товар \"{updatedProduct.Name}\" успешно обновлен!", NotificationType.Success);
                }
                else
                {
                    NotifyUser("Ошибка при обновлении товара", NotificationType.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 0)
            {
                NotifyUser("Выберите товар для удаления", NotificationType.Warning);
                return;
            }

            var selectedProduct = (Product)dataGridViewProducts.SelectedRows[0].DataBoundItem;
            
            if (MessageBox.Show($"Вы уверены, что хотите удалить товар \"{selectedProduct.Name}\"?", 
                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (db.DeleteProduct(selectedProduct.Id))
                {
                    LoadDataFromDatabase(); // Перезагружаем данные из БД
                    NotifyUser($"Товар \"{selectedProduct.Name}\" удален!", NotificationType.Info);
                }
                else
                {
                    NotifyUser("Ошибка при удалении товара", NotificationType.Error);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PerformSearch();
                e.Handled = true;
            }
        }

        private void PerformSearch()
        {
            string searchText = txtSearch.Text.Trim();
            
            if (string.IsNullOrEmpty(searchText))
            {
                ClearFilter();
                return;
            }
            
            filteredProducts = db.SearchProducts(searchText);
            
            if (filteredProducts.Count == 0)
            {
                MessageBox.Show("Товары не найдены", "Результат поиска", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFilter();
            }
            else
            {
                RefreshProductGrid();
                NotifyUser($"Найдено товаров: {filteredProducts.Count}", NotificationType.Info);
            }
        }

        private void ClearFilter()
        {
            filteredProducts = null;
            txtSearch.Clear();
            LoadDataFromDatabase(); // Перезагружаем данные из БД
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearFilter();
            UpdateStatistics();
            NotifyUser("Список обновлен из базы данных", NotificationType.Info);
        }

        private void btnIncoming_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 0)
            {
                NotifyUser("Выберите товар для пополнения", NotificationType.Warning);
                return;
            }

            var selectedProduct = (Product)dataGridViewProducts.SelectedRows[0].DataBoundItem;
            var quantityForm = new QuantityForm("Поступление", selectedProduct.Name);
            
            if (quantityForm.ShowDialog() == DialogResult.OK)
            {
                int newQuantity = selectedProduct.Quantity + quantityForm.Quantity;
                
                if (db.UpdateQuantity(selectedProduct.Id, newQuantity))
                {
                    LoadDataFromDatabase(); // Перезагружаем данные из БД
                    NotifyUser($"Товар \"{selectedProduct.Name}\" пополнен на {quantityForm.Quantity} шт.", 
                        NotificationType.Success);
                }
                else
                {
                    NotifyUser("Ошибка при обновлении количества", NotificationType.Error);
                }
            }
        }

        private void btnOutgoing_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 0)
            {
                NotifyUser("Выберите товар для списания", NotificationType.Warning);
                return;
            }

            var selectedProduct = (Product)dataGridViewProducts.SelectedRows[0].DataBoundItem;
            
            if (selectedProduct.Quantity == 0)
            {
                NotifyUser($"Товар \"{selectedProduct.Name}\" отсутствует на складе!", NotificationType.Error);
                return;
            }
            
            var quantityForm = new QuantityForm("Списание", selectedProduct.Name);
            
            if (quantityForm.ShowDialog() == DialogResult.OK)
            {
                if (selectedProduct.Quantity >= quantityForm.Quantity)
                {
                    int newQuantity = selectedProduct.Quantity - quantityForm.Quantity;
                    
                    if (db.UpdateQuantity(selectedProduct.Id, newQuantity))
                    {
                        LoadDataFromDatabase(); // Перезагружаем данные из БД
                        NotifyUser($"С товара \"{selectedProduct.Name}\" списано {quantityForm.Quantity} шт.", 
                            NotificationType.Success);
                    }
                    else
                    {
                        NotifyUser("Ошибка при обновлении количества", NotificationType.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"Недостаточно товара на складе! Доступно: {selectedProduct.Quantity} шт.", 
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BackupDatabase()
        {
            db.BackupDatabase();
        }

        private void ClearAllData()
        {
            db.ClearAllData();
            LoadDataFromDatabase();
        }

        // Утилитарные методы
        private enum NotificationType { Success, Warning, Error, Info }
        
        private void NotifyUser(string message, NotificationType type)
        {
            MessageBox.Show(message, GetNotificationTitle(type), MessageBoxButtons.OK, GetNotificationIcon(type));
        }
        
        private string GetNotificationTitle(NotificationType type)
        {
            switch (type)
            {
                case NotificationType.Success: return "Успех";
                case NotificationType.Warning: return "Предупреждение";
                case NotificationType.Error: return "Ошибка";
                default: return "Информация";
            }
        }
        
        private MessageBoxIcon GetNotificationIcon(NotificationType type)
        {
            switch (type)
            {
                case NotificationType.Success: return MessageBoxIcon.Information;
                case NotificationType.Warning: return MessageBoxIcon.Warning;
                case NotificationType.Error: return MessageBoxIcon.Error;
                default: return MessageBoxIcon.Information;
            }
        }

        private void dataGridViewProducts_SelectionChanged(object sender, EventArgs e)
        {
            // Можно обновлять дополнительную информацию о выбранном товаре
        }
    }
}