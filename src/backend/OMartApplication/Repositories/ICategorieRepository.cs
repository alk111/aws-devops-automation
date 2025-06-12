using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartDomain.Models.Categorie.CategorieRequestAndResponse;

namespace OMartApplication.Repositories
{
    public interface ICategorieRepository
    {
        Task<CategorieResponse> GetCategoryByCategoriId(int category_id );
    }
}