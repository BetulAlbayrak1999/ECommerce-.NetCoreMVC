using AutoMapper;
using BussinessLogicLayer.Configrations.Exceptions;
using BussinessLogicLayer.Configrations.Responses;
using BussinessLogicLayer.Dtos.CategoryDtos;
using BussinessLogicLayer.Dtos.ProductDtos;
using BussinessLogicLayer.Services.Abstract;
using BussinessLogicLayer.Validations.FluentValidations.ProductValidations;
using DataAccessLayer.Domains;
using DataAccessLayer.Repositories.EntityFrameworkRepositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Services.Concrete
{
    public class ProductService : GenericService<Product>, IProductService
    {
        #region Field and Ctor
        private readonly IProductRepository _productRepository;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _autoMapper;
        public ProductService(IMapper autoMapper, IProductRepository ProductRepository, ICategoryService categoryService) : base(autoMapper, ProductRepository)
        {
            _autoMapper = autoMapper;
            _productRepository = ProductRepository;
            _categoryService = categoryService;
        }

        #endregion

        #region Activate
        public async Task<CommandResponse> ActivateAsync(Guid Id)
        {
            try
            {
                Product item = await _productRepository.GetByIdAsync(Id);
                if (item == null)
                    return null;
                item.IsActive = true;
                bool IsUpdated = await _productRepository.UpdateAsync(item);
                if (IsUpdated)
                    return new CommandResponse { Status = true, Message = "This operation has not done successfully" };

                return new CommandResponse { Status = false, Message = "This operation has not done successfully" };
            }
            catch (Exception ex)
            {
                return new CommandResponse { Status = false, Message = ex.Message };
            }
        }
        #endregion

        #region getActivatedCategories
        public async Task<IEnumerable<GetAllCategoryRequestDto>> GetAllActivatedCategory()
        {
            try
            {
                var categories = await _categoryService.GetAllActivatedAsync();
                return categories;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion



        #region UnActivate
        public async Task<CommandResponse> UnActivateAsync(Guid Id)
        {
            try
            {
                Product item = await _productRepository.GetByIdAsync(Id);
                if (item == null)
                    return null;
                item.IsActive = false;
                bool IsUpdated = await _productRepository.UpdateAsync(item);
                if (IsUpdated)
                    return new CommandResponse { Status = true, Message = "This operation has not done successfully" };

                return new CommandResponse { Status = false, Message = "This operation has not done successfully" };
            }
            catch (Exception ex)
            {
                return new CommandResponse { Status = false, Message = ex.Message };
            }
        }
        #endregion



        #region GetAll
        public async Task<IEnumerable<GetAllProductRequestDto>> GetAllAsync()
        {
            try
            {
                IEnumerable<Product> items = await _productRepository.GetAllByAsync();
                IEnumerable<GetAllProductRequestDto> result = _autoMapper.Map<IEnumerable<Product>, IEnumerable<GetAllProductRequestDto>>(items);

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion


        #region GetAllActivated
        public async Task<IEnumerable<GetAllProductRequestDto>> GetAllActivatedAsync()
        {
            try
            {
                IEnumerable<Product> items = await _productRepository.GetAllByAsync(x => x.IsActive == true);

                IEnumerable<GetAllProductRequestDto> result = _autoMapper.Map<IEnumerable<Product>, IEnumerable<GetAllProductRequestDto>>(items);

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion


        #region GetAllUnActivated

        public async Task<IEnumerable<GetAllProductRequestDto>> GetAllUnActivatedAsync()
        {
            try
            {
                IEnumerable<Product> items = await _productRepository.GetAllByAsync(x => x.IsActive == false);

                IEnumerable<GetAllProductRequestDto> result = _autoMapper.Map<IEnumerable<Product>, IEnumerable<GetAllProductRequestDto>>(items);

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        #endregion


        #region Update

        public async Task<CommandResponse> UpdateAsync(UpdateProductRequestDto item)
        {
            try
            {
                var getItem = await _productRepository.GetByIdAsync(item.Id);
                if (item is not null && getItem is not null)
                {
                    //validation
                    var validator = new UpdateProductRequestValidator();
                    validator.Validate(item).throwIfValidationException();
                    //set last modify time
                    //mapping
                    Product mappedItem = _autoMapper.Map<Product>(item);
                   
                    bool IsUpdated = await _productRepository.UpdateAsync(mappedItem);
                    if (IsUpdated == true)
                        return new CommandResponse { Status = true, Message = "This operation has not done successfully" };

                    return new CommandResponse { Status = false, Message = "This operation has not done successfully" };
                }

                { return new CommandResponse { Status = false, Message = "This operation has not done successfully" }; }

            }
            catch (Exception ex) { throw new Exception(ex.Message); }

        }
        #endregion


        #region Create
        public async Task<CommandResponse> CreateAsync(CreateProductRequestDto item)
        {
            try
            {
                if (item is not null)
                {

                    item.Sku = GenerateString(7);
                    var discount = item.Price * item.Discount_Percentage / 100;
                    if(discount > 0)
                        item.Price = item.Price - discount;

                    //validation
                    var validator = new CreateProductRequestValidator();
                    validator.Validate(item).throwIfValidationException();

                    //mapping
                    Product mappedItem = _autoMapper.Map<Product>(item);
                    mappedItem.CreatedDate = DateTime.Now;
                    var IsCreated = await _productRepository.CreateAsync(mappedItem);
                    if (IsCreated == true)
                        return new CommandResponse { Status = true, Message = "This operation has not done successfully" };
                    return new CommandResponse { Status = false, Message = "This operation has not done successfully" };
                }

                { return new CommandResponse { Status = false, Message = "This operation has not done successfully" }; }

            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        private string GenerateString(int length)
        {
            // Creating object of random class
            Random rand = new Random();

            // Choosing the size of string
            // Using Next() string
            int stringlen = rand.Next(length, length);
            int randValue;
            string str = "";
            char letter;
            for (int i = 0; i < stringlen; i++)
            {

                // Generating a random number.
                randValue = rand.Next(0, 26);

                // Generating random character by converting
                // the random number into character.
                letter = Convert.ToChar(randValue + 65);

                // Appending the letter to string.
                str = str + letter;
            }
            return str;
        }
        #endregion

        #region GetByIdAsync
        public async Task<GetProductRequestDto> GetByIdAsync(Guid Id)
        {
            try
            {
                Product item = await _productRepository.GetByIdAsync(Id);
                    if (item is not null)
                    {
                        //mapping
                        GetProductRequestDto mappedItem = _autoMapper.Map<GetProductRequestDto>(item);

                        return mappedItem;
                    }
                    return null;

            }
            catch (Exception ex) { return null; }
        }

        #endregion
    }
}
