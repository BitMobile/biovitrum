using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class AuthScreen : Screen
    {
        private EditText _loginEditText;
        private EditText _passwordEditText;

        public override void OnLoading()
        {
            DConsole.WriteLine("AuthScreen init");

            _loginEditText = (EditText) GetControl("AuthScreenLoginET", true);
            _passwordEditText = (EditText) GetControl("AuthScreenPasswordET", true);

            Settings.Server = @"http://192.168.107.3/bitmobile/testsolution/device";
            Settings.Host = @"http://192.168.107.3";


        }

        public override void OnShow()
        {
            _loginEditText.Text = Settings.User;
            _passwordEditText.Text = Settings.Password;
        }

        internal void CantSigningButton_OnClick(object sender, EventArgs e)
        {
            DConsole.WriteLine("Can't sign in?");
            //TODO: В релизе удалить
            Settings.User = "";
            Settings.Password = "";
        }

        internal void connectButton_OnClick(object sender, EventArgs e)
        {
            var webRequest = new WebRequest
            {
                Host = Settings.Host,
                UserName = Settings.User = _loginEditText.Text,
                Password = Settings.Password = _passwordEditText.Text
            };

            webRequest.Get(Settings.Server + @"/GetUserId", (o, args) =>
            {
                if (args.Result.Success)
                {
#if DEBUG
                    DConsole.WriteLine(Translator.Translate("successful_auth"));
                    DConsole.WriteLine($"{nameof(Settings.UserId)}->{Settings.UserId = args.Result.Result}");
#endif
                    DBHelper.FullSync((sender1, eventArgs) =>
                    {
                        if (eventArgs.Result) Navigation.ModalMove("EventListScreen");
                        else
                            DConsole.WriteLine(DBHelper.LastError);
                    });
                }
                else
                {
                    _passwordEditText.Text = Settings.Password = "";
                    Toast.MakeToast(Translator.Translate("unsuccessful_auth"));
#if DEBUG
                    DConsole.WriteLine($"{args.Result.Result}{Environment.NewLine}" +
                                       $"{Translator.Translate("unsuccessful_auth")}");
#endif
                }
            });
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }
    }
}