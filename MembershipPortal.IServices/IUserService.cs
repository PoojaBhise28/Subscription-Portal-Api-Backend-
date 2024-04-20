﻿using MembershipPortal.DTOs;
using MembershipPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MembershipPortal.DTOs.UserDTO;

namespace MembershipPortal.IServices
{
    public interface IUserService
    {
        public Task<IEnumerable<GetUserDTO>> GetUsersAsync();
        public Task<GetUserDTO> GetUserAsync(long Id);

        public Task<GetUserDTO> CreateUserAsync(CreateUserDTO createUserDTO);
        
        public Task<GetUserDTO> UpdateUserAsync(long Id ,UpdateUserDTO updateUserDTO);   
    
        public Task<bool> DeleteUserAsync(long Id);

        public Task<IEnumerable<GetUserDTO>> GetUserSearchAsync(string find);

        public Task<IEnumerable<GetUserDTO>> GetUserAdvanceSearchAsync(GetUserDTO getUserDTO);

       

        Task<(IEnumerable<GetUserDTO>, int)> GetAllPaginatedAndSortedUserAsync(int page, int pageSize, string? sortColumn, string? sortOrder, User userObj);


    }
}
