using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Contract> Contracts { get; set; }
    }
}
