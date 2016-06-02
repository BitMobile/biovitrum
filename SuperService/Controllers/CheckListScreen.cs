using System;
using System.Collections;
using System.Collections.Generic;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;

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

        //Для опертивного обновления css'ки индикатора обязательности
        private VerticalLayout _vlRequired;
        // TODO: Не можем получить Parent (для смены индикатора обязательности сразу после ввода значения в ответ)

        public override void OnLoading()
        {
            DConsole.WriteLine("CheckListScreen init");
        }

        // Камера
        internal void CheckListSnapshot_OnClick(object sender, EventArgs eventArgs)
        {
            _currentCheckListItemID = ((HorizontalLayout) sender).Id;
            _newGuid = Guid.NewGuid().ToString();
            _pathToImg = @"\private\" + _newGuid + @".jpg";

            _imgToReplace = (Image) ((HorizontalLayout) sender).GetControl(0);
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
            DConsole.WriteLine("КОЛЛБЭК");

            //Document.Order order = (Document.Order)state;
            //order.HasPhoto = args.Result;


            DConsole.WriteLine("_newGuid: " + _newGuid);
            DConsole.WriteLine("_pathToImg: " + _pathToImg);
            DConsole.WriteLine("File Exists: " + FileSystem.Exists(_pathToImg));

            _imgToReplace.Source = _pathToImg;
        }

        // Список
        internal void CheckListValList_OnClick(object sender, EventArgs e)
        {
            _currentCheckListItemID = ((HorizontalLayout) sender).Id;
            _textView = (TextView) ((HorizontalLayout) sender).GetControl(0);

            var items = new Dictionary<object, string>();
            var temp = DBHelper.GetActionValuesList(((HorizontalLayout) sender).Id);
            while (temp.Next())
            {
                items[temp["Id"].ToString()] = temp["Val"].ToString();
            }

            Dialog.Choose("Выберите вариант", items, ValListCallback);
        }

        internal void ValListCallback(object state, ResultEventArgs<KeyValuePair<object, string>> args)
        {
            _textView.Text = args.Result.Value;
            DBHelper.UpdateCheckListItem(_currentCheckListItemID, _textView.Text);
        }

        // Дата
        internal void CheckListDateTime_OnClick(object sender, EventArgs e)
        {
            _currentCheckListItemID = ((HorizontalLayout) sender).Id;
            _textView = (TextView) ((HorizontalLayout) sender).GetControl(0);
            Dialog.DateTime(@"Выберите дату", DateTime.Now, DateCallback);
        }

        internal void DateCallback(object state, ResultEventArgs<DateTime> args)
        {
            _textView.Text = args.Result.ToString();
            DBHelper.UpdateCheckListItem(_currentCheckListItemID, _textView.Text);
        }

        // Булево
        internal void CheckListBoolean_OnClick(object sender, EventArgs e)
        {
            _currentCheckListItemID = ((HorizontalLayout) sender).Id;
            _checkBox = (CheckBox) ((HorizontalLayout) sender).GetControl(0);
            DConsole.WriteLine(_checkBox.Checked.ToString());

            DBHelper.UpdateCheckListItem(_currentCheckListItemID, _checkBox.Checked ? "Нет" : "Да");
        }

        // С точкой
        internal void CheckListDecimal_OnChange(object sender, EventArgs e)
        {
            _editText = (EditText) sender;
            _currentCheckListItemID = ((EditText) sender).Id;

            DBHelper.UpdateCheckListItem(_currentCheckListItemID, _editText.Text);
        }

        //Целое
        internal void CheckListInteger_OnChange(object sender, EventArgs e)
        {
            _editText = (EditText) sender;
            _currentCheckListItemID = ((EditText) sender).Id;

            DBHelper.UpdateCheckListItem(_currentCheckListItemID, _editText.Text);
        }

        // Строка
        internal void CheckListString_OnChange(object sender, EventArgs e)
        {
            _editText = (EditText) sender;
            _currentCheckListItemID = ((EditText) sender).Id;

            DBHelper.UpdateCheckListItem(_currentCheckListItemID, _editText.Text);
        }


        internal IEnumerable GetCheckList()
        {
            return DBHelper.GetCheckListByEventID((string) BusinessProcess.GlobalVariables["currentEventId"]);
        }

        internal void BackButton_OnClick(object sender, EventArgs eventArgs)
        {
            BusinessProcess.DoAction("BackToEvent");
        }

        internal bool IsNotEmptyString(string item)
        {
            return !(string.IsNullOrEmpty(item) && string.IsNullOrWhiteSpace(item));
        }
    }
}