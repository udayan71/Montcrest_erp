using Montcrest.BLL.DTOs;
using Montcrest.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montcrest.BLL.Interfaces
{
    public interface ITechnologyService
    {
        Task<IEnumerable<Technology>> GetAllAsync();

        Task<TechnologyDto?> GetIdByAsync(int id); 
    }
}
