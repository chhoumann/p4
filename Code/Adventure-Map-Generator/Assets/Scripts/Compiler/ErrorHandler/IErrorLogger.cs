namespace Dazel.Compiler.ErrorHandler
{
    public interface IErrorLogger
    {
        void Log();
        void AddToErrorList(string error);
        bool HasErrors { get; }
    }
}