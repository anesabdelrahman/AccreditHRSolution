using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HR.Domain.Models
{
    public class Employee : Entity
    {
        [DisplayName("Employee Number")]
        public int EmployeeNumber { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(30)]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(30)]
        public string LastName { get; set; }

        [DisplayName("Date Of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }


        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Department { get; set; }

        [DisplayName("Employee Status")]
        public EmployeeStatus EmployeeStatus { get; set; }

        public string Status { get; set; }

        public override string DisplayName
        {
            get
            {
                return FirstName + ' ' + LastName;
            }
            internal set { }
        }
    }
}
