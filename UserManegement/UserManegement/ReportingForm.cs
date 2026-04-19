using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using UserManegement.Models;

namespace UserManegement
{
    public partial class ReportingForm : Form
    {
        private User currentUser;

        public ReportingForm(User user)
        {
            InitializeComponent();
            currentUser = user;
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Text = "Отчёты — " + currentUser.Username;

            
            cmbReportType.Items.Add("Отчёт по задачам");
            cmbReportType.Items.Add("Отчёт по времени");
            cmbReportType.SelectedIndex = 0; 

            UpdateStatus("Выберите тип отчёта и нажмите «Сформировать отчёт»");
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                switch (cmbReportType.SelectedItem.ToString())
                {
                    case "Отчёт по задачам":
                        GenerateTaskReport();
                        break;
                    case "Отчёт по времени":
                        GenerateTimeReport();
                        break;
                }
                UpdateStatus("Отчёт сформирован: " + cmbReportType.SelectedItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateTaskReport()
        {
            // Тестовые данные для отчёта по задачам
            var testTasks = new List<Models.Task>
            {
                new Models.Task { TaskId = 1, Name = "Задача 1", Priority = "Высокий", DueDate = DateTime.Now, IsCompleted = false },
                new Models.Task { TaskId = 2, Name = "Задача 2", Priority = "Средний", DueDate = DateTime.Now.AddDays(-1), IsCompleted = true }
            };

            dgvReport.AutoGenerateColumns = true;
            dgvReport.DataSource = testTasks;
        }

        private void GenerateTimeReport()
        {
            // Тестовые данные для отчёта по времени
            var testTimeEntries = new List<TimeEntry>
            {
                new TimeEntry { EntryId = 1, Date = DateTime.Now.Date, Project = "Проект A", Hours = 2.5, Description = "Работа 1" },
                new TimeEntry { EntryId = 2, Date = DateTime.Now.Date.AddDays(-1), Project = "Проект B", Hours = 4.0, Description = "Работа 2" }
            };

            dgvReport.AutoGenerateColumns = true;
            dgvReport.DataSource = testTimeEntries;
        }

        private void UpdateStatus(string message)
        {
            lblStatus.Text = message;
        }

        private void dgvReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

