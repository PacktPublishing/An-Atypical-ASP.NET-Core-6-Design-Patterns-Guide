using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models;

public class Student
{
    public Student(int id, string name, IEnumerable<Class> classes)
    {
        Id = id;
        Name = name;
        Classes = classes;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Class> Classes { get; set; } = Enumerable.Empty<Class>();
}
