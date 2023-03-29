using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace AppWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Nome é necessário!")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "O tamanho do nome precisa ser entre {2} e {1} caracteres")]         
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "{0} é necessário!")]
        [EmailAddress(ErrorMessage = "Entre com um email válido!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "{0} é necessário!")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} deve estar entre {1} a {2}")]
        [Display(Name = "Salário base")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double BaseSalary { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Data de aniversário é necessária!")]
        public DateTime BirthDate { get; set; }

        //[System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Necessário o departamento!")]
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }

        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(string name, string email, double baseSalary, DateTime birthDate, Department department)
        {
            Name = name;
            Email = email;
            BaseSalary = baseSalary;
            BirthDate = birthDate;
            Department = department;
        }

        public void AddSales(SalesRecord sales)
        {
            Sales.Add(sales);
        }

        public void RemoveSales(SalesRecord sales)
        {
            Sales.Remove(sales);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(s => s.Date >= initial && s.Date <= final).Sum(s => s.Amount);
        }
    }
}
