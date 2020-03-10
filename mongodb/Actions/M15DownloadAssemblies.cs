using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Logging;

namespace mongodb.Actions
{
    class M15DownloadAssemblies : IMenuAction
    {
        public int Id => 15;
        public string Description => "Download assemblies";
        public async void Execute(IAppConfigurations configurations, AppMongoClient appMongoClient, ILogger logger)
        {
            var urls = new[]
            {
                "http://localhost:8080/databases/gccdev/static/ProductAssembly/505222",
                "http://localhost:8080/databases/gccdev/static/ProductAssembly/507702",
                "http://localhost:8080/databases/gccdev/static/ProductAssembly/507703"
            };

            var client = new HttpClient();

            foreach (var url in urls)
            {
                var response = await client.GetStreamAsync(url);
                using (var ms = new MemoryStream())
                {
                    response.CopyTo(ms);
                    Console.WriteLine($"stream {url} is {ms.Length} bytes");
                }
            }
            

        }
    }
}
