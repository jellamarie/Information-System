using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Information_System
{
    public partial class Form1 : Form
    {
        string connectionString = "server=localhost;port=3306;database=information system;uid=root;pwd=Caceres25_;";
        MySqlConnection con;

        public Form1()
        {
            InitializeComponent();
            con = new MySqlConnection(connectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadUsers();
            LoadProducts();
            LoadOrders();
        }

        // ======================= USERS =======================

        private void LoadUsers()
        {
            MySqlDataAdapter sda = new MySqlDataAdapter("SELECT * FROM users", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvUsers.DataSource = dt;
        }

        private void ClearUserFields()
        {
            txtUserID.Clear();
            txtName.Clear();
            txtEmail.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (MySqlCommand cmd = new MySqlCommand("INSERT INTO users (name, email) VALUES (@name, @mail)", con))
            {
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@mail", txtEmail.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            MessageBox.Show("User added successfully!");
            LoadUsers();
            ClearUserFields();
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUsers.Rows[e.RowIndex];
                txtUserID.Text = row.Cells["user_id"].Value.ToString();
                txtName.Text = row.Cells["name"].Value.ToString();
                txtEmail.Text = row.Cells["email"].Value.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserID.Text))
            {
                MessageBox.Show("Please select a user to update.");
                return;
            }

            try
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE users SET name = @name, email = @mail WHERE user_id = @id", con))
                {
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@mail", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@id", txtUserID.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                MessageBox.Show("User updated successfully!");
                LoadUsers();
                ClearUserFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserID.Text))
            {
                MessageBox.Show("Please select a user to delete.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this user?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand("DELETE FROM users WHERE user_id = @id", con))
                    {
                        cmd.Parameters.AddWithValue("@id", txtUserID.Text);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    MessageBox.Show("User deleted successfully!");
                    LoadUsers();
                    ClearUserFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
        }

        // ======================= PRODUCTS =======================

        private void LoadProducts()
        {
            MySqlDataAdapter sda = new MySqlDataAdapter("SELECT * FROM products", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvProducts.DataSource = dt;
        }

        private void ClearProductFields()
        {
            txtProductID.Clear();
            txtProductName.Clear();
            txtPrice.Clear();
            txtStock.Clear();
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProducts.Rows[e.RowIndex];
                txtProductID.Text = row.Cells["product_id"].Value.ToString();
                txtProductName.Text = row.Cells["name"].Value.ToString();
                txtPrice.Text = row.Cells["price"].Value.ToString();
                txtStock.Text = row.Cells["stock"].Value.ToString();
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text) || string.IsNullOrWhiteSpace(txtPrice.Text) || string.IsNullOrWhiteSpace(txtStock.Text))
            {
                MessageBox.Show("Please fill in all product details.");
                return;
            }

            using (MySqlCommand cmd = new MySqlCommand("INSERT INTO products (name, price, stock) VALUES (@name, @price, @stock)", con))
            {
                cmd.Parameters.AddWithValue("@name", txtProductName.Text);
                cmd.Parameters.AddWithValue("@price", txtPrice.Text);
                cmd.Parameters.AddWithValue("@stock", txtStock.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            MessageBox.Show("Product added successfully!");
            LoadProducts();
            ClearProductFields();
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductID.Text))
            {
                MessageBox.Show("Please select a product to update.");
                return;
            }

            try
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE products SET name = @name, price = @price, stock = @stock WHERE product_id = @id", con))
                {
                    cmd.Parameters.AddWithValue("@name", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@price", txtPrice.Text);
                    cmd.Parameters.AddWithValue("@stock", txtStock.Text);
                    cmd.Parameters.AddWithValue("@id", txtProductID.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                MessageBox.Show("Product updated successfully!");
                LoadProducts();
                ClearProductFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductID.Text))
            {
                MessageBox.Show("Please select a product to delete.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this product?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand("DELETE FROM products WHERE product_id = @id", con))
                    {
                        cmd.Parameters.AddWithValue("@id", txtProductID.Text);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    MessageBox.Show("Product deleted successfully!");
                    LoadProducts();
                    ClearProductFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
        }

        // ======================= ORDERS =======================

        private void LoadOrders()
        {
            MySqlDataAdapter sda = new MySqlDataAdapter("SELECT * FROM orders", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvOrders.DataSource = dt;
        }

        private void ClearOrderFields()
        {
            txtOrderID.Clear();
            txtOrderUserID.Clear();
            txtOrderDate.Clear();
            txtTotalAmount.Clear();
        }

        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvOrders.Rows[e.RowIndex];
                txtOrderID.Text = row.Cells["order_id"].Value.ToString();
                txtOrderUserID.Text = row.Cells["user_id"].Value.ToString();
                txtOrderDate.Text = Convert.ToDateTime(row.Cells["order_date"].Value).ToString("yyyy-MM-dd");
                txtTotalAmount.Text = row.Cells["total_amount"].Value.ToString();
            }
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOrderUserID.Text) || string.IsNullOrWhiteSpace(txtOrderDate.Text) || string.IsNullOrWhiteSpace(txtTotalAmount.Text))
            {
                MessageBox.Show("Please fill in all order details.");
                return;
            }

            using (MySqlCommand cmd = new MySqlCommand("INSERT INTO orders (user_id, order_date, total_amount) VALUES (@user_id, @order_date, @total_amount)", con))
            {
                cmd.Parameters.AddWithValue("@user_id", txtOrderUserID.Text);
                cmd.Parameters.AddWithValue("@order_date", txtOrderDate.Text);
                cmd.Parameters.AddWithValue("@total_amount", txtTotalAmount.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            MessageBox.Show("Order added successfully!");
            LoadOrders();                                                   
            ClearOrderFields();
        }

        private void btnUpdateOrder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOrderID.Text))
            {
                MessageBox.Show("Please select an order to update.");
                return;
            }

            using (MySqlCommand cmd = new MySqlCommand("UPDATE orders SET user_id=@user_id, order_date=@order_date, total_amount=@total_amount WHERE order_id=@id", con))
            {
                cmd.Parameters.AddWithValue("@user_id", txtOrderID.Text);
                cmd.Parameters.AddWithValue("@order_date", txtOrderDate.Text);
                cmd.Parameters.AddWithValue("@total_amount", txtTotalAmount.Text);
                cmd.Parameters.AddWithValue("@id", txtOrderID.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            MessageBox.Show("Order updated successfully!");
            LoadOrders();
            ClearOrderFields();
        }

        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOrderID.Text))
            {
                MessageBox.Show("Please select an order to delete.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this order?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand("DELETE FROM orders WHERE order_id = @id", con))
                    {
                        cmd.Parameters.AddWithValue("@id", txtOrderID.Text);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    MessageBox.Show("Order deleted successfully!");
                    LoadOrders();
                    ClearOrderFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
        }


        // ======================= PAYMENTS  =======================


        private void LoadPayments()
        {
            MySqlDataAdapter sda = new MySqlDataAdapter("SELECT * FROM payments", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvPayments.DataSource = dt;
        }

        private void ClearPaymentFields()
        {
            txtPaymentID.Clear();
            txtPaymentOrderID.Clear();
            txtAmount.Clear();
            txtPaymentDate.Clear();
        }

        private void dgvPayments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPayments.Rows[e.RowIndex];
                txtPaymentID.Text = row.Cells["payment_id"].Value.ToString();
                txtPaymentOrderID.Text = row.Cells["order_id"].Value.ToString();
                txtAmount.Text = row.Cells["amount"].Value.ToString();
                txtPaymentDate.Text = Convert.ToDateTime(row.Cells["payment_date"].Value).ToString("yyyy-MM-dd");
            }
        }

        private void btnAddPayment_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPaymentOrderID.Text) || string.IsNullOrWhiteSpace(txtAmount.Text) || string.IsNullOrWhiteSpace(txtPaymentDate.Text))
            {
                MessageBox.Show("Please fill in all payment details.");
                return;
            }

            using (MySqlCommand cmd = new MySqlCommand("INSERT INTO payments (order_id, amount, payment_date) VALUES (@order_id, @amount, @payment_date)", con))
            {
                cmd.Parameters.AddWithValue("@order_id", txtPaymentOrderID.Text);
                cmd.Parameters.AddWithValue("@amount", txtAmount.Text);
                cmd.Parameters.AddWithValue("@payment_date", txtPaymentDate.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            MessageBox.Show("Payment added successfully!");
            LoadPayments();
            ClearPaymentFields();
        }

        private void btnUpdatePayment_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPaymentID.Text))
            {
                MessageBox.Show("Please select a payment to update.");
                return;
            }

            try
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE payments SET order_id = @order_id, amount = @amount, payment_date = @payment_date WHERE payment_id = @id", con))
                {
                    cmd.Parameters.AddWithValue("@order_id", txtPaymentOrderID.Text);
                    cmd.Parameters.AddWithValue("@amount", txtAmount.Text);
                    cmd.Parameters.AddWithValue("@payment_date", txtPaymentDate.Text);
                    cmd.Parameters.AddWithValue("@id", txtPaymentID.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                MessageBox.Show("Payment updated successfully!");
                LoadPayments();
                ClearPaymentFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

      private void btnDeletePayment_Click(object sender, EventArgs e)
{
    if (string.IsNullOrWhiteSpace(txtPaymentID.Text))
    {
        MessageBox.Show("Please select a payment to delete.");
        return;
    }

    DialogResult result = MessageBox.Show(
        "Are you sure you want to delete this payment?",
        "Confirm Delete",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning
    );

    if (result == DialogResult.Yes)
    {
        try
        {
            using (MySqlCommand cmd = new MySqlCommand("DELETE FROM payments WHERE payment_id = @id", con))
            {
                cmd.Parameters.AddWithValue("@id", txtPaymentID.Text);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Payment deleted successfully!");
            LoadPayments();
            ClearPaymentFields();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
        }
        finally
        {
            if (con != null && con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}


        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

     
    }
}
