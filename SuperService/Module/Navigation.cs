using System.Collections.Generic;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public static class Navigation
    {
        private static readonly Stack ScreenStack = new Stack();

        public static ScreenInfo CurrentScreenInfo { get; private set; }

        public static void Back(bool reload = false)
        {
            if (ScreenStack.Count == 0)
            {
                DConsole.WriteLine("Can't go back when stack is empty");
                return;
            }
            var screenInfo = (ScreenInfo) ScreenStack.Pop();
            if (reload)
                screenInfo.Screen = (Screen) Application.CreateInstance($"Test.{screenInfo.Name}");
            ModalMove(screenInfo);
        }

        public static void MoveTo(string name, string css = null, Dictionary<string, object> args = null)
        {
            var screenInfo = CreateScreenInfoFromName(name, css);
            MoveTo(screenInfo, args);
        }

        private static ScreenInfo CreateScreenInfoFromName(string name, string css)
        {
            return new ScreenInfo
            {
                Name = name,
                Screen = (Screen) Application.CreateInstance($"Test.{name}"),
                Xml = $@"Screen\{name}.xml",
                Css = css ?? $@"Style\{name}.css"
            };
        }

        public static void MoveTo(ScreenInfo screenInfo, Dictionary<string, object> args = null)
        {
            if (CurrentScreenInfo != null)
                ScreenStack.Push(CurrentScreenInfo);
            ModalMove(screenInfo, args);
        }

        public static void ModalMove(string name, string css = null, Dictionary<string, object> args = null)
        {
            var screenInfo = CreateScreenInfoFromName(name, css);
            ModalMove(screenInfo, args);
        }

        public static void ModalMove(ScreenInfo screenInfo, Dictionary<string, object> args = null)
        {
            var screen = screenInfo.Screen;
            screen.SetData(args);
            screen.LoadFromStream(Application.GetResourceStream(screenInfo.Xml));
            screen.LoadStyleSheet(Application.GetResourceStream(screenInfo.Css));
            screen.Show();
            CurrentScreenInfo = screenInfo;
        }
    }

    public class ScreenInfo
    {
        public string Name { get; set; }
        public Screen Screen { get; set; }
        public string Xml { get; set; }
        public string Css { get; set; }
    }
}