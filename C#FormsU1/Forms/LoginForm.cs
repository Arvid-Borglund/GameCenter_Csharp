using GameCenter.DAL;
using GameCenter.DAL.DALErrorHandling;
using GameCenter.Interfaces;
using GameCenter.Models;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace GameCenter.Forms
{
    

    public partial class LoginForm : Form
    {
        DAL_ExceptionHandler exceptionHandler = new DAL_ExceptionHandler();
        private readonly GameCenterDatabaseContext context;
        private List<Image> _welcomeImages = new List<Image>();
        private List<Image> _errorImages = new List<Image>();
        private int _errorImageIndex = 0;


        public LoginForm()
        {
            InitializeComponent();
            context = new GameCenterDatabaseContext();
            textBoxUsername.PlaceholderText = "Username:";
            textBoxPassword.PlaceholderText = "Password:";

            // Load the welcome images
            _welcomeImages.Add(Image.FromFile(@"..\..\..\Images\StandardSeal.png"));
            _welcomeImages.Add(Image.FromFile(@"..\..\..\Images\SealOpen.png"));
            _welcomeImages.Add(Image.FromFile(@"..\..\..\Images\StandardSeal.png"));
            _welcomeImages.Add(Image.FromFile(@"..\..\..\Images\StandardOpenBark.png"));
            _welcomeImages.Add(Image.FromFile(@"..\..\..\Images\StandardSeal.png"));
            _welcomeImages.Add(Image.FromFile(@"..\..\..\Images\SealOpen.png"));
            _welcomeImages.Add(Image.FromFile(@"..\..\..\Images\StandardSeal.png"));
            _welcomeImages.Add(Image.FromFile(@"..\..\..\Images\SealOpenWelcome.png"));


            // Load the error images
            _errorImages.Add(Image.FromFile(@"..\..\..\Images\StandardSeal.png"));
            _errorImages.Add(Image.FromFile(@"..\..\..\Images\SealOpen.png"));
            _errorImages.Add(Image.FromFile(@"..\..\..\Images\StandardSeal.png"));
            _errorImages.Add(Image.FromFile(@"..\..\..\Images\StandardOpenBark.png"));
            _errorImages.Add(Image.FromFile(@"..\..\..\Images\StandardSeal.png"));
            _errorImages.Add(Image.FromFile(@"..\..\..\Images\SealOpen.png"));
            _errorImages.Add(Image.FromFile(@"..\..\..\Images\StandardSeal.png"));
            _errorImages.Add(Image.FromFile(@"..\..\..\Images\SealOpenDenied.png"));
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            ICRUDRepository<Login> loginRepository = new LoginRepository(context);
            var enteredUsername = textBoxUsername.Text;
            var enteredPassword = textBoxPassword.Text;
            var login = loginRepository.GetById(enteredUsername);
            lblError.Text = exceptionHandler.GetErrorMessage();

            if (login == null || login.Password != enteredPassword)
            {
                // Display error animation
                Timer errorTimer = new Timer();
                errorTimer.Interval = 500;
                errorTimer.Start();
                errorTimer.Tick += (s, ev) =>
                {
                    if (_errorImageIndex < _errorImages.Count - 1)
                    {
                        pictureBox1.Image = _errorImages[_errorImageIndex];
                        _errorImageIndex++;
                    }
                    else
                    {
                        errorTimer.Stop();
                        _errorImageIndex = 0;

                        // Display the final error image for 3 seconds
                        pictureBox1.Image = _errorImages[_errorImages.Count - 1];
                        Timer errorFinalImageTimer = new Timer();
                        errorFinalImageTimer.Interval = 3000;
                        errorFinalImageTimer.Start();
                        errorFinalImageTimer.Tick += (s2, ev2) =>
                        {
                            errorFinalImageTimer.Stop();
                            pictureBox1.Image = _welcomeImages[0];
                        };
                    }
                    textBoxUsername.Clear();
                    textBoxPassword.Clear();

                };
                return;
            }

            // Display welcome animation
            Timer welcomeTimer = new Timer();
            welcomeTimer.Interval = 500;
            welcomeTimer.Start();
            int welcomeImageIndex = 0;
            welcomeTimer.Tick += (s, ev) =>
            {
                if (welcomeImageIndex < _welcomeImages.Count - 1)
                {
                    pictureBox1.Image = _welcomeImages[welcomeImageIndex];
                    welcomeImageIndex++;
                }
                else
                {
                    welcomeTimer.Stop();
                    welcomeImageIndex = 0;

                    // Display the final welcome image for 3 seconds
                    pictureBox1.Image = _welcomeImages[_welcomeImages.Count - 1];
                    Timer welcomeFinalImageTimer = new Timer();
                    welcomeFinalImageTimer.Interval = 3000;
                    welcomeFinalImageTimer.Start();
                    welcomeFinalImageTimer.Tick += (s2, ev2) =>
                    {
                        welcomeFinalImageTimer.Stop();

                        if (login.AccessLevel == "Employee" || login.AccessLevel == "Admin")
                        {
                            EmployeeForm employeeForm = new EmployeeForm();
                            employeeForm.UserRole = login.AccessLevel;
                            employeeForm.UserId = login.EmployeeId;
                            employeeForm.Show();
                        }
                        else if (login.AccessLevel == "Customer")
                        {
                            CustomerForm customerForm = new CustomerForm();
                            customerForm.UserRole = login.AccessLevel;
                            customerForm.UserId = login.CustomerId;
                            customerForm.Show();
                        }

                        LoginForm loginForm = this;
                        loginForm.Hide();
                    };
                }
            };
        }
    }
}