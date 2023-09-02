using System.Reflection.Metadata.Ecma335;

namespace PayrollManagement.Domain.ValueObjects
{
    public record EmploymentContract
    {
        public string EmploymentContractType { get; private set; } = string.Empty;
        
        public DateTime JoiningDate { get; private set; }
        
        public DateTime? EndedContractTime { get; private set; }

        private EmploymentContract(string employmentContractType, DateTime joiningDate, DateTime endedContractTime)
        {
            EmploymentContractType = employmentContractType;
            JoiningDate = joiningDate;
            EndedContractTime = endedContractTime;
        }

        private EmploymentContract(string employmentContractType, DateTime joiningDate, DateTime? endedContractTime)
        {
            EmploymentContractType = employmentContractType;
            JoiningDate = joiningDate;
            EndedContractTime = endedContractTime;
        }

        private EmploymentContract(string employmentContractType, DateTime joiningDate)
        {
            EmploymentContractType = employmentContractType;
            JoiningDate = joiningDate;
        }

        public static EmploymentContract? Create(string employmentContractType, int contractTime)
        {
            if (string.IsNullOrEmpty(employmentContractType))
            {
                return null;
            }

            if (contractTime == 0)
            {
                // Si no hay un tiempo limite de contrato, quiere decir que es indefinido, y que por tanto no 
                //debemos colocar un mes de terminación del contrato.
                return new EmploymentContract(employmentContractType, DateTime.UtcNow);
            }

            var joiningDate = DateTime.UtcNow;
            var endedContractTime = DateTime.UtcNow.AddMonths(contractTime);

            return new EmploymentContract(employmentContractType, joiningDate, endedContractTime);
        }

        public static EmploymentContract? Create(string employmentContractType, int contractTime, DateTime joiningDate, DateTime? endedContractTime)
        {
            if (string.IsNullOrEmpty(employmentContractType)) 
            { 
               return null;
            }

            if (endedContractTime is null)
            {
                return null;
            }

            endedContractTime.Value.AddMonths(contractTime);

            return new EmploymentContract(employmentContractType, joiningDate, endedContractTime);
            
        }

    }
}
