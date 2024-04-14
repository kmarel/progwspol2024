using Data;

namespace DataTest
{
    [TestClass]
    public class DataLayerTest
    {
        [TestMethod]
        public void DataVariableTest()
        {
            DataAPI dataAPI = DataAPI.createInstance();
            Assert.AreEqual(dataAPI.radius, 10);
            Assert.AreEqual(dataAPI.height, 330);
            Assert.AreEqual(dataAPI.width, 795);
            Assert.IsNotNull(dataAPI);
        }
    }
}