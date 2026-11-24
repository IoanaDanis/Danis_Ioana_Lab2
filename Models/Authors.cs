using System.ComponentModel.DataAnnotations;

namespace Danis_Ioana_Lab2.Models
{
    public class Authors
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [ScaffoldColumn(false)]
        public ICollection<Book>? Books { get; set; }
    }
}
