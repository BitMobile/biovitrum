using BitMobile.Application;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using System;
using Test.Components;

namespace Test
{
    public class SettingsScreen : Screen
    {
        private TabBarComponent _tabBarComponent;
        private TopInfoComponent _topInfoComponent;
        private string[] _userFirstAndLastName;
        private string _version;

        public override void OnLoading()
        {
            DConsole.WriteLine("SettingsScreen init");

            _topInfoComponent = new TopInfoComponent(this)
            {
                Header = Translator.Translate("settings"),
            };

            _tabBarComponent = new TabBarComponent(this);
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs e)
        {
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs e)
        {
        }

        internal void TabBarFirstTabButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabBarComponent.Events_OnClick(sender, eventArgs);
            DConsole.WriteLine("Settings Events");
        }

        internal void TabBarSecondTabButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabBarComponent.Bag_OnClick(sender, eventArgs);
            DConsole.WriteLine("Settings Bag");
        }

        internal void TabBarThirdButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabBarComponent.Clients_OnClick(sender, eventArgs);
            DConsole.WriteLine("Settings Clients");
        }

        internal void TabBarFourthButton_OnClick(object sender, EventArgs eventArgs)
        {
            //_tabBarComponent.Settings_OnClick(sender, eventArgs);
            DConsole.WriteLine("Settings Settings");
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal string GetVersion()
        {
            try
            {
                using (var stream = Application.GetResourceStream("settings.xml"))
                {
                    var xDocument = new XmlDocument();
                    xDocument.Load(stream);
                    if (xDocument.DocumentElement != null)
                        _version = xDocument.DocumentElement.ChildNodes[0].ChildNodes[0].Attributes?["version"].Value;
                }
            }
            catch (Exception)
            {
                DConsole.WriteLine($"File settings.xml Not Found");
            }

            return _version != null ? $"v. {_version}" : "v. 0.0.0.0";
        }

        internal string GetUserInitials()
        {
            var result = string.Empty;
            foreach (var str in _userFirstAndLastName)
            {
                result += $"{str.Substring(0, 1)}.";
            }
            return result;
        }

        internal string GetUserFirstAndLastName()
        {
            var result = string.Empty;

            foreach (var str in _userFirstAndLastName)
            {
                result += $"{str} ";
            }
            return result.Trim();
        }

        internal bool Init()
        {
            var settings = ApplicationContext.Current.Settings;
            var result = DBHelper.GetUserInfoByUserName(settings.UserName);
            var userDescription = string.Empty;

            if (result.Next())
            {
                userDescription = (string)result["Description"];

                var split = userDescription.Split(null);
                _userFirstAndLastName = new string[split.Length >= 2 ? 2 : split.Length];
                var i = 0;
                foreach (var str in split)
                {
                    if (string.IsNullOrWhiteSpace(str)) continue;
                    if (i < _userFirstAndLastName.Length)
                    {
                        _userFirstAndLastName[i] = str;
                        ++i;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                _userFirstAndLastName = new[] { string.Empty, string.Empty };
            }
#if DEBUG
            foreach (var str in _userFirstAndLastName)
            {
                DConsole.WriteLine(str);
            }
#endif

            return true;
        }

        internal void Logout_OnClick(object sender, EventArgs e)
        {
            Navigation.CleanStack();
            Navigation.ModalMove("AuthScreen");
        }

        internal void Twitter_OnClick(object sender, EventArgs e)
        {
            DConsole.WriteLine($"Чирик чирик ты в твиттере");
        }

        internal void Facebook_OnClick(object sender, EventArgs e)
        {
            DConsole.WriteLine($"Ты перешел на Лицокнигу");
        }

        internal void SendErrorReport_OnClick(object sender, EventArgs e)
        {
            DConsole.WriteLine($"Отчет об ошибке отправлен");
        }

        internal void SendLog_OnClick(object sender, EventArgs e)
        {
            DConsole.WriteLine($"Лог отправлен");
        }
    }
}