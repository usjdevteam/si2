﻿using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace si2.dal.Repositories
{
    public interface IInstitutionRepository : IRepository<Institution>
    {
        IQueryable<Institution> GetAllComplete();
        Task<Institution> GetCompleteAsync(Guid id, CancellationToken ct);
    }
}
