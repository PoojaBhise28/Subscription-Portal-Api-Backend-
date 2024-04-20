﻿using MembershipPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MembershipPortal.IRepositories
{
    public interface IUserRepository :IRepository<User>
    {
        Task<IEnumerable<User>> GetUserSearchAsync(String find);

        Task<IEnumerable<User>> GetUserAdvanceSearchAsync(User userobj);

       
        Task<(IEnumerable<User>, int)> GetAllPaginatedAndSortedUserAsync(int page, int pageSize, string? sortColumn, string? sortOrder, User userObj);


    }
}
