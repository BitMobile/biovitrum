using System;
using System.Collections;
using System.Collections.Generic;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class EditServicesOrMaterialsScreen : Screen
    {
        private EditText _countEditText;
        private TextView _totalPriceTextView;

        private decimal _price;
        private bool _showPrices;
        private bool _editPrices;
        private bool _isInsert;
        private int _minimum;

        private int Count
        {
            get { return GetAndCheckCountEditText(_countEditText); }
            set
            {
                value = Math.Max(value, _minimum);
                _countEditText.Text = value.ToString();
                if (_totalPriceTextView != null)
                    _totalPriceTextView.Text = _price*Count + Translator.Translate("currency");
            }
        }

        public override void OnLoading()
        {
//            DConsole.WriteLine(BusinessProcess.GlobalVariables["currentServicesMaterialsId"].ToString());


            _minimum = (int) Variables.GetValueOrDefault("minimum", 0);
            _showPrices = (bool) Variables.GetValueOrDefault("priceVisible", true);
            _editPrices = (bool) Variables.GetValueOrDefault("priceEditable", false);
            _isInsert = (bool) Variables.GetValueOrDefault("isInsert", false);

            DConsole.WriteLine($"Minimum = {_minimum}");

            _countEditText = (EditText) GetControl("CountEditText", true);
            _totalPriceTextView = (TextView) GetControl("TotalPriceTextView", true);
        }

        public override void OnShow()
        {
            FindTextViewAndChangeVisibility("PriceTitleTextView", _showPrices);
            FindTextViewAndChangeVisibility("PriceTextView", _showPrices);
            FindTextViewAndChangeVisibility("TotalPriceTitleTextView", _showPrices);
            FindTextViewAndChangeVisibility("TotalPriceTextView", _showPrices);
        }

        private void FindTextViewAndChangeVisibility(string id, bool visibility)
        {
            ((TextView) Variables[id]).Visible = visibility;
        }

        internal void Back_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoBack();
        }

        internal void RemoveButton_OnClick(object sender, EventArgs eventArgs)
        {
            Count--;
        }

        internal void AddButton_OnClick(object sender, EventArgs eventArgs)
        {
            Count++;
        }

        internal void CountEditText_OnLostFocus(object sender, EventArgs eventArgs)
        {
            GetAndCheckCountEditText((EditText) sender);
        }

        internal int SetPrice(decimal price)
        {
            _price = Convert.ToDecimal(price);
            return 0;
        }

        private int GetAndCheckCountEditText(EditText countEditText)
        {
            int res;
            if (int.TryParse(countEditText.Text, out res))
            {
                res = Convert.ToInt32(res);
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