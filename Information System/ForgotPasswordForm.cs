using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Information_System
{
    public partial class ForgotPasswordForm : Form
    {
        string connectionString = "server=localhost;port=3306;database=information system;uid=root;pwd=Caceres25_;";
        string correctAnswer = "";
        string usernameOrEmail = "";

        public ForgotPasswordForm()
        {
            InitializeComponent();
            lblRecoveryQuestion.Text = ""; // Hide question initially
        }

        private void btnSubmitAnswer_Click(object sender, EventArgs e)
        {
            usernameOrEmail = txtUsername.Text.Trim();

            if (string.IsNullOrEmpty(usernameOrEmail))
            {
                MessageBox.Show("Please enter your username or email.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT recovery_question, recovery_answer FROM users WHERE username = @user OR email = @user";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@user", usernameOrEmail);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lblRecoveryQuestion.Text = reader["recovery_question"].ToString();
                        correctAnswer = reader["recovery_answer"].ToString();
                    }
                    else
                    {
                        lblRecoveryQuestion.Text = "";
                        MessageBox.Show("User not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message);
                }
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string answer = txtAnswer.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrEmpty(lblRecoveryQuestion.Text))
            {
                MessageBox.Show("Please submit your username or email first.");
                return;
            }

            if (answer != correctAnswer)
            {
                MessageBox.Show("Incorrect answer to the recovery question.");
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("Password cannot be empty.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string updateQuery = "UPDATE users SET password = @password WHERE username = @user OR email = @user";
                    MySqlCommand cmd = new MySqlCommand(updateQuery, conn);
                    cmd.Parameters.AddWithValue("@password", newPassword);
                    cmd.Parameters.AddWithValue("@user", usernameOrEmail);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Password has been reset successfully!");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to reset password. Please try again.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message);
                }
            }
        }
    }
}
