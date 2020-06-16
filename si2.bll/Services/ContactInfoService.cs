using AutoMapper;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.ContactInfo;
using si2.dal.Entities;
using si2.dal.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public class ContactInfoService : ServiceBase, IContactInfoService
    {
        public ContactInfoService(IUnitOfWork uow, IMapper mapper, ILogger<IContactInfoService> logger) : base(uow, mapper, logger)
        {
        }

        public async Task<ContactInfo> CreateContactInfoAsync(CreateContactInfoDto createContactInfo, CancellationToken ct)
        {
            ContactInfo contactInfo = null;

            try
            {
                var contactInfoEntity = _mapper.Map<ContactInfo>(createContactInfo);
                await _uow.ContactInfos.AddAsync(contactInfoEntity, ct);
                await _uow.SaveChangesAsync(ct);
                contactInfo = _mapper.Map<ContactInfo>(contactInfoEntity);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
            }

            return contactInfo;

        }

        public async Task<ContactInfo> UpdateContactInfoAsync(Guid id, UpdateContactInfoDto updateContactInfo, CancellationToken ct)
        {
            ContactInfo contactInfo = null;

            try
            {
                var updatedEntity = _mapper.Map<ContactInfo>(updateContactInfo);
                updatedEntity.Id = id;
                await _uow.ContactInfos.UpdateAsync(updatedEntity, id, ct, updatedEntity.RowVersion);
                await _uow.SaveChangesAsync(ct);
                var contactInfoEntity = await _uow.ContactInfos.GetAsync(id, ct);
                contactInfo = _mapper.Map<ContactInfo>(contactInfoEntity);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
            }

            return contactInfo;
        }
    }
}
