using System;
using System.Windows.Forms;

namespace WarehouseManagementSystem
{
    public partial class QuantityForm : Form
    {
        public int Quantity { get; private set; }
        private string operation;
        private string productName;

        public QuantityForm(string operation, string productName)
        {
            InitializeComponent();
            
            this.operation = operation;
            this.productName = productName;
            
            // Устанавливаем текст операции
            lblOperation.Text = operation;
            
            // Устанавливаем название товара
            lblProductName.Text = $"\"{productName}\"";
            
            // Фокус на поле ввода при загрузке
            this.Shown += (s, e) => txtQuantity.Focus();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                // Валидация ввода
                if (string.IsNullOrWhiteSpace(txtQuantity.Text))
                {
                    MessageBox.Show("Введите количество", "Ошибка валидации", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQuantity.Focus();
                    return;
                }

                if (!int.TryParse(txtQuantity.Text, out int quantity))
                {
                    MessageBox.Show("Введите корректное целое число", "Ошибка валидации", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQuantity.Focus();
                    txtQuantity.SelectAll();
                    return;
                }

                if (quantity <= 0)
                {
                    MessageBox.Show("Количество должно быть больше нуля", "Ошибка валидации", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQuantity.Focus();
                    txtQuantity.SelectAll();
                    return;
                }

                // Для операции списания можно добавить дополнительную проверку
                if (operation == "Списание")
                {
                    var result = MessageBox.Show($"Вы уверены, что хотите списать {quantity} шт. товара \"{productName}\"?", 
                        "Подтверждение списания", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result != DialogResult.Yes)
                    {
                        return;
                    }
                }

                Quantity = quantity;
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

        // Обработчик для ограничения ввода только цифрами
        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем только цифры и клавишу Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        // Обработчик для быстрого добавления типовых количеств
        private void OnQuantityButtonClick(int quantity)
        {
            txtQuantity.Text = quantity.ToString();
            btnOk_Click(this, EventArgs.Empty);
        }

        // Дополнительный метод для установки максимального количества (для списания)
        public void SetMaxQuantity(int maxQuantity)
        {
            if (operation == "Списание")
            {
                var toolTip = new ToolTip();
                toolTip.SetToolTip(txtQuantity, $"Максимальное доступное количество: {maxQuantity}");
                
                // Можно добавить валидацию при вводе
                txtQuantity.TextChanged += (s, e) => 
                {
                    if (int.TryParse(txtQuantity.Text, out int value) && value > maxQuantity)
                    {
                        txtQuantity.BackColor = System.Drawing.Color.LightCoral;
                        btnOk.Enabled = false;
                        errorProvider1.SetError(txtQuantity, $"Недостаточно товара! Доступно: {maxQuantity}");
                    }
                    else
                    {
                        txtQuantity.BackColor = System.Drawing.Color.White;
                        btnOk.Enabled = true;
                        errorProvider1.Clear();
                    }
                };
            }
        }

        // Добавляем компонент для отображения ошибок (нужно добавить в дизайнер)
        private System.Windows.Forms.ErrorProvider errorProvider1;
        
        // Дополнительная инициализация компонентов (если нет в дизайнере)
        private void InitializeAdditionalComponents()
        {
            errorProvider1 = new System.Windows.Forms.ErrorProvider();
            errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
        }
    }
}