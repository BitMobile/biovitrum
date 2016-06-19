using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;

namespace Test
{
    public class MapScreen : Screen
    {
        private WebMapGoogle _map;
        private ArrayList _location;
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
            FillMap();
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


        private void IsNotEmptyCoordinate()
        {
            DConsole.WriteLine(nameof(IsNotEmptyCoordinate));
            if (_data != null)
            {
                DConsole.WriteLine($"{_data.IsClosed}");
                if (!_data.IsClosed)
                {
                    while (_data.Next())
                    {
                        var latitude = (double)_data["Latitude"];
                        var longitude = (double) _data["Longitude"];

                        Dictionary<string,object> dictionary =
                            new Dictionary<string, object>()
                            {
                                {"Description",_data["Description"] },
                                {"Latitude", latitude},
                                {"Longitude",longitude }
                            };
                        _location.Add(dictionary);
                    }

                    _isNotEmptyLocationData = Convert.ToBoolean(_location.Count > 0 ? "True" : "False");
                }
                else
                {
                    DConsole.WriteLine($"Error in {nameof(IsNotEmptyCoordinate)}");
                }
            }
            else
            {
                DConsole.WriteLine($"{_data} is null");
            }
            DConsole.WriteLine(nameof(_isNotEmptyLocationData) + " = " +
                               _isNotEmptyLocationData);
        }

        internal bool IsZeroArrayLenght()
        {
            return _location.Count == 0;
        }

        private bool Init()
        {
            DConsole.WriteLine("Start " + nameof(Init));
            _location = new ArrayList();

            var screenState = BusinessProcess.GlobalVariables.GetValueOrDefault("screenState", MapScreenStates.Default);
            var locationData = BusinessProcess.GlobalVariables.GetValueOrDefault("clientId");

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
                    _location = null;
                    _isDefault = Convert.ToBoolean("True");
                    break;
            }

            _isInit = Convert.ToBoolean("True");
            IsNotEmptyCoordinate();
            DConsole.WriteLine("End " + nameof(Init));
            return true;
        }

        private void FillMap()
        {
            DConsole.WriteLine("start FillMap");
            DConsole.WriteLine($"{nameof(_location)}.{nameof(_location.Count)} = {_location.Count}");
            foreach (var element in _location)
            {
                var item = (Dictionary<string,object>) element;
                string description = (string) item["Description"];
                double latitude = (double) item["Latitude"];
                double longitude = (double) item["Longitude"];

                DConsole.WriteLine($"{nameof(description)}:{description} {Environment.NewLine} {nameof(latitude)}={latitude} {nameof(longitude)}={longitude}");


                _map.AddMarker(description,latitude,longitude,"red");
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