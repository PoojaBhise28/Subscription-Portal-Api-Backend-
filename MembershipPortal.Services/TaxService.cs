﻿using MembershipPortal.DTOs;
using MembershipPortal.IRepositories;
using MembershipPortal.IServices;
using MembershipPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace MembershipPortal.Services
{
    public class TaxService : ITaxService
    {
        private readonly ITaxRepository _taxRepository;
        public TaxService(ITaxRepository taxRepository)
        {
            _taxRepository = taxRepository;
        }
        public async Task<GetTaxDTO> CreateTaxAsync(CreateTaxDTO taxDTO)
        {
            try
            {
                if (taxDTO == null) throw new ArgumentNullException(nameof(taxDTO));
                var tax = new Tax()
                {
                    StateName = taxDTO.StateName,
                    SGST = taxDTO.SGST,
                    CGST = taxDTO.CGST,
                    TotalTax = taxDTO.CGST + taxDTO.SGST,
                };
                var result = await _taxRepository.CreateAsync(tax);
                var newTaxDTO = new GetTaxDTO(result.Id, result.StateName,result.CGST, result.SGST, result.TotalTax);
                return newTaxDTO;
            }
            catch (Exception ex)
            {
               // Console.WriteLine($"Error occurred in CreateTaxAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteTaxAsync(long Id)
        {
            try
            {
                if (Id < 0) throw new ArgumentOutOfRangeException(nameof(Id));
                var tax = await _taxRepository.GetAsyncById(Id);
                return await _taxRepository.DeleteAsync(tax);
            }
            catch (Exception ex)
            {
              //  Console.WriteLine($"Error occurred in DeleteTaxAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<GetTaxDTO> GetTaxByIdAsync(long id)
        {
            try
            {
                var tax = await _taxRepository.GetAsyncById(id);
                if (tax != null)
                {
                    return new GetTaxDTO(tax.Id, tax.StateName, tax.SGST, tax.CGST, tax.TotalTax);
                }
                return null;
            }
            catch (Exception ex)
            {
               // Console.WriteLine($"Error occurred in GetTaxByIdAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<GetTaxDTO>> GetTaxesAsync()
        {
            try
            {
                var taxList = await _taxRepository.GetAsyncAll();
                if (taxList != null)
                {
                    var taxDTOList = taxList.Select(tax => new GetTaxDTO(tax.Id, tax.StateName, tax.SGST, tax.CGST, tax.TotalTax));
                    return taxDTOList;
                }
                return null;
            }
            catch (Exception ex)
            {
               // Console.WriteLine($"Error occurred in GetTaxesAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<GetTaxDTO> UpdateTaxAsync(long Id, UpdateTaxDTO taxDTO)
        {
            try
            {
                var oldTax = await _taxRepository.GetAsyncById(Id);
                if (oldTax != null)
                {
                    oldTax.SGST = taxDTO.SGST;
                    oldTax.CGST = taxDTO.CGST;
                    oldTax.TotalTax = taxDTO.SGST + taxDTO.CGST;
                    var tax = await _taxRepository.UpdateAsync(oldTax);
                    return new GetTaxDTO(tax.Id, tax.StateName, tax.SGST, tax.CGST, tax.TotalTax);
                }
                return null;
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error occurred in UpdateTaxAsync: {ex.Message}");
                throw;
            }
        }
    }
}
