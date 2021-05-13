namespace Dazel.Compiler.CodeGeneration
{
    public interface ICodeGenerator<out TGameObject>
    {
        public TGameObject Generate();
    }
}