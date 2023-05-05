public static class Setup
{
    private readonly static  string _path = Path.Combine(Environment.CurrentDirectory +"/downloads/") ;
    public static void Initialize()
    {
        System.IO.Directory.Delete(_path);
        System.IO.Directory.CreateDirectory(_path);
    }
}