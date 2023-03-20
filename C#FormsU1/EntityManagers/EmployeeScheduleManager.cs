using GameCenter.DAL;
using GameCenter.DAL.DALErrorHandling;
using GameCenter.Forms;
using GameCenter.Interfaces;
using GameCenter.Models;
using System;

namespace GameCenter.EntityManagers
{
    internal class EmployeeScheduleManager : IEntityManager
    {
        private readonly GameCenterDatabaseContext _context = new();
        public void Load(EmployeeForm form)
        {
            DAL_ExceptionHandler exceptionHandler = new DAL_ExceptionHandler();
            var employeeSchedules = _context.EmployeeSchedules.Select(s => new { s.EmployeeId, s.ShiftDate, s.Name, s.ShiftResponsibilities }).ToList();
            var employeeScheduleSource = new BindingSource(employeeSchedules, null);
            form.dataGridView1.DataSource = employeeScheduleSource;

            form.textBox1.Enabled = true;
            form.textBox2.Enabled = false; 
            form.textBox3.Enabled = false;
            form.textBox4.Enabled = false;
            form.textBox5.Enabled = false;
            form.textBox6.Enabled = false;
            form.textBox1.PlaceholderText = "EmployeeID:";
            form.label1.Text = "->ShiftDate";
            form.dateTimePicker1.Enabled = true;
            form.comboBoxRole.Enabled = true;
            form.CreateButton.Enabled = true;
            form.DeleteButton.Enabled = true;
            form.UpdateButton.Enabled = true;

            List<string> shiftResponsibilities = new List<string>() { ">Shift responsibilities<", "Manager", "Assistant Manager", "Technician", "Customer Service", "Cashier", "janitor", "Game Technician", "Security Guard" };
            form.comboBoxRole.DataSource = shiftResponsibilities;
            
            if (form.UserRole == "Employee")
            {
                form.CreateButton.Enabled = false;
                form.DeleteButton.Enabled = false;
                form.UpdateButton.Enabled = false;
                form.comboBoxRole.Enabled = false;
                form.comboBoxRole.Text = "";
            }
            form.lblError.Text = exceptionHandler.GetErrorMessage();
        }



        // recreate the delete method from employee but for employee schedule
        public void Delete(EmployeeForm form)
        {
            ICRUDRepository<EmployeeSchedule> employeeScheduleRepository = new EmployeeScheduleRepository(_context);
            var selectedId = form.textBox1.Text;
            var selectedDate = form.dateTimePicker1.Value;

            if (string.IsNullOrWhiteSpace(selectedId))
            {
                MessageBox.Show("Please enter a EmployeeID value in the EmployeeID: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var employeeSchedule = employeeScheduleRepository.GetByCompositeId(selectedId, selectedDate);

            if (employeeSchedule == null)
            {
                MessageBox.Show("No employee schedule found with the specified EmployeeID and Shift Date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to permanently delete this shift?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                employeeScheduleRepository.Delete(employeeSchedule);
            }
 

        }

        // make a create method for employee schedule
        public void Create(EmployeeForm form)
        {
            ICRUDRepository<Employee> employeeRepository = new EmployeeRepository(_context);
            ICRUDRepository<EmployeeSchedule> employeeScheduleRepository = new EmployeeScheduleRepository(_context);
            var selectedId = form.textBox1.Text;
            var selectedDate = form.dateTimePicker1.Value;
            var selectedRole = form.comboBoxRole.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(selectedId))
            {
                MessageBox.Show("Please enter a EmployeeID value in the EmployeeID: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (selectedRole == ">Shift responsibilities<")
            {
                MessageBox.Show("Please select a shiftResponsibilities value from the shiftResponsibilities: dropdown menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var employee = employeeRepository.GetById(selectedId);
            if (employee == null)
            {
                MessageBox.Show("No employee found with the specified EmployeeID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var employeeSchedule = employeeScheduleRepository.GetByCompositeId(selectedId, selectedDate);

            if (employeeSchedule != null)
            {
                MessageBox.Show("An employee schedule already exists with the specified EmployeeID and Shift Date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            employeeScheduleRepository.Create(new EmployeeSchedule
            {
                EmployeeId = selectedId,
                Name = employee.Name,
                ShiftDate = selectedDate,
                ShiftResponsibilities = selectedRole
            });
        }

        // create a update method for employee schedule
        public void Update(EmployeeForm form)
        {
            ICRUDRepository<EmployeeSchedule> employeeScheduleRepository = new EmployeeScheduleRepository(_context);
            var selectedId = form.textBox1.Text;
            var selectedDate = form.dateTimePicker1.Value;
            var selectedName = form.textBox3.Text;
            var selectedRole = form.comboBoxRole.SelectedItem.ToString();

            if (selectedRole == ">Shift responsibilities<")
            {
                MessageBox.Show("Please select a ShiftResponsibilities from the ShiftResponsibilities dropdown list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(selectedId))
            {
                MessageBox.Show("Please enter a EmployeeID value in the EmployeeID: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(selectedName))
            {
                MessageBox.Show("Please enter a Name value in the Name: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var employeeSchedule = employeeScheduleRepository.GetByCompositeId(selectedId, selectedDate);

            if (employeeSchedule == null)
            {
                MessageBox.Show("No employee schedule found with the specified EmployeeID and Shift Date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            employeeSchedule.Name = selectedName;
            employeeSchedule.ShiftResponsibilities = selectedRole;

            employeeScheduleRepository.Update(employeeSchedule);
        }

        public void View(EmployeeForm form)
        {
            ICRUDRepository<EmployeeSchedule> employeeScheduleRepository = new EmployeeScheduleRepository(_context);
            var selectedId = form.textBox1.Text;
            var selectedDate = form.dateTimePicker1.Value;

            if (string.IsNullOrWhiteSpace(selectedId))
            {
                MessageBox.Show("Please enter an Employee ID value in the Employee ID: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                form.textBox1.Text = "";
                return;
            }
           
            var employeeSchedule = employeeScheduleRepository.GetByCompositeId(selectedId, selectedDate);

            if (employeeSchedule == null)
            {
                MessageBox.Show("No employee schedule found with the specified EmployeeID and Shift Date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var employeeSchedules = new List<EmployeeSchedule> { employeeSchedule };
            var employeeScheduleSource = new BindingSource(employeeSchedules.Select(s => new { s.EmployeeId, s.ShiftDate, s.Name, s.ShiftResponsibilities }).ToList(), null);
            form.dataGridView1.DataSource = employeeScheduleSource;

        }

    }

}
