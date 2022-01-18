using System.Collections.Generic;
using System.Threading.Tasks;

namespace Compentio.Pipes.Contracts
{
    public interface INotes
    {
        Task<IEnumerable<Note>> GetNotes();
    }
}
