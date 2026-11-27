using Danis_Ioana_Lab2.Models;
namespace Danis_Ioana_Lab2.Models.ViewModels
{
    public class PublisherIndexData
    {
        public IEnumerable<Publisher> Publishers { get; set; }
        public IEnumerable<Book> Books{ get; set; }
    }
}
