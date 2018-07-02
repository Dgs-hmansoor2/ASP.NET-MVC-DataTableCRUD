using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataTableCRUD.Models.Extended
{
    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee
    {
    }

    public class EmployeeMetadata
    {
        [Required (AllowEmptyStrings=false,ErrorMessage ="Please Provide First Name")]
        public String FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide Last Name")]
        public String LastName { get; set; }


    }

}