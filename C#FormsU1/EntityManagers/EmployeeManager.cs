using GameCenter.DAL;
using GameCenter.DAL.DALErrorHandling;
using GameCenter.Forms;
using GameCenter.Interfaces;
using GameCenter.Models;
using System;
using System.Data;



namespace GameCenter.EntityManagers
{
    internal class EmployeeManager : IEntityManager
    {
        private readonly GameCenterDatabaseContext _context = new();

        public void Load(EmployeeForm form)
        {
            DAL_ExceptionHandler exceptionHandler = new DAL_ExceptionHandler();
            var employees = _context.Employees.Select(e => new { e.EmployeeId, e.Name, e.Adress, e.Phonenumber, e.Email, e.HireDate, e.JobTitle }).ToList();
            var employeeSource = new BindingSource(employees, null);
            form.dataGridView1.DataSource = employeeSource;

            form.textBox1.Enabled = true;
            form.textBox2.Enabled = true;
            form.textBox3.Enabled = true;
            form.textBox4.Enabled = true;
            form.textBox5.Enabled = true;
            form.textBox6.Enabled = false;
            form.textBox1.PlaceholderText = "EmployeeId:";
            form.textBox2.PlaceholderText = "Name:";
            form.textBox3.PlaceholderText = "Address:";
            form.textBox4.PlaceholderText = "PhoneNumber:";
            form.textBox5.PlaceholderText = "Email:";
            form.label1.Text = "->HireDate";
            form.dateTimePicker1.Enabled = true;
            form.comboBoxRole.Enabled = true;
            form.comboBoxRole.Text = ">Select job title<";
            form.CreateButton.Enabled = true;

            List<string> ruleOptions = new List<string>() { ">Select job title<", "Manager", "Cashier", "Technician", "Security Guard", "janitor", "intern", "pleb" };
            form.comboBoxRole.DataSource = ruleOptions;

            if (form.UserRole == "Employee")
            {
                form.CreateButton.Enabled = false;
                form.DeleteButton.Enabled = false;
                form.UpdateButton.Enabled = false;
                form.textBox1.Enabled = true;
                form.textBox2.Enabled = false;
                form.textBox3.Enabled = false;
                form.textBox4.Enabled = false;
                form.textBox5.Enabled = false;
                form.textBox6.Enabled = false;
            }
            form.lblError.Text = exceptionHandler.GetErrorMessage();
        }


        public void Delete(EmployeeForm form)
        {
            ICRUDRepository<Employee> employeeRepository = new EmployeeRepository(_context);
            var selectedId = form.textBox1.Text;

            if (string.IsNullOrWhiteSpace(selectedId))
            {
                MessageBox.Show("Please enter a EmployeeID value in the EmployeeID: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var employee = employeeRepository.GetById(selectedId);

            if (employee == null)
            {
                MessageBox.Show("No employee found with the specified EmployeeID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to permanently delete this employee?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                employeeRepository.Delete(employee);
            }


        }




        public void Create(EmployeeForm form)
        {
            ICRUDRepository<Employee> employeeRepository = new EmployeeRepository(_context);
            var employeeId = form.textBox1.Text;
            var name = form.textBox2.Text;
            var address = form.textBox3.Text;
            var phoneNumber = form.textBox4.Text;
            var email = form.textBox5.Text;
            var selectedRole = form.comboBoxRole.SelectedItem.ToString();
            var selectedDate = form.dateTimePicker1.Value;

            if (string.IsNullOrWhiteSpace(employeeId))
            {
                MessageBox.Show("Please enter a EmployeeID value in the EmployeeID: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (employeeRepository.GetById(employeeId) != null)
            {
                MessageBox.Show("A employee with this ID already exists. Please enter a unique EmployeeID value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please enter a Name value in the Name: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("Please enter a Address value in the Address: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                MessageBox.Show("Please enter a PhoneNumber value in the PhoneNumber: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please enter a Email value in the Email: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (selectedRole == ">Select job title<")
            {
                MessageBox.Show("Please select a JobTitle from the JobTitle combobox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var employee = new Employee
            {
                EmployeeId = employeeId,
                Name = name,
                Adress = address,
                Phonenumber = phoneNumber,
                Email = email,
                HireDate = selectedDate,
                JobTitle = selectedRole
            };

            employeeRepository.Create(employee);
        }

        //create the update method
        public void Update(EmployeeForm form)
        {
            ICRUDRepository<Employee> employeeRepository = new EmployeeRepository(_context);
            var employeeId = form.textBox1.Text;
            var name = form.textBox2.Text;
            var address = form.textBox3.Text;
            var phoneNumber = form.textBox4.Text;
            var email = form.textBox5.Text;
            var hireDate = form.dateTimePicker1.Value;
            var jobTitle = form.comboBoxRole.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(employeeId))
            {
                MessageBox.Show("Please enter a EmployeeID value in the EmployeeID: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var employee = employeeRepository.GetById(employeeId);

            if (employee == null)
            {
                MessageBox.Show("No employee found with the specified EmployeeID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please enter a Name value in the Name: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("Please enter a Address value in the Address: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                MessageBox.Show("Please enter a PhoneNumber value in the PhoneNumber: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please enter a Email value in the Email: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (jobTitle == ">Select job title<")
            {
                MessageBox.Show("Please select a valid JobTitle from the JobTitle combobox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            employee.Name = name;
            employee.Adress = address;
            employee.Phonenumber = phoneNumber;
            employee.Email = email;
            employee.HireDate = hireDate;
            employee.JobTitle = jobTitle;

            employeeRepository.Update(employee);
        }

        public void View(EmployeeForm form)
        {
            ICRUDRepository<Employee> employeeRepository = new EmployeeRepository(_context);
            var selectedId = form.textBox1.Text;

            if (string.IsNullOrWhiteSpace(selectedId))
            {
                MessageBox.Show("Please enter an EmployeeID value in the EmployeeID: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                form.textBox1.Text = "";
                return;
            }

            var employee = employeeRepository.GetById(selectedId);

            if (employee == null)
            {
                MessageBox.Show("No employee found with the specified EmployeeID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var employees = new List<Employee> { employee };
            var employeeSource = new BindingSource(employees.Select(e => new { e.EmployeeId, e.Name, e.Adress, e.Phonenumber, e.Email, e.HireDate, e.JobTitle }).ToList(), null);
            form.dataGridView1.DataSource = employeeSource;
        }


    }
}
