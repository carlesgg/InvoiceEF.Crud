using InvoiceEF.Crud.Domain;

namespace InvoiceEF.Crud.Infrastructure.Repositories.Contracts
{
    public interface IStudentRepository
    {
        Task<IEnumerable<StudentTest>> GetAllAsync(CancellationToken cancellationToken);
        
        Task<StudentTest?> GetByIdAsync(int id, CancellationToken cancellationToken);
        
        Task<bool> AddAsync(StudentTest student, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(StudentTest student, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
