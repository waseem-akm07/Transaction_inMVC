using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferLayer.DTO;

namespace BusinessAccessLayer.Repo
{
    public interface IBusinessLayer
    {
        IEnumerable<HelperModel> GetEmployees();
        IEnumerable<HelperModel> Getemployee(int id);
        void Create(List<HelperModel> model);
        void Update(int id, List<HelperModel> model);
        void Delete(int id);
    }
}
