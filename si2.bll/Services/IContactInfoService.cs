using si2.bll.Dtos.Requests.ContactInfo;
using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public interface IContactInfoService : IServiceBase
    {
        Task<ContactInfo> CreateContactInfoAsync(CreateContactInfoDto createContactInfo, CancellationToken ct);

        Task<ContactInfo> UpdateContactInfoAsync(Guid id, UpdateContactInfoDto updateContactInfo, CancellationToken ct);
    }
}
