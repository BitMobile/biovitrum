using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class CheckListScreen : Screen
    {
        private HorizontalLayout _buf;

        private Image _previewImage;
        private Image _img;
        private string _temp;

        //private EditText _passwordEditText;

        public override void OnLoading()
        {
            DConsole.WriteLine("AuthScreen init");

            _previewImage = (Image)GetControl("loginEditText", true);
        }


        internal void BackButton_OnClick(object sender, EventArgs eventArgs)
        {
            BusinessProcess.DoAction("BackToEvent");
        }

        //internal void CheckListLayout_OnClick(object sender, EventArgs eventArgs)
        //{
        //    //BusinessProcess.GlobalVariables["currentTaskId"] = ((HorizontalLayout)sender).Id;
        //    //BusinessProcess.DoAction("ViewTask");
        //}


        void OrderCamera(object state, ResultEventArgs<bool> args)
        {
            //Document.Order order = (Document.Order)state;
            //order.HasPhoto = args.Result;
            _img.Source = _temp;
        }


        internal void CheckListSnapshot_OnClick(object sender, EventArgs eventArgs)
        {
            _temp = Guid.NewGuid().ToString();
            _temp = @"\private\" + _temp + @".jpg";

            _buf = (HorizontalLayout) sender;
            _img = (Image) _buf.GetControl(0);
            DConsole.WriteLine(_img.Id + " _img.Id");
            DConsole.WriteLine(_temp + " _temp");

            //var zz = (Image)(_buf GetControl(0));

            //try
            //{
            Camera.MakeSnapshot(_temp, OrderCamera);
            //}
            //catch (Exception cam)
            //{
            //    DConsole.WriteLine(cam.Message);
            //    DConsole.WriteLine(@"ASD");

            //}
            //Gallery.Copy(temp);
            //Gallery.Copy(@"\private\order.jpg",
        }

        internal void CheckListValList_OnClick(object sender, EventArgs e)
        {
            var items = new Dictionary<object, string>();

            var temp = DBHelper.GetActionValuesList(((HorizontalLayout)sender).Id);

            while (temp.Next())
            {
                items[temp["Id"].ToString()] = temp["Val"].ToString();
            }

            Dialog.Choose("Select a channel", items, Callback);
        }

        internal void CheckListDateTime_OnClick(object sender, EventArgs e)
        {
            Dialog.DateTime(@"Выберите дату", DateTime.Now, null);
        }

        internal void CheckListBoolean_OnClick(object sender, EventArgs e)
        {

        }

        internal void CheckListDecimal_OnClick(object sender, EventArgs e)
        {

        }

        internal void CheckListInteger_OnClick(object sender, EventArgs e)
        {

        }

        internal void CheckListString_OnClick(object sender, EventArgs e)
        {

        }


        internal void Callback(object state, ResultEventArgs<KeyValuePair<object, string>> args)
        {
            //((Document.Order)state).Channel = args.Result.Key;
        }

        internal IEnumerable GetCheckList()
        {
            return DBHelper.GetCheckListByEventID((string)BusinessProcess.GlobalVariables["currentEventId"]);
        }
    }
}
