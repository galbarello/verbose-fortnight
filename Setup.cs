public static class Setup
{
    private readonly static  string _path = Path.Combine(Environment.CurrentDirectory +"/downloads/") ;
    public static void Initialize()
    {
        if (System.IO.Directory.Exists(_path)) 
        {
            System.IO.Directory.Delete(_path);
        }
        else
        {
            System.IO.Directory.CreateDirectory(_path);
        }
        
    }
}