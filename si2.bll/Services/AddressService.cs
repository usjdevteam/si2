using AutoMapper;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.Address;
using si2.dal.Entities;
using si2.dal.UnitOfWork;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public class AddressService : ServiceBase, IAddressService
    {
        //the methods here aren't currently used, we will need them most probably in the UI later
        public AddressService(IUnitOfWork uow, IMapper mapper, ILogger<IAddressService> logger) : base(uow, mapper, logger)
        {
        }

        public async Task<Address> CreateAddressAsync(CreateAddressDto createAddress, CancellationToken ct)
        {
            Address address = null;

            try
            {
                var addressEntity = _mapper.Map<Address>(createAddress);
                await _uow.Addresses.AddAsync(addressEntity, ct);
                await _uow.SaveChangesAsync(ct);
                address = _mapper.Map<Address>(addressEntity);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
            }

            return address;

        }

        public async Task<Address> UpdateAddressAsync(Guid id, UpdateAddressDto updateAddress, CancellationToken ct)
        {
            Address address = null;

            try
            {
                var updatedEntity = _mapper.Map<Address>(updateAddress);
                updatedEntity.Id = id;
                await _uow.Addresses.UpdateAsync(updatedEntity, id, ct, updatedEntity.RowVersion);
                await _uow.SaveChangesAsync(ct);
                var addressEntity = await _uow.Addresses.GetAsync(id, ct);
                address = _mapper.Map<Address>(addressEntity);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
            }

            return address;
        }

        public async Task DeleteAddressByIdAsync(Guid id, CancellationToken ct)
        {
            try
            {
                var addressEntity = await _uow.Addresses.FirstAsync(c => c.Id == id, ct);
                _uow.Addresses.Delete(addressEntity);
                await _uow.SaveChangesAsync(ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
            }
        }
    }
}
