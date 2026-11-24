using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Danis_Ioana_Lab2.Models
{
    public class Publisher
    {
        public int ID { get; set; }
        public string PublisherName { get; set; }
        [ScaffoldColumn(false)]
        public ICollection<Book>? Books { get; set; }  //navigation property 
    }
}