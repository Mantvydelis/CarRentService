using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using AutomobiliuNuoma.Core.Services;
using Moq;
using System.Data;
using static System.Reflection.Metadata.BlobBuilder;

namespace AutonuomaTests
{
    public class AutonuomaTest
    {


        //Klientai

        [Fact]
        public void TestPridetiNaujaKlienta()
        {
            Mock<IKlientaiRepository> _klientaiRepository = new Mock<IKlientaiRepository>();
            Mock<IMongoDbCacheRepository> _mongoRepository = new Mock<IMongoDbCacheRepository>();

            IKlientaiService klientaiService = new KlientaiService(_klientaiRepository.Object, _mongoRepository.Object);

            Klientas klientas1 = new Klientas
            {
                KlientasId = 11,
                Vardas = "Petras",
                Pavarde = "Petraitis",
                GimimoMetai = DateOnly.Parse("1990-01-01"),

            };
            //Act
            klientaiService.PridetiNaujaKlienta(klientas1);
            //Assert
            _klientaiRepository.Verify(x => x.PridetiNaujaKlienta(It.IsAny<Klientas>()), Times.Once);

        }

        [Fact]
        public void TestGautiVisusKlientus()
        {
            //Arrange
            Klientas klientas1 = new Klientas
            {
                KlientasId = 11,
                Vardas = "Petras",
                Pavarde = "Petraitis",
                GimimoMetai = DateOnly.Parse("1990-01-01"),

            };
            Klientas klientas2 = new Klientas
            {
                KlientasId = 10,
                Vardas = "Antanas",
                Pavarde = "Antanaitis",
                GimimoMetai = DateOnly.Parse("1993-08-04"),

            };

            Mock<IKlientaiRepository> _klientaiRepository = new Mock<IKlientaiRepository>();
            Mock<IMongoDbCacheRepository> _mongoRepository = new Mock<IMongoDbCacheRepository>();
            List<Klientas> klientai = new List<Klientas> { klientas1, klientas2 };

            _klientaiRepository.Setup(x => x.GautiVisusKlientus().Result).Returns(klientai);


            //Act
            var result = _klientaiRepository.Object.GautiVisusKlientus().Result;

            //Assert
            Assert.Contains(result, k => k.KlientasId == klientas1.KlientasId);
            Assert.Contains(result, k => k.KlientasId == klientas2.KlientasId);


        }

        [Fact]
        public async Task TestGautiKlientaPagalId()
        {
            // Arrange
            Klientas klientas1 = new Klientas
            {
                KlientasId = 11,
                Vardas = "Petras",
                Pavarde = "Petraitis",
                GimimoMetai = DateOnly.Parse("1990-01-01"),
            };
            Klientas klientas2 = new Klientas
            {
                KlientasId = 10,
                Vardas = "Antanas",
                Pavarde = "Antanaitis",
                GimimoMetai = DateOnly.Parse("1993-08-04"),
            };

            var _klientaiRepository = new Mock<IKlientaiRepository>();
            var _mongoRepository = new Mock<IMongoDbCacheRepository>();

            _mongoRepository.Setup(x => x.GautiKlientaPagalId(10)).ReturnsAsync((Klientas)null);
            _klientaiRepository.Setup(x => x.GautiKlientaPagalId(10)).ReturnsAsync(klientas2);

            var service = new KlientaiService(_klientaiRepository.Object, _mongoRepository.Object);

            // Act
            var result = await service.GautiKlientaPagalId(10);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(klientas2.KlientasId, result.KlientasId);

        }

        [Fact]
        public void TestIstrintiKlienta()
        {
            Mock<IKlientaiRepository> _klientaiRepository = new Mock<IKlientaiRepository>();
            Mock<IMongoDbCacheRepository> _mongoRepository = new Mock<IMongoDbCacheRepository>();

            IKlientaiService klientaiService = new KlientaiService(_klientaiRepository.Object, _mongoRepository.Object);

            Klientas klientas1 = new Klientas
            {
                KlientasId = 11,
                Vardas = "Petras",
                Pavarde = "Petraitis",
                GimimoMetai = DateOnly.Parse("1990-01-01"),

            };
            klientaiService.PridetiNaujaKlienta(klientas1);
            //Act
            klientaiService.IstrintiKlienta(11);
            //Assert
            






        }
    }

}








//public async Task<Klientas> KoreguotiKlientoInfo(int id, string vardas, string pavarde, DateOnly gimimoMetai)
//{
//    string sqlCommand = "UPDATE Klientai " +
//        "SET Vardas = @Vardas, Pavarde = @Pavarde, GimimoMetai = @GimimoMetai " +
//        "WHERE Id = @Id; SELECT* FROM Klientai WHERE Id = @Id";
//    using (var connection = new SqlConnection(_dbConnectionString))
//    {
//        var pakeistiDuomenys = new
//        {
//            Id = id,
//            Vardas = vardas,
//            Pavarde = pavarde,
//            GimimoMetai = gimimoMetai.ToDateTime(new TimeOnly(0, 0))

//        };

//        var result = await connection.QueryFirstOrDefaultAsync<dynamic>(sqlCommand, pakeistiDuomenys);

//        return new Klientas(result.KlientasId, result.Vardas, result.Pavarde, DateOnly.FromDateTime(result.GimimoMetai));
//    }

//}

//public async Task IstrintiKlienta(int id)
//{
//    using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
//    dbConnection.Open();

//    await dbConnection.ExecuteAsync(@"DELETE FROM NuomosUzsakymas WHERE KlientasId = @id", new { Id = id });
//    await dbConnection.ExecuteAsync(@"DELETE FROM Klientai WHERE Id = @id", new { Id = id });

//    dbConnection.Close();

