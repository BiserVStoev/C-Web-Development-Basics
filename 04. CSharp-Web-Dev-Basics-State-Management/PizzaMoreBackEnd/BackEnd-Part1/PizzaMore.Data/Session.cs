namespace PizzaMore.Data
{
    using System.ComponentModel.DataAnnotations;

    public class Session
    {
        [Key]
        public string Id { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public override string ToString()
        {
            return $"{this.Id}\t{this.User.Id}";
        }
    }
}
