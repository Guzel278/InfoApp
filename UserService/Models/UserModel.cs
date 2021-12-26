using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Models
{
    public class UserModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

    }
}
