public static class GuardExtensions
    {
        
        public static void Null<T>(this IGuardClause guardClause, T argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
      
        public static void NullOrEmpty(this IGuardClause guardClause, string argument, string argumentName)
        {
            if (string.IsNullOrEmpty(argument))
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    public static void MinimumLength(this IGuardClause guardClause, string argument, string argumentName, int minLength)
        {
            if (argument.Length < minLength)
            {
                throw new ArgumentException(string.Format("{0} is not allowing characters less than {1}", argumentName, minLength));
            }
        }

        public static void NoItems(this IGuardClause guardclause, string[] argument)
        {
            if (argument.Length < 1)
            {
                throw new ArgumentException("Usage: dotnet run <amount> [<threads>]");
            }
        }
        
        public static void MaxThreads(this IGuardClause guardClause,int threads)
        {
        if (threads < 1 || threads > 5)
            {
                throw new ArgumentException("The number of threads must be between 1 and 5");
            }
        }
        
    }