namespace WebStone.Khiputech.Platform.Shared.Application.Model;

public class Result
{
    protected Result(bool isSuccess, Error? error)   // ← Cambiado a protected
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error? Error { get; }

    public static Result Success() => new(true, null);
    public static Result Failure(Error error) => new(false, error);
}