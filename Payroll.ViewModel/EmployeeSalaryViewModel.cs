using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.ViewModel
{
    public class EmployeeSalaryViewModel
    {
        public int Id { get; set; }

        public string BadgeId { get; set; }
                
        public int PayrollPeriodId { get; set; }

        [Display(Name = "Period Year")]
        public int PeriodYear { get; set; }

        [Display(Name = "Period Month")]
        public int PeriodMonth { get; set; }

        public string Description
        {
            get
            {
                if (PeriodMonth > 0)
                {
                    return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(PeriodMonth) + " " + PeriodYear.ToString();
                }
                else
                {
                    return "No Selected Period";
                }
            }
        }

        [Display(Name = "Code")]
        public string SalaryComponentCode { get; set; }

        public int SalaryComponentId { get; set; }

        [Display(Name = "Salary Component")]
        public string SalaryComponentName { get; set; }

        public decimal BasicValue { get; set; }

        public decimal FinalValue { get; set; }

        public bool IsActivated { get; set; }
    }
}
