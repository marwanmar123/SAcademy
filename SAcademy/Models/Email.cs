using System.ComponentModel.DataAnnotations.Schema;

namespace SAcademy.Models
{
    public class Email
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? EmailAdress { get; set; }
        public string? From { get; set; }

        public string? To { get; set; }

        public string? Subject { get; set; }

        public string? Body { get; set; }

        public string? Password { get; set; }

        public Email()
        {
            To = "simplonacademy@simplon.co";
            Password = "oimdvwnzdfnnzaqj";
        }
    }
}
