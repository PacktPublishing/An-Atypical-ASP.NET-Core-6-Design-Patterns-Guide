using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class CreateStudentViewModel : StudentFormViewModel
    {

    }

    public class EditStudentViewModel
    {
        public int Id { get; set; }
        public IEnumerable<string> Classes { get; set; }
        public StudentFormViewModel Form { get; set; }
    }

    public class StudentFormViewModel
    {
        public string Name { get; set; }
    }
}
