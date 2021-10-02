using System.Collections.Generic;
using System.Threading.Tasks;

namespace TransformTemplateView.Models
{
    public interface IComponent
    {
        void Add(IComponent bookComponent);
        void Remove(IComponent bookComponent);
        int Count();
    }
}
