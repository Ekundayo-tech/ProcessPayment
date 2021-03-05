using MediatR;
using Microsoft.EntityFrameworkCore;
using Payment.Application.Interface;
using Payment.Common.DTO;
using Payment.Common.Enum;
using Payment.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Application.Handlers.Queries
{
    public class ViewPaymentProcess : IRequest<ResponseModel<List<PaymentDto>>>
    {
    }
    public class ViewPaymentProcessHandler : IRequestHandler<ViewPaymentProcess, ResponseModel<List<PaymentDto>>>
    {
        private readonly ICheapPaymentGateway _dbcheapcontext;
        private readonly IExpensivePaymentGateway _dbexpensivecontext;
        private readonly PremiumPaymentGateway _dbpremiumcontext;
        public ViewPaymentProcessHandler(ICheapPaymentGateway dbContext, IExpensivePaymentGateway dbexpensive, PremiumPaymentGateway premiumPaymentGateway)
        {
            _dbcheapcontext = dbContext;
            _dbexpensivecontext = dbexpensive;
            _dbpremiumcontext = premiumPaymentGateway;
        }
        public async Task<ResponseModel<List<PaymentDto>>> Handle(ViewPaymentProcess request, CancellationToken cancellationToken)
        {
            var response = new ResponseModel<List<PaymentDto>>();
            var cheap = await _dbcheapcontext.Payments.ToListAsync();
            var expensive = await _dbexpensivecontext.Payments.ToListAsync();
            var premium = await _dbpremiumcontext.Payments.ToListAsync();


            if (cheap == null || expensive == null || premium == null)
            {
                response = Utils.GetResponse<List<PaymentDto>>(false, "No Payment Found", null);
                response.Result = null;
            }
            else
                response.IsSuccessResponse = true;
            response.Message = $"{cheap.Count} Record(s) selected";
            response.Message = $"{expensive.Count} Record(s) selected";
            response.Message = $"{premium.Count} Record(s) selected";

            response.ResponseCode = (int)PaymentEnum.Processed;
            response.Result = cheap.Select(c => new PaymentDto
            {
                Id = c.Id,
                CardHolder = c.CardHolder,
                CreditCardNumber = c.CreditCardNumber,
                Amount = c.Amount,
                ExpirationDate = c.ExpirationDate,
                SecurityCode = c.SecurityCode
            }).ToList();
            response.Result = expensive.Select(c => new PaymentDto
            {
                Id = c.Id,
                CardHolder = c.CardHolder,
                CreditCardNumber = c.CreditCardNumber,
                Amount = c.Amount,
                ExpirationDate = c.ExpirationDate,
                SecurityCode = c.SecurityCode
            }).ToList();
            response.Result = premium.Select(c => new PaymentDto
            {
                Id = c.Id,
                CardHolder = c.CardHolder,
                CreditCardNumber = c.CreditCardNumber,
                Amount = c.Amount,
                ExpirationDate = c.ExpirationDate,
                SecurityCode = c.SecurityCode
            }).ToList();
            return response;
        }
    }
}
