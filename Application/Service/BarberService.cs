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
                Specialty = barber.Specialty

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
                Specialty = barber.Specialty

            };

        }

        public bool CreateBarber(CreateBarberRequest request)
        {
            // Aca iria la logica de negocio ej: Si el email existe

            // Esto es el mapeo del DTO a entidad dominio
            var BarberEntity = new Barber
            {
                Name = request.Name,
                Specialty = request.Specialty

                //Aca se hashea la contraseña.
            };
            return _barberRepository.CreateBarber(BarberEntity);
        }

        public bool UpdateBarber(int id, UpdateBarberRequest request)
        {
            var BarberToUpdate = _barberRepository.GetBarberById(id);

            if (BarberToUpdate == null)
            {
                return false;
            }
            BarberToUpdate.Name = request.Name;
            BarberToUpdate.Specialty = request.Specialty;

            return _barberRepository.UpdateBarber(BarberToUpdate);
        }

        public bool DeleteBarber(int id)
        {
            return _barberRepository.DeleteBarber(id);
        }
    }
}
