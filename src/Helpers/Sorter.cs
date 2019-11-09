namespace Helpers
{
    public class Sorter
    {
        public string Property { get; set; }
        public string Direction { get; set; }

        public Sorter(string property, string direction)
        {
            Property = property;
            Direction = direction;
        }
    }
}