namespace AtmApp
{
    public interface IInputValidator
    {
        T LoopValidateFormat<T>(Func<string, T> ParseFunc, IMessages outputMessage);
        bool ValidateAgainstList<T>(T? givenInput, List<T> acceptedInputs);
        public bool ValidateLength<T>(T input, int length);
        bool ValidateFormat<T>(Func<string, T> ParseFunc, string? input, out T? result);
    }
}