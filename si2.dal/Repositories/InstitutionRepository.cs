﻿using Microsoft.EntityFrameworkCore;
using si2.dal.Context;
using si2.dal.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace si2.dal.Repositories
{
    public class InstitutionRepository : Repository<Institution>, IInstitutionRepository
    {
        public InstitutionRepository(Si2DbContext _db) : base(_db)
        {
        }

        public IQueryable<Institution> GetAllComplete()
        {
            return _db.Set<Institution>()
                .AsNoTracking()
                .Include(c => c.Address)
                .Include(c => c.ContactInfo)
                .Include(c => c.Parent)
                .Include(c => c.Parent.Address)
                .Include(c => c.Parent.ContactInfo);    
        }

        public async Task<Institution> GetCompleteAsync(Guid id, CancellationToken ct)
        {
            return await _db.Set<Institution>()
                .AsNoTracking()
                .Include(c => c.Address)
                .Include(c => c.ContactInfo)
                .Include(c => c.Parent)
                .Include(c => c.Parent.Address)
                .Include(c => c.Parent.ContactInfo)
                .FirstOrDefaultAsync(c => c.Id == id, ct);
        }
    }
}
