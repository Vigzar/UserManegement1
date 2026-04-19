using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserManegement.Models;

namespace UserManegement
{
    public partial class TaskManagementForm : Form
    {
        private User currentUser;
        private List<Models.Task> tasks;

        public TaskManagementForm(User user)
        {
            InitializeComponent();
            currentUser = user;
            tasks = new List<Models.Task>();
            InitializeForm();
            LoadTasks();
        }

        private void InitializeForm()
        {
            this.Text = $"Управление задачами — {currentUser.Username}";

            // Настройка ComboBox приоритетов
            cmbPriority.Items.AddRange(new object[] { "Высокий", "Средний", "Низкий" });
            cmbPriority.SelectedIndex = 1; // По умолчанию «Средний»

            // Настройка DataGridView
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            dgvTasks.AutoGenerateColumns = false;
            dgvTasks.Columns.Clear();

            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TaskId",
                HeaderText = "ID",
                Width = 50,
                ReadOnly = true
            });

            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Название задачи",
                Width = 200
            });

            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Priority",
                HeaderText = "Приоритет",
                Width = 80
            });

            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DueDate",
                HeaderText = "Срок",
                Width = 100
            });

            dgvTasks.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsCompleted",
                HeaderText = "Выполнено",
                Width = 60
            });
        }

        private void LoadTasks()
        {
            try
            {
                tasks = new List<Models.Task>
                {
                    new Models.Task { TaskId = 1, Name = "Подготовить отчёт", Priority = "Высокий", DueDate = DateTime.Now.AddDays(2), IsCompleted = false },
            new Models.Task { TaskId = 2, Name = "Провести встречу", Priority = "Средний", DueDate = DateTime.Now.AddDays(1), IsCompleted = true },
            new Models.Task { TaskId = 3, Name = "Обновить документацию", Priority = "Низкий", DueDate = DateTime.Now.AddDays(5), IsCompleted = false }
                };

                dgvTasks.DataSource = tasks;
                UpdateStatus($"Загружено задач: {tasks.Count}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки задач: {ex.Message}",
                    "Ошибка",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
            }
        }

        private void UpdateStatus(string message)
        {
            lblStatus.Text = message;
        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaskName.Text))
            {
                MessageBox.Show("Введите название задачи", "Внимание",
                    MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
                return;
            }

            var newTask = new Models.Task
            {
                Name = txtTaskName.Text.Trim(),
                Priority = cmbPriority.SelectedItem.ToString(),
                DueDate = dtpDueDate.Value,
                IsCompleted = false
            };

            tasks.Add(newTask);
            dgvTasks.DataSource = null;
            dgvTasks.DataSource = tasks;
            UpdateStatus($"Добавлена задача: {newTask.Name}");

            ClearInputFields();
        }

        private void ClearInputFields()
        {
            txtTaskName.Clear();
            cmbPriority.SelectedIndex = 1;
            dtpDueDate.Value = DateTime.Now.AddDays(1);
        }

        private void btnDeleteTask_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count > 0)
            {
                var selectedTask = (Models.Task)dgvTasks.SelectedRows[0].DataBoundItem;
                if (MessageBox.Show($"Удалить задачу: {selectedTask.Name}?",
                    "Подтверждение удаления",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    tasks.Remove(selectedTask);
                    dgvTasks.DataSource = null;
                    dgvTasks.DataSource = tasks;
                    UpdateStatus($"Удалена задача: {selectedTask.Name}");
                }
            }
            else
            {
                MessageBox.Show("Выберите задачу для удаления", "Внимание");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadTasks();
        }

        private void dgvTasks_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvTasks.Columns["IsCompleted"].Index)
            {
                var task = (Models.Task)dgvTasks.Rows[e.RowIndex].DataBoundItem;
                UpdateStatus($"Статус задачи изменён: {task.Name}");
            }
        }
    }
}
