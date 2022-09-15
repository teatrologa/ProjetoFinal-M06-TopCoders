using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProjetoFinal.M06.Core.Interface;
using ProjetoFinal.M06.Core.Models;

namespace ProjetoFinal.M06.Infra.Data.Repository
{
    public class CityEventRepository : ICityEventRepository
    {
        private readonly IConfiguration _configuration;
        public CityEventRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<CityEvent> GetAllEvents()
        {
            var query = "SELECT * FROM CityEvent";

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query).ToList();
        }

        public CityEvent GetIdEvent (long idEvent)
        {
            var query = "SELECT * FROM CityEvent WHERE idEvent = @idEvent";

            var parameters = new DynamicParameters(new
            {
                idEvent,
            });

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.QueryFirstOrDefault<CityEvent>(query, parameters);
        }

        public List<CityEvent> GetTitleEvent(string title)
        {

            var query = "SELECT * FROM CityEvent WHERE title LIKE ('%' + @title + '%')";

            var parameters = new DynamicParameters(new
            {
                title,
            });

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query, parameters).ToList();

            
            //FORMA GAMBIARRADA DE FAZER, NÃO FAÇA

            //var query = "SELECT * FROM CityEvent";

            //using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            //var listEvent = conn.Query<CityEvent>(query).ToList();

            //List<CityEvent> eventTitle = (from CityEvent in listEvent where CityEvent.Title.Contains(title) select CityEvent).ToList();

            //return eventTitle;
        }

        public List<CityEvent> GetLocalDateEvent(string local, DateTime dateHourEvent)
        {
            dateHourEvent = Convert.ToDateTime(dateHourEvent);

            var query = "SELECT * FROM CityEvent WHERE Local LIKE ('%' + @local + '%') AND DateHourEvent between (@dateHourEvent + '00:00:00.000') and (@dateHourEvent + '23:59:59.000')";

            var parameters = new DynamicParameters(new
            {
                local,
                dateHourEvent,
            });

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query, parameters).ToList();
        }

        public List<CityEvent> GetPriceDateEvent(decimal priceMin, decimal priceMax, DateTime dateHourEvent)
        {
            dateHourEvent = Convert.ToDateTime(dateHourEvent);

            var query = "SELECT * FROM CityEvent WHERE (Price BETWEEN (@priceMin) AND (@priceMax)) AND DateHourEvent between (@dateHourEvent + '00:00:00.000') and (@dateHourEvent + '23:59:59.000')";

            var parameters = new DynamicParameters(new
            {
                priceMin,
                priceMax,
                dateHourEvent,
            });

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query, parameters).ToList();

        }

        public bool InsertNewEvent (CityEvent cityEvent)
        {
            var query = "INSERT INTO CityEvent VALUES (@title, @description, @dateHourEvent, @local, @address, @price, @status)";

            var parameters = new DynamicParameters(new
            {
                cityEvent.Title,
                cityEvent.Description,
                cityEvent.DateHourEvent,
                cityEvent.Local,
                cityEvent.Address,
                cityEvent.Price,
                cityEvent.Status,
            });

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool ChangeEvent (long idEvent, CityEvent cityEvent)
        {
            var query = @"UPDATE CityEvent SET title = @title, description = @description,
                          dateHourEvent = @dateHourEvent, local = @local, address = @address,
                             price = @price, status = @status WHERE idEvent = @idEvent";

            cityEvent.Idevent = idEvent;

            var parameters = new DynamicParameters(cityEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool DeleteEvent (long idEvent)
        {
            var query = "DELETE FROM CityReservation WHERE idEvent = @idEvent";

            var parameters = new DynamicParameters(new { idEvent });

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool IsThereAnyReservation(long idEvent)
        {
            var query = "SELECT * FROM EventReservation WHERE (idEvent = @idEvent) and quantity > 0";

            var parameters = new DynamicParameters(new { idEvent });

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            var listEvent = conn.Query<CityEvent>(query, parameters).ToList();

            if (listEvent.Count > 0)
            {
                return true;
            }
            return false;
        }

    }
}
