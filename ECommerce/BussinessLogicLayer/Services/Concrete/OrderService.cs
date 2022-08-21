using AutoMapper;
using BussinessLogicLayer.Configrations.Exceptions;
using BussinessLogicLayer.Configrations.Responses;
using BussinessLogicLayer.Dtos.ApplicationUserDtos;
using BussinessLogicLayer.Dtos.OrderDtos;
using BussinessLogicLayer.Dtos.ProductDtos;
using BussinessLogicLayer.Services.Abstract;
using BussinessLogicLayer.Validations.FluentValidations.OrderValidations;
using DataAccessLayer.Domains;
using DataAccessLayer.Repositories.EntityFrameworkRepositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Services.Concrete
{
    public class OrderService : GenericService<Order>, IOrderService
    {
        #region Field and Ctor
        private readonly IOrderRepository _orderRepository;
        private readonly IApplicationUserService _applicationUserService;
        private readonly IProductService _productService;
        private readonly IMapper _autoMapper;
        public OrderService(IMapper autoMapper, IOrderRepository OrderRepository) : base(autoMapper, OrderRepository)
        {
            _autoMapper = autoMapper;
            _orderRepository = OrderRepository;
        }

        #endregion

        #region Activate
        public async Task<CommandResponse> ActivateAsync(Guid Id)
        {
            try
            {
                Order item = await _orderRepository.GetByIdAsync(Id);
                if (item == null)
                    return null;
                item.IsActive = true;
                bool IsUpdated = await _orderRepository.UpdateAsync(item);
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
                Order item = await _orderRepository.GetByIdAsync(Id);
                if (item == null)
                    return null;
                item.IsActive = false;
                bool IsUpdated = await _orderRepository.UpdateAsync(item);
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
        public async Task<IEnumerable<GetAllOrderRequestDto>> GetAllAsync()
        {
            try
            {
                IEnumerable<Order> items = await _orderRepository.GetAllByAsync();
                IEnumerable<GetAllOrderRequestDto> result = _autoMapper.Map<IEnumerable<Order>, IEnumerable<GetAllOrderRequestDto>>(items);

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion


        #region GetAllActivated
        public async Task<IEnumerable<GetAllOrderRequestDto>> GetAllActivatedAsync()
        {
            try
            {
                IEnumerable<Order> items = await _orderRepository.GetAllByAsync(x => x.IsActive == true);

                IEnumerable<GetAllOrderRequestDto> result = _autoMapper.Map<IEnumerable<Order>, IEnumerable<GetAllOrderRequestDto>>(items);

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion


        #region GetAllUnActivated

        public async Task<IEnumerable<GetAllOrderRequestDto>> GetAllUnActivatedAsync()
        {
            try
            {
                IEnumerable<Order> items = await _orderRepository.GetAllByAsync(x => x.IsActive == false);

                IEnumerable<GetAllOrderRequestDto> result = _autoMapper.Map<IEnumerable<Order>, IEnumerable<GetAllOrderRequestDto>>(items);

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        #endregion


        #region Update

        public async Task<CommandResponse> UpdateAsync(UpdateOrderRequestDto item)
        {
            try
            {
                var getItem = await _orderRepository.GetByIdAsync(item.Id);
                if (item is not null && getItem is not null)
                {
                    //validation
                    var validator = new UpdateOrderRequestValidator();
                    validator.Validate(item).throwIfValidationException();
                    //set last modify time
                    //mapping
                    Order mappedItem = _autoMapper.Map<Order>(item);

                    bool IsUpdated = await _orderRepository.UpdateAsync(mappedItem);
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
        public async Task<CommandResponse> CreateAsync(CreateOrderRequestDto item)
        {
            try
            {
                if (item is not null)
                {
                    Random rand = new Random();
                    item.OrderNo = rand.Next(1000000, 9999999).ToString();

                    //validation
                    var validator = new CreateOrderRequestValidator();
                    validator.Validate(item).throwIfValidationException();

                    //mapping
                    Order mappedItem = _autoMapper.Map<Order>(item);
                    mappedItem.CreatedDate = DateTime.Now;
                    var IsCreated = await _orderRepository.CreateAsync(mappedItem);
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
        public async Task<GetOrderRequestDto> GetByIdAsync(Guid Id)
        {
            try
            {
                Order item = await _orderRepository.GetByIdAsync(Id);
                if (item is not null)
                {
                    //mapping
                    GetOrderRequestDto mappedItem = _autoMapper.Map<GetOrderRequestDto>(item);

                    return mappedItem;
                }
                return null;

            }
            catch (Exception ex) { return null; }
        }

        #endregion

        #region getActivatedAppUsers
        public async Task<IEnumerable<GetAllApplicationUserRequestDto>> GetAllActivatedApplicationUser()
        {
            try
            {
                var users = await _applicationUserService.GetAllActivatedAsync();
                return users;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region getActivatedCategories
        public async Task<IEnumerable<GetAllProductRequestDto>> GetAllActivatedProduct()
        {
            try
            {
                var products = await _productService.GetAllActivatedAsync();
                return products;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
