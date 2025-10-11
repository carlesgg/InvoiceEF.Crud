using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceEF.Crud.Domain;

namespace InvoiceEF.Crud.Application.Services.Contracts
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentTest>> GetAllAsync(CancellationToken cancellationToken);
        Task<StudentTest?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> AddAsync(StudentTest student, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(StudentTest student, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
