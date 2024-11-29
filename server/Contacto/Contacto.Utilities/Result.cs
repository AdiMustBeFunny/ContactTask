namespace Contacto.Utilities
{
    public class Result
    {
        protected readonly Error _error;

        protected Result(Error error)
        {
            if (error == null)
            {
                throw new ArgumentException("Error cannot be set to null");
            }

            _error = error;
        }

        public Error Error => _error;

        public bool IsSuccess => _error == Error.Empty;

        public bool IsFailure => _error != Error.Empty;

        public static Result Success() => new Result(Error.Empty);

        public static Result Failure(Error error) => new Result(error);

        public static Result<TData> Success<TData>(TData data) => new(Error.Empty, data);

        public static Result<TData> Failure<TData>(Error error) => new(error, default);
    }

    public class Result<TValue> : Result
    {
        private readonly TValue? _value;

        internal Result(Error error, TValue? value) : base(error) => _value = value;

        public TValue Value => _error == Error.Empty
            ? _value!
            : throw new InvalidOperationException("Cannnot acces value of a failed result");

        public static implicit operator Result<TValue>(TValue? value) => Create(value);

        private static Result<TData> Create<TData>(TData? value) => value != null
            ? new(Error.Empty, value)
            : new(Error.NullObject, value);
    }
}
