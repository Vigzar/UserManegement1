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
    public partial class MainForm : Form
    {
        private User currentUser;

        public MainForm(User user)
        {
            InitializeComponent();
            currentUser = user;
            InitializeMainForm();
            UpdateUI();
        }

        private void InitializeMainForm()
        {
            this.Text = $"Главная форма — {currentUser.Username} ({currentUser.Role.Rolename})";
            statusLabelUser.Text = $"Пользователь: {currentUser.Username} | Роль: {currentUser.Role.Rolename}";
        }

        private void UpdateUI()
        {
            switch (currentUser.Role.Rolename)
            {
                case "Admin":
                    menuItemTimeTracking.Enabled = true;
                    menuItemTaskManagement.Enabled = true;
                    menuItemReporting.Enabled = true;
                    break;
                case "Manager":
                    menuItemTimeTracking.Enabled = true;
                    menuItemTaskManagement.Enabled = true;
                    menuItemReporting.Enabled = true;
                    break;
                case "Employee":
                    menuItemTimeTracking.Enabled = true;
                    menuItemTaskManagement.Enabled = false;
                    menuItemReporting.Enabled = true;
                    break;
            }
        }

        private void menuItemTimeTracking_Click(object sender, EventArgs e)
        {
            ShowChildForm(new TimeTrackingForm(currentUser));
        }

        private void menuItemTaskManagement_Click(object sender, EventArgs e)
        {
            ShowChildForm(childForm: new TaskManagementForm(currentUser));
        }

        private void menuItemReporting_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ReportingForm(currentUser));
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ShowChildForm(Form childForm)
        {
            foreach (Control control in mainPanel.Controls)
            {
                if (control is Form)
                {
                    control.Dispose();
                }
            }

            mainPanel.Controls.Clear();
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(childForm);
            childForm.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Пустой обработчик события рисования
        }
    }
}

