using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using UserManegement.Models;

namespace UserManegement
{
    public partial class LoginForm : Form
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=UserManagementApp;Integrated Security=True;TrustServerCertificate=True;";

        public LoginForm()
        {
            InitializeComponent();
            LoadRoles();
            TestDatabaseConnection();
        }

        private void LoadRoles()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT RoleId, RoleName FROM Roles ORDER BY RoleName", conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable rolesTable = new DataTable();
                    adapter.Fill(rolesTable);

                    cmbRoles.DataSource = rolesTable;
                    cmbRoles.DisplayMember = "RoleName";
                    cmbRoles.ValueMember = "RoleId";
                    cmbRoles.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка загрузки ролей: {ex.Message}");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            int roleId = (int)cmbRoles.SelectedValue;
            if (AuthenticateUser(txtUsername.Text.Trim(), txtPassword.Text, roleId, out User user))
            {
                LogAuditAction("Login", $"User {user.Username} (Role: {user.Role.Rolename}) logged in");
                new MainForm(user).Show();
                this.Hide();
            }
            else
            {
                SetStatus("Неверные учётные данные или роль", Color.Red);
            }
        }

        private bool ValidateInput()
        {
            string username = txtUsername.Text.Trim();
            if (string.IsNullOrEmpty(username))
            {
                SetStatus("Введите логин пользователя", Color.Red);
                txtUsername.Focus();
                return false;
            }

            if (cmbRoles.SelectedValue == null)
            {
                SetStatus("Выберите роль", Color.Red);
                cmbRoles.Focus();
                return false;
            }

            return true;
        }

        private bool AuthenticateUser(string username, string password, int roleId, out User user)
        {
            user = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"SELECT u.UserId, u.Username, u.PasswordHash, r.RoleId, r.RoleName
                  FROM Users u
          INNER JOIN Roles r ON u.Role = r.RoleName
          WHERE u.Username = @Username AND r.RoleId = @RoleId";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@RoleId", roleId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedPasswordHash = reader.GetString(2);
                            if (password == storedPasswordHash)
                            {
                                user = new User
                                {
                                    UserId = reader.GetInt32(0),
                                    Username = reader.GetString(1),
                                    Role = new Role
                                    {
                                        RoleId = reader.GetInt32(3),
                                        Rolename = reader.GetString(4)
                                    }
                                };
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка аутентификации: {ex.Message}");
            }
            return false;
        }

        private void LogAuditAction(string actionType, string details)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "INSERT INTO AuditLog (UserId, ActionType, Details) VALUES (@UserId, @ActionType, @Details)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    int? userId = GetUserIdByUsername(txtUsername.Text);
                    cmd.Parameters.AddWithValue("@UserId", userId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ActionType", actionType);
                    cmd.Parameters.AddWithValue("@Details", details);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка записи в журнал аудита: {ex.Message}");
            }
        }

        private int? GetUserIdByUsername(string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT UserId FROM Users WHERE Username = @Username", conn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    object result = cmd.ExecuteScalar();
                    return result != DBNull.Value ? (int)result : (int?)null;
                }
            }
            catch
            {
                return null;
            }
        }

        private bool TestDatabaseConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SetStatus("Готов к работе", Color.Green);
                    return true;
                }
            }
            catch
            {
                SetStatus("Ошибка подключения к базе данных", Color.Red);
                btnLogin.Enabled = false;
                return false;
            }
        }

        private void SetStatus(string message, Color color)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = color;
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "userManagementDataSet.Roles". При необходимости она может быть перемещена или удалена.
            this.rolesTableAdapter1.Fill(this.userManagementDataSet.Roles);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "userManegementAppDataSet.Users". При необходимости она может быть перемещена или удалена.
            this.usersTableAdapter.Fill(this.userManegementAppDataSet.Users);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "userManegementAppDataSet.Roles". При необходимости она может быть перемещена или удалена.
            this.rolesTableAdapter.Fill(this.userManegementAppDataSet.Roles);

        }
    }
}
