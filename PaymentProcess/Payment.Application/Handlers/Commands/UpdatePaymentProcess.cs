using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Payment.Application.Interface;
using Payment.Common.DTO;
using Payment.Common.Enum;
using Payment.Common.Utilities;
using Payment.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Application.Handlers.Commands
{
    public class UpdatePaymentProcess : IRequest<ResponseModel<PaymentDto>>
    {
        public string Id { get; set; }
        public string CreditCardNumber { get; set; }
        public DateTime ExpirationDate { get; set; } = DateTime.Now;
        public string CardHolder { get; set; }
        public string SecurityCode { get; set; }
        public decimal Amount { get; set; }
    }

    public class UpdatePaymentProcessQueryValidator : AbstractValidator<UpdatePaymentProcess>
    {
        public UpdatePaymentProcessQueryValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.SecurityCode).NotEmpty();
            RuleFor(c => c.CardHolder).NotEmpty();
            RuleFor(c => c.CreditCardNumber).NotEmpty();
            RuleFor(c => c.Amount).NotEmpty();

        }
    }

    public class UpdatePaymentProcessQueryHandler : IRequestHandler<UpdatePaymentProcess, ResponseModel<PaymentDto>>
    {
        private readonly ICheapPaymentGateway _dbcheapcontext;
        private readonly IExpensivePaymentGateway _dbexpensivecontext;
        private readonly PremiumPaymentGateway _dbpremiumcontext;
        public UpdatePaymentProcessQueryHandler(ICheapPaymentGateway dbContext, IExpensivePaymentGateway dbexpensive, PremiumPaymentGateway premiumPaymentGateway)
        {
            _dbcheapcontext = dbContext;
            _dbexpensivecontext = dbexpensive;
            _dbpremiumcontext = premiumPaymentGateway;
        }

        public async Task<ResponseModel<PaymentDto>> Handle(UpdatePaymentProcess request, CancellationToken cancellationToken)
        {
            var ResponseModel = new ResponseModel<PaymentDto>();
            var item = await _dbcheapcontext.Payments.FirstOrDefaultAsync(x => x.Amount == request.Amount);
            var expensive = await _dbexpensivecontext.Payments.FirstOrDefaultAsync(x => x.Amount == request.Amount);
            var premium = await _dbpremiumcontext.Payments.FirstOrDefaultAsync(x => x.Amount == request.Amount);


            if (item == null || expensive == null || premium == null)
            {

                var payment = new Payments()
                {
                    Id = request.Id,
                    CreditCardNumber = request.CreditCardNumber,
                    CardHolder = request.CardHolder,
                    Amount = request.Amount,
                    ExpirationDate = request.ExpirationDate
                };
                if (request.Amount < 21)
                {
                    _dbcheapcontext.Payments.Update(payment);
                }
                if (request.Amount > 21 || request.Amount < 500)
                {
                    _dbexpensivecontext.Payments.Update(payment);
                }
                if (request.Amount > 500)
                {
                    _dbpremiumcontext.Payments.Update(payment);
                }
                await _dbcheapcontext.SaveChangesAsync();
                await _dbexpensivecontext.SaveChangesAsync();
                await _dbpremiumcontext.SaveChangesAsync();
                ResponseModel.IsSuccessResponse = true;
                ResponseModel.Message = "Payment is Processed";
                ResponseModel.ResponseCode = (int)PaymentEnum.Processed;
                ResponseModel.Result = new PaymentDto()
                {
                    Id = request.Id,
                    CardHolder = request.CardHolder,
                    CreditCardNumber = request.CreditCardNumber,
                    Amount = request.Amount,
                    SecurityCode = request.SecurityCode,
                    ExpirationDate = request.ExpirationDate
                };
                return ResponseModel;

            }
            else
            {
                ResponseModel.Result = null;
                ResponseModel.IsSuccessResponse = false;
                ResponseModel.Message = "Request is Invalid";
                ResponseModel.ResponseCode = (int)PaymentEnum.Failed;
            }
            return ResponseModel;

        }
    }
}
