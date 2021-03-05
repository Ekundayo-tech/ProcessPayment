using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payment.Application.Handlers.Commands;
using Payment.Application.Handlers.Queries;
using Payment.Common.DTO;
using Payment.Common.Utilities;

namespace PaymentProcess.Controllers
{
    [Route("Payments/api")]
    [ApiController]
    public class PaymentController : BaseController
    {
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseModel<PaymentDto>), (int)HttpStatusCode.OK)]
        [Produces("application/json")]
        [HttpPost, Route("Initiatepayment")]
        public async Task<IActionResult> Initiatepayment([FromBody] InitiatePaymentProcess command)
        {
            var res = await Mediator.Send(command);

            if (res.IsSuccessResponse)
            {
                return Ok(res);
            }
            else
            {
                return new BadRequestObjectResult((ResponseModel)res);

            }
        }
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseModel<Response<PaymentDto>>), (int)HttpStatusCode.OK)]
        [Produces("application/json")]
        [HttpPut, Route("updatepayment")]
        public async Task<IActionResult> Updatepayment([FromBody] UpdatePaymentProcess command)
        {
            var res = await Mediator.Send(command);

            if (res.IsSuccessResponse)
            {
                return Ok(res);
            }
            else
            {
                return new BadRequestObjectResult((ResponseModel)res);
            }
        }

        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseModel<List<PaymentDto>>), (int)HttpStatusCode.OK)]
        [Produces("application/json")]
        [HttpGet, Route("viewallpayment")]
        public async Task<IActionResult> ViewPaymentProcess()
        {
            var command = new ViewPaymentProcess();
            var res = await Mediator.Send(command);

            if (res.IsSuccessResponse)
            {
                return Ok(res);
            }
            else
            {
                return new BadRequestObjectResult((ResponseModel)res);

            }
        }
    }
}
