using System.Collections.Generic;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public static class Navigation
    {
        private const string DefaultStyle = @"Style\style.css";
        private static readonly Stack ScreenInfoStack = new Stack();
        private static readonly Stack ScreenStack = new Stack();


        public static ScreenInfo CurrentScreenInfo { get; private set; }
        public static Screen CurrentScreen { get; private set; }


        public static void Back(bool reload = false)
        {
            if (ScreenInfoStack.Count == 0)
            {
                DConsole.WriteLine("Can't go back when stack is empty");
                return;
            }
            var screenInfo = (ScreenInfo) ScreenInfoStack.Pop();
            var nextScreen = (Screen) ScreenStack.Pop();
            if (!reload)
                ModalMove(screenInfo, screen: nextScreen);
            else
                ModalMove(screenInfo);
        }

        public static void MoveTo(string name, Dictionary<string, object> args = null, string css = null)
        {
            var screenInfo = CreateScreenInfoFromName(name, css);
            MoveTo(screenInfo, args);
        }

        private static ScreenInfo CreateScreenInfoFromName(string name, string css)
        {
            return new ScreenInfo
            {
                Name = name,
                Xml = $@"Screen\{name}.xml",
                Css = css ?? $@"Style\{name}.css"
            };
        }

        public static void MoveTo(ScreenInfo screenInfo, Dictionary<string, object> args = null)
        {
            if (CurrentScreenInfo != null)
            {
                ScreenInfoStack.Push(CurrentScreenInfo);
                ScreenStack.Push(CurrentScreen);
            }
            ModalMove(screenInfo, args);
        }

        public static void ModalMove(string name, Dictionary<string, object> args = null, string css = null)
        {
            var screenInfo = CreateScreenInfoFromName(name, css);
            ModalMove(screenInfo, args);
        }

        public static void ModalMove(ScreenInfo screenInfo, Dictionary<string, object> args = null, Screen screen = null)
        {
            screen = screen ?? (Screen) Application.CreateInstance($"Test.{screenInfo.Name}");
            screen.SetData(args);
            try
            {
                screen.LoadFromStream(Application.GetResourceStream(screenInfo.Xml));
            }
            catch
            {
                DConsole.WriteLine($"Can't find xml file for {screenInfo.Name}");
            }
            try
            {
                screen.LoadStyleSheet(Application.GetResourceStream(screenInfo.Css));
            }
            catch
            {
                screen.LoadStyleSheet(Application.GetResourceStream(DefaultStyle));
            }

            screen.Show();

            CurrentScreen = screen;
            CurrentScreenInfo = screenInfo;
        }

        public static void CleanStack()
        {
            ScreenInfoStack.Clear();
            ScreenStack.Clear();
        }
    }

    public class ScreenInfo
    {
        public string Name { get; set; }
        public string Xml { get; set; }
        public string Css { get; set; }
    }
}