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
        public async Task<List<Klientas>> GautiVisusKlientus()
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            var result = await dbConnection.QueryAsync<dynamic>(@"SELECT [Id] AS KlientasId
            ,[Vardas]
            ,[Pavarde]
            ,[GimimoMetai] FROM [dbo].[Klientai]");
            dbConnection.Close();
            return result.Select(dto => new Klientas(
                dto.KlientasId,
                dto.Vardas,
                dto.Pavarde,
                DateOnly.FromDateTime(dto.GimimoMetai)
            )).ToList();
        }


        public async Task PridetiNaujaKlienta(Klientas klientas)
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

                await connection.ExecuteAsync(sqlCommand, naujiduomenys);
            }
        }

        public async Task<Klientas> GautiKlientaPagalId(int id)
        {

            using (IDbConnection dbConnection = new SqlConnection(_dbConnectionString))
            {

                dbConnection.Open();
                var result = await dbConnection.QueryFirstOrDefaultAsync<dynamic>(
                    "SELECT Id AS KlientasId, Vardas, Pavarde, GimimoMetai FROM Klientai WHERE Id = @Id",
                    new { Id = id }
                );
                if (result != null)
                {
                    return new Klientas(result.KlientasId, result.Vardas, result.Pavarde, DateOnly.FromDateTime(result.GimimoMetai));
                }
                return null;

            }

        }

        public async Task<Klientas> KoreguotiKlientoInfo(int id, string vardas, string pavarde, DateOnly gimimoMetai)
        {
            string sqlCommand = "UPDATE Klientai " +
                "SET Vardas = @Vardas, Pavarde = @Pavarde, GimimoMetai = @GimimoMetai " +
                "WHERE Id = @Id; SELECT* FROM Klientai WHERE Id = @Id";
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                var pakeistiDuomenys = new
                {
                    Id = id,
                    Vardas = vardas,
                    Pavarde = pavarde,
                    GimimoMetai = gimimoMetai.ToDateTime(new TimeOnly(0, 0))

                };

                var result = await connection.QueryFirstOrDefaultAsync<dynamic>(sqlCommand, pakeistiDuomenys);

                return new Klientas(result.KlientasId, result.Vardas, result.Pavarde, DateOnly.FromDateTime(result.GimimoMetai));
            }

        }

        public async Task IstrintiKlienta(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();

            await dbConnection.ExecuteAsync(@"DELETE FROM NuomosUzsakymas WHERE KlientasId = @id", new { Id = id });
            await dbConnection.ExecuteAsync(@"DELETE FROM Klientai WHERE Id = @id", new { Id = id });

            dbConnection.Close();



        }
    }
}
