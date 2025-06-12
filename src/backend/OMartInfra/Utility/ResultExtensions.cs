//using OMartDomain.Models.Wrapper;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.Json.Serialization;
//using System.Text.Json;
//using System.Threading.Tasks;
//using static OMartApplication.Services.Wrapper.IResult;

//namespace OMartInfra.Utility
//{
//    public static class ResultExtensions
//    {
//        public static async Task<IResult<T>> ToResult<T>(this HttpResponseMessage response)
//        {
//            try
//            {
//                var responseAsString = await response.Content.ReadAsStringAsync();
//                var responseObject = JsonSerializer.Deserialize<Result<T>>(responseAsString,
//                    new JsonSerializerOptions
//                    {
//                        PropertyNameCaseInsensitive = true,
//                        ReferenceHandler = ReferenceHandler.Preserve
//                    });
//                return responseObject;
//            }
//            catch (Exception ex)
//            {
//                return null;
//            }

//        }

//        public static async Task<IResult> ToResult(this HttpResponseMessage response)
//        {
//            var responseAsString = await response.Content.ReadAsStringAsync();
//            var responseObject = JsonSerializer.Deserialize<Result>(responseAsString, new JsonSerializerOptions
//            {
//                PropertyNameCaseInsensitive = true,
//                ReferenceHandler = ReferenceHandler.Preserve
//            });
//            return responseObject;
//        }
//    }
//}
