using Microsoft.Extensions.Logging;
using StatusManagerAPI.Models.Dtos;
using StatusManagerAPI.Models;
using StatusManagerAPI.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace StatusManagerAPI.Services
{
    public class StatusService : IStatusService
    {
        private readonly StatusManagerContext _context;
        private readonly ILogger<StatusService> _logger;
        private readonly IMapper _mapper;

        public StatusService(StatusManagerContext context, ILogger<StatusService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Statuses.FindAsync(id);
            if (entity == null) return false;

            _context.Statuses.Remove(entity);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Status with ID {Id} deleted successfully.", id);
            return true;
        }

        public async Task<IEnumerable<StatusDto>> GetAllAsync()
        {
            var statusList = await _context.Statuses.ToListAsync();

            if (!statusList.Any()) return Enumerable.Empty<StatusDto>();

            _logger.LogInformation("{Count} statuses retrieved.", statusList.Count);

            return _mapper.Map<IEnumerable<StatusDto>>(statusList); // noteb: dto olarak donuyoruz objeleri, veritabani objelerini donmuyoruz
        }

        public async Task<StatusDto?> GetByIdAsync(int id)
        {
            var status = await _context.Statuses.FindAsync(id);
            return status == null ? null : _mapper.Map<StatusDto>(status);
        }

        public async Task<StatusDto> PostAsync(StatusDto dto)
        {
            var entity = _mapper.Map<Status>(dto);

            await _context.Statuses.AddAsync(entity);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Status with ID {Id} created.", entity.Id);

            return _mapper.Map<StatusDto>(entity);
        }

        public async Task<bool> UpdateAsync(int id, StatusDto dto)
        {
            if (id != dto.Id) return false;

            var entity = await _context.Statuses.FindAsync(id);
            if (entity == null) return false;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Status with ID {Id} updated.", id);

            return true;
        }
    }
}
