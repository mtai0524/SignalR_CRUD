using System.ComponentModel.DataAnnotations;

namespace SignalR_CRUD.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
