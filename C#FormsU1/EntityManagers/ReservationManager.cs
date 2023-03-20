using GameCenter.DAL;
using GameCenter.DAL.DALErrorHandling;
using GameCenter.Forms;
using GameCenter.Interfaces;
using GameCenter.Models;
using Microsoft.VisualBasic.Logging;
using System.Globalization;

namespace GameCenter.EntityManagers
{
    internal class ReservationManager : IEntityManager
    {
        private readonly GameCenterDatabaseContext _context = new();
        public void Load(EmployeeForm form)
        {
            DAL_ExceptionHandler exceptionHandler = new DAL_ExceptionHandler();
            var reservations = _context.Reservations.Select(r => new { r.ComputerId, r.TimeDate, r.CustomerId, r.EmployeeId }).ToList();
            var reservationSource = new BindingSource(reservations, null);
            form.dataGridView1.DataSource = reservationSource;

            form.textBox1.Enabled = true;
            form.textBox2.Enabled = true;
            form.textBox3.Enabled = false;
            form.textBox4.Enabled = false;
            form.textBox5.Enabled = false;
            form.textBox6.Enabled = false;
            form.textBox1.PlaceholderText = "ComputerId:";
            form.label1.Text = "->Date";
            form.textBox2.PlaceholderText = "CustomerId:";
            form.dateTimePicker1.Enabled = true;
            form.comboBoxRole.Enabled = false;
            form.comboBoxRole.Text = "";
            form.CreateButton.Enabled = true;
            form.DeleteButton.Enabled = true;
            form.UpdateButton.Enabled = true;
            form.lblError.Text = exceptionHandler.GetErrorMessage();

        }
        public void Delete(EmployeeForm form)
        {
            ICRUDRepository<Reservation> reservationRepository = new ReservationRepository(_context);
            var selectedId = form.textBox1.Text;
            var dateTime = form.dateTimePicker1.Value;

            if (string.IsNullOrWhiteSpace(selectedId))
            {
                MessageBox.Show("Please enter a ReservationID value in the ReservationID: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var reservation = reservationRepository.GetByCompositeId(selectedId, dateTime);

            if (reservation == null)
            {
                MessageBox.Show("No reservation found with the specified ReservationID and Date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to permanently delete this reservation?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                reservationRepository.Delete(reservation);
            }

            
        }

        // make a create method for reservation
        public void Create(EmployeeForm form)
        {
            ICRUDRepository<Reservation> reservationRepository = new ReservationRepository(_context);
            ICRUDRepository<Computer> computerRepository = new ComputerRepository(_context);
            ICRUDRepository<Customer> customerRepository = new CustomerRepository(_context);
            ICRUDRepository<Employee> employeeRepository = new EmployeeRepository(_context);

            var selectedComputerId = form.textBox1.Text;
            var selectedDateTime = form.dateTimePicker1.Value;
            var selectedCustomerId = form.textBox2.Text;

            if (string.IsNullOrWhiteSpace(selectedComputerId))
            {
                MessageBox.Show("Please enter a ComputerID value in the ComputerID: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(selectedCustomerId))
            {
                MessageBox.Show("Please enter a CustomerID value in the CustomerID: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (computerRepository.GetById(selectedComputerId) == null)
            {
                MessageBox.Show("The ComputerID entered does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (customerRepository.GetById(selectedCustomerId) == null)
            {
                MessageBox.Show("The CustomerID entered does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (employeeRepository.GetById(form.UserId) == null)
            {
                MessageBox.Show("The EmployeeID entered does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (reservationRepository.GetByCompositeId(selectedComputerId, selectedDateTime) != null)
            {
                MessageBox.Show("The Reservation with the entered ComputerID and DateTime already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var reservation = new Reservation
            {
                ComputerId = selectedComputerId,
                TimeDate = selectedDateTime,
                CustomerId = selectedCustomerId,
                EmployeeId = form.UserId
            };
            reservationRepository.Create(reservation);

            var customer = customerRepository.GetById(selectedCustomerId);
            customer.LoyaltyLevel = reservationRepository.GetAll().Count(r => r.CustomerId == selectedCustomerId);
            customerRepository.Update(customer);
        }

        public void Update(EmployeeForm form)
        {
            MessageBox.Show("Reservations may not be updated. Please create a new reservation or delete the existing reservation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        public void View(EmployeeForm form)
        {
            ICRUDRepository<Reservation> reservationRepository = new ReservationRepository(_context);

            var selectedComputerId = form.textBox1.Text;
            var selectedDateTime = form.dateTimePicker1.Value;

            if (string.IsNullOrWhiteSpace(selectedComputerId))
            {
                form.textBox1.Clear();
                return;
            }

            var reservation = reservationRepository.GetByCompositeId(selectedComputerId, selectedDateTime);

            if (reservation == null)
            {
                MessageBox.Show("No reservation found with the specified ComputerID and DateTime.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var reservations = new List<Reservation> { reservation };
            var reservationSource = new BindingSource(reservations.Select(r => new { r.ComputerId, r.TimeDate, r.CustomerId, r.EmployeeId }).ToList(), null);
            form.dataGridView1.DataSource = reservationSource;
        }


    }
}
