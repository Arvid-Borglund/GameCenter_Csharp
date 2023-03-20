using GameCenter.EntityManagers.EntityManagerFactory;
using GameCenter.Interfaces;
using GameCenter.Models;
using System.Windows.Forms;
using TextBox = System.Windows.Forms.TextBox;

namespace GameCenter.Forms
{
    public partial class EmployeeForm : Form
    {
        public EmployeeForm()
        {

            InitializeComponent();
            
            GameCenterDatabaseContext context = new GameCenterDatabaseContext();
            List<string> entityOptions = new List<string>() { "Customer", "My Profile", "Employee", "Computer", "EmployeeSchedule", "Game", "Login", "Reservation" };
            entityComboBox.DataSource = entityOptions;

        }
        public string UserRole { get; set; }
        public string UserId { get; set; }

        private string _selectedEntity = "Customer";

        private void entityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearTextBoxes();
            _selectedEntity = entityComboBox.SelectedItem.ToString();

            if (_selectedEntity == "Login" && UserRole != "Admin")
            {
                MessageBox.Show("Only Admins can access the login-credentials page. Please select another page.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                entityComboBox.SelectedIndex = 0;
                return;
            }

            IEntityManager entityManager = EntityManagerFactory.GetEntityManager(_selectedEntity);
            entityManager.Load(this);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {

            IEntityManager entityManager = EntityManagerFactory.GetEntityManager(_selectedEntity);
            entityManager.Delete(this);
            entityManager.Load(this);
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {

            IEntityManager entityManager = EntityManagerFactory.GetEntityManager(_selectedEntity);
            entityManager.Create(this);
            entityManager.Load(this);

        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {

            IEntityManager entityManager = EntityManagerFactory.GetEntityManager(_selectedEntity);
            entityManager.Update(this);
            entityManager.Load(this);
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {

            IEntityManager entityManager = EntityManagerFactory.GetEntityManager(_selectedEntity);
            entityManager.View(this);

        }

        public void ClearTextBoxes()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    TextBox textBox = (TextBox)control;
                    textBox.Clear();
                    textBox.PlaceholderText = "";
                    label1.Text = "";
                }
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    TextBox textBox = (TextBox)control;
                    textBox.Clear();
                    IEntityManager entityManager = EntityManagerFactory.GetEntityManager(_selectedEntity);
                    entityManager.Load(this);
                }
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string cellValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                listBox1.Items.Add(cellValue);
                Clipboard.SetText(cellValue);
            }
        }

        private void EraseNotesButton_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clipboard.SetText(Text = listBox1.SelectedItem.ToString());
        }
    }

}




