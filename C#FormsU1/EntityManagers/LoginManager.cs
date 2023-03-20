using GameCenter.DAL;
using GameCenter.DAL.DALErrorHandling;
using GameCenter.Forms;
using GameCenter.Interfaces;
using GameCenter.Models;

namespace GameCenter.EntityManagers
{
    internal class LoginManager : IEntityManager
    {
        private readonly GameCenterDatabaseContext _context = new();
        public void Load(EmployeeForm form)
        {
            DAL_ExceptionHandler exceptionHandler = new DAL_ExceptionHandler();
            var logins = _context.Logins.Select(l => new { l.LoginId, l.Password, l.CustomerId, l.EmployeeId, l.AccessLevel }).ToList();
            var loginSource = new BindingSource(logins, null);
            form.dataGridView1.DataSource = loginSource;

            form.textBox1.Enabled = true;
            form.textBox2.Enabled = true;
            form.textBox3.Enabled = true;
            form.textBox4.Enabled = true;
            form.textBox5.Enabled = false;
            form.textBox6.Enabled = false;
            form.textBox1.PlaceholderText = "LoginID:";
            form.textBox2.PlaceholderText = "Password:";
            form.textBox3.PlaceholderText = "CustomerID:";
            form.textBox4.PlaceholderText = "EmployeeID:";
            form.textBox6.Enabled = false;
            form.dateTimePicker1.Enabled = false;
            form.comboBoxRole.Enabled = true;
            form.CreateButton.Enabled = true;
            form.DeleteButton.Enabled = true;
            form.UpdateButton.Enabled = true;

            List<string> ruleOptions = new List<string>() { ">Select access level<", "Admin", "Employee", "Customer" };
            form.comboBoxRole.DataSource = ruleOptions;
            form.lblError.Text = exceptionHandler.GetErrorMessage();
        }

        public void Create(EmployeeForm form)
        {
            ICRUDRepository<Login> loginRepository = new LoginRepository(_context);
            ICRUDRepository<Customer> customerRepository = new CustomerRepository(_context);
            ICRUDRepository<Employee> employeeRepository = new EmployeeRepository(_context);

            var loginId = form.textBox1.Text;
            var password = form.textBox2.Text;
            var customerId = form.textBox3.Text;
            var employeeId = form.textBox4.Text;
            var accessLevel = form.comboBoxRole.SelectedItem.ToString();

            if (accessLevel == ">Select access level<")
            {
                MessageBox.Show("Please select a access level from the access level: combobox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(loginId))
            {
                MessageBox.Show("Please enter a Login ID value in the Login ID: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter a Password value in the Password: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var customer = string.IsNullOrWhiteSpace(customerId) ? null : customerRepository.GetById(customerId);
            if (customer == null && !string.IsNullOrWhiteSpace(customerId))
            {
                MessageBox.Show("The Customer ID you entered does not exist. Please enter a valid Customer ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var employee = string.IsNullOrWhiteSpace(employeeId) ? null : employeeRepository.GetById(employeeId);
            if (employee == null && !string.IsNullOrWhiteSpace(employeeId))
            {
                MessageBox.Show("The Employee ID you entered does not exist. Please enter a valid Employee ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var existingLogin = loginRepository.GetById(loginId);
            if (existingLogin != null)
            {
                MessageBox.Show("The Login ID you entered already exists. Please enter a unique Login ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var login = new Login
            {
                LoginId = loginId,
                Password = password,
                Customer = customer,
                Employee = employee,
                AccessLevel = accessLevel
            };

            loginRepository.Create(login);
        }






        public void Delete(EmployeeForm form)
        {
            ICRUDRepository<Login> loginRepository = new LoginRepository(_context);
            var selectedId = form.textBox1.Text;

            if (string.IsNullOrWhiteSpace(selectedId))
            {
                MessageBox.Show("Please enter a LoginID value in the LoginID: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var login = loginRepository.GetById(selectedId);

            if (login == null)
            {
                MessageBox.Show("No login found with the specified LoginID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            var result = MessageBox.Show("Are you sure you want to permanently delete this GameCenter login?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                loginRepository.Delete(login);
            }
            
        }


        public void Update(EmployeeForm form)
        {
            ICRUDRepository<Login> loginRepository = new LoginRepository(_context);

            var loginId = form.textBox1.Text;
            var password = form.textBox2.Text;
            var accessLevel = form.comboBoxRole.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(loginId))
            {
                MessageBox.Show("Please enter a Login ID value in the Login ID: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter a Password value in the Password: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (accessLevel == ">Select access level<")
            {
                MessageBox.Show("Please select a accessLevel value in the accessLevel: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var login = loginRepository.GetById(loginId);
            if (login == null)
            {
                MessageBox.Show("The Login ID you entered does not exist. Please enter a valid Login ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrWhiteSpace(form.textBox3.Text))
            {
                MessageBox.Show("The Customer ID cannot be updated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (!string.IsNullOrWhiteSpace(form.textBox4.Text))
            {
                MessageBox.Show("The Employee ID cannot be updated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            login.Password = password;
            login.AccessLevel = accessLevel;

            loginRepository.Update(login);
        }

        public void View(EmployeeForm form)
        {
            ICRUDRepository<Login> loginRepository = new LoginRepository(_context);
            var selectedId = form.textBox1.Text;

            if (string.IsNullOrWhiteSpace(selectedId))
            {
               
                form.textBox1.Clear();
                return;
            }

            var login = loginRepository.GetById(selectedId);

            if (login == null)
            {
                MessageBox.Show("No login found with the specified LoginID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var logins = new List<Login> { login };
            var loginSource = new BindingSource(logins.Select(l => new { l.LoginId, l.Password, l.CustomerId, l.EmployeeId, l.AccessLevel }).ToList(), null);
            form.dataGridView1.DataSource = loginSource;

        }

    }
}
