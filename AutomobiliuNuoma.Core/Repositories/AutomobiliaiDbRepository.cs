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
                Query<Elektromobilis>(@"SELECT * FROM [dbo].[Elektromobiliai]").ToList();
            dbConnection.Close();
            return result;
        }
        public List<NaftosKuroAutomobilis> GautiVisusNaftosKuroAutomobilius()
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            var result = dbConnection.
                Query<NaftosKuroAutomobilis>(@"SELECT * FROM [dbo].[NaftosKuroAuto]").ToList();
            dbConnection.Close();
            return result;
        }

        public void IrasytiElektromobili(Elektromobilis elektromobilis)
        {
            string sqlCommand = "INSERT INTO Elektromobiliai ([Marke],[Modelis],[NuomosKaina],[BaterijosTalpa]" +
                ",[KrovimoLaikas]) VALUES (@Marke, @Modelis, @NuomosKaina, @BaterijosTalpa, @KrovimoLaikas)";

            using(var connection = new SqlConnection(_dbConnectionString))
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
    }
}
