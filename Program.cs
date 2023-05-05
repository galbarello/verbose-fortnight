using System.Text.RegularExpressions;

class Program
{
    static async Task Main(string[] args)
    {
        Setup.Initialize();
        Guard.Against.NoItems(args);
        
        int amount = int.Parse(args[0]);
        int threads = 1;


        //thead declared, if not 1 by default
        
        if (args.Length > 1)
        {
             threads = int.Parse(args[1]);
            Guard.Against.MaxThreads(threads);
        }

        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync("https://icanhas.cheezburger.com/");
        var html = await response.Content.ReadAsStringAsync();

        var imageUrls = new string[amount];
        var regex = new Regex(@"<img.*?src=""(https://i.chzbgr.com/.*?)"".*?>");
        int index = 0;

        foreach (Match match in regex.Matches(html))
        {
            if (index >= amount) break;

            var imageUrl = match.Groups[1].Value;
            if (!imageUrl.Contains("sponsor") && !imageUrl.Contains("ad") && !imageUrl.Contains("teaser")&& imageUrl.Contains("thumb"))
            {
                imageUrls[index++] = imageUrl;
            }
        }

        var tasks = new Task[threads];
        int batchSize = amount / threads;

        for (int i = 0; i < threads; i++)
        {
            int start = i * batchSize;
            int end = i < threads - 1 ? start + batchSize : amount;

            tasks[i] = Task.Run(async () =>
            {
                for (int j = start; j < end; j++)
                {
                    try
                    {
                        var url = imageUrls[j];
                        var fileName = Path.GetFileName(url);
                        var filePath = Path.Combine(Environment.CurrentDirectory+"/downloads/", fileName);
                        

                        if (!File.Exists(filePath))
                        {
                            Console.WriteLine($"Downloading {url}");
                            var imageBytes = await httpClient.GetByteArrayAsync(url);
                            await File.WriteAllBytesAsync(filePath, imageBytes);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error downloading image: {ex.Message}");
                    }
                }
            });
        }

        await Task.WhenAll(tasks);

        Console.WriteLine("Done!");
    }
}