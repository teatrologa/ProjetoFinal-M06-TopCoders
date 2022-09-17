using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProjetoFinal.M06.Core.Interface;
using ProjetoFinal.M06.Core.Models;

namespace ProjetoFinal.M06.Infra.Data.Repository
{
    public class EventReservationRepository : IEventReservationRepository
    {
        private readonly IConfiguration _configuration;
        public EventReservationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<EventReservation> GetAllReservations()
        {
            var query = "SELECT * FROM EventReservation";

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Query<EventReservation>(query).ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public EventReservation GetIdReservation(long idReservation)
        {
            var query = "SELECT * FROM EventReservation WHERE idReservation = @idReservation";

            var parameters = new DynamicParameters(new
            {
                idReservation,
            });

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.QueryFirstOrDefault<EventReservation>(query, parameters);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public List<EventReservation> GetPersonTitleReservation(string personName, string title)
        {
            var query = @"SELECT er.idReservation, er.idEvent, er.personName, 
                            er.Quantity FROM EventReservation AS er
                            INNER JOIN CityEvent AS ce ON ce.idEvent = er.idEvent 
                            WHERE er.personName LIKE ('%' + @personName + '%')  
                            AND ce.Title LIKE ('%' + @title + '%')";

            var parameters = new DynamicParameters(new
            {
                personName,
                title,
            });

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Query<EventReservation>(query, parameters).ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public List<EventReservation> GetEventReservations(long idEvent)
        {
            var query = "SELECT * FROM EventReservation WHERE idEvent = @idEvent";

            var parameters = new DynamicParameters(new
            {
                idEvent,
            });

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Query<EventReservation>(query, parameters).ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public bool InsertNewReservation(EventReservation eventReservation)
        {
            var query = "INSERT INTO EventReservation VALUES (@idEvent, @personName, @quantity)";

            var parameters = new DynamicParameters(new
            {
                eventReservation.IdEvent,
                eventReservation.PersonName,
                eventReservation.Quantity,
            });

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Execute(query, parameters) == 1;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public bool ChangeReservation(long idReservation, EventReservation eventReservation)
        {
            var query = @"UPDATE EventReservation SET quantity = @quantity
                            WHERE idReservation = @idReservation";

            eventReservation.IdReservation = idReservation;

            var parameters = new DynamicParameters(eventReservation);
            
            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Execute(query, parameters) == 1;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }

        }

        public bool DeleteReservation (long idReservation)
        {
            var query = "DELETE FROM EventReservation WHERE idReservation = @idReservation";
            
            var parameters = new DynamicParameters(new
            {
                idReservation
            });

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Execute(query, parameters) == 1;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

    }
}