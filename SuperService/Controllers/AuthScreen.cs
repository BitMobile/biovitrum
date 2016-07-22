using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using System;

namespace Test
{
    public class AuthScreen : Screen
    {
        private EditText _loginEditText;
        private EditText _passwordEditText;
        private WebRequest _webRequest;

        public override void OnLoading()
        {
            DConsole.WriteLine("AuthScreen init");

            _loginEditText = (EditText)GetControl("AuthScreenLoginET", true);
            _passwordEditText = (EditText)GetControl("AuthScreenPasswordET", true);

            if (_webRequest == null)
            {
                _webRequest = new WebRequest
                {
                    Host = Settings.Host,
                    Timeout = new TimeSpan(0,0,5).ToString()
                };
            }
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
            Settings.User = Settings.Password = "";
            _loginEditText.Text = _passwordEditText.Text = "";
        }

        internal void connectButton_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Settings.User))
            {
                _webRequest.UserName = _loginEditText.Text;
                _webRequest.Password = _passwordEditText.Text;

                _webRequest.Get(Settings.AuthUrl, (obj, args) =>
                {
                    if (args.Result.Success)
                    {
                        Settings.UserId = args.Result.Result;
                        EmptyAutorizationData();
                    }
                    else
                    {
                        _passwordEditText.Text = Settings.Password = "";

                        ErrorMessage(args);

                        DConsole.WriteLine($"{args.Result.Result}{Environment.NewLine}" +
                                           $"{Translator.Translate("unsuccessful_auth")} " +
                                           $"Error {args.Result.Result} WebError {args.Result.Error.Message}");
                    }
                });
            }
            else
            {
                _webRequest.UserName = Settings.User;
                _webRequest.Password = _passwordEditText.Text;
                _webRequest.Get(Settings.AuthUrl, (o, args) =>
                {
                    if (args.Result.Success)
                    {
                        Settings.UserId = args.Result.Result;

                // ReSharper disable once StringCompareIsCultureSpecific.3
                if (string.Compare(Settings.User, _loginEditText.Text, true) == 0)
                        {
                            Settings.Password = _passwordEditText.Text;
                            DBHelper.SyncAsync();
                            Toast.MakeToast(Translator.Translate("successful_auth"));
                            NextScreen();
                        }
                        else
                        {
                            EmptyAutorizationData();
                        }
                    }
                    else
                    {
                        _passwordEditText.Text = Settings.Password = "";
                       
                        ErrorMessage(args);

                        DConsole.WriteLine($"{args.Result.Result}{Environment.NewLine}" +
                                           $"{Translator.Translate("unsuccessful_auth")} " +
                                           $"Error {args.Result.Result} WebError {args.Result.Error.Message}");
                    }
                });
            }
        }

        private static void ErrorMessage(ResultEventArgs<WebRequest.WebRequestResult> args)
        {
            switch (args.Result.Error.StatusCode)
            {
                case -1:
                    Toast.MakeToast(Translator.Translate("сonnection_error"));
                    break;
                case 401:
                    Toast.MakeToast(Translator.Translate("uncorrect_login_or_pass"));
                    break;
                default:
                    Toast.MakeToast(Translator.Translate("unexpected_error"));
                    break;
            }
        }

        private void EmptyAutorizationData()
        {
            Settings.User = _loginEditText.Text;
            Settings.Password = _passwordEditText.Text;
            DConsole.WriteLine(Translator.Translate("successful_auth"));
            DBHelper.FullSync((sender1, eventArgs) =>
            {
                if (eventArgs.Result) NextScreen();
                else
                    DConsole.WriteLine(DBHelper.LastError);
            });
        }

        private static void NextScreen()
        {
            Navigation.ModalMove("EventListScreen");
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }
    }
}