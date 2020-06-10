using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public interface IAddressService : IServiceBase
    {
        Task<Address> CreateAddressAsync(Address createAddress, CancellationToken ct);

        Task<Address> UpdateAddressAsync(Guid id, Address updateAddress, CancellationToken ct);

        Task DeleteAddressByIdAsync(Guid id, CancellationToken ct);
    }
}
