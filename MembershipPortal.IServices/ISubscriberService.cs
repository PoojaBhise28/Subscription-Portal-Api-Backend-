﻿using MembershipPortal.DTOs;
using MembershipPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipPortal.IServices
{
    public interface ISubscriberService
    {
        Task<GetSubscriberDTO> GetSubscriberAsync(long id);

        Task<IEnumerable<GetForeginSubscriberDTO>> GetSubscribersAsync();

        Task<GetSubscriberDTO> UpdateSubscriberAsync(long id, UpdateSubscriberDTO subscriberDTO);

        Task<GetSubscriberDTO> CreateSubscriberAsync(CreateSubscriberDTO subscriberDTO);

        Task<bool> DeleteSubscriberAsync(long id);

        Task<IEnumerable<GetSubscriberDTO>> SearchAsyncAll(string search);

        Task<(IEnumerable<GetForeginSubscriberDTO>, int)> GetAllPaginatedSubscriberAsync(int page, int pageSize, Subscriber subscriber);

        Task<IEnumerable<GetForeginSubscriberDTO>> GetAllSortedSubscribers(string? sortColumn, string? sortOrder);
    }
}
