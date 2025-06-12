using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OMartApplication.Repositories;
using OMartDomain.Common;
using OMartDomain.Models.SampleProductDetails;
using OMartDomain.Models.SampleProductDetails.SampleProductRequestAndResponse;

namespace OMartInfra.Repositories
{
    public class SampleProductDetailsRepository :CentralRepository, ISampleProductRepository 
    {
        
         private readonly string? _connectionString;
        public SampleProductDetailsRepository(IConfiguration configuration) : base(configuration, "OMartDevDbConnection")
        {
            _connectionString = configuration.GetConnectionString(name: "OMartDevDbConnection");

        }

       public async Task<InsertSampleProductResponse> insertSampleProduct(InsertSampleProductRequest request)
       {
        
            try{
                  var parameters=new 
                  {
                       p_BrandName=request.BrandName,
                       p_ProductName=request.ProductName,
                       p_ProductDescription=request.ProductDescription
                  };

                   int result=await ExecuteQueryAsync<int>(SPConstant.insertSampleProduct,parameters);

                    if (result > 0)
                            {
                                throw new Exception("The stored procedure returned no result, indicating that the order might not have been added successfully.");
                            }

                      return new InsertSampleProductResponse { message = "Done" };
                  }
                    catch (Exception ex)
                    {
                        throw new Exception($"BookMark is not added successfully...{ex.Message}");
                    }
         }


         public async Task<UpdateSampleProductResponse> updateSampleProductDetails(UpdateSampleProductRequest request)
         {
             try
            {
                var parameters = new
                {
                       p_SampleProdID=request.SampleProdID,
                       p_BrandName=request.BrandName,
                       p_ProductName=request.ProductName,
                       p_ProductDescription=request.ProductDescription
                };

                int result = await ExecuteQueryAsync<int>(SPConstant.updateSampleProductDetails, parameters);
                return new UpdateSampleProductResponse { message= "Updated Successfully..." };

            }
                catch (Exception ex)
                {
                    throw new Exception($"Exception while Connecting Database.... {ex.Message}");
                }
         }


         public async Task<GetSampleProductDetailsResponse> GetSampleProductBySampleProdID(int SampleProdID)
         {
                try
                {
                    var parameters = new
                    {
                        p_SampleProdID= SampleProdID
                    };

                    var result = await ExecuteQueryListAsync<SampleProductDetails>(SPConstant.GetSampleProductBySampleProdID, parameters);

                    if (result == null)
                    {

                        return new GetSampleProductDetailsResponse { message = "No SampleProduct found for the user.", samples = null };
                    }

                    return new GetSampleProductDetailsResponse { message = "Data Get Successfully...", samples= result };
                }
                catch (Exception ex)
                {
                    throw new Exception($"Exception while connecting to the database: {ex.Message}", ex);
                }
            }


             public async  Task<UpdateSampleProductResponse> deleteSampleProductDetails(int SampleProdID)
        {
            try
            {
                var parameters = new
                {
                    p_SampleProdID= SampleProdID
                };

                int result = await ExecuteQueryAsync<int>(SPConstant.deleteSampleProductDetails, parameters);

                if (result > 0)
                {
                    return new UpdateSampleProductResponse { message = "No sample Product found for the user." };
                }

                return new UpdateSampleProductResponse { message = "deleted SuccessFully..." };
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception while connecting to the database: {ex.Message}", ex);
            }
        }


    }

    }