namespace api.Helpers;

public record DataResult<T>(
    int Count,
    IReadOnlyList<T> Result
) where T : class;
