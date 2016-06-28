using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Entities.Catalog;

namespace Test
{
    public class ContactScreen : Screen
    {
        public ContactScreen()
        {
            Variables["contact"] = new Contacts
            {
                Description = "Сергеев Алексей",
                Position = "Старший инженер",
                Tel = "+7 (915) 382-54-62",
                EMail = "nsergeev@comp.ru"
            };
        }

        public override void OnLoading()
        {
        }
    }
}