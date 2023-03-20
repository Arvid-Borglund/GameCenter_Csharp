using GameCenter.DAL;
using GameCenter.DAL.DALErrorHandling;
using GameCenter.Forms;
using GameCenter.Interfaces;
using GameCenter.Models;
using Microsoft.VisualBasic.Devices;
using System.Data;

namespace GameCenter.EntityManagers
{
    internal class CustomerManager : IEntityManager
    {
        private readonly GameCenterDatabaseContext _context = new();

        public void Load(EmployeeForm form)
        {
            DAL_ExceptionHandler exceptionHandler = new DAL_ExceptionHandler();
            var customers = _context.Customers.Select(c => new { c.CustomerId, c.Name, c.Adress, c.Phonenumber, c.Email, c.LoyaltyLevel }).ToList();
            var customerSource = new BindingSource(customers, null);
            form.dataGridView1.DataSource = customerSource;

            form.textBox1.Enabled = true;
            form.textBox2.Enabled = true;
            form.textBox3.Enabled = true;
            form.textBox4.Enabled = true;
            form.textBox5.Enabled = true;
            form.textBox6.Enabled = true;
            form.textBox1.PlaceholderText = "CustomerID:";
            form.textBox2.PlaceholderText = "Name:";
            form.textBox3.PlaceholderText = "Adress:";
            form.textBox4.PlaceholderText = "Phonenumber:";
            form.textBox5.PlaceholderText = "Email:";
            form.textBox6.PlaceholderText = "LoyaltyLevel:";
            form.dateTimePicker1.Enabled = false;
            form.comboBoxRole.Enabled = false;
            form.comboBoxRole.Text = "";
            form.CreateButton.Enabled = true;
            form.DeleteButton.Enabled = true;
            form.UpdateButton.Enabled = true;
            form.lblError.Text = exceptionHandler.GetErrorMessage();
        }

        public void Delete(EmployeeForm form)
        {
            ICRUDRepository<Customer> customerRepository = new CustomerRepository(_context);
            var selectedId = form.textBox1.Text;

            if (string.IsNullOrWhiteSpace(selectedId))
            {
                MessageBox.Show("Please enter a CustomerID value in the CustomerID: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var customer = customerRepository.GetById(selectedId);

            if (customer == null)
            {
                MessageBox.Show("No customer found with the specified CustomerID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to permanently delete this customer?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                customerRepository.Delete(customer);
            }                        

        }

        public void Create(EmployeeForm form)
        {
            ICRUDRepository<Customer> customerRepository = new CustomerRepository(_context);
            var customerId = form.textBox1.Text;
            var name = form.textBox2.Text;
            var address = form.textBox3.Text;
            var phoneNumber = form.textBox4.Text;
            var email = form.textBox5.Text;
            var loyaltyLevel = form.textBox6.Text;

            if (string.IsNullOrWhiteSpace(customerId) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(loyaltyLevel))
            {
                MessageBox.Show("Please enter a value in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (customerRepository.GetById(customerId) != null)
            {
                MessageBox.Show("A customer with the same CustomerId already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(loyaltyLevel, out int loyaltyLevelValue))
            {
                MessageBox.Show("Please enter a valid integer value for Loyalty Level.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var customer = new Customer
            {
                CustomerId = customerId,
                Name = name,
                Adress = address,
                Phonenumber = phoneNumber,
                Email = email,
                LoyaltyLevel = loyaltyLevelValue
            };

            customerRepository.Create(customer);
            
        }


        

        // create the update method for customer
        public void Update(EmployeeForm form)
        {
            ICRUDRepository<Customer> customerRepository = new CustomerRepository(_context);
            var customerId = form.textBox1.Text;
            var name = form.textBox2.Text;
            var address = form.textBox3.Text;
            var phoneNumber = form.textBox4.Text;
            var email = form.textBox5.Text;
            var loyaltyLevel = form.textBox6.Text;

            if (string.IsNullOrWhiteSpace(customerId) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(loyaltyLevel))
            {
                MessageBox.Show("Please enter a value in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(loyaltyLevel, out int loyaltyLevelValue))
            {
                MessageBox.Show("Please enter a valid integer value for Loyalty Level.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            customer.LoyaltyLevel = loyaltyLevelValue;

            customerRepository.Update(customer);
            
        }

        // create a view method for customer
        public void View(EmployeeForm form)
        {
            ICRUDRepository<Customer> customerRepository = new CustomerRepository(_context);
            var selectedId = form.textBox1.Text;

            if (string.IsNullOrWhiteSpace(selectedId))
            {
                MessageBox.Show("Please enter a CustomerID value in the CustomerID: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              
                form.textBox1.Text = "";
                return;
            }

            var customer = customerRepository.GetById(selectedId);

            if (customer == null)
            {
                MessageBox.Show("No customer found with the specified CustomerID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var customers = new List<Customer> { customer };
            var customerSource = new BindingSource(customers.Select(c => new { c.CustomerId, c.Name, c.Adress, c.Phonenumber, c.Email, c.LoyaltyLevel }).ToList(), null);
            form.dataGridView1.DataSource = customerSource;
        }

    }
}
