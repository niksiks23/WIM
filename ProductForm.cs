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
                // Валидация названия
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Введите название товара", "Ошибка валидации", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return;
                }

                // Валидация количества
                if (!int.TryParse(txtQuantity.Text, out int quantity))
                {
                    MessageBox.Show("Введите корректное целое число для количества", "Ошибка валидации", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQuantity.Focus();
                    txtQuantity.SelectAll();
                    return;
                }

                if (quantity < 0)
                {
                    MessageBox.Show("Количество не может быть отрицательным", "Ошибка валидации", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQuantity.Focus();
                    txtQuantity.SelectAll();
                    return;
                }

                // Валидация цены
                if (!decimal.TryParse(txtPrice.Text, out decimal price))
                {
                    MessageBox.Show("Введите корректную цену (число)", "Ошибка валидации", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPrice.Focus();
                    txtPrice.SelectAll();
                    return;
                }

                if (price < 0)
                {
                    MessageBox.Show("Цена не может быть отрицательной", "Ошибка валидации", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPrice.Focus();
                    txtPrice.SelectAll();
                    return;
                }

                // Валидация категории
                if (string.IsNullOrWhiteSpace(txtCategory.Text))
                {
                    MessageBox.Show("Введите категорию товара", "Ошибка валидации", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCategory.Focus();
                    return;
                }

                // Создание объекта товара
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

        // Дополнительный метод для очистки формы
        private void ClearForm()
        {
            txtName.Clear();
            txtQuantity.Clear();
            txtPrice.Clear();
            txtCategory.Clear();
        }

        // Обработчик для удобства - нажатие Enter в полях ввода
        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем только цифры и клавишу Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем цифры, точку и Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
            
            // Разрешаем только одну точку
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }
        }
    }
}