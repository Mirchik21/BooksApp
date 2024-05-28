using Domain.Common;

namespace Domain.Entities
{
    public class Book : AuditableEntity
    {
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public DateTime PublishYear { get; set; }
        public string Genre { get; set; }
    }
}