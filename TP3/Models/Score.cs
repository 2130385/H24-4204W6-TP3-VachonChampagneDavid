namespace TP3.Models
{
    public class Score
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public virtual User? User { get; set; }
    }
}
