using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montcrest.DAL.Models
{
    public class Technology
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<JobApplication>? JobApplications { get; set; }

    }
}
