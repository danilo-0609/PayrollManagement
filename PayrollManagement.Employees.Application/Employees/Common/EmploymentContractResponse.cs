using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagement.Application.Employees.Common
{
    public record EmploymentContractResponse(string EmploymentContractType, 
        DateTime JoiningDate, 
        DateTime? EndedContractTime);
}
