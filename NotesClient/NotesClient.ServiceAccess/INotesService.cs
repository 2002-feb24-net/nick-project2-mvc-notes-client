using System.Collections.Generic;
using System.Threading.Tasks;
using NotesClient.ServiceAccess.Models;

namespace NotesClient.ServiceAccess
{
    public interface INotesService
    {
        Task AddAsync(Note note);

        Task<IEnumerable<Note>> GetAllAsync();
    }
}
