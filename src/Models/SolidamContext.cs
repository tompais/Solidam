namespace Models
{
    public class SolidamContext : SolidamEntities
    {
        private SolidamContext()
        {
        }

        private static SolidamContext _instance;
        private static readonly object Obj = new object();

        public static SolidamContext Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (Obj)
                {
                    if (_instance == null)
                        _instance = new SolidamContext();
                }

                return _instance;
            }
        }
    }
}