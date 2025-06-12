using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartDomain.Models.Categorie.CategorieRequestAndResponse;

namespace OMartApplication.Services
{
    public interface ICategoriesService
    {
         Task<CategorieResponse> GetCategoryByCategoriId(int category_id );
    }
}