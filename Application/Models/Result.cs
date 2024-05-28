namespace Application.Models
{
    public record Result(bool Succeed, string[] Messages)
    {
        public static Result Success() => new(true, null);

        public static Result Success(string message) => new(true, new[] { message });

        public static Result Success(IList<string> messages) => new(true, messages.ToArray());

        public static Result Failure(string error) => new(false, new[] { error });

        public static Result Failure(IList<string> errors) => new(false, errors.ToArray());
    }
}