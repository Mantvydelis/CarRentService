using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Enums;
using AutomobiliuNuoma.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.Common;
using System.Net.Http.Headers;

namespace AutomobiliuNuoma.Core.Repositories
{
    public class DarbuotojaiDbRepository : IDarbuotojaiRepository
    {
        private readonly string _dbConnectionString;
        public DarbuotojaiDbRepository(string connectionString)
        {
            _dbConnectionString = connectionString;
        }

        public async Task<Darbuotojas> GautiDarbuotojaPagalId(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            var result = await dbConnection.
                QueryFirstAsync<Darbuotojas>(@"SELECT * FROM Darbuotojai WHERE Id = @id", new { Id = id });
            dbConnection.Close();
            return result;
        }



        public async Task<List<Darbuotojas>> GautiVisusDarbuotojus()
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            var result = await dbConnection.QueryAsync<Darbuotojas>(@"SELECT [Id]
             ,[Vardas]
             ,[Pavarde]
             ,[Pareigos] 
             ,[BazinisAtlyginimas]
             ,[AtliktuUzsakymuKiekis]
             FROM [dbo].[Darbuotojai]");
            dbConnection.Close();
            return result.Select(dto => new Darbuotojas(dto.Id, dto.Vardas, dto.Pavarde, dto.Pareigos, dto.BazinisAtlyginimas, dto.AtliktuUzsakymuKiekis)).ToList();
        }


        public async Task IstrintiDarbuotoja(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();

            await dbConnection.ExecuteAsync(@"DELETE FROM Darbuotojai WHERE Id = @id", new { Id = id });

            dbConnection.Close();
        }

        public async Task<Darbuotojas> KoreguotiDarbuotojoInfo(int id, string vardas, string pavarde, DarbuotojasPareigos pareigos, double bazinisAtlyginimas, int atliktuUzsakymuSkaicius)
        {

            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                string query = "UPDATE Darbuotojai SET Vardas = @Vardas, Pavarde = @Pavarde, Pareigos = @Pareigos, BazinisAtlyginims = @BazinisAtlyginimas, AtliktuUzsakymuKiekis = @AtliktuUzsakymuKiekis WHERE Id = @Id";

                await connection.ExecuteAsync(query, new
                {
                    Id = id,
                    Vardas = vardas,
                    Pavarde = pavarde,
                    Pareigos = pareigos,
                    BazinisAtlyginimas = bazinisAtlyginimas,
                    AtliktuUzsakymuSkaicius = atliktuUzsakymuSkaicius
                });

                
            }
            return new Darbuotojas(id, vardas, pavarde, pareigos, bazinisAtlyginimas, atliktuUzsakymuSkaicius);

        }

        public async Task PridetiDarbuotoja(Darbuotojas darbuotojas)
        {
            string sqlCommand = "INSERT INTO Darbuotojai ([Vardas],[Pavarde],[Pareigos]) VALUES (@Vardas, @Pavarde, @Pareigos)";

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                await connection.ExecuteAsync(sqlCommand, darbuotojas);
            }
        }
    }
}
