using System;

namespace Models
{
    public class SolidamContext : SolidamEntities
    {
        private static readonly Lazy<SolidamContext>
            Lazy =
                new Lazy<SolidamContext>
                    (() => new SolidamContext());

        public static SolidamContext Instance => Lazy.Value;

        private SolidamContext()
        {
        }
    }
}