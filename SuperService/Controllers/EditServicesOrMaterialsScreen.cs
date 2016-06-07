using System;
using System.Collections;
using System.Collections.Generic;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class EditServicesOrMaterialsScreen : Screen
    {
        private int _minimum;
        private EditText _countEditText;

        public override void OnLoading()
        {
            DConsole.WriteLine("Edit Services Or Materials Screen");
//            DConsole.WriteLine(BusinessProcess.GlobalVariables["currentServicesMaterialsId"].ToString());4

            _minimum = (int) Variables["minimum"];
            DConsole.WriteLine($"Minimum = {_minimum}");
            _countEditText = (EditText) GetControl("CountEditText", true);
        }

        internal void Back_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoBack();
        }

        internal void RemoveButton_OnClick(object sender, EventArgs eventArgs)
        {
            long i = GetAndCheckCountEditText(_countEditText);
            DConsole.WriteLine($"Count before dec = {i}");
            i = i - 1;
            DConsole.WriteLine($"Count = {i}");
            if (i < _minimum) i = _minimum;
            DConsole.WriteLine($"Count = {i}");
            _countEditText.Text = i.ToString();
        }

        internal void CountEditText_OnLostFocus(object sender, EventArgs eventArgs)
        {
            DConsole.WriteLine("test");
            GetAndCheckCountEditText((EditText) sender);
        }

        private long GetAndCheckCountEditText(EditText countEditText)
        {
            long res;
            if (long.TryParse(countEditText.Text, out res))
            {
                res = Convert.ToInt64(res);
                if (res > _minimum) return res;
                countEditText.Text = _minimum.ToString();
                return _minimum;
            }
            DConsole.WriteLine($"Unparsed text = {countEditText.Text}");
            countEditText.Text = _minimum.ToString();
            return _minimum;
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal IEnumerable GetServiceMaterialInfo()
        {
            return new Dictionary<string, object>
            {
                {"Count", 2},
                {"Price", 620},
                {"Description", "Прокладка кабеля"},
                {"FullPrice", 1240}
            };
        }
    }
}