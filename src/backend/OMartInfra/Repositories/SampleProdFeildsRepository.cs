using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OMartApplication.Repositories;
using OMartDomain.Common;
using OMartDomain.Models.BookMark.requestAndResponse;
using OMartDomain.Models.SampleProdFields;
using OMartDomain.Models.SampleProdFields.ProductFieldsRequestAndResponse;

namespace OMartInfra.Repositories
{
    public class SampleProdFeildsRepository :CentralRepository,ISampleProdFieldsRepository
    {
                private readonly string? _connectionString;
                public SampleProdFeildsRepository(IConfiguration configuration) : base(configuration, "OMartDevDbConnection")
                {
                    _connectionString = configuration.GetConnectionString(name: "OMartDevDbConnection");

                }

        public async Task<InsertSampleProdFieldsResponse> InsertSampleProductField(InsertSampleProdFieldsRequest request)
             {
                    try{
                            var parameters=new 
                            {
                                p_SampleProdID=request.SampleProdID,
                                p_SampleProdOptionalFieldID=request.SampleProdOptionalFieldID,
                                p_fieldName=request.fieldName,
                                p_fieldValue=request.fieldValue
                            };

                           int result=await ExecuteQueryAsync<int>(SPConstant.InsertSampleProductField,parameters);

                           if (result > 0)
                                {
                                    throw new Exception("The stored procedure returned no result, indicating that the order might not have been added successfully.");
                                }

                            return new InsertSampleProdFieldsResponse{Message="Inserted successfully..."};
                        }
                    catch (Exception ex)
                    {
                        throw new Exception($"SampleProdFields is not added successfully...{ex.Message}");
                    }
             }


         public async Task<UpdateSampleProdFieldsResponse> UpdateSampleProductField(UpdateSampleProdFieldsRequest request)
             {
                try{
                    var parameters=new 
                    {
                                p_SampleProdID=request.SampleProdID,
                                p_SampleProdOptionalFieldID=request.SampleProdOptionalFieldID,
                                p_fieldName=request.fieldName,
                                p_fieldValue=request.fieldValue
                    };
                    var result=await ExecuteQueryAsync<int>(SPConstant.UpdateSampleProductField,parameters);
                    return new UpdateSampleProdFieldsResponse{Message="updated successfully data..."};
                }
                catch (Exception ex)
                {
                    throw new Exception($"Exception while Connecting Database.... {ex.Message}");
                }
             }

         public async  Task<GetBySampleProdFieldsResponse> GetBySampleProdOptionalFieldID(int SampleProdOptionalFieldID)
            {
                try{
                        var parameters=new
                        {
                        p_SampleProdOptionalFieldID=SampleProdOptionalFieldID
                        };

                        var result=await ExecuteQueryListAsync<dynamic>(SPConstant.GetBySampleProdOptionalFieldID,parameters);
                        
                        if(result==null)
                        {
                            return new GetBySampleProdFieldsResponse{Message="SampleProdOptionalFieldID is not found in db", };
                        }
                        return new GetBySampleProdFieldsResponse{Message="SampleProdOptionalFieldID is getting successfully...",  };
                   }
                    catch(Exception ex)
                        {
                            throw new Exception($"Exception while connecting to the database: {ex.Message}", ex);
                        }

               }


         public async Task<UpdateSampleProdFieldsResponse> DeleteBySampleProdOptionalFieldID(int SampleProdOptionalFieldID)
          {
             try{
                    var parameters=new
                    {
                        p_SampleProdOptionalFieldID=SampleProdOptionalFieldID  
                    };
                    var result= await ExecuteQueryAsync<int>(SPConstant.DeleteBySampleProdOptionalFieldID,parameters);
                    if(result > 0)
                    {
                        return new UpdateSampleProdFieldsResponse{Message="No SampleProdOptionalFieldID in DB...",};
                    }
                    return new UpdateSampleProdFieldsResponse{Message="Deleted successfully...",};
                } 
                catch(Exception ex)
                    {
                    throw new Exception($"Exception while connecting to the database: {ex.Message}", ex);
                     } 
            }


    }

}
