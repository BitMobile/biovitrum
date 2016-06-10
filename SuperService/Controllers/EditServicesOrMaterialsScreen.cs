using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class EditServicesOrMaterialsScreen : Screen
    {
        private BehaviourEditServicesOrMaterialsScreen _behaviourEditServicesOrMaterialsScreen;
        private EditText _countEditText;
        private bool _editPrices;
        private int _minimum;
        private string _rimId;

        private decimal _price;
        private EditText _priceEditText;

        private bool _showPrices;
        private TextView _totalPriceTextView;
        private string _key;

        private decimal Price
        {
            get { return GetAndCheckPriceEditText(_priceEditText); }
            set
            {
                value = Math.Max(value, 0);
                _priceEditText.Text = value.ToString(CultureInfo.CurrentCulture);
            }
        }

        private int Count
        {
            get { return GetAndCheckCountEditText(_countEditText); }
            set
            {
                value = Math.Max(value, _minimum);
                _countEditText.Text = value.ToString();
                if (_totalPriceTextView != null)
                    _totalPriceTextView.Text = (Price*Count).ToString(CultureInfo.CurrentCulture);
            }
        }

        public override void OnLoading()
        {
            _key = (string)Variables.GetValueOrDefault("returnKey", "somNewValue");
            _minimum = (int) Variables.GetValueOrDefault("minimum", 0);
            _showPrices = (bool) Variables.GetValueOrDefault("priceVisible", true);
            _editPrices = (bool) Variables.GetValueOrDefault("priceEditable", false);
            _rimId = (string) Variables.GetValueOrDefault("rimId");

            _behaviourEditServicesOrMaterialsScreen =
                (BehaviourEditServicesOrMaterialsScreen)
                    Variables.GetValueOrDefault("behaviour", BehaviourEditServicesOrMaterialsScreen.None);


            BusinessProcess.GlobalVariables.Remove(_key);

            _countEditText = (EditText) GetControl("CountEditText", true);
            _priceEditText = (EditText) Variables["PriceEditText"];
            _totalPriceTextView = (TextView) GetControl("TotalPriceTextView", true);
        }

        public override void OnShow()
        {
            FindTextViewAndChangeVisibility("PriceTitleTextView", _showPrices);
            FindTextViewAndChangeVisibility("TotalPriceTitleTextView", _showPrices);
            FindTextViewAndChangeVisibility("TotalPriceTextView", _showPrices);

            FindEditTextAndChangeVisibilityAndEditable("PriceEditText", _showPrices, _editPrices);
            Price = _price;
        }

        private void FindTextViewAndChangeVisibility(string id, bool visibility)
        {
            ((TextView) Variables[id]).Visible = visibility;
        }

        private void FindEditTextAndChangeVisibilityAndEditable(string id, bool visibility, bool editable)
        {
            var et = (EditText) Variables[id];
            et.Visible = visibility;
            et.Enabled = editable;
        }

        internal void BackButton_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoBack();
        }

        internal void AddServiceMaterialButton_OnClick(object sender, EventArgs eventArgs)
        {
            switch (_behaviourEditServicesOrMaterialsScreen)
            {
                case BehaviourEditServicesOrMaterialsScreen.InsertIntoDB:
                    InsertIntoDb();
                    break;
                case BehaviourEditServicesOrMaterialsScreen.UpdateDB:
                    UpdateDb();
                    break;
                case BehaviourEditServicesOrMaterialsScreen.ReturnValue:
                    ReturnValue();
                    break;
            }
            BusinessProcess.DoBack();
        }

        private void ReturnValue()
        {
            var value = new EditServiceOrMaterialsScreenResult(Count, Price, Count*Price, _rimId);

            if (BusinessProcess.GlobalVariables.ContainsKey(_key))
                BusinessProcess.GlobalVariables.Remove(_key);
            BusinessProcess.GlobalVariables.Add(_key, value);
        }

        private void UpdateDb()
        {
            // TODO: Работа с базой данных
            throw new NotImplementedException();
        }

        private void InsertIntoDb()
        {
            // TODO: Работа с базой данных
            throw new NotImplementedException();
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

        private decimal GetAndCheckPriceEditText(EditText priceEditText)
        {
            // TODO: Разделитель целой и дробной части
            decimal res;
            if (decimal.TryParse(priceEditText.Text, out res))
            {
                res = Convert.ToDecimal(res);
                if (res > _minimum) return res;
                priceEditText.Text = _minimum.ToString();
                return _minimum;
            }
            DConsole.WriteLine($"Unparsed text = {priceEditText.Text}");
            res = new decimal(0, 0, 0, false, 0);
            priceEditText.Text = res.ToString(CultureInfo.CurrentCulture);
            return res;
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal IEnumerable GetServiceMaterialInfo()
        {
            // TODO: Доставать реальные данные
            return new Dictionary<string, object>
            {
                {"Count", 2},
                {"Price", 620},
                {"Description", "Прокладка кабеля"},
                {"FullPrice", 1240}
            };
        }
    }

    /// <summary>
    ///     Прокидывать через DoAction в экран EditServicesOrMaterialsScreen. Если указан ReturnValue, прокинуть строку с
    ///     ключем returnKey, в глобальные переменные под ключом, равным этой строке запишется результат типа
    ///     EditServiceOrMaterialsScreenResult. Ключ по-умолчанию somNewValue.
    /// </summary>
    public enum BehaviourEditServicesOrMaterialsScreen
    {
        None,
        UpdateDB,
        InsertIntoDB,
        ReturnValue
    }

    public class EditServiceOrMaterialsScreenResult
    {
        public EditServiceOrMaterialsScreenResult(int count, decimal price, decimal fullPrice, string rimId)
        {
            Count = count;
            Price = price;
            FullPrice = fullPrice;
            RimId = rimId;
        }

        public int Count { get; }
        public decimal Price { get; }
        public decimal FullPrice { get; }
        public string RimId { get; }
    }
}