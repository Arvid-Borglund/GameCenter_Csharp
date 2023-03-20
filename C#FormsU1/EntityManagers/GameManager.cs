using GameCenter.DAL;
using GameCenter.DAL.DALErrorHandling;
using GameCenter.Forms;
using GameCenter.Interfaces;
using GameCenter.Models;

namespace GameCenter.EntityManagers
{
    internal class GameManager : IEntityManager, IEntityManagerCustomer
    {
        private readonly GameCenterDatabaseContext _context = new();
        public void Load(EmployeeForm form)
        {
            DAL_ExceptionHandler exceptionHandler = new DAL_ExceptionHandler();
            var games = _context.Games.Select(g => new { g.GameId, g.ComputerId, g.Title, g.Genre }).ToList();
            var gameSource = new BindingSource(games, null);
            form.dataGridView1.DataSource = gameSource;

            form.textBox1.Enabled = true;
            form.textBox2.Enabled = true;
            form.textBox3.Enabled = true;
            form.textBox4.Enabled = true;
            form.textBox5.Enabled = false;
            form.textBox6.Enabled = false;
            form.textBox1.PlaceholderText = "GameId:";
            form.textBox2.PlaceholderText = "ComputerId:";
            form.textBox3.PlaceholderText = "Title:";
            form.textBox4.PlaceholderText = "Genre:";
            form.dateTimePicker1.Enabled = false;
            form.comboBoxRole.Enabled = false;
            form.comboBoxRole.Text = "";
            form.CreateButton.Enabled = true;
            form.DeleteButton.Enabled = true;
            form.UpdateButton.Enabled = true;
            form.lblError.Text = exceptionHandler.GetErrorMessage();
        }

        public void Load(CustomerForm form)
        {
            DAL_ExceptionHandler exceptionHandler = new DAL_ExceptionHandler();
            var games = _context.Games.Select(g => new { g.Title, g.Genre }).Distinct().ToList();
            var gameSource = new BindingSource(games, null);
            form.dataGridView1.DataSource = gameSource;

            form.textBox2.Enabled = true;
            form.textBox3.Enabled = true;
            form.textBox4.Enabled = true;
            form.textBox5.Enabled = false;
            form.textBox2.PlaceholderText = "ComputerId:";
            form.textBox3.PlaceholderText = "Title:";
            form.textBox4.PlaceholderText = "Genre:";
            form.comboBoxRole.Enabled = false;
            form.comboBoxRole.Text = "";
            form.DeleteButton.Enabled = false;
            form.UpdateButton.Enabled = false;
            form.lblError.Text = exceptionHandler.GetErrorMessage();
        }


        public void Create(CustomerForm form)
        {
            throw new NotImplementedException();
        }

        public void Update(CustomerForm form)
        {
            throw new NotImplementedException();
        }

        public void Delete(CustomerForm form)
        {
            throw new NotImplementedException();
        }
    



    public void Delete(EmployeeForm form)
        {
            ICRUDRepository<Game> gameRepository = new GameRepository(_context);
            var selectedId = form.textBox1.Text;

            if (string.IsNullOrWhiteSpace(selectedId))
            {
                MessageBox.Show("Please enter a GameID value in the GameID: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var game = gameRepository.GetById(selectedId);

            if (game == null)
            {
                MessageBox.Show("No game found with the specified GameID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to permanently delete the game with this ID?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                gameRepository.Delete(game);
            }

            
        }

        public void Create(EmployeeForm form)
        {
            ICRUDRepository<Game> gameRepository = new GameRepository(_context);
            ICRUDRepository<Computer> computerRepository = new ComputerRepository(_context);

            var gameId = form.textBox1.Text;
            var computerId = form.textBox2.Text;
            var title = form.textBox3.Text;
            var genre = form.textBox4.Text;

            if (string.IsNullOrWhiteSpace(gameId))
            {
                MessageBox.Show("Please enter a GameId value in the GameId: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (gameRepository.GetById(gameId) != null)
            {
                MessageBox.Show("The GameId already exists. Please enter a unique GameId.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (computerRepository.GetById(computerId) == null)
            {
                MessageBox.Show("Please enter a existing ComputerId value in the ComputerId: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(computerId))
            {
                MessageBox.Show("Please enter a ComputerId value in the ComputerId: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Please enter a Title value in the Title: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(genre))
            {
                MessageBox.Show("Please enter a Genre value in the Genre: write field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var game = new Game
            {
                GameId = gameId,
                ComputerId = computerId,
                Title = title,
                Genre = genre
            };

            gameRepository.Create(game);
            
        }

        public void Update(EmployeeForm form)
        {
            MessageBox.Show("Games cannot be updated. Please delete the game and create a new one if necessary.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        public void View(EmployeeForm form)
        {
            ICRUDRepository<Game> gameRepository = new GameRepository(_context);

            var gameId = form.textBox1.Text;
            var computerId = form.textBox2.Text;
            var title = form.textBox3.Text;
            var genre = form.textBox4.Text;

            List<Game> games = new List<Game>();

            if (!string.IsNullOrWhiteSpace(gameId))
            {
                var game = gameRepository.GetById(gameId);
                if (game != null)
                {
                    games.Add(game);
                }
                else
                {
                    MessageBox.Show("No game found with the specified GameID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (!string.IsNullOrWhiteSpace(computerId))
            {
                games = gameRepository.GetAll().Where(g => g.ComputerId == computerId).ToList();
                if (!games.Any())
                {
                    MessageBox.Show("No games found with the specified Computer ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (!string.IsNullOrWhiteSpace(title))
            {
                games = gameRepository.GetAll().Where(g => g.Title == title).ToList();
                if (!games.Any())
                {
                    MessageBox.Show("No games found with the specified Title.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (!string.IsNullOrWhiteSpace(genre))
            {
                games = gameRepository.GetAll().Where(g => g.Genre == genre).ToList();
                if (!games.Any())
                {
                    MessageBox.Show("No games found with the specified Genre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter a value in any of the fields (Game ID, Computer ID, Title, or Genre).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var gameSource = new BindingSource(games.Select(g => new { g.GameId, g.ComputerId, g.Title, g.Genre }).ToList(), null);
            form.dataGridView1.DataSource = gameSource;
        }

        public void View(CustomerForm form)
        {
            ICRUDRepository<Game> gameRepository = new GameRepository(_context);
            var computerId = form.textBox2.Text;
            var title = form.textBox3.Text;
            var genre = form.textBox4.Text;

            var games = gameRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(computerId))
            {
                games = games.Where(g => g.ComputerId == computerId).ToList();
                if (games.Count == 0)
                {
                    MessageBox.Show("No games found with the specified ComputerID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    form.textBox2.Text = "";
                    return;
                }
            }
            else if (!string.IsNullOrWhiteSpace(title))
            {
                games = games.Where(g => g.Title == title).ToList();
                if (games.Count == 0)
                {
                    MessageBox.Show("No games found with the specified title.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    form.textBox3.Text = "";
                    return;
                }
            }
            else if (!string.IsNullOrWhiteSpace(genre))
            {
                games = games.Where(g => g.Genre == genre).ToList();
                if (games.Count == 0)
                {
                    MessageBox.Show("No games found with the specified genre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    form.textBox4.Text = "";
                    return;
                }
            }

            var uniqueGames = games.GroupBy(g => g.Title)
                                   .Select(g => g.First())
                                   .ToList();
            var gameSource = new BindingSource(uniqueGames.Select(g => new { g.Title, g.Genre }).ToList(), null);
            form.dataGridView1.DataSource = gameSource;
        }


    }
}
