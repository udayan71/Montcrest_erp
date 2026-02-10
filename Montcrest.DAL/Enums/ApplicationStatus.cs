using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montcrest.DAL.Enums
{
    public enum ApplicationStatus
    {
        Applied=1,
        Reviewed = 2,
        ExamSent = 3,
        ExamCompleted = 4,
        Selected = 5,
        Rejected = 6
    }
}
