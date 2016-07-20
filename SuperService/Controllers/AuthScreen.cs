using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using System;

namespace Test
{
    public class AuthScreen : Screen
    {
        private EditText _loginEditText;
        private EditText _passwordEditText;

        public override void OnLoading()
        {
            DConsole.WriteLine("AuthScreen init");

            _loginEditText = (EditText)GetControl("AuthScreenLoginET", true);
            _passwordEditText = (EditText)GetControl("AuthScreenPasswordET", true);
        }

        public override void OnShow()
        {
            //TODO: Опастно так хранить юзера. Потом удалить.
            Settings.Server = @"http://192.168.10.2/bitmobile/testsolution/device";
        }

        internal void CantSigningButton_OnClick(object sender, EventArgs e)
        {
            DConsole.WriteLine("Can't signing?");
        }

        internal void connectButton_OnClick(object sender, EventArgs e)
        {
            Settings.User = _loginEditText.Text;
            Settings.Password = _passwordEditText.Text;

            var webRequest = new WebRequest
            {
                Host = @"http://192.168.10.2",
                UserName = Settings.User,
                Password = Settings.Password
            };

            webRequest.Get(Settings.Server + @"/GetUserId", (o, args) =>
            {
                if (args.Result.Success)
                {
                    Toast.MakeToast("Удачно");
                    DConsole.WriteLine($"{Settings.UserId = args.Result.Result}");
                    DBHelper.FullSync((sender1, eventArgs) =>
                    {
                        if (eventArgs.Result)
                        {
                            DConsole.WriteLine($"Sync is OK");
                            Dialog.Message("Синхронизация выполнена!", (o1, args1) =>
                            {
                                Navigation.ModalMove("EventListScreen");
                            });
                        }
                    });
                }
                else
                {
                    Toast.MakeToast("Не удачно");
                    DConsole.WriteLine($"{args.Result.Result}");
                }
            });

            //var db = DBHelper.GetDatabase();
            //Navigation.ModalMove("EventListScreen");
            // TODO: Сделать авторизацию когда она будет работать
            /*            var req = WebRequest.Create("http://bitmobile1.bt/bitmobileX/platform/device/GetClientMetadata");
                        DConsole.WriteLine("Web Request Created");
                        //var svcCredentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("sr" + ":" + "sr"));
                        var svcCredentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(_loginEditText.Text + ":" + _passwordEditText.Text));
                        req.Headers.Add("Authorization", "Basic " + svcCredentials);
                        DConsole.WriteLine("Headers added");

                        WebResponse resp = null;
                        bool flag = false;
                        try
                        {
                            resp = req.GetResponse();
                            flag = true;
                            DConsole.WriteLine("Стучимся по URL");
                        }
                        catch (Exception authException)
                        {
                            DConsole.WriteLine("Неверный логин/пароль\n" + authException.Message);
                            Dialog.Message("Неверный логин/пароль\n" + authException.Message);
                        }
                        finally
                        {
                            resp?.Dispose();
                        }
                        if (flag)
                        {
                            DConsole.WriteLine("Вход выполнен");
                            Dialog.Message("Вход выполнен");
                            BusinessProcess.DoAction("Auth");
                        }*/
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }
    }
}