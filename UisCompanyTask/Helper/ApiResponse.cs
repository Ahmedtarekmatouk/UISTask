namespace UisCompanyTask.Helper
{
    public class ApiResponse<T>
    {
        public int StateCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public ApiResponse(int stateCode, bool isSuccess, string? message = default, T? data = default)
        {

            StateCode = stateCode;
            Message = string.IsNullOrEmpty(message) ? GetDefaultMessage(stateCode) : message;
            IsSuccess = isSuccess;
            Data = data;
        }

        private string GetDefaultMessage(int stateCode)
        {
            return stateCode switch
            {
                200 => "Ok",
                201 => "Created successfully",
                204 => "No content",
                400 => "The Provided Data Doesn't Meet The Validation Requirments",
                404 => "Not found",
            };


        }
    }
}
