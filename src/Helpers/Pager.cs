namespace Helpers
{
    public class Pager
    {
        public uint Start { get; set; }
        public uint Size { get; set; }

        public Pager(uint start, uint size)
        {
            Start = start;
            Size = size;
        }
    }
}