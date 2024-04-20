﻿using MembershipPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MembershipPortal.DTOs.ProductDTO;

namespace MembershipPortal.IRepositories
{
   
    public interface IProductRepository : IRepository<Product>
    {

        Task<IEnumerable<Product>> GetProductSearchAsync(String find);

        Task<IEnumerable<Product>> GetProductAdvanceSearchAsync(Product productobj);

       

        Task<(IEnumerable<Product>, int)> GetAllPaginatedAndSortedProductAsync(int page, int pageSize, string? sortColumn, string? sortOrder, Product productObj);
    }
}
