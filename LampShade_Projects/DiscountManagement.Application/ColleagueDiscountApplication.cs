using _0_Framework.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;

namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {
            var operation = new OperationResult();
            if (_colleagueDiscountRepository.Exist(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            else
            {
                var Colleague = new ColleagueDiscount(command.ProductId, command.DiscountRate);
                _colleagueDiscountRepository.Create(Colleague);
                _colleagueDiscountRepository.SaveChanges();
                return operation.Succeeded();
            }
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operation = new OperationResult();
            var Colleague = _colleagueDiscountRepository.Get(command.Id);
            if (Colleague == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            if (_colleagueDiscountRepository.Exist(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            else
            {
                Colleague.Edit(command.ProductId,command.DiscountRate);
                _colleagueDiscountRepository.SaveChanges();
                return operation.Succeeded();
            }
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var Colleague = _colleagueDiscountRepository.Get(id);
            if (Colleague == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            else
            {
                Colleague.Restore();
                _colleagueDiscountRepository.SaveChanges();
                return operation.Succeeded();
            }
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var Colleague = _colleagueDiscountRepository.Get(id);
            if (Colleague == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            else
            {
                Colleague.Remove();
                _colleagueDiscountRepository.SaveChanges();
                return operation.Succeeded();
            }
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _colleagueDiscountRepository.GetDetails(id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            return _colleagueDiscountRepository.Search(searchModel);
        }
    }
}
