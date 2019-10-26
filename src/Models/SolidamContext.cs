namespace Models
{
    public class SolidamContext : SolidamEntities
    {
        private SolidamContext()
        {
        }

        public static SolidamContext Instance { get; } = new SolidamContext();
    }
}