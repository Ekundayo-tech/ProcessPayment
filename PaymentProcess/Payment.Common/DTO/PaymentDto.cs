using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Common.DTO
{
    public class PaymentDto
    {
        public string Id { get; set; }
        public string CreditCardNumber { get; set; }
        public DateTime ExpirationDate { get; set; } = DateTime.Now;
        public string CardHolder { get; set; }
        public string SecurityCode { get; set; }
        public decimal Amount { get; set; }
    }
}
