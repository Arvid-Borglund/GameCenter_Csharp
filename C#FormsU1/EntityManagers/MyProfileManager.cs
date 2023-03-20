using GameCenter.DAL;
using GameCenter.DAL.DALErrorHandling;
using GameCenter.Forms;
using GameCenter.Interfaces;
using GameCenter.Models;

namespace GameCenter.EntityManagers
{
    internal class MyProfileManager : IEntityManager, IEntityManagerCustomer
    {
        private readonly GameCenterDatabaseContext _context = new();

        public void Load(EmployeeForm form)
        {
            DAL_ExceptionHandler exceptionHandler = new DAL_ExceptionHandler();
            GameCenterDatabaseContext context = new GameCenterDatabaseContext();
            ICRUDRepository<Employee> employeeRepository = new EmployeeRepository(context);

            var employee = employeeRepository.GetById(form.UserId);
            if (employee == null)
            {
                MessageBox.Show("No employee found with the provided ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var employees = new List<Employee> { employee };
            var employeeSource = new BindingSource(employees.Select(e => new { e.EmployeeId, e.Name, e.Adress, e.Phonenumber, e.Email, e.HireDate, e.JobTitle }).ToList(), null);
            form.dataGridView1.DataSource = employeeSource;
            List<string> profileOptions = new List<string>() { ">Your options<", "Your schedule", "Profile info" };
            form.comboBoxRole.DataSource = profileOptions;

            form.textBox1.Enabled = false;
            form.textBox2.Enabled = true;
            form.textBox3.Enabled = true;
            form.textBox4.Enabled = true;
            form.textBox5.Enabled = true;
            form.textBox6.Enabled = false;
            form.textBox2.PlaceholderText = "Name:";
            form.textBox3.PlaceholderText = "Adress:";
            form.textBox4.PlaceholderText = "Phonenumber:";
            form.textBox5.PlaceholderText = "Email:";
            form.dateTimePicker1.Enabled = false;
            form.comboBoxRole.Enabled = true;
            form.comboBoxRole.Text = ">Your options<";
            form.CreateButton.Enabled = false;
            form.DeleteButton.Enabled = true;
            form.UpdateButton.Enabled = true;
            form.lblError.Text = exceptionHandler.GetErrorMessage();
        }

        public void Load(CustomerForm form)
        {
            DAL_ExceptionHandler exceptionHandler = new DAL_ExceptionHandler();
            GameCenterDatabaseContext context = new GameCenterDatabaseContext();
            ICRUDRepository<Customer> customerRepository = new CustomerRepository(context);

            var customer = customerRepository.GetById(form.UserId);
            if (customer == null)
            {
                MessageBox.Show("No customer found with the provided ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var customers = new List<Customer> { customer };
            var customerSource = new BindingSource(customers.Select(c => new { c.CustomerId, c.Name, c.Adress, c.Phonenumber, c.Email, c.LoyaltyLevel }).ToList(), null);
            form.dataGridView1.DataSource = customerSource;
            List<string> profileOptions = new List<string>() { ">Your options<", "Your bookings", "Profile info" };
            form.comboBoxRole.DataSource = profileOptions;

            form.textBox2.Enabled = true;
            form.textBox3.Enabled = true;
            form.textBox4.Enabled = true;
            form.textBox5.Enabled = true;
            form.textBox2.PlaceholderText = "Name:";
            form.textBox3.PlaceholderText = "Adress:";
            form.textBox4.PlaceholderText = "Phonenumber:";
            form.textBox5.PlaceholderText = "Email:";
            form.comboBoxRole.Enabled = true;
            form.comboBoxRole.Text = ">Your options<";
            form.DeleteButton.Enabled = true;
            form.UpdateButton.Enabled = true;
            form.ViewButton.Enabled = true;
            form.lblError.Text = exceptionHandler.GetErrorMessage();
        }




        public void Create(CustomerForm form)
        {
            throw new NotImplementedException();
        }
 

        public void Create(EmployeeForm form)
        {
            throw new NotImplementedException();
        }

        public void Update(EmployeeForm form)
        {
            GameCenterDatabaseContext context = new GameCenterDatabaseContext();
            ICRUDRepository<Employee> employeeRepository = new EmployeeRepository(context);
            ICRUDRepository<Customer> customerRepository = new CustomerRepository(context);

            if (form.UserRole == "Employee" || form.UserRole == "Admin")
            {
                var employee = employeeRepository.GetById(form.UserId);
                if (employee == null)
                {
                    MessageBox.Show("No employee found with the provided ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(form.textBox2.Text))
                {
                    MessageBox.Show("Please enter a Name value in the Name: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(form.textBox3.Text))
                {
                    MessageBox.Show("Please enter a Address value in the Address: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(form.textBox4.Text))
                {
                    MessageBox.Show("Please enter a PhoneNumber value in the PhoneNumber: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(form.textBox5.Text))
                {
                    MessageBox.Show("Please enter a Email value in the Email: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                employee.Name = form.textBox2.Text;
                employee.Adress = form.textBox3.Text;
                employee.Phonenumber = form.textBox4.Text;
                employee.Email = form.textBox5.Text;

                employeeRepository.Update(employee);
            }
        }

        public void Update(CustomerForm form)
        {
            GameCenterDatabaseContext context = new GameCenterDatabaseContext();
            ICRUDRepository<Customer> customerRepository = new CustomerRepository(context);

            var customerId = form.UserId;
            var name = form.textBox2.Text;
            var address = form.textBox3.Text;
            var phoneNumber = form.textBox4.Text;
            var email = form.textBox5.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please enter a value in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var customer = customerRepository.GetById(customerId);

            if (customer == null)
            {
                MessageBox.Show("No customer found with the specified CustomerID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            customer.Name = name;
            customer.Adress = address;
            customer.Phonenumber = phoneNumber;
            customer.Email = email;

            customerRepository.Update(customer);
        }

        public void Delete(EmployeeForm form)
        {
            GameCenterDatabaseContext context = new GameCenterDatabaseContext();
            ICRUDRepository<Employee> employeeRepository = new EmployeeRepository(context);

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this profile and the associated login account permanently? The application will also sign out.", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            var employee = employeeRepository.GetById(form.UserId);
            if (employee == null)
            {
                MessageBox.Show("No employee found with the specified EmployeeID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            employeeRepository.Delete(employee);

            MessageBox.Show("Profile and associated login account deleted successfully! The application will now sign out.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            form.Close();
        }

        public void Delete(CustomerForm form)
        {
            GameCenterDatabaseContext context = new GameCenterDatabaseContext();
            ICRUDRepository<Customer> customerRepository = new CustomerRepository(context);

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this profile and the associated login account permanently? The application will also sign out.", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            var customer = customerRepository.GetById(form.UserId);
            if (customer == null)
            {
                MessageBox.Show("No customer found with the specified CustomerID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            customerRepository.Delete(customer);

            MessageBox.Show("Profile and associated login account deleted successfully! The application will now sign out.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            form.Close();
        }

        public void View(EmployeeForm form)
        {
            GameCenterDatabaseContext context = new GameCenterDatabaseContext();
            ICRUDRepository<Employee> employeeRepository = new EmployeeRepository(context);
            ICRUDRepository<EmployeeSchedule> employeeScheduleRepository = new EmployeeScheduleRepository(context);

            if (form.comboBoxRole.SelectedItem.ToString() == "Profile info")
            {
                var employee = employeeRepository.GetById(form.UserId);
                if (employee == null)
                {
                    MessageBox.Show("No employee found with the provided ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var employees = new List<Employee> { employee };
                var employeeSource = new BindingSource(employees.Select(e => new { e.EmployeeId, e.Name, e.Adress, e.Phonenumber, e.Email, e.HireDate, e.JobTitle }).ToList(), null);
                form.dataGridView1.DataSource = employeeSource;
            }
            if (form.comboBoxRole.SelectedItem.ToString() == "Your schedule")
            {
                var employeeSchedules = employeeScheduleRepository.GetAll().Where(s => s.EmployeeId == form.UserId).ToList();
                var employeeScheduleSource = new BindingSource(employeeSchedules.Select(s => new { s.ShiftDate, s.ShiftResponsibilities }).ToList(), null);
                form.dataGridView1.DataSource = employeeScheduleSource;
            }
        }


        public void View(CustomerForm form)
        {
            GameCenterDatabaseContext context = new GameCenterDatabaseContext();
            ICRUDRepository<Customer> customerRepository = new CustomerRepository(context);

            var customer = customerRepository.GetById(form.UserId);
            if (customer == null)
            {
                MessageBox.Show("No customer found with the provided ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (form.comboBoxRole.SelectedItem.ToString() == "Profile info")
            {
                var customers = new List<Customer> { customer };
                var customerSource = new BindingSource(customers.Select(c => new { c.CustomerId, c.Name, c.Adress, c.Phonenumber, c.Email, c.LoyaltyLevel }).ToList(), null);
                form.dataGridView1.DataSource = customerSource;
            }
            else if (form.comboBoxRole.SelectedItem.ToString() == "Your bookings")
            {
                ICRUDRepository<Reservation> reservationRepository = new ReservationRepository(context);

                var reservations = reservationRepository.GetAll().Where(r => r.CustomerId == customer.CustomerId).ToList();
                if (reservations.Count == 0)
                {
                    MessageBox.Show("No bookings found for this customer.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var reservationSource = new BindingSource(reservations.Select(r => new { r.ComputerId, r.TimeDate }).ToList(), null);
                form.dataGridView1.DataSource = reservationSource;
            }
        }


    }
}

