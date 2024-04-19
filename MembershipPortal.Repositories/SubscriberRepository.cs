﻿using MembershipPortal.Data;
using MembershipPortal.IRepositories;
using MembershipPortal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipPortal.Repositories
{
    public class SubscriberRepository : Repository<Subscriber>, ISubscriberRepository
    {
        private readonly MembershipPortalDbContext _dbContext;

        public SubscriberRepository(MembershipPortalDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Subscriber>> SearchAsyncAll(string search)
        {
            var keyword = search.ToLower();

            var subscriber = await _dbContext.Subscribers
                .Where(subscriber => subscriber.FirstName.ToLower() == keyword ||
                                     subscriber.LastName.ToLower() == keyword ||
                                     subscriber.Email.ToLower() == keyword ||
                                     subscriber.ContactNumber.ToLower() == keyword ||
                                     subscriber.Gender.GenderName.ToLower() == keyword 

                                     )
                .ToListAsync();

            return subscriber;
        }


       
       
        public async Task<(IEnumerable<Subscriber>, int)> GetAllPaginatedSubscriberAsync(int page, int pageSize, Subscriber subscriberObj)
        {
            var query = _dbContext.Subscribers.AsQueryable();



            if (!string.IsNullOrWhiteSpace(subscriberObj.FirstName))
            {
                query = query.Where(subscriber => subscriber.FirstName.Contains(subscriberObj.FirstName));
            }
            if (!string.IsNullOrWhiteSpace(subscriberObj.LastName))
            {
                query = query.Where(subscriber => subscriber.LastName.Contains(subscriber.LastName));
            }
            if (!string.IsNullOrWhiteSpace(subscriberObj.Email))
            {
                query = query.Where(subscriber => subscriber.Email.Contains(subscriber.Email));
            }
            if (!string.IsNullOrWhiteSpace(subscriberObj.ContactNumber))
            {
                query = query.Where(subscriber => subscriber.ContactNumber.Contains(subscriber.ContactNumber));
            }

            query = query.Include(gender=>gender.Gender);

            int totalCount = query.Count();
            int totalPages = (int)(Math.Ceiling((decimal)totalCount / pageSize));

            query = query.Skip((page - 1) * pageSize).Take(pageSize);
            return (await query.ToListAsync(), totalPages);
        }


        public async Task<IEnumerable<Subscriber>> GetAllSortedSubscribers(string? sortColumn, string? sortOrder)
        {
            IQueryable<Subscriber> query = _dbContext.Subscribers.Include(s => s.Gender);
            if (!string.IsNullOrWhiteSpace(sortColumn) && !string.IsNullOrWhiteSpace(sortOrder))
            {
                // Determine the sort order based on sortOrder parameter
                bool isAscending = sortOrder.ToLower() == "asc";
                switch (sortColumn.ToLower())
                {
                    case "firstname":
                        query = isAscending ? query.OrderBy(s => s.FirstName) : query.OrderByDescending(s => s.FirstName);
                        break;
                    case "lastname":
                        query = isAscending ? query.OrderBy(s => s.LastName) : query.OrderByDescending(s => s.LastName);
                        break;
                    case "email":
                        query = isAscending ? query.OrderBy(s => s.Email) : query.OrderByDescending(s => s.Email);
                        break;
                    case "contactnumber":
                        query = isAscending ? query.OrderBy(s => s.ContactNumber) : query.OrderByDescending(s => s.ContactNumber);
                        break;
                    case "gendername":
                        query = isAscending ? query.OrderBy(s => s.Gender.GenderName) : query.OrderByDescending(s => s.Gender.GenderName);
                        break;
                    default:
                        query = query.OrderBy(s => s.Id);
                        break;
                }

            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Subscriber>> GetAllForeginSubscribers()
        {
            var result = await _dbContext.Subscribers.Include(gender => gender.Gender).ToListAsync();
            return result;
        }

    }
}
