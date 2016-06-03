using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class TestScreen : Screen
    {
        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal string GetResourceComponent(string tag)
        {
            return ResourceManager.GetComponent(tag);
        }
    }
}