using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.DTO
{
    public class TransactionDTO
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
    }
}
