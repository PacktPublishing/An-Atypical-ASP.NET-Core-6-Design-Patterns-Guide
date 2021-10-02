using ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Services
{
    public class StudentService
    {
        public Task<IEnumerable<Student>> ReadAllAsync()
        {
            return Task.FromResult(FakeData.Students.AsEnumerable());
        }

        public Task<Student> ReadOneAsync(int id)
        {
            return Task.FromResult(FakeData.Students.FirstOrDefault(x => x.Id == id));
        }

        public Task<Student> CreateAsync(string name)
        {
            var student = new Student
            {
                Id = FakeData.NextId,
                Name = name,
                Classes = new List<Class>()
            };
            FakeData.Students.Add(student);
            return Task.FromResult(student);
        }

        public async Task<Student> UpdateAsync(int id, string name)
        {
            var student = await ReadOneAsync(id);
            student.Name = name;
            return student;
        }

        private class FakeData
        {
            public static List<Student> Students { get; }
            private static int _lastId = 0;

            static FakeData()
            {
                var mathClass = new Class { Id = 1, Name = "Math" };
                var scienceClass = new Class { Id = 2, Name = "Science" };
                var computerClass = new Class { Id = 3, Name = "Computer" };
                Students = new List<Student>
                {
                    new Student
                    {
                        Id = NextId,
                        Name = "Maddie Powers",
                        Classes = new List<Class>{ mathClass, scienceClass }
                    },
                    new Student
                    {
                        Id = NextId,
                        Name = "Harper Black",
                        Classes = new List<Class>{ mathClass, scienceClass, computerClass }
                    },
                    new Student
                    {
                        Id = NextId,
                        Name = "Allen York",
                        Classes = new List<Class>{ mathClass, computerClass }
                    },
                    new Student
                    {
                        Id = NextId,
                        Name = "Lillie Adkins",
                        Classes = new List<Class>{ scienceClass, computerClass }
                    }
                };
            }

            public static int NextId => ++_lastId;
        }
    }
}
