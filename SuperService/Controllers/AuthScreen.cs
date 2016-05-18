using System;
using System.Net;
using System.Text;
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
            DConsole.WriteLine("Onloading Init");
            DConsole.WriteLine("Loading controls");

            _loginEditText = (EditText)GetControl("loginEditText", true);
            _passwordEditText = (EditText)GetControl("passwordEditText", true);
            //Initialize();
        }

        void Initialize()
        {
            DConsole.WriteLine("Init begin");

            //var vl = new VerticalLayout
            //{
            //    CssClass = "RootLayout"
            //};

            //_loginEditText = new EditText()
            //{
            //    Placeholder = "Введите логин",
            //    CssClass = "loginEditText"
            //};

            //_passwordEditText = new EditText()
            //{
            //    CssClass = "passwordEditText",
            //    Placeholder = "Введите пароль"
            //};
            //Button ConnectButton = new Button("Подключиться", ConnectButton_OnClick)
            //{
            //    CssClass = "ConnectButton"
            //};
            //Button ExitButton = new Button("Выход", ExitButton_OnClick)
            //{
            //    CssClass = "ExitButton"
            //};

            //vl.AddChild(_loginEditText);
            //vl.AddChild(_passwordEditText);
            //vl.AddChild(ConnectButton);
            //vl.AddChild(ExitButton);

            //AddChild(vl);
        }

        internal void exitButton_OnClick(object sender, EventArgs e)
        {
            Application.Terminate();
        }

        internal void connectButton_OnClick(object sender, EventArgs e)
        {
            //BusinessProcess.DoAction("Auth");
            var req = WebRequest.Create("http://bitmobile1.bt/bitmobileX/platform/device/GetClientMetadata");
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
            catch (Exception AuthException)
            {
                DConsole.WriteLine("Неверный логин/пароль\n" + AuthException.Message);
                Dialog.Message("Неверный логин/пароль\n" + AuthException.Message);
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
            }
        }
    }
}