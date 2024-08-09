using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Repositories
{
    public class AutomobiliaiDbRepository : IAutomobiliaiRepository
    {
        private readonly string _dbConnectionString;
        public AutomobiliaiDbRepository(string connectionString)
        {
            _dbConnectionString = connectionString;
        }
        public async Task IrasytiAutomobilius()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Automobilis>> NuskaitytiAutomobilius()
        {
            throw new NotImplementedException();
        }
        public async Task<List<Elektromobilis>> GautiVisusElektromobilius()
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            var result = await dbConnection.
                QueryAsync<Elektromobilis>(@"SELECT [Id] AS AutomobilisId, [Marke],[Modelis],[NuomosKaina],[BaterijosTalpa] FROM [dbo].[Elektromobiliai]");
            dbConnection.Close();
            return result.ToList();

        }


        public async Task<List<NaftosKuroAutomobilis>> GautiVisusNaftosKuroAutomobilius()
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            var result = await dbConnection.
                QueryAsync<NaftosKuroAutomobilis>(@"SELECT [Id] AS AutomobilisId, [Marke],[Modelis],[NuomosKaina],[DegaluSanaudos] FROM [dbo].[NaftosKuroAuto]");
            dbConnection.Close();
            return result.ToList();
        }

        public async Task IrasytiElektromobili(Elektromobilis elektromobilis)
        {
            string sqlCommand = "INSERT INTO Elektromobiliai ([Marke],[Modelis],[NuomosKaina],[BaterijosTalpa]" +
                ",[KrovimoLaikas]) VALUES (@Marke, @Modelis, @NuomosKaina, @BaterijosTalpa, @KrovimoLaikas)";

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                await connection.ExecuteAsync(sqlCommand, elektromobilis);
            }
        }

        public async Task IrasytiNaftosKuroAutomobili(NaftosKuroAutomobilis naftosKuroAutomobilis)
        {
            string sqlCommand = "INSERT INTO NaftosKuroAuto ([Marke],[Modelis],[NuomosKaina],[DegaluSanaudos]) VALUES (@Marke, @Modelis, @NuomosKaina, @DegaluSanaudos)";

            using (var connection = new SqlConnection(_dbConnectionString))
            {
               await connection.ExecuteAsync(sqlCommand, naftosKuroAutomobilis);
            }
        }


        public async Task<Elektromobilis> GautiElektromobiliPagalId(int id)
        {

            using (IDbConnection dbConnection = new SqlConnection(_dbConnectionString))
            {

                dbConnection.Open();
                var result = await dbConnection.QueryFirstOrDefaultAsync<Elektromobilis>(
                    "SELECT Id AS AutomobilisId, Marke, Modelis, NuomosKaina, BaterijosTalpa, KrovimoLaikas FROM Elektromobiliai WHERE Id = @Id",
                    new { Id = id }
                );
                if (result != null)
                {
                    return new Elektromobilis(result.AutomobilisId, result.Marke, result.Modelis, result.NuomosKaina, result.BaterijosTalpa, result.KrovimoLaikas);
                }
                return null;

            }



        }

        public async Task<NaftosKuroAutomobilis> GautiNaftosAutoPagalId(int id)
        {

            using (IDbConnection dbConnection = new SqlConnection(_dbConnectionString))
            {

                dbConnection.Open();
                var result = await dbConnection.QueryFirstOrDefaultAsync<NaftosKuroAutomobilis>(
                    "SELECT Id AS AutomobilisId, Marke, Modelis, NuomosKaina, DegaluSanaudos FROM NaftosKuroAuto WHERE Id = @Id",
                    new { Id = id }
                );
                if (result != null)
                {
                    return new NaftosKuroAutomobilis(result.AutomobilisId, result.Marke, result.Modelis, result.NuomosKaina, result.DegaluSanaudos);
                }
                return null;

            }



        }

        public async Task<NaftosKuroAutomobilis> KoreguotiNaftaAutoInfo(int id, string marke, string modelis, decimal nuomosKaina, double degaluSanaudos)
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            var query = @"UPDATE NaftosKuroAuto
                  SET Marke = @Marke, Modelis = @Modelis, NuomosKaina = @NuomosKaina, DegaluSanaudos = @DegaluSanaudos 
                  WHERE Id = @Id;
                  SELECT * FROM NaftosKuroAuto WHERE Id = @Id";
            var result =  await dbConnection.QueryFirstAsync<NaftosKuroAutomobilis>(query, new { Id = id, Marke = marke, Modelis = modelis, NuomosKaina = nuomosKaina, DegaluSanaudos = degaluSanaudos });
            dbConnection.Close();
            return result;


        }

        public async Task<Elektromobilis> KoreguotiElektromobilioInfo(int id, string marke, string modelis, decimal nuomosKaina, int baterijosTalpa, int krovimoLaikas)
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            var query = @"UPDATE Elektromobiliai
                  SET Marke = @Marke, Modelis = @Modelis, NuomosKaina = @NuomosKaina, BaterijosTalpa = @BaterijosTalpa 
                  WHERE Id = @Id;
                  SELECT * FROM Elektromobiliai WHERE Id = @Id";
            var result = await dbConnection.QueryFirstAsync<Elektromobilis>(query, new { Id = id, Marke = marke, Modelis = modelis, NuomosKaina = nuomosKaina, BaterijosTalpa = baterijosTalpa, KrovimoLaikas = krovimoLaikas });
            dbConnection.Close();
            return result;
        }


        public async Task IstrintiNaftaAuto(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();

            await dbConnection.ExecuteAsync(@"DELETE FROM NuomosUzsakymas WHERE BenzAutomobilisId = @id", new { Id = id });
            await dbConnection.ExecuteAsync(@"DELETE FROM NaftosKuroAuto WHERE Id = @id", new { Id = id });

            dbConnection.Close();
        }


        public async Task IstrintiElektromobili(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();

            await dbConnection.ExecuteAsync(@"DELETE FROM NuomosUzsakymas WHERE ElektromobilisId = @id", new { Id = id });
            await dbConnection.ExecuteAsync(@"DELETE FROM Elektromobiliai WHERE Id = @id", new { Id = id });

            dbConnection.Close();
        }


        public async Task<int> GautiElektromobiliuSkaiciu()
        {
            using IDbConnection dbConnection = new SqlConnection (_dbConnectionString);
            dbConnection.Open();
            var result = await dbConnection.ExecuteScalarAsync<int>(@"SELECT COUNT(Id) FROM Elektromobiliai");
            dbConnection.Close() ;
            return result;

        }

        public async Task<int> GautiNaftosKuroAutoSkaiciu()
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            var result = await dbConnection.ExecuteScalarAsync<int>(@"SELECT COUNT(Id) FROM NaftosKuroAuto");
            dbConnection.Close();
            return result;

        }


    }

        
    }

