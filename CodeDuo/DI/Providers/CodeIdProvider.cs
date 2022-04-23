namespace CodeDuo.DI.Providers
{
    public class CodeIdProvider : ICodeIdProvider
    {
        public Guid GetCodeId()
        {
            return Guid.NewGuid();
        }
    }
}
