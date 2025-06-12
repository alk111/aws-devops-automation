using OMartDomain.Models.App;

namespace OMartInfra.Utility
{
    public static class ExtResponse
    {
        public static ApiResponse AsSuccess(this string Message)
        {
            var response = new ApiResponse();
            response.Success = true;
            List<string> SuccessMessages = new List<string>();
            SuccessMessages.Add(Message);
            response.Messages = SuccessMessages;

            return response;
        }

        public static ApiResponse AsSuccess(this List<string> Messages)
        {
            var response = new ApiResponse();
            response.Success = true;
            response.Messages = Messages;

            return response;
        }

        public static ApiResponse<T> AsSuccess<T>(this T Data, string Message) where T : class
        {
            var response = new ApiResponse<T>();
            response.Success = true;
            List<string> SuccessMessages = new List<string>();
            SuccessMessages.Add(Message);
            response.Messages = SuccessMessages;

            try
            {
                response.Data = Data;
            }
            catch (Exception ex)
            {
                response.Success = false;
                List<string> ErrorMessages = new List<string>();
                ErrorMessages.Add(ex.Message);
                response.Messages = ErrorMessages;
            }

            return response;
        }

        public static ApiResponse<T> AsSuccess<T>(this T Data, List<string> Messages = null) where T : class
        {
            var response = new ApiResponse<T>();
            response.Success = true;
            response.Messages = Messages;

            try
            {
                response.Data = Data;
            }
            catch (Exception ex)
            {
                response.Success = false;
                List<string> ErrorMessages = new List<string>();
                ErrorMessages.Add(ex.Message);
                response.Messages = ErrorMessages;
            }

            return response;
        }

        public static ApiResponse AsFailure(this string Message)
        {
            var response = new ApiResponse();
            response.Success = false;
            List<string> FailureMessages = new List<string>();
            FailureMessages.Add(Message);
            response.Messages = FailureMessages;

            return response;
        }

        public static ApiResponse AsFailure(this List<string> Messages)
        {
            var response = new ApiResponse();
            response.Success = false;
            response.Messages = Messages;

            return response;
        }

        public static ApiResponse<T> AsFailure<T>(this T Data, string Message) where T : class
        {
            var response = new ApiResponse<T>();
            response.Success = false;
            List<string> FailureMessages = new List<string>();
            FailureMessages.Add(Message);
            response.Messages = FailureMessages;

            try
            {
                response.Data = Data;
            }
            catch (Exception ex)
            {
                response.Success = false;
                List<string> ErrorMessages = new List<string>();
                ErrorMessages.Add(ex.Message);
                response.Messages = ErrorMessages;
            }

            return response;
        }

        public static ApiResponse<T> AsFailure<T>(this T Data, List<string> Messages = null) where T : class
        {
            var response = new ApiResponse<T>();
            response.Success = false;
            response.Messages = Messages;

            try
            {
                response.Data = Data;
            }
            catch (Exception ex)
            {
                response.Success = false;
                List<string> ErrorMessages = new List<string>();
                ErrorMessages.Add(ex.Message);
                response.Messages = ErrorMessages;
            }

            return response;
        }
    }
}