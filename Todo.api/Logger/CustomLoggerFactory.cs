namespace Todo.api.Logger
{
    public static class CustomLoggerFactory
    {
        private readonly static ICustomLogger _logger;

        static CustomLoggerFactory()
        {
            _logger = new CustomLogger();
        }

        public static ICustomLogger GetLogger()
        {
            return _logger;
        }
    }
}
