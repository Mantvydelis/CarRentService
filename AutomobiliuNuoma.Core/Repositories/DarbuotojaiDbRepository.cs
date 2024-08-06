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

        public Darbuotojas GautiDarbuotojaPagalId(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            var result = dbConnection.
                QueryFirst<Darbuotojas>(@"SELECT * FROM Darbuotojai WHERE Id = @id", new { Id = id });
            dbConnection.Close();
            return result;
        }

        public List<Darbuotojas> GautiVisusDarbuotojus()
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            var result = dbConnection.Query<Darbuotojas>(@"SELECT [Id]
             ,[Vardas]
             ,[Pavarde]
             ,[Pareigos] FROM [dbo].[Darbuotojai]").ToList();
            dbConnection.Close();
            return result.Select(dto => new Darbuotojas(dto.Id, dto.Vardas, dto.Pavarde, dto.Pareigos)).ToList();
        }


        public void IstrintiDarbuotoja(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();

            dbConnection.Execute(@"DELETE FROM Darbuotojai WHERE Id = @id", new { Id = id });

            dbConnection.Close();
        }

        public Darbuotojas KoreguotiDarbuotojoInfo(int id, string vardas, string pavarde, DarbuotojasPareigos pareigos)
        {

            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                string query = "UPDATE Darbuotojai SET Vardas = @Vardas, Pavarde = @Pavarde, Pareigos = @Pareigos WHERE Id = @Id";

                connection.Execute(query, new
                {
                    Id = id,
                    Vardas = vardas,
                    Pavarde = pavarde,
                    Pareigos = pareigos,
                });

                
            }
            return new Darbuotojas(id, vardas, pavarde, pareigos);

        }

        public void PridetiDarbuotoja(Darbuotojas darbuotojas)
        {
            string sqlCommand = "INSERT INTO Darbuotojai ([Vardas],[Pavarde],[Pareigos]) VALUES (@Vardas, @Pavarde, @Pareigos)";

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, darbuotojas);
            }
        }
    }
}
