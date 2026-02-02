using AutoMapper;
using PetProject.Application.DTOs;
using PetProject.Application.Interfaces;
using PetProject.Domain.Entities;
using PetProject.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetProject.Application.Services
{
    public class ShopService : IShopService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShopService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductListDto?> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);
            return _mapper.Map<ProductListDto>(product);
        }

        public async Task<IEnumerable<ProductListDto>> GetProductsAsync()
        {
            var products = await _unitOfWork.Repository<Product>().GetAllAsync(asNoTracking: true);
            return _mapper.Map<IEnumerable<ProductListDto>>(products);
        }

        public async Task<IEnumerable<ServiceListDto>> GetServicesAsync()
        {
            var services = await _unitOfWork.Repository<Service>().GetAllAsync(asNoTracking: true);
            return _mapper.Map<IEnumerable<ServiceListDto>>(services);
        }
    }
}
