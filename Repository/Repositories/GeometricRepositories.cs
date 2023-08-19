using Npgsql;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class GeoStorageRepository : IGeometricRepositories
    {
        private readonly string _connectionString;

        public GeoStorageRepository()
        {
            _connectionString = "Server=localhost;Port=5432;Database=geomodel;UserId=postgres;Password=123456;";
        }

        public async Task AddPointAsync(double latitude, double longitude)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT INTO public.geomodel (name, geojson) " +
                                          "VALUES (@name, ST_SetSRID(ST_MakePoint(@longitude, @latitude), 4326))";

                    command.Parameters.AddWithValue("name", "Test Marker");
                    command.Parameters.AddWithValue("latitude", latitude);
                    command.Parameters.AddWithValue("longitude", longitude);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}






