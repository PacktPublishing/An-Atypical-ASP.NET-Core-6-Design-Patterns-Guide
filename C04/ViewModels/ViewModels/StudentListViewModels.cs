using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class StudentListViewModel
    {
        public IEnumerable<StudentListItemViewModel> Students { get; set; }
    }

    public class StudentListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClassCount { get; set; }
    }
}
