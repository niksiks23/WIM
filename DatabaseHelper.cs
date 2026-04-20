using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace WarehouseManagementSystem
{
    public class DatabaseHelper
    {
        private string connectionString;
        
        public DatabaseHelper()
        {
            // База данных создается в папке приложения
            string dbPath = Path.Combine(Application.StartupPath, "warehouse.db");
            connectionString = $"Data Source={dbPath};Version=3;";
            
            // Создаем базу данных и таблицы, если их нет
            InitializeDatabase();
        }
        
        private void InitializeDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    
                    // SQL запрос для создания таблицы товаров
                    string createTableQuery = @"
                        CREATE TABLE IF NOT EXISTS Products (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            Quantity INTEGER NOT NULL,
                            Price REAL NOT NULL,
                            Category TEXT NOT NULL,
                            CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                            UpdatedDate DATETIME DEFAULT CURRENT_TIMESTAMP
                        )";
                    
                    using (var command = new SQLiteCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    
                    // Создаем индекс для быстрого поиска
                    string createIndexQuery = @"
                        CREATE INDEX IF NOT EXISTS idx_products_name ON Products(Name);
                        CREATE INDEX IF NOT EXISTS idx_products_category ON Products(Category);
                    ";
                    
                    using (var command = new SQLiteCommand(createIndexQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации базы данных: {ex.Message}", 
                    "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        // Получить все товары
        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();
            
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Products ORDER BY Id";
                    
                    using (var command = new SQLiteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                Price = Convert.ToDecimal(reader["Price"]),
                                Category = reader["Category"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения данных: {ex.Message}", 
                    "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return products;
        }
        
        // Добавить товар
        public int AddProduct(Product product)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        INSERT INTO Products (Name, Quantity, Price, Category, CreatedDate, UpdatedDate)
                        VALUES (@Name, @Quantity, @Price, @Category, @CreatedDate, @UpdatedDate);
                        SELECT last_insert_rowid();
                    ";
                    
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", product.Name);
                        command.Parameters.AddWithValue("@Quantity", product.Quantity);
                        command.Parameters.AddWithValue("@Price", product.Price);
                        command.Parameters.AddWithValue("@Category", product.Category);
                        command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        command.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                        
                        int newId = Convert.ToInt32(command.ExecuteScalar());
                        return newId;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления товара: {ex.Message}", 
                    "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }
        
        // Обновить товар
        public bool UpdateProduct(Product product)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        UPDATE Products 
                        SET Name = @Name, 
                            Quantity = @Quantity, 
                            Price = @Price, 
                            Category = @Category,
                            UpdatedDate = @UpdatedDate
                        WHERE Id = @Id
                    ";
                    
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", product.Id);
                        command.Parameters.AddWithValue("@Name", product.Name);
                        command.Parameters.AddWithValue("@Quantity", product.Quantity);
                        command.Parameters.AddWithValue("@Price", product.Price);
                        command.Parameters.AddWithValue("@Category", product.Category);
                        command.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                        
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления товара: {ex.Message}", 
                    "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        
        // Удалить товар
        public bool DeleteProduct(int productId)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Products WHERE Id = @Id";
                    
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", productId);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления товара: {ex.Message}", 
                    "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        
        // Поиск товаров
        public List<Product> SearchProducts(string searchText)
        {
            var products = new List<Product>();
            
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT * FROM Products 
                        WHERE Name LIKE @Search 
                        OR Category LIKE @Search
                        ORDER BY Id
                    ";
                    
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Search", $"%{searchText}%");
                        
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(new Product
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["Name"].ToString(),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    Category = reader["Category"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}", 
                    "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return products;
        }
        
        // Получить статистику
        public (int totalItems, int totalProducts, decimal totalValue, int lowStock) GetStatistics()
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    
                    string query = @"
                        SELECT 
                            SUM(Quantity) as TotalItems,
                            COUNT(*) as TotalProducts,
                            SUM(Quantity * Price) as TotalValue,
                            COUNT(CASE WHEN Quantity < 10 THEN 1 END) as LowStock
                        FROM Products
                    ";
                    
                    using (var command = new SQLiteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return (
                                totalItems: reader["TotalItems"] != DBNull.Value ? Convert.ToInt32(reader["TotalItems"]) : 0,
                                totalProducts: Convert.ToInt32(reader["TotalProducts"]),
                                totalValue: reader["TotalValue"] != DBNull.Value ? Convert.ToDecimal(reader["TotalValue"]) : 0,
                                lowStock: Convert.ToInt32(reader["LowStock"])
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения статистики: {ex.Message}", 
                    "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return (0, 0, 0, 0);
        }
        
        // Обновить количество товара (для операций поступления/списания)
        public bool UpdateQuantity(int productId, int newQuantity)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        UPDATE Products 
                        SET Quantity = @Quantity,
                            UpdatedDate = @UpdatedDate
                        WHERE Id = @Id
                    ";
                    
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", productId);
                        command.Parameters.AddWithValue("@Quantity", newQuantity);
                        command.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                        
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления количества: {ex.Message}", 
                    "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        
        // Резервное копирование базы данных
        public void BackupDatabase()
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "SQLite DB files (*.db)|*.db";
                    sfd.Title = "Резервное копирование базы данных";
                    sfd.FileName = $"warehouse_backup_{DateTime.Now:yyyyMMdd_HHmmss}.db";
                    
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string sourceFile = Path.Combine(Application.StartupPath, "warehouse.db");
                        File.Copy(sourceFile, sfd.FileName, true);
                        MessageBox.Show($"База данных сохранена в: {sfd.FileName}", 
                            "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка резервного копирования: {ex.Message}", 
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        // Очистить все данные (с подтверждением)
        public void ClearAllData()
        {
            var result = MessageBox.Show("Вы уверены, что хотите удалить ВСЕ товары из базы данных?\nЭто действие нельзя отменить!", 
                "Подтверждение очистки", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Products";
                        
                        using (var command = new SQLiteCommand(query, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    
                    MessageBox.Show("База данных очищена", "Успех", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка очистки данных: {ex.Message}", 
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}