using System.ComponentModel.DataAnnotations;

namespace SignalR_CRUD.Models
{
    public class Product
    {
        [Key]
        public int ProdId { get; set; }
        public string? Name { get; set; }
        public string? Category { get;set ; }
    }
}
