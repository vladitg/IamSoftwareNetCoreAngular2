using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using ImSoftware.DTO;
using ImSoftware.Model;

namespace ImSoftware.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region User
                CreateMap<User, UserDTO>().ReverseMap();
            #endregion
        }
    }
}
