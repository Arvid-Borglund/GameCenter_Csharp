using GameCenter.Interfaces;
using GameCenter.Models;
using GameCenter.DAL.DALErrorHandling;

namespace GameCenter.DAL
{//Written partly with AI assistance
    internal class CustomerRepository : ICRUDRepository<Customer>
    {
        private readonly GameCenterDatabaseContext _context;

        public CustomerRepository(GameCenterDatabaseContext context)
        {
            _context = context;

        }

        public void Create(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
               DAL_ExceptionHandler.HandleSqlException(ex);
            }
        }

        public List<Customer> GetAll()
        {
            try
            {
                return _context.Customers.ToList();
            }
            catch (Exception ex)
            {
               DAL_ExceptionHandler.HandleSqlException(ex);
                return null;
            }
        }

        public Customer GetById(string id)
        {
            try
            {
                return _context.Customers.Find(id);
            }
            catch (Exception ex)
            {
               DAL_ExceptionHandler.HandleSqlException(ex);
                return null;
            }
        }


        public void Update(Customer customer)
        {
            try
            {
                _context.Customers.Update(customer);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
               DAL_ExceptionHandler.HandleSqlException(ex);
            }
        }

        public void Delete(Customer customer)
        {
            try
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
               DAL_ExceptionHandler.HandleSqlException(ex);
            }
        }


        Customer ICRUDRepository<Customer>.GetByCompositeId(string id, DateTime dateTime)
        {
            throw new NotImplementedException();
        }



    }
}
