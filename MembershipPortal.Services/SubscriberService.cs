﻿using MembershipPortal.DTOs;
using MembershipPortal.IRepositories;
using MembershipPortal.IServices;
using MembershipPortal.Models;

namespace MembershipPortal.Services
{
    public class SubscriberService : ISubscriberService
    {
        private readonly ISubscriberRepository _subscriberRepository;

        public SubscriberService(ISubscriberRepository subscriberRepository)
        {
            _subscriberRepository = subscriberRepository;
        }
        public async Task<GetSubscriberDTO> CreateSubscriberAsync(CreateSubscriberDTO subscriberDTO)
        {
            try
            {
                var subscriber = await _subscriberRepository.CreateAsync(
                    new Subscriber()
                    {
                        FirstName = subscriberDTO.FirstName,
                        LastName = subscriberDTO.LastName,
                        ContactNumber = subscriberDTO.ContactNumber,
                        Email = subscriberDTO.Email,
                        GenderId = subscriberDTO.GenderId
                    });

                var getSubscriber = new GetSubscriberDTO
                                                    (subscriber.Id,
                                                        subscriber.FirstName,
                                                        subscriber.LastName,
                                                        subscriber.ContactNumber,
                                                        subscriber.Email,
                                                        subscriber.GenderId);

                return getSubscriber;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return null;
        }

        public async Task<bool> DeleteSubscriberAsync(long id)
        {
            try
            {
                var subscriber = await _subscriberRepository.GetAsyncById(id);

                if (subscriber != null)
                {
                    return await _subscriberRepository.DeleteAsync(subscriber);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public async Task<GetSubscriberDTO> GetSubscriberAsync(long id)
        {
            try
            {
                var subscriber = await _subscriberRepository.GetAsyncById(id);

                var getSubscriber = new GetSubscriberDTO
                                                    (subscriber.Id,
                                                        subscriber.FirstName,
                                                        subscriber.LastName,
                                                        subscriber.ContactNumber,
                                                        subscriber.Email,
                                                        subscriber.GenderId);

                return getSubscriber;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<IEnumerable<GetSubscriberDTO>> GetSubscribersAsync()
        {
            try
            {
                var subscribers = await _subscriberRepository.GetAsyncAll();

                var subscribersDto = subscribers.Select(
                                                         subscriber =>
                                                         new GetSubscriberDTO
                                                                                (subscriber.Id,
                                                                                subscriber.FirstName,
                                                                                subscriber.LastName,
                                                                                subscriber.ContactNumber,
                                                                                subscriber.Email,
                                                                                subscriber.GenderId));
                return subscribersDto;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<GetSubscriberDTO> UpdateSubscriberAsync(long id, UpdateSubscriberDTO subscriberDTO)
        {
            try
            {
                var oldSubscriber = await _subscriberRepository.GetAsyncById(id);

                if (oldSubscriber != null)
                {
                    oldSubscriber.FirstName = subscriberDTO.FirstName;
                    oldSubscriber.LastName = subscriberDTO.LastName;
                    oldSubscriber.ContactNumber = subscriberDTO.ContactNumber;
                    oldSubscriber.Email = subscriberDTO.Email;
                    oldSubscriber.GenderId = subscriberDTO.GenderId;

                    var subscriber = await _subscriberRepository.UpdateAsync(oldSubscriber);

                    var subscriberDto = new GetSubscriberDTO
                                        (subscriber.Id,
                                            subscriber.FirstName,
                                            subscriber.LastName,
                                            subscriber.ContactNumber,
                                            subscriber.Email,
                                            subscriber.GenderId);

                    return subscriberDto;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
                    
        }
    }
}