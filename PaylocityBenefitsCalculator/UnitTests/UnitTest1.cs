using Api.Controllers;
using Api.Models.Database;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        IDataRepository repository = new InMemoryRepository();

        [TestMethod]
        public async void PaycheckCalculating()
        {
            PaycheckController controller = new PaycheckController(repository);

            var response = await controller.Get(0);

            Assert.IsTrue(response.Value.Success);

            Assert.Fail();
        }
    }
}