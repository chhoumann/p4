namespace Dazel.Interpreter.ErrorHandler
{
    public interface IErrorLogger
    {
        void Log();
        void AddToErrorList(string error);
        bool HasErrors();
    }
}