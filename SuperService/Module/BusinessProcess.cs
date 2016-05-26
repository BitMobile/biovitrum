using System.Collections.Generic;
using System.IO;
using System.Xml;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using XmlDocument = BitMobile.ClientModel3.XmlDocument;

// ReSharper disable PossibleNullReferenceException

namespace Test
{
    public static class BusinessProcess
    {
        private static XmlDocument _doc;

        //private static readonly Stack StackNodes = new Stack();
        //private static readonly Stack StackScreens = new Stack();

        public static XmlNode CurrentNode { get; private set; }
        public static Screen CurrentScreen { get; private set; }

        public static Dictionary<string, object> GlobalVariables { get; } = new Dictionary<string, object>();

        public static void Init()
        {
            DConsole.WriteLine("Started BP.Init");
            _doc = new XmlDocument();
            _doc.Load(Application.GetResourceStream("BusinessProcess.BusinessProcess.xml"));
            DConsole.WriteLine("Loaded BP.xml");

#if DEBUG
            GlobalVariables["currentEventId"] = "@ref[Document_Event]:6422e731-149a-11e6-80e3-005056011152";
#endif

            var firstStepName = _doc.DocumentElement.ChildNodes[0].ChildNodes[0].Attributes["Name"].Value;
            MoveTo(firstStepName);
            //MoveTo("EventList");
        }

        private static void MoveTo(string stepName)
        {
            DConsole.WriteLine($"Moving to {stepName}");
            var n = _doc.DocumentElement.SelectSingleNode($"//BusinessProcess/Workflow/Step[@Name='{stepName}']");
            var stepController = n.Attributes["Controller"].Value;
            var styleSheet = n.Attributes["StyleSheet"].Value;

            DConsole.WriteLine($"Loading controler: ${stepController}");
            var scr = GetScreenByControllerName(stepController);

            //StackScreens.Push(scr);
            //StackNodes.Push(n);

            CurrentScreen = scr;
            CurrentNode = n;

            scr.LoadStyleSheet(Application.GetResourceStream(styleSheet));
            scr.Show();
        }

        public static void DoAction(string actionName)
        {
            DConsole.WriteLine($"Doing action: {actionName}");
            //var currentNode = StackNodes.peek();
            var n = CurrentNode.SelectSingleNode($"Action[@Name='{actionName}']");
            var stepName = n.Attributes["NextStep"].Value;
            MoveTo(stepName);
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