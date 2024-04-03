﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MembershipPortal.DTOs.ProductDTO;

namespace MembershipPortal.IServices
{
    public interface IProductService
    {

        public Task<IEnumerable<GetProductDTO>> GetProductAsync();
        public Task<GetProductDTO> GetProductAsync(long Id);

        public Task<GetProductDTO> CreateProductAsync(CreateProductDTO createProductDTO);

        public Task<GetProductDTO> UpdateProductAsync(long id, UpdateProductDTO updateProductDTO);

        public Task<bool> DeleteProductAsync(long Id);

    }
}