namespace GBP.Domain.Entities
{
    public class SubCategory
    {
        public int Id { get; set; }
        public required string Name { get; set; } = null!;

        // Foreign key to Category
        public int CategoryId { get; set; }

        // Navigation property - Parent
        public Category Category { get; set; }

        // Navigation property - Children
        public ICollection<Transaction> Transactions { get; set; } = [];
    }
}