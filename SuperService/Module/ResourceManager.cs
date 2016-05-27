using System.Collections.Generic;
using BitMobile.ClientModel3;

namespace Test
{
    public static class ResourceManager
    {
        private const string ImageNotFound = @"Image\not_found.png";

        private static readonly Dictionary<string, object> Paths = new Dictionary<string, object>
        {
            {"tabbar_bag", @"Image\_Components\TabBar\Bag.png"},
            {"topinfo_back", @"Image\_Components\topback.png" }
        };

        public static string GetImage(string tag)
        {
            object res;
            if (!Paths.TryGetValue(tag, out res))
            {
                DConsole.WriteLine($"{tag} is not found in ResourceManager!");
                return ImageNotFound;
            }
            try
            {
                Application.GetResourceStream(res.ToString());
            }
            catch
            {
                DConsole.WriteLine($"{tag}:{res} does not exists!");
                return ImageNotFound;
            }
            return (string) res;
        }
    }
}