namespace Models
{
    public class BaseModel
    {
        public int Id { get; set; }

        public BaseModel()
        {
        }

        public BaseModel(int id)
        {
            Id = id;
        }
    }
}