namespace Dangl.Data.Shared
{
    public class RepositoryResult<T>
    {
        private RepositoryResult() { }

        public bool IsSuccess { get; private set; }
        public T Result { get; private set; }
        public string ErrorMessage { get; private set; }

        public static RepositoryResult<T> Fail(string errorMessage)
        {
            return new RepositoryResult<T>
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };
        }

        public static RepositoryResult<T> Success(T value)
        {
            return new RepositoryResult<T>
            {
                IsSuccess = true,
                Result = value
            };
        }
    }

    public class RepositoryResult
    {
        private RepositoryResult() { }

        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; }

        public static RepositoryResult Fail(string errorMessage)
        {
            return new RepositoryResult
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };
        }

        public static RepositoryResult Success()
        {
            return new RepositoryResult
            {
                IsSuccess = true
            };
        }
    }
}
