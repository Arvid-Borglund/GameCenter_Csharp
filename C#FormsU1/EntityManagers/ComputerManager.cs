using GameCenter.DAL;
using GameCenter.Forms;
using GameCenter.Interfaces;
using GameCenter.Models;
using System.Windows.Forms;
using System.Drawing;
using GameCenter.DAL.DALErrorHandling;

namespace GameCenter.EntityManagers
{
    internal class ComputerManager : IEntityManager
    {
        private readonly GameCenterDatabaseContext _context = new();

       
        public void Load(EmployeeForm form)
        {
            DAL_ExceptionHandler exceptionHandler = new DAL_ExceptionHandler();
            var computers = _context.Computers.Select(c => new { c.ComputerId, c.Cpu, c.Gpu, c.Ram, c.DataStorage, c.Reserved }).ToList();
            var computerSource = new BindingSource(computers, null);
            form.dataGridView1.DataSource = computerSource;

            form.textBox1.Enabled = true;
            form.textBox2.Enabled = true;
            form.textBox3.Enabled = true;
            form.textBox4.Enabled = true; 
            form.textBox5.Enabled = true;
            form.textBox6.Enabled = false;
            form.textBox1.PlaceholderText = "ComputerId:";
            form.textBox2.PlaceholderText = "Cpu:";
            form.textBox3.PlaceholderText = "Gpu:";
            form.textBox4.PlaceholderText = "Ram:";
            form.textBox5.PlaceholderText = "DataStorage:";
            form.dateTimePicker1.Enabled = false;
            form.comboBoxRole.Enabled = false;
            form.comboBoxRole.Text = "";
            form.CreateButton.Enabled = true;
            form.DeleteButton.Enabled = true;
            form.UpdateButton.Enabled = true;


            ICRUDRepository<Computer> computerRepository = new ComputerRepository(_context);
            ICRUDRepository<Reservation> reservationRepository = new ReservationRepository(_context);

            var computaz = computerRepository.GetAll();
            var reservations = reservationRepository.GetAll();

            var presentDate = DateTime.Now.Date;

            foreach (var computer in computaz)
            {
                var reserved = reservations.Any(x => x.ComputerId == computer.ComputerId && x.TimeDate.Date == presentDate);
                computer.Reserved = reserved;
                computerRepository.Update(computer);
            }
            form.lblError.Text = exceptionHandler.GetErrorMessage();
        }

        // create the add method
        public void Create(EmployeeForm form)
        {
            ICRUDRepository<Computer> computerRepository = new ComputerRepository(_context);
            var computerId = form.textBox1.Text;
            var cpu = form.textBox2.Text;
            var gpu = form.textBox3.Text;
            var ram = form.textBox4.Text;
            var dataStorage = form.textBox5.Text;

            if (string.IsNullOrWhiteSpace(computerId) || string.IsNullOrWhiteSpace(cpu) || string.IsNullOrWhiteSpace(gpu) || string.IsNullOrWhiteSpace(ram) || string.IsNullOrWhiteSpace(dataStorage))
            {
                MessageBox.Show("Please enter a value in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (computerRepository.GetById(computerId) != null)
            {
                MessageBox.Show("A computer with the same ComputerId already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(ram, out int ramValue))
            {
                MessageBox.Show("Please enter a valid integer value for RAM.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var computer = new Computer
            {
                ComputerId = computerId,
                Cpu = cpu,
                Gpu = gpu,
                Ram = ramValue,
                DataStorage = dataStorage
            };

            computerRepository.Create(computer);
        }



        
        public void Delete(EmployeeForm form)
        {
            ICRUDRepository<Computer> computerRepository = new ComputerRepository(_context);
            var selectedId = form.textBox1.Text;

            if (string.IsNullOrWhiteSpace(selectedId))
            {
                MessageBox.Show("Please enter a ComputerID value in the ComputerID: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var computer = computerRepository.GetById(selectedId);

            if (computer == null)
            {
                MessageBox.Show("No computer found with the specified ComputerID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var reservations = _context.Reservations.Where(x => x.ComputerId == computer.ComputerId).ToList();
            if (reservations.Count > 0)
            {
                MessageBox.Show($"This computer is present in {reservations.Count} reservation(s). Please remove the reservation(s) before deleting the computer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to permanently delete this computer?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                computerRepository.Delete(computer);
            }

        }


        // create uppdate method
        public void Update(EmployeeForm form)
        {
            ICRUDRepository<Computer> computerRepository = new ComputerRepository(_context);
            var computerId = form.textBox1.Text;
            var cpu = form.textBox2.Text;
            var gpu = form.textBox3.Text;
            var ram = form.textBox4.Text;
            var dataStorage = form.textBox5.Text;

            if (string.IsNullOrWhiteSpace(computerId) || string.IsNullOrWhiteSpace(cpu) || string.IsNullOrWhiteSpace(gpu) || string.IsNullOrWhiteSpace(ram) || string.IsNullOrWhiteSpace(dataStorage))
            {
                MessageBox.Show("Please enter a value in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(ram, out int ramValue))
            {
                MessageBox.Show("Please enter a valid integer value for RAM.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var computer = computerRepository.GetById(computerId);

            if (computer == null)
            {
                MessageBox.Show("No computer found with the specified ComputerID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            computer.Cpu = cpu;
            computer.Gpu = gpu;
            computer.Ram = ramValue;
            computer.DataStorage = dataStorage;

            computerRepository.Update(computer);
        }


        public void View(EmployeeForm form)
        {
            ICRUDRepository<Computer> computerRepository = new ComputerRepository(_context);
            var selectedId = form.textBox1.Text;

            if (string.IsNullOrWhiteSpace(selectedId))
            {
                MessageBox.Show("Please enter a ComputerID value in the ComputerID: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             
                
                form.textBox1.Text = "";
                return;
            }

            var computer = computerRepository.GetById(selectedId);

            if (computer == null)
            {
                MessageBox.Show("No computer found with the specified ComputerID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var computers = new List<Computer> { computer };
            var computerSource = new BindingSource(computers.Select(c => new { c.ComputerId, c.Cpu, c.Gpu, c.Ram, c.DataStorage, c.Reserved }).ToList(), null);
            form.dataGridView1.DataSource = computerSource;
        }


    }
}
