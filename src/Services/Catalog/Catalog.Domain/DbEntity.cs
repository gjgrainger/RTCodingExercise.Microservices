using System.ComponentModel.DataAnnotations;

namespace Catalog.Domain
{
    public class DbEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
