using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montcrest.BLL.Interfaces
{
    public interface IGoogleSheetService
    {
        Task<int?> GetCandidateScoreAsync(string candidateEmail);
    }
}
