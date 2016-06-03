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
            DConsole.WriteLine("AuthScreen init");

            _loginEditText = (EditText) GetControl("loginEditText", true);
            _passwordEditText = (EditText) GetControl("passwordEditText", true);
        }

        internal void exitButton_OnClick(object sender, EventArgs e)
        {
            Application.Terminate();
        }

        internal void connectButton_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("Auth");
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