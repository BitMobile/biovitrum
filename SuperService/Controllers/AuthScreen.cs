﻿using System;
using System.Net;
using System.Text;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using BitMobile.Application;

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
        }

        public override void OnShow()
        {
            var displayInfo = ApplicationContext.Current.DisplayProvider;

            DConsole.WriteLine($"{nameof(displayInfo.Height)}={displayInfo.Height} {Environment.NewLine}" +
                               $"{nameof(displayInfo.Width)}={displayInfo.Width} {Environment.NewLine}" +
                               $"{nameof(displayInfo.PxPerMm)}={displayInfo.PxPerMm}");
        }

        internal void exitButton_OnClick(object sender, EventArgs e)
        {
            Application.Terminate();
        }

        internal void connectButton_OnClick(object sender, EventArgs e)
        {
            Navigation.ModalMove("EventListScreen");
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