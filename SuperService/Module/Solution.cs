using BitMobile.ClientModel3;

namespace Test
{
    public class Solution : Application
    {
        public override void OnCreate()
        {
            DConsole.WriteLine("DB init...");
            DBHelper.Init();
            DConsole.WriteLine("Settings init...");
            Settings.Init();
            DConsole.WriteLine("Authorization init...");
            Authorization.Init();
            //TODO: Если бытрая авторизация произойдёт то переходим на экран EventListScreen. Это пометка. Причины, читай комментарий ниже.
            //Так как из WebRequest мы можем облажаться со статусом авторизации(из-за асинхронности), пришлось немного накостылять
            Authorization.FastAuthorization();
            DConsole.WriteLine("Loading first screen...");
            Navigation.Move(nameof(AuthScreen));
        }
    }
}