using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CC;
using Microsoft.AspNetCore.Http;
using CommerceCore.ML;
using NuGet.Configuration;
using CommerceCore.DAL.Commerce;
using CommerceCore.ML.cc.sale.Warehouse;
using CommerceCore.Api.Controllers.cc.logistics.warehouse;

namespace CommerceCore.BL.cc.logistics.warehouse
{
    public class Categories : LogicBase
    {
        public Categories(Configuration settings) {
            configuration = settings;
        }

        public List<Categoria> GetListCategoriesActive(string userName)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    var categories = new List<Categoria>();
                     categories = db.Categorias
                    .Where(c => c.Estatus == true)  // Filtra solo categorías activas
                    .Select(c => new Categoria
                    {
                        IdCategoria = c.IdCategoria,
                        Nombre = c.Nombre,
                        Descripcion = c.Descripcion,
                        Estatus = c.Estatus,
                        CreateBy = c.CreateBy,
                        UpdateBy = c.UpdateBy,
                        FechaCreacion = c.FechaCreacion,
                        FechaActualizacion = c.FechaActualizacion,

                        // Incluye las subcategorías relacionadas
                        Subcategoria = db.Subcategorias
                            .Where(sc => sc.IdCategoria == c.IdCategoria)
                            .Select(sc => new Subcategoria
                            {
                                IdSubcategoria = sc.IdSubcategoria,
                                IdCategoria = sc.IdCategoria,
                                Nombre = sc.Nombre,
                                Descripcion = sc.Descripcion,
                                Estatus = sc.Estatus,
                                CreateBy = sc.CreateBy,
                                UpdateBy = sc.UpdateBy,
                                FechaCreacion = sc.FechaCreacion,
                                FechaActualizacion = sc.FechaActualizacion
                            }).ToList()
                    })
                    .ToList();

                    return categories;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Categoria> GetListCategories(string userName)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    var categories = new List<Categoria>();
                    categories = db.Categorias
                   .Select(c => new Categoria
                   {
                       IdCategoria = c.IdCategoria,
                       Nombre = c.Nombre,
                       Descripcion = c.Descripcion,
                       Estatus = c.Estatus,
                       CreateBy = c.CreateBy,
                       UpdateBy = c.UpdateBy,
                       FechaCreacion = c.FechaCreacion,
                       FechaActualizacion = c.FechaActualizacion,

                       // Incluye las subcategorías relacionadas
                       Subcategoria = db.Subcategorias
                           .Where(sc => sc.IdCategoria == c.IdCategoria)
                           .Select(sc => new Subcategoria
                           {
                               IdSubcategoria = sc.IdSubcategoria,
                               IdCategoria = sc.IdCategoria,
                               Nombre = sc.Nombre,
                               Descripcion = sc.Descripcion,
                               Estatus = sc.Estatus,
                               CreateBy = sc.CreateBy,
                               UpdateBy = sc.UpdateBy,
                               FechaCreacion = sc.FechaCreacion,
                               FechaActualizacion = sc.FechaActualizacion
                           }).ToList()
                   })
                   .ToList();

                    return categories;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public bool UpdateCategory(CategoriaEditDto categoriaEdit, string userName)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // Buscar la categoría por su ID
                    var existingCategory = db.Categorias.FirstOrDefault(c => c.IdCategoria == categoriaEdit.IdCategoria);

                    if (existingCategory == null)
                    {
                        throw new Exception("Categoría no encontrada.");
                    }

                    // Actualizar los campos permitidos
                    existingCategory.Nombre = categoriaEdit.Nombre;
                    existingCategory.Descripcion = categoriaEdit.Descripcion;

                    // Actualizar los campos de auditoría
                    existingCategory.UpdateBy = userName;
                    existingCategory.FechaActualizacion = DateTime.Now;

                    // Guardar los cambios en la base de datos
                    db.SaveChanges();

                    return true; // Retornar true si la actualización fue exitosa
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar la categoría: {ex.Message}");
            }
        }


        public bool CreateCategory(CategoriaCreateDto categoriaCreate, string userName)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // Crear una nueva instancia de la categoría
                    var newCategory = new Categoria
                    {
                        Nombre = categoriaCreate.Nombre,
                        Descripcion = categoriaCreate.Descripcion,
                        Estatus = true,  // Asumimos que la categoría está activa al momento de crearla
                        CreateBy = userName,
                        UpdateBy = userName,
                        FechaCreacion = DateTime.Now,
                        FechaActualizacion = DateTime.Now
                    };

                    // Agregar la nueva categoría a la base de datos
                    db.Categorias.Add(newCategory);

                    // Guardar los cambios en la base de datos
                    db.SaveChanges();

                    return true;  // Retornar true si la creación fue exitosa
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear la categoría: {ex.Message}");
            }
        }



        public List<CategoriesList> GetListCategoriesSubCategories()
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                var categoriesList = new List<CategoriesList>();

                    categoriesList = db.Categorias
                    .Where(c => c.Estatus) 
                    .GroupJoin(
                        db.Subcategorias,
                        c => c.IdCategoria, 
                        sc => sc.IdCategoria, 
                        (c, subcategories) => new CategoriesList
                        {
                            categoryId = c.IdCategoria,
                            name = c.Nombre,
                            status = c.Estatus,
                            SubcategoriaList = subcategories
                                .Select(sc => new SubCategoriesList
                                {
                                    IdSubcategoria = sc.IdSubcategoria,
                                    categoryId = sc.IdCategoria,
                                    name = sc.Nombre,
                                    status = sc.Estatus
                                }).ToList()
                        })
                    .ToList();

                    return categoriesList;
                }
      
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        
        }

        public bool ToggleCategoryStatus(int idCategoria, string userName)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // Busca la categoría por su ID
                    var category = db.Categorias.FirstOrDefault(c => c.IdCategoria == idCategoria);

                    if (category == null)
                    {
                        throw new Exception("Categoría no encontrada.");
                    }

                    // Cambia el estado de la categoría
                    category.Estatus = !category.Estatus;

                    // Actualiza los campos de auditoría
                    category.UpdateBy = userName;
                    category.FechaActualizacion = DateTime.Now;

                    // Guarda los cambios en la base de datos
                    db.SaveChanges();

                    return category.Estatus; 
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al cambiar el estado de la categoría: {ex.Message}");
            }
        }

    }
}
