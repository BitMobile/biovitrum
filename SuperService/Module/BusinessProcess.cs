using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using XmlDocument = BitMobile.ClientModel3.XmlDocument;

namespace Test
{
    public static class BusinessProcess
    {
        private static XmlDocument _doc;

        private static readonly Stack StackNodes = new Stack();
        private static readonly Stack StackScreens = new Stack();
        private static Screen lastScreen;

        public static XmlNode CurrentNode => (XmlNode) StackNodes.Peek();
        public static Screen CurrentScreen => (Screen) StackScreens.Peek();

        public static Dictionary<string, object> GlobalVariables { get; } = new Dictionary<string, object>();

        public static void Init()
        {
            DConsole.WriteLine("Started BP.Init");
            _doc = new XmlDocument();
            _doc.Load(Application.GetResourceStream("BusinessProcess.BusinessProcess.xml"));
            DConsole.WriteLine("Loaded BP.xml");

            var firstStepName = _doc.DocumentElement?.ChildNodes[0].ChildNodes[0].Attributes?["Name"].Value;
            MoveTo(firstStepName);
            //MoveTo("EditServicesOrMaterials");
        }

        private static void MoveTo(string stepName, IDictionary<string, object> args = null, bool putOnStack = true)
        {
            DConsole.WriteLine($"Moving to {stepName}");
            var n = _doc.DocumentElement?.SelectSingleNode($"//BusinessProcess/Workflow/Step[@Name='{stepName}']");
            if (n == null)
            {
                DConsole.WriteLine($"Step {stepName} is not found in BusinessProcess.xml");
                return;
            }
            if (n.Attributes == null)
            {
                DConsole.WriteLine($"Step {stepName}.Attrubutes is not found in BusinessProcess.xml");
                return;
            }
            var stepController = n.Attributes["Controller"].Value;
            var styleSheet = n.Attributes["StyleSheet"].Value;

            DConsole.WriteLine($"Loading controler: {stepController}");
            var scr = GetScreenByControllerName(stepController);
            scr.SetData(args);
            if (putOnStack)
            {
                StackScreens.Push(scr);
                StackNodes.Push(n);
            }

//            CurrentScreen = scr;
//            CurrentNode = n;

            scr.LoadStyleSheet(Application.GetResourceStream(styleSheet));
            scr.Show();
        }

        public static void DoAction(string actionName, IDictionary<string, object> args = null, bool putOnStack = true)
        {
            DConsole.WriteLine($"Doing action: {actionName}");
            var n = CurrentNode.SelectSingleNode($"Action[@Name='{actionName}']");
            if (n?.Attributes == null)
            {
                DConsole.WriteLine($"Can't find {actionName} or {actionName}.Attributes");
                return;
            }
            var stepName = n.Attributes["NextStep"].Value;
            MoveTo(stepName, args, putOnStack);
        }

        public static void DoBack()
        {
            DConsole.WriteLine("Moving back");
            //remove current 
            if (StackScreens.Count <= 1)
            {
                DConsole.WriteLine("I'm only screen on stack, can't go back");
                return;
            }

            StackNodes.Pop();
            lastScreen = (Screen) StackScreens.Pop();
            var scr = (Screen) StackScreens.Peek();
            scr.Show();
        }

        private static Screen GetScreenByControllerName(string name)
        {
            var scr = (Screen) Application.CreateInstance("Test." + name);
            //full type name should be specified   

            Stream s = null;
            string path = $@"Screen\{name}.xml";
            try
            {
                s = Application.GetResourceStream(path); //try to find markup resource
            }
            catch
            {
                DConsole.WriteLine($"Resource {path} has not been found");
            }

            if (s != null)
                scr.LoadFromStream(s);

            return scr;
        }
    }
}