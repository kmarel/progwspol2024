using Data;
using Logic;

namespace LogicTest
{
    [TestClass]
    public class LogicLayerTest
    {
        internal class TestData : DataAPI
        {
            public override int radius => 5;

            public override int height => 300;

            public override int width => 500;
        }

        [TestMethod]
        public void GettersTest()
        {
            LogicAPI logicAPI = LogicAPI.createTableInstance(new TestData());
            Assert.AreEqual(logicAPI.getHeight(), 300);
            Assert.AreEqual(logicAPI.getWidth(), 500);
            Assert.IsNotNull(logicAPI);
        }

    }
}