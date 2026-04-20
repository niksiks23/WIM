using System;
using System.Windows.Forms;

namespace WarehouseManagementSystem
{
    public partial class ProductForm : Form
    {
        public Product Product { get; private set; }
        private bool isEditMode;

        public ProductForm(Product product = null)
        {
            InitializeComponent();
            
            if (product != null)
            {
                isEditMode = true;
                this.Text = "Редактирование товара";
                txtName.Text = product.Name;
                txtQuantity.Text = product.Quantity.ToString();
                txtPrice.Text = product.Price.ToString();
                txtCategory.Text = product.Category;
                btnSave.Text = "Обновить";
            }
            else
            {
                isEditMode = false;
                this.Text = "Добавление товара";
                btnSave.Text = "Добавить";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Валидация (как в предыдущей версии)
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Введите название товара", "Ошибка валидации", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return;
                }

                if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity < 0)
                {
                    MessageBox.Show("Введите корректное количество", "Ошибка валидации", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQuantity.Focus();
                    return;
                }

                if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
                {
                    MessageBox.Show("Введите корректную цену", "Ошибка валидации", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPrice.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCategory.Text))
                {
                    MessageBox.Show("Введите категорию товара", "Ошибка валидации", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCategory.Focus();
                    return;
                }

                Product = new Product
                {
                    Name = txtName.Text.Trim(),
                    Quantity = quantity,
                    Price = price,
                    Category = txtCategory.Text.Trim()
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}