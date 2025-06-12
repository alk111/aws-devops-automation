using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartApplication.Repositories;
using OMartApplication.Services;
using OMartDomain.Models.Categorie.CategorieRequestAndResponse;

namespace OMartInfra.Services
{
    public class CategorieService : ICategoriesService
    {
            private readonly ICategorieRepository categorieRepository;
            public CategorieService(ICategorieRepository categorieRepository)
            {
                this.categorieRepository=categorieRepository;
            }
           
             public async  Task<CategorieResponse> GetCategoryByCategoriId(int category_id )
                {
                  var result=await categorieRepository.GetCategoryByCategoriId(category_id);
                  return result;  
                }
         

    }
}