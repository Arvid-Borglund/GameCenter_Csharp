using GameCenter.EntityManagers.EntityManagerFactory;
using GameCenter.Interfaces;
using GameCenter.Models;
using System.Windows.Forms;
using TextBox = System.Windows.Forms.TextBox;


namespace GameCenter.Forms
{
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();
            GameCenterDatabaseContext context = new GameCenterDatabaseContext();
            List<string> entityOptions = new List<string>() { "Games", "My Profile & bookings" };
            entityComboBox.DataSource = entityOptions;
        }

        public string UserRole { get; set; }
        public string UserId { get; set; }

        private string _selectedEntity = "Games";

        private void entityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearTextBoxes();
            _selectedEntity = entityComboBox.SelectedItem.ToString();
            IEntityManagerCustomer entityManager = EntityManagerFactory.GetEntityManagerCustomer(_selectedEntity);
            entityManager.Load(this);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {

            IEntityManagerCustomer entityManager = EntityManagerFactory.GetEntityManagerCustomer(_selectedEntity);
            entityManager.Delete(this);
            entityManager.Load(this);
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {

            IEntityManagerCustomer entityManager = EntityManagerFactory.GetEntityManagerCustomer(_selectedEntity);
            entityManager.Create(this);
            entityManager.Load(this);

        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {

            IEntityManagerCustomer entityManager = EntityManagerFactory.GetEntityManagerCustomer(_selectedEntity);
            entityManager.Update(this);
            entityManager.Load(this);
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {

            IEntityManagerCustomer entityManager = EntityManagerFactory.GetEntityManagerCustomer(_selectedEntity);
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
                    IEntityManagerCustomer entityManager = EntityManagerFactory.GetEntityManagerCustomer(_selectedEntity);
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
