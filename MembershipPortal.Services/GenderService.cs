﻿using MembershipPortal.Data;
using MembershipPortal.DTOs;
using MembershipPortal.IRepositories;
using MembershipPortal.IServices;
using MembershipPortal.Models;
using System.Reflection;

namespace MembershipPortal.Services
{
    public class GenderService : IGenderService
    {
        private readonly IGenderRepository _genderRepository;
        private readonly MembershipPortalDbContext _dbContext;
        public GenderService(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }
        public async Task<GetGenderDTO> CreateGenderAsync(CreateGenderDTO genderDTO)
        {
            try
            {
                var gender =  await _genderRepository.CreateAsync(new Gender()
                {
                    GenderName = genderDTO.GenderName
                });

                var genderDto = new GetGenderDTO(gender.Id, gender.GenderName);

                return genderDto;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return null;
        }

        public async Task<bool> DeleteGenderAsync(long id)
        {
            try
            {
                var gender = await _genderRepository.GetAsyncById(id);

                if(gender != null)
                {
                    return await _genderRepository.DeleteAsync(gender);
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public async Task<GetGenderDTO> GetGenderAsync(long id)
        {
            try
            {
                var gender = await _genderRepository.GetAsyncById(id);

                var genderDto = new GetGenderDTO(gender.Id, gender.GenderName);

                return genderDto;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<IEnumerable<GetGenderDTO>> GetGendersAsync()
        {
            try
            {
                var genders = await _genderRepository.GetAsyncAll();

                var genderDto = genders.Select(gender => new GetGenderDTO(

                    gender.Id,
                    gender.GenderName
                ));

                return genderDto;
                
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);////
            }
            return null;
        }
        
        public async Task<GetGenderDTO> UpdateGenderAsync(long id, UpdateGenderDTO genderDTO)
        {
            try
            {
                var oldGender = await _genderRepository.GetAsyncById(id);

                if(oldGender != null)
                {
                    oldGender.GenderName = genderDTO.GenderName;

                    await _genderRepository.UpdateAsync(oldGender); 

                   return new GetGenderDTO(oldGender.Id, oldGender.GenderName);
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        

    }
}