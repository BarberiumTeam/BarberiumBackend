using Application.Abstraction;
using Contracts.Barber.Request;
using Contracts.Barber.Response;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Service
{
    public class BarberService : IBarberService
    {
        private readonly IBarberRepository _barberRepository;
        
        public BarberService(IBarberRepository barberRepository)
        {
            _barberRepository = barberRepository;
        }

        public List<BarberResponse> GetAllBarbers()
        {
            var barbers = _barberRepository.GetAllBarbers();

            return barbers.Select(barber => new BarberResponse
            {
                Id = barber.Id,
                Name = barber.Name,
                Email = barber.Email,
                Phone = barber.Phone

            }).ToList();
        }

        // trae un solo barber por id
        public BarberResponse? GetBarberById(int barberId)
        {
            var barber = _barberRepository.GetBarberById(barberId);

            if (barber == null)
            {
                return null;
            }

            return new BarberResponse
            {
                Id = barber.Id,
                Name = barber.Name,
                Email = barber.Email,
                Phone = barber.Phone
            };

        }


        public bool UpdateBarber(int id, UpdateBarberRequest request)
        {
            var BarberToUpdate = _barberRepository.GetBarberById(id);

            if (BarberToUpdate == null)
            {
                return false;
            }
            BarberToUpdate.Name = request.Name;
            BarberToUpdate.Email = request.Email;
            BarberToUpdate.Phone = request.Phone;

            return _barberRepository.UpdateBarber(BarberToUpdate);
        }

        public bool DeleteBarber(int id)
        {
            return _barberRepository.DeleteBarber(id);
        }
    }
}
