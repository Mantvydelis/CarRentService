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
        public List<NuomosUzsakymas> GautiVisusNuomosUzsakymus()
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
                END AS AutoTipas
            FROM 
                NuomosUzsakymas";

                var uzsakymai = connection.Query<NuomosUzsakymas>(query).ToList();
                return uzsakymai;
            }
        }

        public void PridetiNaujaUzsakyma(NuomosUzsakymas nuomosUzsakymas)
        {
            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                string query = "";


                if (nuomosUzsakymas.AutoTipas == "2")
                {
                    query = "INSERT INTO NuomosUzsakymas (KlientasId, BenzAutomobilisId, NuomosPradzia, DienuKiekis) VALUES (@KlientasId, @AutomobilisId, @NuomosPradzia, @DienuKiekis)";
                }
                else if (nuomosUzsakymas.AutoTipas == "1")
                {
                    query = "INSERT INTO NuomosUzsakymas (KlientasId, ElektromobilisId, NuomosPradzia, DienuKiekis) VALUES (@KlientasId, @AutomobilisId, @NuomosPradzia, @DienuKiekis)";
                }

                using (var connection1 = new SqlConnection(_dbConnectionString))
                {
                    connection.Execute(query, nuomosUzsakymas);
                }
            }
        }


        public NuomosUzsakymas GautiUzsakymaPagalId(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
                dbConnection.Open();
                var result = dbConnection.QueryFirstOrDefault<NuomosUzsakymas>(
                    @"SELECT Id AS UzsakymasId, KlientasId, BenzAutomobilisId, ElektromobilisId, NuomosPradzia, DienuKiekis 
          FROM NuomosUzsakymas WHERE Id = @Id", new { Id = id });
            return result;
            
        }

        public void KoreguotiNuomosInfo(int id, int klientasId, string autoTipas, int automobilisId, DateTime nuomosPradzia, int dienuKiekis)
        {
            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                string query = "UPDATE NuomosUzsakymas SET KlientasId = @KlientasId, BenzAutomobilisId = CASE WHEN @AutoTipas = 'NaftosKuroAutomobilis' THEN @AutomobilisId ELSE NULL END, ElektromobilisId = CASE WHEN @AutoTipas = 'Elektromobilis' THEN @AutomobilisId ELSE NULL END, NuomosPradzia = @NuomosPradzia, DienuKiekis = @DienuKiekis WHERE Id = @Id";

                connection.Execute(query, new
                {
                    KlientasId = klientasId,
                    AutoTipas = autoTipas,
                    AutomobilisId = automobilisId,
                    NuomosPradzia = nuomosPradzia,
                    DienuKiekis = dienuKiekis,
                    Id = id
                });
            }
        }





    }



    }




