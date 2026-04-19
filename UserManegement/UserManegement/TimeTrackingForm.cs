using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Windows.Forms;
using UserManegement.Models;

namespace UserManegement
{
    public partial class TimeTrackingForm : Form
    {
        private User currentUser;
        private List<TimeEntry> timeEntries;
        private DateTime? timerStartTime;
        private TimeSpan totalTimerTime;

        public TimeTrackingForm(User user)
        {
            InitializeComponent();
            currentUser = user;
            timeEntries = new List<TimeEntry>();
            totalTimerTime = TimeSpan.Zero;

            InitializeForm();
            LoadTimeEntries();
        }

        private void InitializeForm()
        {
            this.Text = $"Учёт времени — {currentUser.Username}";

            cmbProject.Items.AddRange(new object[] { "Проект A", "Проект B", "Прочее" });
            cmbProject.SelectedIndex = 0;

            SetupDataGridView();

            timer1.Interval = 1000;
            timer1.Tick += Timer1_Tick;
        }

        private void SetupDataGridView()
        {
            dgvTimeEntries.AutoGenerateColumns = false;
            dgvTimeEntries.Columns.Clear();

            dgvTimeEntries.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "EntryId",
                HeaderText = "ID",
                Width = 50,
                ReadOnly = true
            });

            dgvTimeEntries.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Date",
                HeaderText = "Дата",
                Width = 80
            });

            dgvTimeEntries.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Project",
                HeaderText = "Проект",
                Width = 120
            });

            dgvTimeEntries.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Hours",
                HeaderText = "Часы",
                Width = 60
            });

            dgvTimeEntries.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Description",
                HeaderText = "Описание",
                Width = 200
            });
        }

        private void LoadTimeEntries()
        {
            try
            {
                timeEntries = new List<TimeEntry>
                {
                    new TimeEntry
            {
                EntryId = 1,
                Date = DateTime.Now.Date,
                Project = "Проект A",
                Hours = 2.5,
                Description = "Разработка функционала"
            },
            new TimeEntry
            {
                EntryId = 2,
                Date = DateTime.Now.Date.AddDays(-1),
                Project = "Проект B",
                Hours = 4.0,
                Description = "Тестирование"
            }
                };

                dgvTimeEntries.DataSource = timeEntries;
                UpdateStatus($"Загружено записей: {timeEntries.Count}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки записей: {ex.Message}",
                    "Ошибка",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
            }
        }

        private void UpdateStatus(string message)
        {
            lblStatus.Text = message;
        }

        private void btnStartTimer_Click(object sender, EventArgs e)
        {
            if (timerStartTime == null)
            {
                timerStartTime = DateTime.Now;
                timer1.Start();
                lblTimer.ForeColor = Color.Green;
                UpdateStatus("Таймер запущен");
            }
        }

        private void btnStopTimer_Click(object sender, EventArgs e)
        {
            if (timerStartTime.HasValue)
            {
                TimeSpan elapsed = DateTime.Now - timerStartTime.Value;
                totalTimerTime += elapsed;

                timer1.Stop();
                timerStartTime = null;
                lblTimer.Text = "00:00:00";
                lblTimer.ForeColor = Color.Black;

                AddTimeEntryFromTimer(elapsed);
                UpdateStatus("Таймер остановлен и запись добавлена");
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (timerStartTime.HasValue)
            {
                TimeSpan currentElapsed = DateTime.Now - timerStartTime.Value + totalTimerTime;
                lblTimer.Text = currentElapsed.ToString(@"hh\:mm\:ss");
            }
        }

        private void AddTimeEntryFromTimer(TimeSpan elapsed)
        {
            var newEntry = new TimeEntry
            {
                Date = DateTime.Now.Date,
                Project = cmbProject.SelectedItem.ToString(),
                Hours = elapsed.TotalHours,
                Description = $"Работа по таймеру: {elapsed.ToString(@"hh\:mm")}"
            };

            timeEntries.Add(newEntry);
            dgvTimeEntries.DataSource = null;
            dgvTimeEntries.DataSource = timeEntries;
        }

        private void btnAddEntry_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(txtHours.Text, out double hours) || hours <= 0)
            {
                MessageBox.Show("Введите корректное количество часов", "Внимание",
                    MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
                return;
            }

            var newEntry = new TimeEntry
            {
                Date = dtpDate.Value.Date,
                Project = cmbProject.SelectedItem.ToString(),
                Hours = hours,
                Description = txtDescription.Text.Trim()
            };

            timeEntries.Add(newEntry);
            dgvTimeEntries.DataSource = null;
            dgvTimeEntries.DataSource = timeEntries;
            UpdateStatus($"Добавлена запись: {hours} часов на {newEntry.Project}");

            ClearInputFields();
        }

        private void ClearInputFields()
        {
            txtHours.Clear();
            txtDescription.Clear();
            dtpDate.Value = DateTime.Now.Date;
            cmbProject.SelectedIndex = 0;
        }

        private void btnDeleteEntry_Click(object sender, EventArgs e)
        {
            if (dgvTimeEntries.SelectedRows.Count > 0)
            {
                var selectedEntry = (TimeEntry)dgvTimeEntries.SelectedRows[0].DataBoundItem;
                if (MessageBox.Show($"Удалить запись: {selectedEntry.Hours} часов на {selectedEntry.Project}?",
                    "Подтверждение удаления",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    timeEntries.Remove(selectedEntry);
                    dgvTimeEntries.DataSource = null;
                    dgvTimeEntries.DataSource = timeEntries;
                    UpdateStatus($"Удалена запись: {selectedEntry.Hours} часов на {selectedEntry.Project}");
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления", "Внимание");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadTimeEntries();
            UpdateStatus("Данные обновлены");
        }

        private void dgvTimeEntries_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedEntry = (TimeEntry)dgvTimeEntries.Rows[e.RowIndex].DataBoundItem;

                dtpDate.Value = selectedEntry.Date;
                cmbProject.SelectedItem = selectedEntry.Project;
                txtHours.Text = selectedEntry.Hours.ToString();
                txtDescription.Text = selectedEntry.Description;

                timeEntries.Remove(selectedEntry);
                dgvTimeEntries.DataSource = null;
                dgvTimeEntries.DataSource = timeEntries;

                UpdateStatus($"Редактирование записи: {selectedEntry.Project}");
            }
        }

        private void UpdateTotalHours()
        {
            double totalHours = timeEntries.Sum(entry => entry.Hours);
            lblStatus.Text = $"Загружено записей: {timeEntries.Count}, Всего часов: {totalHours:F1}";
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
            }
            base.OnFormClosing(e);
        }

        private void btnStopTimer_Click_1(object sender, EventArgs e)
        {

        }

        private void lblTimer_Click(object sender, EventArgs e)
        {

        }

        private void btnStartTimer_Click_1(object sender, EventArgs e)
        {

        }

        private void dgvTimeEntries_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
