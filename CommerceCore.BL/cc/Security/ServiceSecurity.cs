﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommerceCore.ML;
using CC;
using CommerceCore.ML.Security;
using System.Reflection;
using CommerceCore.DAL.Commerce;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Security.Permissions;


namespace CommerceCore.BL.cc.Security
{
    public class ServiceSecurity : LogicBase
    {
        public ServiceSecurity(Configuration settings) {
            configuration = settings;
            general = new General(settings);
        }

        ///<summary>
        ///Crea una nueva opcion de seguridad
        ///</summary>
        ///<return></return>
        ///<param name="optionModel">
        ///Informacion de la nueva opcion de seguridad para registrar
        ///</param>
        public string CreateOption(SecurityOption optionModel)
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);

            try
            {
                // INSERCION DE NUEVA OPCION
                if (!db.Opcions.Any(x => x.Menu.Equals(optionModel.menu) && x.Agrupador.Equals(optionModel.grouper) && x.Nombre.Equals(optionModel.name)))
                {
                    db.Opcions.Add(new Opcion()
                    {
                        Menu = optionModel.menu,
                        Agrupador = optionModel.grouper,
                        Nombre = optionModel.name,
                        Texto = optionModel.text,
                        Pathicono = optionModel.pathIcon,
                        Url = optionModel.url,
                        Ordenmostrar = optionModel.orderShow,
                        Activo = true
                    });

                    db.SaveChanges();
                    return "Registro exitoso";
                }
                return "Ya existe una opcion con los datos ingresados";
            }
            catch (Exception ex)
            {
                
              
                throw new Exception("No se pudo registrar una nueva opcion de seguridad Error: " + ex.Message);
            }
        }


        ///<summary>
        ///Crea un nuevo rol de seguridad
        ///</summary>
        ///<return></return>
        ///<param name="roleModel">
        ///Informacion del nuevo rol de seguridad para registrar
        ///</param>
        public string CreateRole(SecurityRole roleModel, int aplication)
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);

            try
            {
                // INSERCION DE NUEVO ROL
                if (!db.Rols.Any(x => x.Aplicacion.Equals(aplication) && x.Nombre.Equals(roleModel.name)))
                {
                    db.Rols.Add(new Rol()
                    {
                        Aplicacion = aplication,
                        Nombre = roleModel.name,
                        Activo = true
                    });

                    db.SaveChanges();
                    return "Registro exitoso";
                };
                return "Ya existe un rol con los datos ingresados";
            }
            catch (Exception ex)
            {
   
                throw new Exception("No se pudo registrar un nuevo rol de seguridad Error: " + ex.Message);
            }
        }



        ///<summary>
        ///Crea una nueva accion de seguridad
        ///</summary>
        ///<return></return>
        ///<param name="actionModel">
        ///Informacion de la nueva accion de seguridad para registrar
        ///</param>
        public string CreateAction(SecurityActions actionModel, int aplication)
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);

            try
            {
                // INSERCION DE NUEVO ROL
                if (!db.Accions.Any(x => x.Opcion.Equals(actionModel.option) && x.Nombre.Equals(actionModel.name)))
                {
                    db.Accions.Add(new Accion()
                    {
                        Opcion = actionModel.option,
                        Nombre = actionModel.name,
                        Activo = true,
                        aplicacion = aplication
                    });

                    db.SaveChanges();
                    return "Registro exitoso";
                };
                return "Ya existe una acción con los datos ingresados";
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar una nueva accion de seguridad Error: " + ex.Message);
            }

        }





        ///<summary>
        ///Crea un nuevo rolusuario de seguridad
        ///</summary>
        ///<return></return>
        ///<param name="rolUserModel">
        ///Informacion con el rol y usuario a asignar
        ///</param>
        public string CreateRoleUser(SecurityRoleUser roleUserModel)
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);
            string roleUserExistingMessage = "";

            try
            {
                roleUserModel.roles.ForEach(role =>
                {
                    // INSERCION DE NUEVO ROLUSUARIO
                    if (!db.Rolusuarios.Any(x => x.Rol.Equals(role) && x.Usuario.Equals(roleUserModel.user)))
                    {
                        db.Rolusuarios.Add(new Rolusuario()
                        {
                            Rol = role,
                            Usuario = roleUserModel.user
                        });

                        db.SaveChanges();
                    }
                    else
                    {
                        string rolName = db.Rols.FirstOrDefault(x => x.Id.Equals(role)).Nombre;
                        roleUserExistingMessage = $"El usuario: {roleUserModel.user} ya tiene asignado el rol {rolName}";
                    };
                });
                return roleUserExistingMessage != "" ? roleUserExistingMessage : "Registro exitoso";
            }
            catch (Exception ex)
            {

                throw new Exception("No se pudo asignar un nuevo rolusuario Error: " + ex.Message);
            }
        }




        ///<summary>
        ///Crea un nuevo rolOpcion de seguridad
        ///</summary>
        ///<return></return>
        ///<param name="rolOptionModel">
        ///Informacion con el rol y opcion a asginar
        ///</param>
        public string CreateRoleOption(SecurityRoleOption rolOptionModel)
        {
            using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
            {
                string rolOptionExistingMessage = "";

                try
                {
                    List<Rolopcion> newRoleOptions = new List<Rolopcion>();

                    rolOptionModel.options.ForEach(option =>
                    {
                        rolOptionModel.roles.ForEach(role =>
                        {
                            if (!db.Rolopcions.Any(x => x.Rol == role && x.Opcion == option))
                            {
                                newRoleOptions.Add(new Rolopcion()
                                {
                                    Rol = role,
                                    Opcion = option,
                                    Permitido = true
                                });
                            }
                            else
                            {
                                string rolName = db.Rols.FirstOrDefault(x => x.Id == role)?.Nombre ?? "Desconocido";
                                string optionExisting = db.Opcions.FirstOrDefault(x => x.Id == option)?.Nombre ?? "Desconocida";
                                rolOptionExistingMessage += $"El rol: {rolName} ya tiene asignada la opción {optionExisting}\n";
                            }
                        });
                    });

                    if (newRoleOptions.Any())
                    {
                        db.Rolopcions.AddRange(newRoleOptions);
                        db.SaveChanges();
                    }

                    return !string.IsNullOrEmpty(rolOptionExistingMessage) ? rolOptionExistingMessage : "Registro exitoso";
                }
                catch (Exception ex)
                {
                    throw new Exception($"No se pudo asignar las opciones al rol. Error: {ex.Message}");
                }
            }
        }





        ///<summary>
        ///Crea un nuevo rolOpcionAccion de seguridad
        ///</summary>
        ///<return></return>
        ///<param name="rolOptionActionModel">
        ///Informacion con el rolOpcion y accion a asginar
        ///</param>
        public void CreateRolOpcionAccion(SecurityRoleOptionAction rolOptionActionModel)
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);

            try
            {
                // INSERCION DE NUEVO RolOpcionAccion
                if (!db.Rolopcionaccions.Any(x => x.Rolopcion.Equals(rolOptionActionModel.rolOpcion) && x.Accion.Equals(rolOptionActionModel.accion)))
                {
                    db.Rolopcionaccions.Add(new Rolopcionaccion()
                    {
                        Rolopcion = rolOptionActionModel.rolOpcion,
                        Accion = rolOptionActionModel.accion,
                        Permitido = rolOptionActionModel.permitido
                    });

                    db.SaveChanges();
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo asignar el rolOpcion {rolOptionActionModel.rolOpcion} a la accion {rolOptionActionModel.accion} Error: " + ex.Message);
            }
        }




        ///<summary>
        ///Crea un nuevo usuarioOpcion de seguridad
        ///</summary>
        ///<return></return>
        ///<param name="userOptionModel">
        ///Informacion con el usuario y opcion a asginar
        ///</param>
        public string CreateUsuarioOpcion(SecurityUserOption userOptionModel)
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);
            string userOptionExistingMessage = "";

            try
            {
                userOptionModel.options.ForEach(opcion =>
                {
                    // INSERCION DE NUEVO USUARIO OPCION
                    if (!db.Usuarioopcions.Any(x => x.Usuario.Equals(userOptionModel.user) && x.Opcion.Equals(opcion)))
                    {
                        db.Usuarioopcions.Add(new Usuarioopcion()
                        {
                            Usuario = userOptionModel.user,
                            Opcion = opcion,
                            Permitido = true
                        });

                        db.SaveChanges();
                    }
                    else
                    {
                        string optionName = db.Opcions.FirstOrDefault(x => x.Id == opcion).Nombre;
                        userOptionExistingMessage = $"El usuario: {userOptionModel.user} ya tiene asignada la opción {optionName}";
                    };
                });
                return userOptionExistingMessage != "" ? userOptionExistingMessage : "Registro exitoso";
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo asignar la opcion {userOptionModel.option} al usuario {userOptionModel.option} Error: " + ex.Message);
            }
        }






        ///<summary>
        ///Crea un nuevo usuarioOpcionAccion de seguridad
        ///</summary>
        ///<return></return>
        ///<param name="userOptionActionModel">
        ///Informacion con la accion y usuarioOpcion a asginar
        ///</param>
        public string CreateUserOptionAction(SecurityUserOptionAction userOptionActionModel)
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);
            string userOptionActionExistingMessage = "";

            try
            {
                userOptionActionModel.userOptions.ForEach(userOption =>
                {
                    // INSERCION DE NUEVO USUARIO OPCION ACCION
                    if (!db.Usuarioopcionaccions.Any(x => x.Usuarioopcion.Equals(userOption) && x.Accion.Equals(userOptionActionModel.action)))
                    {
                        db.Usuarioopcionaccions.Add(new Usuarioopcionaccion()
                        {
                            Usuarioopcion = userOption,
                            Accion = userOptionActionModel.action,
                            Permitido = true
                        });

                        db.SaveChanges();
                    }
                    else
                    {
                        string actionName = db.Accions.FirstOrDefault(x => x.Id == userOptionActionModel.action).Nombre;
                        string optionName = db.Opcions.FirstOrDefault(x => x.Id == db.Usuarioopcions.FirstOrDefault(x => x.Id == userOption).Opcion).Nombre;
                        userOptionActionExistingMessage = $"La acción: {actionName} ya tiene asignada la opción {optionName}";
                    };

                });
                return userOptionActionExistingMessage != "" ? userOptionActionExistingMessage : "Registro exitoso";
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo asignar el usuarioOpcion {userOptionActionModel.userOption} a la accion {userOptionActionModel.action} Error: " + ex.Message);
            }
        }



        ///<summary>
        ///Actualizar estado en relacion Usuario-opcion
        ///</summary>
        ///<return></return>
        ///<param name="userOptionModel">
        ///Informacion con el usuario y opcion a asginar
        ///</param>
        public string UpdateUserOptionStatus(SecurityUserOption userOptionModel)
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);

            try
            {
                Usuarioopcion securityUserOption = db.Usuarioopcions.FirstOrDefault(x =>
                x.Usuario.Equals(userOptionModel.user) && x.Opcion == userOptionModel.option);

                // ACTUALIZAR USUARIO OPCION
                if (securityUserOption != null)
                {
                    securityUserOption.Permitido = userOptionModel.allowed;
                    db.Usuarioopcions.Update(securityUserOption);

                    db.SaveChanges();
                    return "Actualizado exitosamente";
                };
                return "Registro no existente";
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo actualizar la opcion {userOptionModel.option} al usuario {userOptionModel.user} Error: " + ex.Message);
            }
        }





        ///<summary>
        ///Actualizar estado en relacion Usuario-Opcion-Accion
        ///</summary>
        ///<return></return>
        ///<param name="userOptionActionModel">
        ///Informacion con el usuario-opcion y accion a asginar
        ///</param>
        public string RemoveUserOptionActionStatus(SecurityUserOptionAction userOptionActionModel)
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);

            try
            {
                // BUSQUEDA DE USUARIO OPCION ACCION
                Usuarioopcionaccion securityUserOptionAction = db.Usuarioopcionaccions.FirstOrDefault(x =>
                x.Usuarioopcion == userOptionActionModel.userOption && x.Accion == userOptionActionModel.action);

                // ACTUALIZAR USUARIO OPCION ACCION
                if (securityUserOptionAction != null)
                {
                    db.Usuarioopcionaccions.Remove(securityUserOptionAction);
                    //securityUserOptionAction.Permitido = !userOptionActionModel.allowed;
                    //db.UsuarioOpcionAccion.Update(securityUserOptionAction);

                    db.SaveChanges();
                    return "Actualizado exitosamente";
                };
                return "Registro no existente";
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo actualizar el Usuario-Opcion {userOptionActionModel.userOption} - a la acción {userOptionActionModel.action}" + ex.Message);
            }
        }



        ///<summary>
        ///Actualiza una opcion de seguridad
        ///</summary>
        ///<return></return>
        ///<param name="optionModel">
        ///Informacion de la nueva opcion de seguridad para registrar
        ///</param>
        public string UpdateOption(int optionId, bool status)
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);

            try
            {
                if (!db.Rolopcions.Any(x => x.Opcion == optionId))
                {
                    Opcion securityOption = db.Opcions.FirstOrDefault(x => x.Id.Equals(optionId));
                    // ACTUALIZAR OPCION
                    if (securityOption != null)
                    {
                        securityOption.Activo = status;
                        db.Opcions.Update(securityOption);
                        db.SaveChanges();
                        return "Actualizado";
                    };
                    return "No existe";
                }
                return "Tiene rol asignado";
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar una nueva opcion de seguridad Error: " + ex.Message);
            }
        }


        ///<summary>
        ///Actualiza un rol de seguridad
        ///</summary>
        ///<return></return>
        ///<param name="rolModel">
        ///Informacion del rol de seguridad para actualizar
        ///</param>
        public string UpdateRole(int roleId, bool status)
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);

            try
            {
                Rol securityRol = db.Rols.FirstOrDefault(x => x.Id == roleId);
                // INSERCION DE NUEVO ROL
                if (securityRol != null)
                {
                    securityRol.Activo = status;
                    db.Rols.Update(securityRol);
                    db.SaveChanges();
                    return "Actualizado";
                };
                return "No existe";
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo actualizar el rol de seguridad {roleId} Error: " + ex.Message);
            }
        }



        ///<summary>
        ///Actualiza un rol de seguridad
        ///</summary>
        ///<return></return>
        ///<param name="rolModel">
        ///Informacion del rol de seguridad para actualizar
        ///</param>
        public string UpdateAction(int actionId, bool status)
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);

            try
            {
                Accion securityAction = db.Accions.FirstOrDefault(x => x.Id == actionId);
                // ACTUALIZAR ACCION
                if (securityAction != null)
                {
                    securityAction.Activo = status;
                    db.Accions.Update(securityAction);
                    db.SaveChanges();
                    return "Actualizado";
                };
                return "No existe";
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo actualizar el rol de seguridad {actionId} Error: " + ex.Message);
            }
        }



        ///<summary>
        ///Obtiene todos los menus regustrados en la db
        ///</summary>
        ///<return></return>
        ///<param></param>
        public dynamic GetMenus(int aplication)
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);
            var result = db.Menus.Join(
                db.Aplicacions,
                menu => menu.Aplicacion,
                aplicacion => aplicacion.Id,
                (menu, aplicacion) => new { menu, aplicacion }
            ).Where(o =>
                o.menu.Activo.Equals(true) &&  o.menu.Aplicacion == aplication
            ).Select(x => new
            {
                opcion = x.menu.Opcions,
                nombre = x.menu.Nombre,
                aplicacion = x.aplicacion.Nombre,
                idMenu = x.menu.Id
            }
            );
            return result;
        }

        ///<summary>
        ///Obtiene todos los menus regustrados en la db
        ///</summary>
        ///<return></return>
        ///<param></param>
        public dynamic GetAllMenus()
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);
            var result = db.Menus.Join(
                db.Aplicacions,
                menu => menu.Aplicacion,
                aplicacion => aplicacion.Id,
                (menu, aplicacion) => new { menu, aplicacion }
            ).Where(o =>
                o.menu.Activo.Equals(true) 
            ).Select(x => new
            {
                opcion = x.menu.Opcions,
                nombre = x.menu.Nombre,
                aplicacion = x.aplicacion.Nombre,
                idMenu = x.menu.Id
            }
            );
            return result;
        }


        ///<summary>
        ///Obtiene todas las aplicaciones registrados en la db
        ///</summary>
        ///<return></return>
        ///<param></param>
        public dynamic GetAplications()
        {

            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);
            var result = db.Aplicacions.Where(x =>
                    x.Activo.Equals(true)
                ).Select(x => new
                {
                    id = x.Id,
                    nombre = x.Nombre
                }
            );
            return result;
        }



        ///<summary>
        ///Obtiene todas las opciones registradas en la db haciendo join con los menus para hacer distincion
        ///</summary>
        ///<return></return>
        ///<param></param>
        public dynamic GetOptions(int aplication)
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);
            var result = db.Opcions.Join(
                db.Menus,
                opcion => opcion.Menu,
                menu => menu.Id,
                (opcion, menu) => new { opcion, menu }
            )
            .Where( x => x.menu.Aplicacion == aplication)
            .Select(x => new
            {
                menu = x.menu.Nombre,
                opcion = x.opcion.Nombre,
                idOpcion = x.opcion.Id,
                estado = x.opcion.Activo,
                descripcion = x.opcion.Texto,
                nombreMostrar = $"{x.opcion.Nombre} - menú: {x.menu.Nombre}"
            }
            ).OrderByDescending(e => e.idOpcion);
            return result;
        }



        ///<summary>
        ///Obtiene todos los roles registrados en la db haciendo join con la tabla aplicacion para hacer distincion
        ///</summary>
        ///<return></return>
        ///<param></param>
        public dynamic GetRoles(int aplication)
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);
            var result = db.Rols.Join(
                db.Aplicacions,
                rol => rol.Aplicacion,
                aplicacion => aplicacion.Id,
                (rol, aplicacion) => new { rol, aplicacion }
            ).Where(o =>
                o.aplicacion.Id == aplication)
           .Select(x => new
               {
                   rol = x.rol.Nombre,
                   aplicacion = x.aplicacion.Nombre,
                   idRol = x.rol.Id,
                   estado = x.rol.Activo,
                   nombreMostrar = $"{x.rol.Nombre} - app: {x.aplicacion.Nombre}"
               }
            ).OrderByDescending(e => e.idRol);
            return result;
        }


        ///<summary>
        ///Obtiene todos los usuarios registrados en la db
        ///</summary>
        ///<return></return>
        ///<param></param>
        public dynamic GetUsers(int aplication)
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);
            var result = db.Usuarios.Where(x =>
                x.Activo.Equals("S") && x.Aplicacion == aplication
            ).Select(x => new
            {
                userName = x.Nombre,
                userId = x.Usuario1,
                estado = x.Activo,
                claveVista = $"{x.Usuario1} - {x.Nombre}",
                parent = x.Usuario1
            }
            ).OrderBy(x => x.userName);
            return result;
        }



        ///<summary>
        ///Obtiene todos las acciones registrados en la db
        ///</summary>
        ///<return></return>
        ///<param></param>
        public dynamic GetActions(int aplication)
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);
            var result = db.Accions.Join(
                db.Opcions,
                accion => accion.Opcion,
                opcion => opcion.Id,
                (accion, opcion) => new { accion, opcion }
            )
             .Where( x => x.accion.aplicacion == aplication)
            .Select(x => new
            {
                accion = x.accion.Nombre,
                opcion = x.opcion.Nombre,
                idAccion = x.accion.Id,
                estado = x.accion.Activo
            }
            ).OrderByDescending(e => e.idAccion);
            return result;
        }



        ///<summary>
        ///Obtiene todos los roles registrados en la db haciendo join con la tabla aplicacion para hacer distincion
        ///</summary>
        ///<return></return>
        ///<param></param>
        public dynamic GetUserOptions(int aplication)
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);
            var result = db.Usuarioopcions.Join(
                db.Opcions,
                usuarioOpcion => usuarioOpcion.Opcion,
                opcion => opcion.Id,
                (usuarioOpcion, opcion) => new { usuarioOpcion, opcion }
            ).Join(
                db.Menus,
                UsuarioOpcion => UsuarioOpcion.opcion.Menu,
                menu => menu.Id,
                (usuarioOpcion, menu) => new { usuarioOpcion, menu }
            ).Where(u =>
                u.menu.Aplicacion == aplication)
            .Select(x => new
               {
                   usuario = x.usuarioOpcion.usuarioOpcion.Usuario,
                   opcion = x.usuarioOpcion.opcion.Nombre,
                   idOpcion = x.usuarioOpcion.opcion.Id,
                   idUsuarioOpcion = x.usuarioOpcion.usuarioOpcion.Id,
                   userId = $"{x.usuarioOpcion.usuarioOpcion.Usuario}_{x.usuarioOpcion.usuarioOpcion.Id}",
                   nombreMostrar = $"{x.usuarioOpcion.opcion.Nombre} - usuario: {x.usuarioOpcion.usuarioOpcion.Usuario} - menú: {x.menu.Nombre}",
                   claveVista = $"{x.usuarioOpcion.opcion.Nombre} - menú: {x.menu.Nombre}",
                   parent = x.usuarioOpcion.usuarioOpcion.Usuario,
                   selected = x.usuarioOpcion.usuarioOpcion.Permitido
               }
            ).OrderBy(e => e.usuario);
            return result;
        }


        ///<summary>
        ///Obtiene todos los roles asignados a un usuario por su ID
        ///</summary>
        ///<return></return>
        ///<param>userId</param>
        public dynamic GetRolesByUserId(string userId)
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);
            var result = db.Rolusuarios.Join(
                db.Rols,
                rolUsuario => rolUsuario.Rol,
                rol => rol.Id,
                (rolUsuario, rol) => new { rolUsuario, rol }
            ).Where(o =>
                o.rolUsuario.Usuario.Equals(userId)
            ).Select(x => new
            {
                rolName = x.rol.Nombre,
                rolId = x.rol.Id,
                aplicacion = x.rol.Aplicacion,
                estado = x.rol.Activo
            }
            );
            return result;
        }


        ///<summary>
        ///Obtiene todos los roles asignados a un usuario por su ID
        ///</summary>
        ///<return></return>
        ///<param>userId</param>
        public dynamic GetOptionsByUserId(string userId)
        {

            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);
            var result = db.Usuarioopcions.Join(
                db.Opcions,
                usuariOpcion => usuariOpcion.Opcion,
                opcion => opcion.Id,
                (usuariOpcion, opcion) => new { usuariOpcion, opcion }
            )
            .Where(o =>
                o.usuariOpcion.Usuario.Equals(userId)
            ).Select(x => new
            {
                optionName = x.opcion.Nombre,
                optionId = x.opcion.Id,
                menu = x.opcion.Menu,
                estado = x.opcion.Activo,
                permitido = x.usuariOpcion.Permitido
            }
            ).ToArray();

            return result;
        }



        ///<summary>
        ///Obtiene todos los usuarios con algun rol asignado
        ///</summary>
        ///<return></return>
        ///<param>userId</param>
        public dynamic GetUsersWithRole(int aplication)
        {

            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);

            var result = db.Rolusuarios
                .Join(db.Usuarios, r => r.Usuario, u => u.Usuario1, (r, u) => new {r, u})
                .Where( x => x.u.Aplicacion == aplication )
            .GroupBy(c => c.r.Usuario)
            .Select(x => new
            {
                user = x.First().u.Usuario1
            }
            );
            return result;
        }




        ///<summary>
        ///Obtiene todos los usuarios con algun rol asignado
        ///</summary>
        ///<return></return>
        ///<param>userId</param>
        public dynamic GetRoleUsers( int aplication)
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);

            var result = db.Rolusuarios
            .Join(
                db.Rols,
                rolUser => rolUser.Rol,
                rol => rol.Id,
                (rolUser, rol) => new { rolUser, rol })
            .Where( x => x.rol.Aplicacion == aplication)
            .Select(x => new
            {
                idRol = x.rolUser.Rol,
                usuario = x.rolUser.Usuario,
                claveVista = x.rol.Nombre,
                selected = true
            }
            );
            return result;
        }


        ///<summary>
        ///Obtiene todos los usuarios con alguna opcion asignada
        ///</summary>
        ///<return></return>
        ///<param>userId</param>
        public dynamic GetUsersWithOptions(int aplication)
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);
            var result = db.Usuarioopcions
                .Join(db.Usuarios,
                      o => o.Usuario,
                      u => u.Usuario1,
                      (o, u) => new { o, u })
                .Where(x => x.u.Aplicacion == aplication)
                .GroupBy(c => c.u.Usuario1) 
                .Select(x => new
                {
                    user = x.First().u.Usuario1  
                });
            return result;
        }



        ///<summary>
        ///Obtiene todas las relaciones rol-opcion 
        ///</summary>
        ///<return></return>
        ///<param></param>
        public dynamic GetRoleOptions(int aplication)
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);
            var result = db.Rolopcions
            .Join(
                db.Rols,
                rolOpcion => rolOpcion.Rol,
                rol => rol.Id,
                (rolOpcion, rol) => new { rolOpcion, rol }
            )
            .Join(
                db.Opcions,
                rolOpcion => rolOpcion.rolOpcion.Opcion,
                opcion => opcion.Id,
                (rolOpcion, opcion) => new { rolOpcion, opcion }
            )
            .Join(
                db.Aplicacions,
                opcion => opcion.rolOpcion.rol.Aplicacion,
                aplicacion => aplicacion.Id,
                (opcion, aplicacion) => new { opcion, aplicacion }
            )
            .Where( x => x.aplicacion.Id == aplication)
            .Select(x => new
            {
                roleOptionId = x.opcion.rolOpcion.rolOpcion.Id,
                rol = x.opcion.rolOpcion.rol.Nombre,
                option = x.opcion.opcion.Nombre,
                aplication = x.aplicacion.Nombre
            }
            ).OrderByDescending(e => e.roleOptionId);

            return result;
        }


        ///<summary>
        ///Obtiene todas las relaciones usuario-opcion-accion
        ///</summary>
        ///<return></return>
        ///<param></param>
        public dynamic GetUserOptionActions(int aplication)
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);
            var result = db.Usuarioopcionaccions
            .Join(
                db.Usuarioopcions,
                usuarioOpcionAccion => usuarioOpcionAccion.Usuarioopcion,
                UsuarioOpcion => UsuarioOpcion.Id,
                (usuarioOpcionAccion, UsuarioOpcion) => new { usuarioOpcionAccion, UsuarioOpcion }
            )
            .Join(
                db.Accions,
                uoa => uoa.usuarioOpcionAccion.Accion,
                accion => accion.Id,
                (uoa, accion) => new { uoa, accion }
            )
            .Join(
                db.Opcions,
                uoa => uoa.uoa.UsuarioOpcion.Opcion,
                opcion => opcion.Id,
                (uoa, opcion) => new { uoa, opcion }
            )
            .Join(
                db.Menus,
                uoa => uoa.opcion.Menu,
                menu => menu.Id,
                (uoa, menu) => new { uoa, menu }
            )
            .Where( x => x.menu.Aplicacion == aplication)
            .Select(x => new
            {
                nombreUsuario = x.uoa.uoa.uoa.UsuarioOpcion.Usuario,
                opcion = x.uoa.opcion.Nombre,
                accion = x.uoa.uoa.accion.Nombre,
                usuarioOpcionId = x.uoa.uoa.uoa.UsuarioOpcion.Id,
                accionId = x.uoa.uoa.accion.Id,
                menu = x.menu.Nombre,
                usuario = $"{x.uoa.uoa.uoa.UsuarioOpcion.Usuario}_{x.uoa.uoa.uoa.UsuarioOpcion.Id}",
                claveVista = x.uoa.uoa.accion.Nombre,
                selected = x.uoa.uoa.uoa.usuarioOpcionAccion.Permitido
            }
            );
            return result;
        }




        ///<summary>
        ///Eliminar Rol-Opcion por medio de su id
        ///</summary>
        ///<return></return>
        ///<param name="roleOptionId">
        ///Informacion del rol de seguridad para actualizar
        ///</param>
        public dynamic DeleteRoleOption(int roleOptionId)
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);

            try
            {
                var rolOption = db.Rolopcions.FirstOrDefault(x => x.Id.Equals(roleOptionId));
                // ELIMINACION DE ROL-OPCION
                if (rolOption != null)
                {
                    db.Rolopcions.Remove(rolOption);
                    db.SaveChanges();
                    return new
                    {
                        result = "Registro exitoso",
                        message = String.Format("Rol-Opcion {0} eliminado satisfactoriamente", roleOptionId)
                    };
                };
                return new
                {
                    result = "Registro existente",
                    message = String.Format("No se pudo eliminar la asignación Rol-Opcion {0} debido a que no existe", roleOptionId)
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo actualizar el usuario {roleOptionId} Error: " + ex.Message);
            }
        }





        ///<summary>
        ///Eliminar Rol-Usuario
        ///</summary>
        ///<return></return>
        ///<param name="roleOptionId">
        ///Informacion del rol de seguridad para actualizar
        ///</param>
        public dynamic DeleteRolUser(int roleId, string userId)
        {
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);

            try
            {
                var roleUser = db.Rolusuarios.FirstOrDefault(x => x.Rol == roleId && x.Usuario.Equals(userId));
                // ELIMINACION DE ROL-OPCION
                if (roleUser != null)
                {
                    db.Rolusuarios.Remove(roleUser);
                    db.SaveChanges();
                    return new
                    {
                        result = "Registro exitoso",
                        message = String.Format("Rol {0} desasignado a Usuario {1} de forma exitosa", roleId, userId)
                    };
                };
                return new
                {
                    result = "Registro no existente",
                    message = String.Format("No se pudo desasignar el rol {0} al usuario {1} debido a que no existe", roleId, userId)
                };
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("No se pudo desasignar el rol {0} al usuario {1} debido a que no existe la asignación ", roleId, userId) + "Error: " + ex.Message);
            }
        }




        ///<summary>
        ///Consultar opciones del usuario por usuario
        ///</summary>
        ///<return></return>
        ///<param name="user">
        ///</param>
        public List<ItemsToPermission> GetUserOption(string user)
        {
            List<ItemsToPermission> lstPermissions = new List<ItemsToPermission>();
            SoftByte db = new SoftByte(configuration.appSettings.cadenaSql);
            lstPermissions = db.Usuarioopcions.Join(
                db.Opcions,
                usuarioOpcion => usuarioOpcion.Opcion,
                opcion => opcion.Id,
                (usuarioOpcion, opcion) => new { usuarioOpcion, opcion }
            ).Where(u =>
                u.usuarioOpcion.Usuario.Equals(user)
            ).Select(x => new ItemsToPermission
            {
                usuario = x.usuarioOpcion.Usuario,
                opcion = x.opcion.Nombre
            }
            ).ToList();
            return lstPermissions;
        }




    }
}
