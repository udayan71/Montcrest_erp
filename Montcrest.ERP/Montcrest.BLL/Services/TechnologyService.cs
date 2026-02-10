using Microsoft.Identity.Client;
using Montcrest.BLL.DTOs;
using Montcrest.BLL.Interfaces;
using Montcrest.DAL.Models;
using Montcrest.DAL.Repositories.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montcrest.BLL.Services
{
    public class TechnologyService: ITechnologyService
    {
        private readonly ITechnologyRepository _techRepo;

        public TechnologyService(ITechnologyRepository techRepo)
        {
            _techRepo = techRepo;
        }

        public async Task<IEnumerable<Technology>> GetAllAsync()
        {
            return await _techRepo.GetAllAsync();
        }

        public async Task<TechnologyDto?> GetIdByAsync(int id)
        {
            var tech = await _techRepo.GetByIdAsync(id);

            if (tech == null)
                return null;

            return new TechnologyDto
            {
                Id = tech.Id,
                Name = tech.Name
                
            };
        }

    }
}
