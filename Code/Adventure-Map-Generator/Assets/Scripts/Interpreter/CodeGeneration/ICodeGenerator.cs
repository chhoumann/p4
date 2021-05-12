namespace Dazel.Interpreter.CodeGeneration
{
    public interface ICodeGenerator<out TGameObject>
    {
        public TGameObject Generate();
    }
}