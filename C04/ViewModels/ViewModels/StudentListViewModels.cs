using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels;

public record class StudentListViewModel(IEnumerable<StudentListItemViewModel> Students);
public record class StudentListItemViewModel(int Id, string Name, int ClassCount);
