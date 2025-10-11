using System.Threading;
using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.Domain;
using InvoiceEF.Crud.Infrastructure.Repositories.Contracts;

namespace InvoiceEF.Crud.Application.Services.Implementations
{
    public class StudentService(IStudentRepository studentRepository) : IStudentService
    {
        private readonly IStudentRepository _studentRepository = studentRepository;

        public async Task<IEnumerable<StudentTest>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _studentRepository.GetAllAsync(cancellationToken);
        }

        public async Task<StudentTest?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _studentRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<bool> UpdateAsync(StudentTest student, CancellationToken cancellationToken)
        {
            return await _studentRepository.UpdateAsync(student, cancellationToken);
        }

        public async Task<bool> AddAsync(StudentTest student, CancellationToken cancellationToken)
        {
            return await _studentRepository.AddAsync(student, cancellationToken);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            return await _studentRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
