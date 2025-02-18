using System;
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
        public string CreateAction(SecurityActions actionModel)
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
                        Activo = true
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













    }
}
