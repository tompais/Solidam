using System;

namespace Models
{
    public class SolidamContext : SolidamEntities
    {
        private static SolidamContext _instance;

        private SolidamContext()
        {
        }

        public static SolidamContext Instance => _instance ?? (_instance = new SolidamContext());

        public void CustomSaveChanges()
        {
            try
            {
                SaveChanges();
            }
            finally
            {
                Dispose();
                _instance = null;
            }
        }
    }
}