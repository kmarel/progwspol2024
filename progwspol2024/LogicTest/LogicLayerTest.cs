using Logic;

namespace LogicTest
{
    [TestClass]
    public class LogicLayerTest
    {
        [TestMethod]
        public void GettersTest()
        {
            LogicAPI logicAPI = LogicAPI.createTableInstance();
            Assert.AreEqual(logicAPI.getHeight(), 330);
            Assert.AreEqual(logicAPI.getWidth(), 795);
            Assert.IsNotNull(logicAPI);
        }

        public void SettersTest()
        {
            LogicAPI logicAPI = LogicAPI.createTableInstance();
            logicAPI.setHeight(15);
            logicAPI.setWidth(30);

            Assert.AreEqual(logicAPI.getHeight(), 15);
            Assert.AreEqual(logicAPI.getWidth(), 30);
        }

        public void BallsTest()
        {
            LogicAPI logicAPI = LogicAPI.createTableInstance();
            logicAPI.addBallsToTable(3);
            Assert.AreEqual(logicAPI.getBalls().Count, 3);
            Assert.IsFalse(logicAPI.isWithinTable(new System.Numerics.Vector2(-1, 0)));
            Assert.IsFalse(logicAPI.isWithinTable(new System.Numerics.Vector2(0, -1)));
            Assert.IsTrue(logicAPI.isWithinTable(new System.Numerics.Vector2(1, 1)));

        }
    }
}