using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Core.Dtos.PaymentDtos
{
    public class PaymentStatusDto
    {
        public string PaymentIntentId { get; set; } // Payment Intent ID
        public bool PaymentStatus { get; set; } // Current status of the payment
    }
}
