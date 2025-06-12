using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OMartApplication.Repositories;
using OMartDomain.Common;
using OMartDomain.Models.Categorie;
using OMartDomain.Models.Categorie.CategorieRequestAndResponse;

namespace OMartInfra.Repositories
{
    public class CategorieRepository :CentralRepository,ICategorieRepository
    {
        private readonly string? _connectionString;

        public CategorieRepository(IConfiguration configuration) : base(configuration, "OMartDevDbConnection")
        {
            _connectionString = configuration.GetConnectionString(name: "OMartDevDbConnection");

        }
        public async  Task<CategorieResponse> GetCategoryByCategoriId(int category_id )
        {
            try{
                    var parameters= new 
                    {
                        p_category_id=category_id
                    };
                    var result = await ExecuteQueryListAsync<Categoreis>(SPConstant.GetCategoryByCategoriId,parameters);
                    if(result==null)
                    {
                        return new CategorieResponse {message="categoreis not found in the databases" , categoreis=null};
                    }
                    Console.WriteLine(result);  
                return new CategorieResponse{message="Data Get Successfully....", categoreis=result};
               }
               catch (Exception ex)
               {
                   throw new Exception($"Exception while connecting to the database: {ex.Message}", ex);  
               }
        }
    }
}