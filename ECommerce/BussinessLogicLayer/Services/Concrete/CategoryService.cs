using AutoMapper;
using BussinessLogicLayer.Configrations.Exceptions;
using BussinessLogicLayer.Configrations.Responses;
using BussinessLogicLayer.Dtos.CategoryDtos;
using BussinessLogicLayer.Services.Abstract;
using BussinessLogicLayer.Validations.FluentValidations.CategoryValidations;
using DataAccessLayer.Domains;
using DataAccessLayer.Repositories.EntityFrameworkRepositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Services.Concrete
{
    public class CategoryService : GenericService<Category>, ICategoryService
    {
        #region Field and Ctor
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _autoMapper;
        public CategoryService(IMapper autoMapper, ICategoryRepository CategoryRepository) : base(autoMapper, CategoryRepository)
        {
            _autoMapper = autoMapper;
            _categoryRepository = CategoryRepository;
        }

        #endregion

        #region Activate
        public async Task<CommandResponse> ActivateAsync(Guid Id)
        {
            try
            {
                Category item = await _categoryRepository.GetByIdAsync(Id);
                if (item == null)
                    return null;
                item.IsActive = true;
                bool IsUpdated = await _categoryRepository.UpdateAsync(item);
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


        #region UnActivate
        public async Task<CommandResponse> UnActivateAsync(Guid Id)
        {
            try
            {
                Category item = await _categoryRepository.GetByIdAsync(Id);
                if (item == null)
                    return null;
                item.IsActive = false;
                bool IsUpdated = await _categoryRepository.UpdateAsync(item);
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
        public async Task<IEnumerable<GetAllCategoryRequestDto>> GetAllAsync()
        {
            try
            {
                IEnumerable<Category> items = await _categoryRepository.GetAllByAsync();
                IEnumerable<GetAllCategoryRequestDto> result = _autoMapper.Map<IEnumerable<Category>, IEnumerable<GetAllCategoryRequestDto>>(items);

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion


        #region GetAllActivated
        public async Task<IEnumerable<GetAllCategoryRequestDto>> GetAllActivatedAsync()
        {
            try
            {
                IEnumerable<Category> items = await _categoryRepository.GetAllByAsync(x => x.IsActive == true);

                IEnumerable<GetAllCategoryRequestDto> result = _autoMapper.Map<IEnumerable<Category>, IEnumerable<GetAllCategoryRequestDto>>(items);

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion


        #region GetAllUnActivated

        public async Task<IEnumerable<GetAllCategoryRequestDto>> GetAllUnActivatedAsync()
        {
            try
            {
                IEnumerable<Category> items = await _categoryRepository.GetAllByAsync(x => x.IsActive == false);

                IEnumerable<GetAllCategoryRequestDto> result = _autoMapper.Map<IEnumerable<Category>, IEnumerable<GetAllCategoryRequestDto>>(items);

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        #endregion


        #region Update

        public async Task<CommandResponse> UpdateAsync(UpdateCategoryRequestDto item)
        {
            try
            {
                var getItem = await _categoryRepository.GetByIdAsync(item.Id);
                if (item is not null && getItem is not null)
                {
                    //validation
                    var validator = new UpdateCategoryRequestValidator();
                    validator.Validate(item).throwIfValidationException();
                    //set last modify time
                    //mapping
                    Category mappedItem = _autoMapper.Map<Category>(item);

                    bool IsUpdated = await _categoryRepository.UpdateAsync(mappedItem);
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
        public async Task<CommandResponse> CreateAsync(CreateCategoryRequestDto item)
        {
            try
            {
                if (item is not null)
                {
                    //validation
                    var validator = new CreateCategoryRequestValidator();
                    validator.Validate(item).throwIfValidationException();

                    //mapping
                    Category mappedItem = _autoMapper.Map<Category>(item);
                    mappedItem.CreatedDate = DateTime.Now;
                    var IsCreated = await _categoryRepository.CreateAsync(mappedItem);
                    if (IsCreated == true)
                        return new CommandResponse { Status = true, Message = "This operation has not done successfully" };
                    return new CommandResponse { Status = false, Message = "This operation has not done successfully" };
                }

                { return new CommandResponse { Status = false, Message = "This operation has not done successfully" }; }

            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        #endregion

        #region GetByIdAsync
        public async Task<GetCategoryRequestDto> GetByIdAsync(Guid Id)
        {
            try
            {
                Category item = await _categoryRepository.GetByIdAsync(Id);
                if (item is not null)
                {
                    //mapping
                    GetCategoryRequestDto mappedItem = _autoMapper.Map<GetCategoryRequestDto>(item);

                    return mappedItem;
                }
                return null;

            }
            catch (Exception ex) { return null; }
        }

        #endregion
    }
}
