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
        public void IrasytiAutomobilius()
        {
            throw new NotImplementedException();
        }

        public List<Automobilis> NuskaitytiAutomobilius()
        {
            throw new NotImplementedException();
        }
        public List<Elektromobilis> GautiVisusElektromobilius()
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            List<Elektromobilis> result = dbConnection.
                Query<Elektromobilis>(@"SELECT [Id] AS AutomobilisId
      ,[Marke]
      ,[Modelis]
      ,[NuomosKaina]
      ,[BaterijosTalpa]
      ,[KrovimoLaikas] FROM [dbo].[Elektromobiliai]").ToList();
            dbConnection.Close();
            return result;
        }
        public List<NaftosKuroAutomobilis> GautiVisusNaftosKuroAutomobilius()
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            var result = dbConnection.
                Query<NaftosKuroAutomobilis>(@"SELECT [Id] AS AutomobilisId
      ,[Marke]
      ,[Modelis]
      ,[NuomosKaina]
      ,[DegaluSanaudos] FROM [dbo].[NaftosKuroAuto]").ToList();
            dbConnection.Close();
            return result;
        }

        public void IrasytiElektromobili(Elektromobilis elektromobilis)
        {
            string sqlCommand = "INSERT INTO Elektromobiliai ([Marke],[Modelis],[NuomosKaina],[BaterijosTalpa]" +
                ",[KrovimoLaikas]) VALUES (@Marke, @Modelis, @NuomosKaina, @BaterijosTalpa, @KrovimoLaikas)";

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, elektromobilis);
            }
        }

        public void IrasytiNaftosKuroAutomobili(NaftosKuroAutomobilis naftosKuroAutomobilis)
        {
            string sqlCommand = "INSERT INTO NaftosKuroAuto ([Marke],[Modelis],[NuomosKaina],[DegaluSanaudos]) VALUES (@Marke, @Modelis, @NuomosKaina, @DegaluSanaudos)";

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, naftosKuroAutomobilis);
            }
        }

        public Automobilis PaieskaPagalId(int id)
        {

            throw new NotImplementedException();

        }

        public Elektromobilis GautiElektromobiliPagalId(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            var result = dbConnection.
                QueryFirst<Elektromobilis>(@"SELECT * FROM Elektromobiliai WHERE Id = @id", new { Id = id });
            dbConnection.Close();
            return result;
        }

        public NaftosKuroAutomobilis GautiNaftosAutoPagalId(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            var result = dbConnection.
                QueryFirst<NaftosKuroAutomobilis>(@"SELECT * FROM NaftosKuroAuto WHERE Id = @id", new { Id = id });
            dbConnection.Close();
            return result;
        }

        public NaftosKuroAutomobilis KoreguotiNaftaAutoInfo(int id, string marke, string modelis, decimal nuomosKaina, double degaluSanaudos)
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            var query = @"UPDATE NaftosKuroAuto
                  SET Marke = @Marke, Modelis = @Modelis, NuomosKaina = @NuomosKaina, DegaluSanaudos = @DegaluSanaudos 
                  WHERE Id = @Id;
                  SELECT * FROM NaftosKuroAuto WHERE Id = @Id";
            var result = dbConnection.QueryFirst<NaftosKuroAutomobilis>(query, new { Id = id, Marke = marke, Modelis = modelis, NuomosKaina = nuomosKaina, DegaluSanaudos = degaluSanaudos });
            dbConnection.Close();
            return result;


        }

        public Elektromobilis KoreguotiElektromobilioInfo(int id, string marke, string modelis, decimal nuomosKaina, int baterijosTalpa, int krovimoLaikas)
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            var query = @"UPDATE Elektromobiliai
                  SET Marke = @Marke, Modelis = @Modelis, NuomosKaina = @NuomosKaina, BaterijosTalpa = @BaterijosTalpa 
                  WHERE Id = @Id;
                  SELECT * FROM Elektromobiliai WHERE Id = @Id";
            var result = dbConnection.QueryFirst<Elektromobilis>(query, new { Id = id, Marke = marke, Modelis = modelis, NuomosKaina = nuomosKaina, BaterijosTalpa = baterijosTalpa, KrovimoLaikas = krovimoLaikas });
            dbConnection.Close();
            return result;
        }

    }
}
