using System;
using System.Collections;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;

namespace Test
{
    public class MapScreen : Screen
    {
        private WebMapGoogle _map;
        private ArrayList _eventListLocation;
        private DbRecordset _data;
        private bool _isClientScreen = Convert.ToBoolean("False");
        private bool _isEventListScreen = Convert.ToBoolean("False");
        private bool _isEventScreen = Convert.ToBoolean("False");
        private bool _isInit = Convert.ToBoolean("False");
        private bool _isNotEmptyLocationData = Convert.ToBoolean("False");
        private TopInfoComponent _topInfoComponent;
        private bool _isDefault = Convert.ToBoolean("False");

        public override void OnLoading()
        {
            _map = (WebMapGoogle) GetControl("Map", true);
            _topInfoComponent = new TopInfoComponent(this)
            {
                ExtraLayoutVisible = false,
                HeadingTextView = {Text = Translator.Translate("map")},
                RightButtonImage = {Visible = false}
            };
            DConsole.WriteLine("MapScreen");
        }

        public override void OnShow()
        {
            var screenState = Variables.GetValueOrDefault("screenState", MapScreenStates.Default);

            var state = (MapScreenStates)screenState;
            DConsole.WriteLine(state.ToString());
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
            DConsole.WriteLine("Back to screen .....");
            BusinessProcess.DoBack();
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs e)
        {
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs e)
        {
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal bool GetIsEventListScreen()
        {
            return _isEventListScreen;
        }

        internal bool GetIsClientOrEventScreen()
        {
            return _isClientScreen || _isEventScreen;
        }


        private void isNotEmptyCoordinate()
        {
            DConsole.WriteLine(nameof(isNotEmptyCoordinate));
            if (_data != null)
            {
                DConsole.WriteLine($"{_data.IsClosed}");
                if (!_data.IsClosed)
                {
                    DConsole.WriteLine($"{_data.Unload().Count() != 0}");
                    DConsole.WriteLine($"{_data.IsClosed}");
                    if (_data.Unload().Count() != 0)
                    {
                        _isNotEmptyLocationData = Convert.ToBoolean("True");
                    }
                }
            }
            else
            {
                DConsole.WriteLine($"{_data} is null");
            }
            DConsole.WriteLine(nameof(_isNotEmptyLocationData) + " = " +
                               _isNotEmptyLocationData);
            //DConsole.WriteLine($"{_data}.Count = {_data.Unload().Count()}");
        }

        internal bool IsZeroArrayLenght()
        {
            return _eventListLocation.Count == 0;
        }

        private bool Init()
        {
            DConsole.WriteLine("Start " + nameof(Init));
            var screenState = BusinessProcess.GlobalVariables.GetValueOrDefault("screenState", MapScreenStates.Default);
            var locationData = BusinessProcess.GlobalVariables.GetValueOrDefault("locationData");

            var state = (MapScreenStates) screenState;
                DConsole.WriteLine(state.ToString());
            
            switch (state)
            {
                case MapScreenStates.EventListScreen:
                    _isEventListScreen = Convert.ToBoolean("True");
                    DConsole.WriteLine("MapScreenStates = EventListScreen");
                    _data = DBHelper.GetEventsLocationToday();
                    break;

                case MapScreenStates.ClientScreen:
                    _isClientScreen = Convert.ToBoolean("True");
                    if (locationData != null)
                        _data = DBHelper.GetClientLocationByClientId((string) locationData);
                    break;

                case MapScreenStates.EventScreen:
                    _isEventScreen = Convert.ToBoolean("True");
                    if (locationData != null)
                        _data = DBHelper.GetClientLocationByClientId((string)locationData);
                    break;

                default:
                    DConsole.WriteLine("is Default");
                    _data = null;
                    _eventListLocation = null;
                    _isDefault = Convert.ToBoolean("True");
                    break;
            }

            _isInit = Convert.ToBoolean("True");
            isNotEmptyCoordinate();
            DConsole.WriteLine("End " + nameof(Init));
            return true;
        }

        internal void FillMap()
        {
            DConsole.WriteLine("start FillMap");
            _map = (WebMapGoogle)GetControl("Map", true);

            DConsole.WriteLine($"{_data.IsClosed}");
            if (!_data.IsClosed)
            {
                DConsole.WriteLine($"{_data.IsClosed}");
                while (_data.Next())
                {
                    DConsole.WriteLine($"{_data.IsClosed}");
                    string description = (string) _data["Description"];
                    decimal latitude = (decimal) _data["Latitude"];
                    decimal longitude = (decimal) _data["Longitude"];
                    _map.AddMarker(description, Convert.ToDouble(latitude), Convert.ToDouble(longitude), "red");
                }
            }
            DConsole.WriteLine("end FillMap");
        }

        internal bool GetIsDefault()
        {
            return _isDefault;
        }

        internal bool GetIsNotEmptyLocationData()
        {
            return _isNotEmptyLocationData;
        }
    }

    public enum MapScreenStates
    {
        EventListScreen,
        ClientScreen,
        EventScreen,
        Default
    }
}