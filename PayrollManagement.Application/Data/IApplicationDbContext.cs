﻿using Microsoft.EntityFrameworkCore;
using PayrollManagement.Domain.Employees;
using PayrollManagement.Domain.ValueObjects;

namespace PayrollManagement.Application.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Employee> Employees { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default); 

        //El paquete de entity framework es el 6.0
    }
}
