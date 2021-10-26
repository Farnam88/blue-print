using System;
using System.ComponentModel.DataAnnotations;

namespace TestAssignment.WebApi.Controllers
{
    public class ValidationTestDto
    {
        [Required] public string Name { get; set; }
        [Required] public int Number { get; set; }
        [Required] public DateTime DateTime { get; set; }
    }
}