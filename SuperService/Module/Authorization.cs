﻿using System;
using BitMobile.ClientModel3;

namespace Test
{
    public static class Authorization
    {
        private static WebRequest _webRequest;
        public static bool Initialized { get; private set; }

        public static void Init()
        {
            _webRequest = new WebRequest
            {
                Host = Settings.Host,
                Timeout = new TimeSpan(0, 0, 5).ToString()
            };

            Initialized = true;
        }

        /// <summary>
        ///     Быстрая авторизация.
        /// </summary>
        /// <returns>
        ///     Возращает true если логин и пароль не пустые
        ///     , иначе false
        /// </returns>
        public static bool FastAuthorization()
        {
            return !string.IsNullOrEmpty(Settings.User) && !string.IsNullOrEmpty(Settings.Password);
        }

        public static void StartAuthorization(string userName, string password, AuthScreen screen)
        {
            _webRequest.UserName = userName;
            _webRequest.Password = password;

            _webRequest.Get(Settings.AuthUrl, (sender, args) =>
            {
                if (args.Result.Success)
                {
                    Settings.UserId = args.Result.Result;
#if DEBUG
                    DConsole.WriteLine("Авторизация успешна");
                    DConsole.WriteLine($"UserId - {Settings.UserId} Web Request Result - {args.Result.Result}");
#endif

                    //Проверяем, если пользователь уже сохранен, то делаем частичную синхронизацию, иначе полную.
                    // ReSharper disable once StringCompareIsCultureSpecific.3
                    if (string.Compare(Settings.User, userName, true) == 0)
                    {
#if DEBUG
                        DConsole.WriteLine($"Авторизировались, пользователь сохранен в системе.");
                        DConsole.WriteLine("Сохраняем пароль");
#endif

                        Settings.Password = password;

#if DEBUG
                        DConsole.WriteLine($"Запустили частичную синхронизацию. From class {nameof(Authorization)}");
#endif
                        DBHelper.SyncAsync();
                        DConsole.WriteLine("Loading first screen...");
                        Navigation.ModalMove("EventListScreen");
                    }
                    else
                    {
#if DEBUG
                        DConsole.WriteLine($"Авторизировались, пользователь НЕ сохранен в системе.");
                        DConsole.WriteLine("Сохраняем пользователя и пароль в системе");
#endif
                        Settings.User = userName;
                        Settings.Password = password;
#if DEBUG
                        DConsole.WriteLine($"Запустили полную синхронизацию. From class {nameof(Authorization)}");
#endif
                        DBHelper.FullSync((o, eventArgs) =>
                        {
                            if (!DBHelper.SuccessSync) return;
#if DEBUG
                            DConsole.WriteLine(Parameters.Splitter);
                            DConsole.WriteLine("Синхронизация удачна");
                            DConsole.WriteLine($"{nameof(DBHelper.SuccessSync)} - {DBHelper.SuccessSync}");
                            DConsole.WriteLine($"In Class {nameof(Authorization)} Method {nameof(StartAuthorization)}");
                            DConsole.WriteLine(Parameters.Splitter);
                            DConsole.WriteLine("Loading first screen...");
#endif
                            Navigation.ModalMove("EventListScreen");
                        });
                    }
                }
                else
                {
#if DEBUG
                    DConsole.WriteLine($"Авторизация не удалась. Сбрасываем пароль.");
#endif
                    Settings.Password = "";
                    screen.ClearPassword();

                    ErrorMessageWithToast(args);
                }
            });
        }

        private static void ErrorMessageWithToast(ResultEventArgs<WebRequest.WebRequestResult> args)
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

        private static void ErrorInfo(ResultEventArgs<WebRequest.WebRequestResult> args)
        {
            switch (args.Result.Error.StatusCode)
            {
                case -1:
                    DConsole.WriteLine($"{Translator.Translate("сonnection_error")} Error - {args.Result.Error.Message}");
                    break;

                case 401:
                    DConsole.WriteLine(
                        $"{Translator.Translate("uncorrect_login_or_pass")} Error - {args.Result.Error.Message}");
                    break;

                default:
                    DConsole.WriteLine($"{Translator.Translate("unexpected_error")} - Error {args.Result.Error.Message}");
                    break;
            }
        }
    }
}