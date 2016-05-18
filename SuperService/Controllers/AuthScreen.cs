using System;
using System.Net;
using System.Text;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class AuthScreen : Screen
    {
        //private ScrollView _scrollView;
        public override void OnLoading()
        {
            DConsole.WriteLine("init onloading");
            Initialize();
        }

        void Initialize()
        {
            DConsole.WriteLine("Init begin");

            var vl = new VerticalLayout();
            
            //Button btn = new Button();
            //btn.CssClass = "Auth";

            //vl.AddChild(btn);
            ////vl.AddChild(new Button("Back", Back_OnClick));
            //vl.AddChild(new Button("Exit", ExitButton_OnClick));

            AddChild(vl);
        }

        //private void ExitButton_OnClick(object sender, EventArgs e)
        //{
        //    Application.Terminate();
        //}

        //void Back_OnClick(object sender, EventArgs e)
        //{
        //    BusinessProcess.DoBack();
        //}

        //private void mtest_OnClick(object sender, EventArgs e)
        //{
        //    BusinessProcess.DoAction("Auth");
        //}

    }
}