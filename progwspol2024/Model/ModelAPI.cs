namespace Model
{
    public abstract class ModelAPI
    {

        public static ModelAPI createTableInstance()
        {
            return new Table();
        }

        public abstract void createBalls(int amount);

        public abstract List<Ball> getBalls();

        public abstract int getBoardWidth();
        public abstract int getBoardHeight();

    }
}
