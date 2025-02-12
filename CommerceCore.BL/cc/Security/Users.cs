using NuGet.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CC;
using Microsoft.AspNetCore.Http;
using CommerceCore.DAL.Commerce;
using CommerceCore.ML.cc.Security.Users;
using System.Security.Cryptography;
using CommerceCore.ML;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace CommerceCore.BL.cc.Security
{

    public static class SecurityExtensions
    {
        public static string EncryptPassword(this string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }

    public class Users : LogicBase
    {
        private readonly string jwtSecret = "f9Jd83NgCkL7pR6tXmYqWv4Zs2H8oBtKyP5VcF1aM0XjTlNhGqW9";

        public Users(Configuration settings) {
            configuration = settings;
        }

        public Usuario RegisterUser(CreateUser createUser, string sessionUser)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // Encriptar la contraseña por defecto "Polar01"
                    createUser.Clave = "Polar01".EncryptPassword();

                    // Generar un userName basado en el nombre del usuario y asegurarse de que sea único
                    string generatedUserName = GenerateUniqueUserName(createUser.Nombre, db);

                    // Mapear CreateUser a Usuario
                    var newUser = new Usuario
                    {
                        Usuario1 = Guid.NewGuid().ToString(),
                        userName = generatedUserName,  // Asignar el userName generado
                        Nombre = createUser.Nombre,
                        Tipo = createUser.Tipo,
                        Activo = createUser.Activo ?? true,
                        ReqCambioClave = createUser.ReqCambioClave,
                        FrecuenciaClave = createUser.FrecuenciaClave,
                        FechaUltClave = DateTime.Now,
                        MaxIntentosConex = createUser.MaxIntentosConex,
                        Clave = createUser.Clave,
                        CorreoElectronico = createUser.CorreoElectronico,
                        Celular = createUser.Celular,
                        Telefono1 = createUser.Telefono1,
                        Telefono2 = createUser.Telefono2,
                        Direccion = createUser.Direccion,
                        DocumentoIdentificacion = createUser.DocumentoIdentificacion,
                        FotoUrl = createUser.FotoUrl,
                        FechaNacimiento = createUser.FechaNacimiento,
                        TipoAcceso = createUser.TipoAcceso,
                        TipoPersonalizado = createUser.TipoPersonalizado,
                        Createdby = sessionUser,
                        Createdate = DateTime.Now,
                        Updatedate = DateTime.Now,
                        Noteexistsflag = createUser.Noteexistsflag ?? false,
                        Recorddate = DateTime.Now,
                        Rowpointer = Guid.NewGuid()
                    };

                    // Agregar el usuario a la base de datos
                    db.Usuarios.Add(newUser);
                    db.SaveChanges();

                    // Retornar el usuario creado
                    return newUser;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar el usuario: " + ex.Message);
            }
        }

        private string GenerateUniqueUserName(string fullName, SoftByte db)
        {
            var names = fullName.Split(' ');
            if (names.Length < 2)
            {
                throw new Exception("El nombre debe contener al menos nombre y apellido.");
            }

            string baseUserName = $"{names[0].ToLower()}.{names[1].ToLower()}";  // primer nombre + primer apellido
            string finalUserName = baseUserName;
            int counter = 1;

            // Verificar si el userName ya existe y generar uno único si es necesario
            while (db.Usuarios.Any(u => u.userName == finalUserName))
            {
                finalUserName = $"{baseUserName}{counter}";
                counter++;
            }

            return finalUserName;
        }

        public string Login(string userNameOrEmail, string password)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // Encriptar la contraseña ingresada para comparar
                    string encryptedPassword = password.EncryptPassword();

                    // Buscar el usuario por userName o correo electrónico y verificar la contraseña
                    var user = db.Usuarios.FirstOrDefault(u =>
                        (u.userName == userNameOrEmail || u.CorreoElectronico == userNameOrEmail) &&
                        u.Clave == encryptedPassword);

                    if (user == null)
                    {
                        throw new Exception("Usuario o contraseña incorrectos.");
                    }

                    // Generar el token JWT
                    return GenerateJwtToken(user);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al iniciar sesión: " + ex.Message);
            }
        }

        private string GenerateJwtToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim("userId", user.Usuario1),  // Identificador del usuario
            new Claim("unique_name", user.userName)  // Nombre de usuario
        }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public Usuario GetUserByUserName(string userName)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // Buscar el usuario por su userName
                    var user = db.Usuarios.FirstOrDefault(u => u.userName == userName);

                    if (user == null)
                    {
                        throw new Exception($"Usuario con userName '{userName}' no encontrado.");
                    }

                    return user;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la información del usuario: " + ex.Message);
            }
        }


    }
}
