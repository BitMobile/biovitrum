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

        internal void СonnectButton_OnClick(object sender, EventArgs e)
        {
            Authorization.StartAuthorization(_loginEditText.Text, _passwordEditText.Text);
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }
    }
}