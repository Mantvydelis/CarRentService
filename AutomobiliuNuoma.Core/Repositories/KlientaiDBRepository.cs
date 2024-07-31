using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace AutomobiliuNuoma.Core.Repositories
{
    public class KlientaiDBRepository : IKlientaiRepository
    {
        private readonly string _dbConnectionString;
        public KlientaiDBRepository(string connectionString)
        {
            _dbConnectionString = connectionString;
        }
        public List<Klientas> GautiVisusKlientus()
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            var result = dbConnection.Query<KlientasDto>(@"SELECT Vardas, Pavarde, GimimoMetai FROM [dbo].[Klientai]").ToList();
            dbConnection.Close();
            return result.Select(dto => new Klientas(dto.Vardas, dto.Pavarde, DateOnly.FromDateTime(dto.GimimoMetai))).ToList();
        }

        private class KlientasDto
        {
            public string Vardas { get; set; }
            public string Pavarde { get; set; }
            public DateTime GimimoMetai { get; set; }
        }

        public void PridetiNaujaKlienta(Klientas klientas)
        {
            string sqlCommand = "INSERT INTO Klientai ([Vardas],[Pavarde],[GimimoMetai]) VALUES (@Vardas, @Pavarde, @GimimoMetai)";

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                var naujiduomenys = new
                {
                    Vardas = klientas.Vardas,
                    Pavarde = klientas.Pavarde,
                    GimimoMetai = klientas.GimimoMetai.ToDateTime(new TimeOnly(0, 0))
                };

                connection.Execute(sqlCommand, naujiduomenys);
            }
        }
    }
}
