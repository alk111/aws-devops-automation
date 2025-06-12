using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OMartApplication.Repositories;
using OMartDomain.Common;
using OMartDomain.Models.ProductModels;
using OMartDomain.Models.ProductModels.RequestAndResponse;

namespace OMartInfra.Repositories
{
    public class AddProductModelRepository :CentralRepository, IAddProductModelRepository
    {
        private readonly string? _connectionString;

        public AddProductModelRepository(IConfiguration configuration) : base(configuration, "OMartDevDbConnection")
        {
            _connectionString = configuration.GetConnectionString(name: "OMartDevDbConnection");
        }

        public async Task<AddProductModelResponse> AddProductModel(AddProductModelRequest request)
        {
           try{
            var parameters=new 
            {
               p_ProductID=request.ProductID,
               p_ModelNum=request.ModelNum,
               p_ModelQuantity=request.ModelQuantity,
               p_Price=request.Price,
               p_SalePrice=request.SalePrice,
               p_TaxID=request.TaxID,
               p_Modelname=request.Modelname
            };
               int result= await ExecuteQueryAsync<int>(SPConstant.AddProductModel,parameters);
                 if (result > 0)
                {
                    throw new Exception("The stored procedure returned no result, indicating that the order might not have been added successfully.");
                }
                return new AddProductModelResponse{message="Data Added Successfully in db..."};
           } 
           catch (Exception ex)
           {
               throw new Exception($"Throw Exception while connecting to database :{ex.Message}",ex);
           }
        }

        public async Task<AddProductModelResponse> UpdateProductModel(UpdateProductModelsRequest request)
        {
             try{
            var parameters=new 
            {
               p_ProductModelsID=request.ProductModelsID,
               p_ProductID=request.ProductID,
               p_ModelNum=request.ModelNum,
               p_ModelQuantity=request.ModelQuantity,
               p_Price=request.Price,
               p_SalePrice=request.SalePrice,
               p_TaxID=request.TaxID,
               p_Modelname=request.Modelname

            };
               int result= await ExecuteQueryAsync<int>(SPConstant.UpdateProductModel,parameters);
                return new AddProductModelResponse{message="Data updated Successfully in db..."};
           } 
           catch (Exception ex)
           {
               throw new Exception($"Throw Exception while connecting to database :{ex.Message}",ex);
           }
        }

        public async Task<GetProductModelsByProductIDResponse> GetProductModelsByProductID(string ProductID)
        {
            try{
                var parameters=new 
                {
                    p_ProductID=ProductID
                };
                var result= await ExecuteQueryListAsync<ProductModels>(SPConstant.GetProductModelsByProductID,parameters);
                if (result==null)
                {
                    return new GetProductModelsByProductIDResponse{message="Not found",productModels=null};
                }
                return new GetProductModelsByProductIDResponse{productModels=result};
            }

            catch (Exception ex)
            {
                throw new Exception($"throw Exception While Connecting db :{ex.Message}");
            }
        }

        public async  Task<AddProductModelResponse> DeleteProductModel(int ProductModelsID)
        {
            try{
                    var parameters=new 
                    {
                        p_ProductModelsID=ProductModelsID
                    };
                    int result=await ExecuteQueryAsync<int>(SPConstant.DeleteProductModel,parameters);
                    if(result >0)
                    {
                    return new AddProductModelResponse{message="ProductModelsId is not found in the db"};
                    }
                    return new AddProductModelResponse{message="Successfully deleted data..."};
                }
                catch(Exception ex)
                {
                    throw new Exception($"Throw Exception While Connecting to database:{ex.Message}");
                }
        }
    }
}