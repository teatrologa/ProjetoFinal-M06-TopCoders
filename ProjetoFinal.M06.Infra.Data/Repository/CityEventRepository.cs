﻿using Dapper;
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
            //TESTE para pegar varios titulos que contem a palavra passada

            var query = "SELECT * FROM CityEvent WHERE title LIKE %@title%";

            var parameters = new DynamicParameters(new
            {
                title,
            });

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query, parameters).ToList();
        }

        public bool InsertNewEvent (CityEvent cityEvent)
        {
            var query = "INSERT INTO CityEvent VALUES (@title, @description, @dateHourEvent, @local, @address, @price)";

            var parameters = new DynamicParameters(new
            {
                cityEvent.Title,
                cityEvent.Description,
                cityEvent.DateHourEvent,
                cityEvent.Local,
                cityEvent.Address,
                cityEvent.Price,
            });

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool ChangeEvent (long idEvent, CityEvent cityEvent)
        {
            var query = @"UPDATE CityEvent SET title = @title, description = @description,
                          dateHourEvent = @dateHourEvent, local = @local, address = @address,
                             price = @price WHERE idEvent = @idEvent";

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
    }
}
