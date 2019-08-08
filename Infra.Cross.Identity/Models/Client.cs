using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Cross.Identity.Models
{
    //bronsers que o usuario marcou remember me
    [Table("AspNetClient", Schema = "DEZESSEIS_NEW")]
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [StringLength(256)]
        public string ClientKey { get; set; }
    }
}