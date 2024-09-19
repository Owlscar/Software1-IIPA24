using Software1_IIPA24.Dtos;
using Software1_IIPA24.Repositories;
using Software1_IIPA24.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Software1_IIPA24.Services
{
    public class UserService
    {
        public UserDto CreateUser(UserDto userModel)
        {
            UserDto responseUserDto = new UserDto();
            UserReposiyoty userReposiyoty = new UserReposiyoty();
            try
            {
                userModel.Password = EncryptUtility.GetSHA256(userModel.Password);

                if (userReposiyoty.BuscarUsuario(userModel.Username))
                {
                    responseUserDto.Response = 0;
                    responseUserDto.Message = "Usuario ya existe";
                }
                else
                {
                    userModel.IdRole = 2;
                    userModel.IdState = 1;
                    if (userReposiyoty.CreateUser(userModel) != 0)
                    {
                        responseUserDto.Response = 1;
                        responseUserDto.Message = "Creacion exitosa";
                    }
                    else
                    {
                        responseUserDto.Response = 0;
                        responseUserDto.Message = "Algo paso";
                    }
                }

                return responseUserDto;
            }
            catch (Exception e)
            {
                responseUserDto.Response = 0;
                responseUserDto.Message = e.InnerException.ToString();
                return responseUserDto;
            }
        }

        public UserDto LoginUser(UserDto userModel)
        {
            UserReposiyoty userReposiyoty = new UserReposiyoty();
            userModel.Password = EncryptUtility.GetSHA256(userModel.Password);
            UserDto userResponse = userReposiyoty.Login(userModel);
            if (userResponse.IdUser != 0)
            {
                userResponse.Message = "Successful Login";
            }
            else
            {
                userResponse.Message = "Error Login, Username or Password are Wrong";
            }

            return userResponse;
        }
    }
}