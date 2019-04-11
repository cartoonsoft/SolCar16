using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdmCartorio.Models.Identity.Entities
{
    //bronsers que o usuario marcou remember me
    [Table("AspNetClient")]
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [StringLength(256)]
        public string ClientKey { get; set; }
    }
}