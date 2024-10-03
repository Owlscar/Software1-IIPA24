using Software1_IIPA24.Dtos;
using Software1_IIPA24.Repositories;
using Software1_IIPA24.Utilities;

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

                EmailConfigUtility gestorCorreo = new EmailConfigUtility();
                string destinatario = "caballero1094@hotmail.com";
                string asunto = "Registro exito / Reporte Semannal / ...";
                string mensaje = this.mensaje(userResponse.Name);
                gestorCorreo.EnviarCorreo(destinatario, asunto, mensaje, true);
            }
            else
            {
                userResponse.Message = "Error Login, Username or Password are Wrong";
            }

            return userResponse;
        }
        public string mensaje(string name)
        {
            string mensaje = "<html>" +
                    "<body>" +
                        "<h1> INGENIERIA DE SOFTWARE 1 </h1>" +
                        "</br>" +
                        "<p> Bienvenido(a) <b> " + name + " </b></p>" +
                        "<p> Notificación Automatica. <b> No responder a este correo.</b></p>" +
                        "<ol>" +
                            "<li> Item 1 </li>" +
                            "<li> Item 2 </li>" +
                            "<li> Item 3 </li>" +
                        "</ol>" +
                    "</body>" +
                    "</html> ";

            return mensaje;
        }
    }
}