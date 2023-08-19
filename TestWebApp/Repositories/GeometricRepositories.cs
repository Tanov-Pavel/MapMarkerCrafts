using Npgsql;
using Repository.Repositories.Interfaces;
using System;
using System.Data;
using System.Text;

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

                    command.Parameters.AddWithValue("name", "Point");
                    command.Parameters.AddWithValue("latitude", latitude);
                    command.Parameters.AddWithValue("longitude", longitude);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task AddLineAsync(List<double[]> coordinates)
        {
            if (coordinates == null || coordinates.Count < 2)
            {
                throw new ArgumentException("Invalid coordinates for line.");
            }

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;

                    StringBuilder sb = new StringBuilder();
                    sb.Append("LINESTRING (");
                    foreach (var coord in coordinates)
                    {
                        sb.AppendFormat("{0} {1},", coord[0], coord[1]);
                    }
                    sb.Remove(sb.Length - 1, 1); // Remove trailing comma
                    sb.Append(")");

                    command.CommandText = "INSERT INTO public.geomodel (name, geojson) " +
                                          "VALUES (@name, ST_SetSRID(ST_GeomFromText(@geom), 4326))";

                    command.Parameters.AddWithValue("name", "Line");
                    command.Parameters.AddWithValue("geom", sb.ToString());

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task AddPolygonAsync(List<double[]> coordinates)
        {
            if (coordinates == null || coordinates.Count < 3)
            {
                throw new ArgumentException("Invalid coordinates for polygon.");
            }

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;

                    StringBuilder sb = new StringBuilder();
                    sb.Append("POLYGON ((");
                    foreach (var coord in coordinates)
                    {
                        sb.AppendFormat("{0} {1},", coord[0], coord[1]);
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("))");

                    command.CommandText = "INSERT INTO public.geomodel (name, geojson) " +
                                          "VALUES (@name, ST_SetSRID(ST_GeomFromText(@geom), 4326))";

                    command.Parameters.AddWithValue("name", "Polygon");
                    command.Parameters.AddWithValue("geom", sb.ToString());

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task AddRectangleAsync(double north, double east, double south, double west)
        {
            if (north <= south || east <= west)
            {
                throw new ArgumentException("Invalid coordinates for rectangle.");
            }

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;

                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("POLYGON(({0} {1},{0} {2},{3} {2},{3} {1},{0} {1}))", west, south, north, east);

                    command.CommandText = "INSERT INTO public.geomodel (name, geojson) " +
                                          "VALUES (@name, ST_SetSRID(ST_GeomFromText(@geom), 4326))";

                    command.Parameters.AddWithValue("name", "Rectangle");
                    command.Parameters.AddWithValue("geom", sb.ToString());

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
