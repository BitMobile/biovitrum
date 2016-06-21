using System;
using System.Collections;
using System.Collections.Generic;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;

namespace Test
{
    public class CheckListScreen : Screen
    {
        // Для булева
        private CheckBox _checkBox;

        // Для обновления
        private string _currentCheckListItemID;

        // Для целого и строки
        private EditText _editText;

        // Для камеры
        private Image _imgToReplace;
        private string _newGuid;
        private string _pathToImg;

        // Для списка и даты
        private TextView _textView;
        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            DConsole.WriteLine("CheckListScreen init");
            _topInfoComponent = new TopInfoComponent(this)
            {
                ExtraLayoutVisible = false,
                HeadingTextView = {Text = Translator.Translate("clist")},
                LeftButtonImage = {Source = ResourceManager.GetImage("topheading_back")},
                RightButtonImage = {Visible = false}
            };
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
            Navigation.Back(true);
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs e)
        {
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs e)
        {
        }

        // Камера
        internal void CheckListSnapshot_OnClick(object sender, EventArgs eventArgs)
        {
            _currentCheckListItemID = ((VerticalLayout) sender).Id;
            _newGuid = Guid.NewGuid().ToString();
            _pathToImg = @"\private\" + _newGuid + @".jpg";

            _imgToReplace = (Image) ((VerticalLayout) sender).GetControl(0);
            //_img.Source =;


            //var zz = (Image)(_buf GetControl(0));

            DConsole.WriteLine("ДО СНЭПШОТА");

            Camera.MakeSnapshot(_pathToImg, int.MaxValue, CameraCallback, sender);

            DConsole.WriteLine("ПОСЛЕ СНЭПШОТА");

            // TODO: понять как получить изображение с памяти устройства
            //Gallery.Copy(temp);
            //Gallery.Copy(@"\private\order.jpg",
        }

        internal void CameraCallback(object state, ResultEventArgs<bool> args)
        {
            DConsole.WriteLine("КОЛЛБЭК1");

            //Document.Order order = (Document.Order)state;
            //order.HasPhoto = args.Result;


            DConsole.WriteLine("_newGuid: " + _newGuid);
            DConsole.WriteLine("_pathToImg: " + _pathToImg);
            DConsole.WriteLine("File Exists: " + FileSystem.Exists(_pathToImg));

            _imgToReplace.Source = _pathToImg;
            DConsole.WriteLine("КОЛЛБЭК2");
            //_imgToReplace.Refresh();
            DConsole.WriteLine("КОЛЛБЭК3");
        }

        // Список
        internal void CheckListValList_OnClick(object sender, EventArgs e)
        {
            _currentCheckListItemID = ((VerticalLayout) sender).Id;
            _textView = (TextView) ((VerticalLayout) sender).GetControl(0);

            var items = new Dictionary<object, string>();
            var temp = DBHelper.GetActionValuesList(_textView.Id);
            while (temp.Next())
            {
                items[temp["Id"].ToString()] = temp["Val"].ToString();
            }
            Dialog.Choose(Translator.Translate("select_answer"), items, ValListCallback);
        }

        internal void ValListCallback(object state, ResultEventArgs<KeyValuePair<object, string>> args)
        {
            _textView.Text = args.Result.Value;
            DBHelper.UpdateCheckListItem(_currentCheckListItemID, _textView.Text);
        }

        // TODO: при загрузке данных из БД (в xml) приводить DateTime в просто Date
        // Дата
        internal void CheckListDateTime_OnClick(object sender, EventArgs e)
        {
            _currentCheckListItemID = ((VerticalLayout) sender).Id;
            _textView = (TextView) ((VerticalLayout) sender).GetControl(0);

            Dialog.DateTime(@"Выберите дату", DateTime.Now, DateCallback);
        }

        internal string ToDate(string datetime)
        {
            DateTime temp;
            if (DateTime.TryParse(datetime, out temp))
            {
                return temp.ToString("dd MMMM yyyy");
            }
            return Translator.Translate("not_specified");
        }

        internal void DateCallback(object state, ResultEventArgs<DateTime> args)
        {
            _textView.Text = args.Result.Date.Date.ToString("dd MMMM yyyy");
            DBHelper.UpdateCheckListItem(_currentCheckListItemID, _textView.Text);
        }

        // Булево
        internal void CheckListBoolean_OnClick(object sender, EventArgs e)
        {
            _currentCheckListItemID = ((VerticalLayout) sender).Id;
            _checkBox = (CheckBox) ((VerticalLayout) sender).GetControl(0);

            DBHelper.UpdateCheckListItem(_currentCheckListItemID, _checkBox.Checked ? "Нет" : "Да");
        }

        // С точкой
        internal void CheckListDecimal_OnLostFocus(object sender, EventArgs e)
        {
            _editText = (EditText) sender;
            _currentCheckListItemID = ((EditText) sender).Id;

            DBHelper.UpdateCheckListItem(_currentCheckListItemID, _editText.Text);
        }

        //Целое
        internal void CheckListInteger_OnLostFocus(object sender, EventArgs e)
        {
            _editText = (EditText) sender;
            _currentCheckListItemID = ((EditText) sender).Id;

            //var vl1 = (IHorizontalLayout3)_editText.Parent;
            //var hl = (IVerticalLayout3)vl1.Parent;
            //var vl2 = (IHorizontalLayout3)hl.Parent;
            //var vltarget = (IVerticalLayout3)vl2.Controls[0];

            //if (_editText.Text.Length > 0)
            //{
            //    vltarget.CssClass = "VLRequiredDone";
            //    vltarget.Refresh();
            //}
            //else
            //{
            //    vltarget.CssClass = "VLRequired";
            //    vltarget.Refresh();
            //}

            DBHelper.UpdateCheckListItem(_currentCheckListItemID, _editText.Text);
        }

        // Строка
        internal void CheckListString_OnLostFocus(object sender, EventArgs e)
        {
            _editText = (EditText) sender;
            _currentCheckListItemID = ((EditText) sender).Id;

            //var vl = (IVerticalLayout3)_editText.Parent;
            //var hl = (IHorizontalLayout3)vl.Parent;
            //var vltarget = (IVerticalLayout3) hl.Controls[0];

            //DConsole.WriteLine("CSS " + vltarget.CssClass.ToString());


            //vltarget.CssClass = "VLRequiredDone";
            //DConsole.WriteLine("1");
            //vltarget.Refresh();

            //DConsole.WriteLine("CSS " + vltarget.CssClass.ToString());

            //if (vltarget.CssClass == "VLRequiredDone" || vltarget.CssClass == "VLRequired")
            //{
            //    DConsole.WriteLine("1");
            //    if (_editText.Text.Length > 0)
            //    {
            //        DConsole.WriteLine("2");
            //        vltarget.CssClass = "VLRequiredDone";
            //        vltarget.Refresh();
            //    }
            //    else
            //    {
            //        DConsole.WriteLine("3");
            //        vltarget.CssClass = "VLRequired";
            //        vltarget.Refresh();
            //    }
            //}
            // TODO: Непонятное поведение Refresh(), из-за чего не можем оперативно сменить индикатор важности. Работает на android 4, не работает на android 6
            DBHelper.UpdateCheckListItem(_currentCheckListItemID, _editText.Text);
        }

        internal bool Checktest(int item)
        {
            DConsole.WriteLine(item.ToString());
            if (item == 1)
            {
                return true;
            }
            return false;
        }

        internal IEnumerable GetCheckList()
        {
            return DBHelper.GetCheckListByEventID((string) BusinessProcess.GlobalVariables["currentEventId"]);
        }

        internal void BackButton_OnClick(object sender, EventArgs eventArgs)
        {
            Navigation.Back(true);
        }

        internal bool IsNotEmptyString(string item)
        {
            DConsole.WriteLine(item);
            return !(string.IsNullOrEmpty(item) && string.IsNullOrWhiteSpace(item));
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }
    }
}