using GameCenter.Interfaces;
using GameCenter.Models;
using GameCenter.DAL.DALErrorHandling;

namespace GameCenter.DAL
{//Written partly with AI assistance
    internal class ReservationRepository : ICRUDRepository<Reservation>
    {
        private readonly GameCenterDatabaseContext _context;

        public ReservationRepository(GameCenterDatabaseContext context)
        {
            _context = context;
        }

        // Create CRUD for reservation class
        public void Create(Reservation reservation)
        {
            try
            {
                _context.Reservations.Add(reservation);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                DAL_ExceptionHandler.HandleSqlException(ex);
            }
        }

        Reservation ICRUDRepository<Reservation>.GetById(string id)
        {
            throw new NotImplementedException();
        }


        public List<Reservation> GetAll()
        {
            try
            {
                return _context.Reservations.ToList();
            }
            catch (Exception ex)
            {
                DAL_ExceptionHandler.HandleSqlException(ex);
                return null;
            }
        }

        public void Update(Reservation reservation)
        {
            try
            {
                _context.Reservations.Update(reservation);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                DAL_ExceptionHandler.HandleSqlException(ex);
            }
        }

        public void Delete(Reservation reservation)
        {
            try
            {
                _context.Reservations.Remove(reservation);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                DAL_ExceptionHandler.HandleSqlException(ex);
            }
        }

        // create method to get reservation by composite id
        public Reservation GetByCompositeId(string id, DateTime dateTime)
        {
            try
            {
                return _context.Reservations.Find(id, dateTime);
            }
            catch (Exception ex)
            {
                DAL_ExceptionHandler.HandleSqlException(ex);
                return null;
            }
        }

    }
}
