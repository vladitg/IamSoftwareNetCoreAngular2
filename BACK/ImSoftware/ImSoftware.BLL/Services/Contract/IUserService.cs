using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ImSoftware.DTO;

namespace ImSoftware.BLL.Services.Contract
{
    public interface IUserService
    {
        Task<List<UserDTO>> List();
        Task<UserDTO> Add(UserDTO model);
        Task<bool> Update(UserDTO model);
        Task<bool> Delete(int id);
    }
}
