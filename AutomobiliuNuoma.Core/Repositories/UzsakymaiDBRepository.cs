using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using System;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Repositories
{
    public class UzsakymaiDBRepository : IUzsakymaiRepository
    {
        private readonly string _dbConnectionString;

        public UzsakymaiDBRepository(string connectionString)
        {
            _dbConnectionString = connectionString;
        }
        public async Task<List<NuomosUzsakymas>> GautiVisusNuomosUzsakymus()
        {
            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                string query = @"
                SELECT 
                Id AS UzsakymasId,
                KlientasId, 
                BenzAutomobilisId AS AutomobilisId, 
                ElektromobilisId AS AutomobilisId,
                NuomosPradzia, 
                DienuKiekis,
                CASE 
                    WHEN BenzAutomobilisId IS NOT NULL THEN 'NaftosKuroAutomobilis' 
                    WHEN ElektromobilisId IS NOT NULL THEN 'Elektromobilis'
                    ELSE 'Unknown'
                END AS AutoTipas,
                DarbuotojasId
            FROM 
                NuomosUzsakymas";

                var uzsakymai = await connection.QueryAsync<NuomosUzsakymas>(query);
                return uzsakymai.ToList();
            }
        }

        public async Task PridetiNaujaUzsakyma(NuomosUzsakymas nuomosUzsakymas)
        {
            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                string query = "";


                if (nuomosUzsakymas.AutoTipas == "NaftosKuroAutomobilis")
                {
                    query = "INSERT INTO NuomosUzsakymas (KlientasId, BenzAutomobilisId, NuomosPradzia, DienuKiekis, DarbuotojasId) VALUES (@KlientasId, @AutomobilisId, @NuomosPradzia, @DienuKiekis, @DarbuotojasId)";
                }
                else if (nuomosUzsakymas.AutoTipas == "Elektromobilis")
                {
                    query = "INSERT INTO NuomosUzsakymas (KlientasId, ElektromobilisId, NuomosPradzia, DienuKiekis, DarbuotojasId) VALUES (@KlientasId, @AutomobilisId, @NuomosPradzia, @DienuKiekis, @DarbuotojasId)";
                }

                using (var connection1 = new SqlConnection(_dbConnectionString))
                {
                   await connection.ExecuteAsync(query, nuomosUzsakymas);
                }
            }
        }


        public async Task<NuomosUzsakymas> GautiUzsakymaPagalId(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
                dbConnection.Open();
                var result = await dbConnection.QueryFirstOrDefaultAsync<NuomosUzsakymas>(
                    @"SELECT Id AS UzsakymasId, KlientasId, BenzAutomobilisId, ElektromobilisId, NuomosPradzia, DienuKiekis, DarbuotojasId 
          FROM NuomosUzsakymas WHERE Id = @Id", new { Id = id });
            return result;
            
        }

        public async Task KoreguotiNuomosInfo(int id, int klientasId, string autoTipas, int automobilisId, DateTime nuomosPradzia, int dienuKiekis, int darbuotojasId)
        {
            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                string query = @"
                UPDATE NuomosUzsakymas 
                 SET KlientasId = @KlientasId, 
                 BenzAutomobilisId = CASE WHEN @AutoTipas = 'NaftosKuroAutomobilis' THEN @AutomobilisId ELSE NULL END, 
                 ElektromobilisId = CASE WHEN @AutoTipas = 'Elektromobilis' THEN @AutomobilisId ELSE NULL END, 
                 NuomosPradzia = @NuomosPradzia, 
                 DienuKiekis = @DienuKiekis, 
                 DarbuotojasId = @DarbuotojasId 
                 WHERE Id = @Id";

                await connection.ExecuteAsync(query, new
                {
                    KlientasId = klientasId,
                    AutoTipas = autoTipas,
                    AutomobilisId = automobilisId,
                    NuomosPradzia = nuomosPradzia,
                    DienuKiekis = dienuKiekis,
                    Id = id,
                    DarbuotojasId = darbuotojasId
                });
            }
        }

        public async Task IstrintiUzsakyma(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();

           await dbConnection.ExecuteAsync(@"DELETE FROM NuomosUzsakymas WHERE Id = @id", new { Id = id });

            dbConnection.Close();



        }


        public async Task<NuomosUzsakymas> gautiUzsakymusPagalKlienta(int klientasid)
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            var result = await dbConnection.QueryFirstOrDefaultAsync<NuomosUzsakymas>(
                @"SELECT Id AS UzsakymasId, KlientasId, BenzAutomobilisId, ElektromobilisId, NuomosPradzia, DienuKiekis, DarbuotojasId 
          FROM NuomosUzsakymas WHERE KlientasId = @KlientasId", new { KlientasId = klientasid });
            return result;

        }




    }



    }




