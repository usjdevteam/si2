﻿using si2.bll.Dtos.Requests.Address;
using si2.dal.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public interface IAddressService : IServiceBase
    {
        Task<Address> CreateAddressAsync(CreateAddressDto createAddress, CancellationToken ct);
        Task<Address> UpdateAddressAsync(Guid id, UpdateAddressDto updateAddress, CancellationToken ct);
        Task DeleteAddressByIdAsync(Guid id, CancellationToken ct);
    }
}
