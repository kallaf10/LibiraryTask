using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;


namespace Libirary.Models
{
    public class BorrowingOperation
    {
        public Guid BSN { get; set; }
        public int numberofCopiestoborrow { get; set; }
    }
}
