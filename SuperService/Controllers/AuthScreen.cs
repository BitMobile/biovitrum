using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using System;
using System.Diagnostics.Eventing.Reader;

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
            Settings.Server = @"http://192.168.107.3/bitmobile/testsolution/device";
            Settings.Host = @"http://192.168.107.3";
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
                Host = Settings.Host,
                UserName = Settings.User,
                Password = Settings.Password
            };

            webRequest.Get(Settings.Server + @"/GetUserId", (o, args) =>
            {
                if (args.Result.Success)
                {
                    DConsole.WriteLine($"{Settings.UserId = args.Result.Result}");
                    DBHelper.FullSync((sender1, eventArgs) =>
                    {
                        if (eventArgs.Result) Navigation.ModalMove("EventListScreen");
                        else
                            DConsole.WriteLine(DBHelper.LastError);
                    });
                }
                else
                {
                    Toast.MakeToast(Translator.Translate("unsuccessful_auth"));
                    DConsole.WriteLine($"{args.Result.Result}");
                }
            });
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }
    }
}