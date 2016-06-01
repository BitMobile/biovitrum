using System;
using System.Collections;
using System.Collections.Generic;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class CheckListScreen : Screen
    {
        private Image _imgToReplace;
        private string _pathToImg ;
        private string _newGuid;
        private TextView _textView;

        public override void OnLoading()
        {
            DConsole.WriteLine("CheckListScreen init");
        }

        internal void CheckListSnapshot_OnClick(object sender, EventArgs eventArgs)
        {
            _newGuid = Guid.NewGuid().ToString();
            _pathToImg = @"\private\" + _newGuid + @".jpg";

            _imgToReplace = (Image)((HorizontalLayout)sender).GetControl(0);
            //_img.Source =;




            //var zz = (Image)(_buf GetControl(0));

            DConsole.WriteLine("ДО СНЭПШОТА");

            Camera.MakeSnapshot(_pathToImg, int.MaxValue, CameraCallback, sender);

            DConsole.WriteLine("ПОСЛЕ СНЭПШОТА");

            //Gallery.Copy(temp);
            //Gallery.Copy(@"\private\order.jpg",
        }
        internal void CameraCallback(object state, ResultEventArgs<bool> args)
        {
            DConsole.WriteLine("КОЛЛБЭК");

            //Document.Order order = (Document.Order)state;
            //order.HasPhoto = args.Result;
            

            DConsole.WriteLine("_newGuid: " + _newGuid);
            DConsole.WriteLine("_pathToImg: " + _pathToImg);
            DConsole.WriteLine("File Exists: " + FileSystem.Exists(_pathToImg));

            _imgToReplace.Source = _pathToImg;
        }

        internal void CheckListValList_OnClick(object sender, EventArgs e)
        {
            _textView = (TextView)((HorizontalLayout)sender).GetControl(0);

            var items = new Dictionary<object, string>();
            var temp = DBHelper.GetActionValuesList(((HorizontalLayout)sender).Id);
            while (temp.Next())
            {
                items[temp["Id"].ToString()] = temp["Val"].ToString();
            }

            Dialog.Choose("Выберите вариант", items, ValListCallback);
        }
        internal void ValListCallback(object state, ResultEventArgs<KeyValuePair<object, string>> args)
        {
            _textView.Text = args.Result.Value;
        }

        internal void CheckListDateTime_OnClick(object sender, EventArgs e)
        {
            _textView = (TextView)((HorizontalLayout)sender).GetControl(0);
            Dialog.DateTime(@"Выберите дату", DateTime.Now, DateCallback);
        }
        internal void DateCallback(object state, ResultEventArgs<DateTime> args)
        {
            _textView.Text = args.Result.ToString();
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

        internal IEnumerable GetCheckList()
        {
            return DBHelper.GetCheckListByEventID((string)BusinessProcess.GlobalVariables["currentEventId"]);
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

        //internal void MemoEdit_OnChange(object sender, EventArgs e)
        //{
        //    ((HorizontalLayout) ((VerticalLayout) ((MemoEdit) sender).Parent).Parent).Refresh();
        //}
    }
}
