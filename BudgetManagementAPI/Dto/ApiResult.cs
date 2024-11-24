namespace BudgetManagementAPI.Dto
{
    public class ApiResult<T> where T : class
    {
        public ApiResult() { }
        public T? Results { get; set; }
        public string? Message { get; set; }

    }
}
