using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO
{
    public class BaseClass
    {
        [Key]
        public string Id { get; set; }
    }
}
