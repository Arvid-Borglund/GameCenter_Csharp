using GameCenter.Interfaces;

namespace GameCenter.EntityManagers.EntityManagerFactory
{//Written partly with AI assistance
    public class EntityManagerFactory
    {
        public static IEntityManager GetEntityManager(string entityName)
        {
            switch (entityName)
            {
                case "Employee":
                    return new EmployeeManager();
                case "Customer":
                    return new CustomerManager();
                case "EmployeeSchedule":
                    return new EmployeeScheduleManager();
                case "Computer":
                    return new ComputerManager();
                case "Reservation":
                    return new ReservationManager();
                case "Game":
                    return new GameManager();
                case "Login":
                    return new LoginManager();
                case "My Profile":
                    return new MyProfileManager();

                default:
                    throw new ArgumentException("Invalid entity name");
            }
        }

        public static IEntityManagerCustomer GetEntityManagerCustomer(string entityName)
        {
            switch (entityName)
            {
                case "Games":
                    return new GameManager();
                
                case "My Profile & bookings":
                    return new MyProfileManager();

                default:
                    throw new ArgumentException("Invalid entity name");
            }
        }

    }
}
