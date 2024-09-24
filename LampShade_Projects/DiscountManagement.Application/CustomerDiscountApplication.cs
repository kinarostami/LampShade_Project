using _0_Framework.Application;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application
{
    public partial class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        public OperationResult Define(DefineCustomerDiscount command)
        {
            var operation = new OperationResult();
            if (_customerDiscountRepository.Exist(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            else
            {
                var startDate = command.StartTime.ToGeorgianDateTime(); 
                var endDate = command.EndTime.ToGeorgianDateTime(); 

                var customer = new CustomerDiscount(command.ProductId, command.DiscountRate, startDate,endDate, command.Reason);
                
                _customerDiscountRepository.Create(customer);
                _customerDiscountRepository.SaveChanges();
                return operation.Succeeded();
            }
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operation = new OperationResult();
            var customer = _customerDiscountRepository.Get(command.Id);
            if (customer == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_customerDiscountRepository.Exist(x => x.ProductId == command.ProductId &&
                                                       x.DiscountRate == command.DiscountRate && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            else
            {
                var startDate = command.StartTime.ToGeorgianDateTime();
                var endDate = command.EndTime.ToGeorgianDateTime();

                customer.Edit(command.ProductId, command.DiscountRate, startDate, endDate, command.Reason);
                _customerDiscountRepository.SaveChanges();
                return operation.Succeeded();
            }
        }

        public EditCustomerDiscount Details(long id)
        {
            return _customerDiscountRepository.GetDetails(id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            return _customerDiscountRepository.Search(searchModel);
        }
    }
}
