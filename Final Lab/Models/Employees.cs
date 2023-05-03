using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final_Lab.Models
{

    public class Employees
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(8)]
        public string empId { get; set; }
        public string empName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public DateTime hireDate { get; set; }
        public string position_Id { get; set; }
    }
}
