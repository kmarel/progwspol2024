namespace Data
{
    public abstract class DataAPI
    {
        public abstract int radius { get; }
        public abstract int height { get; }
        public abstract int width { get; }

        public static DataAPI createInstance()
        {
            return new Data();
        }
    }
}
