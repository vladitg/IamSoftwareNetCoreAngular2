using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using AutoMapper;
using ImSoftware.BLL.Services.Contract;
using ImSoftware.DAL.Repositories.Contract;
using ImSoftware.DTO;
using ImSoftware.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ImSoftware.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDTO>> List()
        {
            try
            {
                var queryUser = await _userRepository.Consult();
                return _mapper.Map<List<UserDTO>>(queryUser.ToList());
            }
            catch {
                throw;
            }
        }

        public async Task<UserDTO> Add(UserDTO model)
        {
            try 
            {
                string validation = validationData(model);
                if(validation != "")
                    throw new TaskCanceledException(validation);

                var userAdd = await _userRepository.Add(_mapper.Map<User>(model));

                if (userAdd.Id == 0)
                    throw new TaskCanceledException("El usuario no fue creado");

                return _mapper.Map<UserDTO>(userAdd);
            } catch 
            {
                throw;
            }
        }

        public async Task<bool> Update(UserDTO model)
        {
            try
            {
                string validation = validationData(model);
                if (validation != "")
                    throw new TaskCanceledException(validation);

                var userModel = _mapper.Map<User>(model);

                var userFind = await _userRepository.Get(u => u.Id == userModel.Id);

                if(userFind == null)
                    throw new TaskCanceledException("El usuario no existe");

                userFind.Name = userModel.Name;
                userFind.Email = userModel.Email;
                userFind.Age = userModel.Age;

                bool response = await _userRepository.Update(userFind);

                if(!response)
                    throw new TaskCanceledException("El usuario no se pudo editar");

                return response;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var userFind = await _userRepository.Get(u => u.Id == id);
                if (userFind == null)
                    throw new TaskCanceledException("El usuario no existe");

                bool response = await _userRepository.Delete(userFind);

                if (!response)
                    throw new TaskCanceledException("El usuario no se pudo eliminar");

                return response;
            }
            catch
            {
                throw;
            }
        }

        static string validationData(UserDTO model)
        {
            string validation = "";
            List<string> errors = new List<string>();

            if(string.IsNullOrWhiteSpace(model.Name))
                errors.Add("\"Name\":\"Nombre requerido.\"");
            else if (model.Name.Length > 50)
                errors.Add("\"Name\":\"El nombre no puede ser de mas de 50 caracteres.\"");

            if (string.IsNullOrWhiteSpace(model.Age))
                errors.Add("\"Name\":\"Edad requerdia.\"");
            else if (!int.TryParse(model.Age, out int edad))
                errors.Add("\"Age\":\"La edad no es válida.\"");

            if (string.IsNullOrWhiteSpace(model.Email))
                errors.Add("\"Name\":\"Correo electrónico requerido.\"");
            else if (!IsValidEmail(model.Email))
                errors.Add("\"Email\":\"El correo eletrónico no es válido.\"");

            if (errors.Count > 0)
            {
                foreach (string error in errors)
                {
                    if (validation == "")
                        validation += "{" + error;
                    else
                        validation += "," + error;
                }
                validation += "}";

            }
            return validation;
        }

        static bool IsValidEmail(string email)
        {
            // Expresión regular para validar direcciones de correo electrónico
            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Realizar la validación utilizando Regex
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
    }
}
