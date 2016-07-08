using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using System;
using System.Collections;
using System.Collections.Generic;
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
        private string _clientId;
        private decimal _clientLatitude;
        private decimal _clientLongitude;

        public override void OnLoading()
        {
            _map = (WebMapGoogle)GetControl("Map", true);
            _topInfoComponent = new TopInfoComponent(this)
            {
                Header = Translator.Translate("map"),
                LeftButtonControl = new Image() { Source = ResourceManager.GetImage("topheading_back") }
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
            Navigation.Back();
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs e)
        {
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs e)
        {
            _topInfoComponent.Arrow_OnClick(sender, e);
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
                        var longitude = (double)_data["Longitude"];

                        Dictionary<string, object> dictionary =
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

        internal bool Init()
        {
            if (_isInit) return _isInit;
            DConsole.WriteLine("Start " + nameof(Init));
            _location = new ArrayList();

            var screenState = BusinessProcess.GlobalVariables.GetValueOrDefault(Parameters.IdScreenStateId, MapScreenStates.Default);
            var locationData = BusinessProcess.GlobalVariables.GetValueOrDefault(Parameters.IdClientId);

            var state = (MapScreenStates)screenState;
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
                    {
                        _data = DBHelper.GetClientLocationByClientId((string)locationData);
                        _clientId = (string)locationData;
                    }
                    break;

                case MapScreenStates.EventScreen:
                    _isEventScreen = Convert.ToBoolean("True");
                    if (locationData != null)
                    {
                        _data = DBHelper.GetClientLocationByClientId((string)locationData);
                        _clientId = (string)locationData;
                    }
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
                var item = (Dictionary<string, object>)element;
                string description = (string)item["Description"];
                double latitude = (double)item["Latitude"];
                double longitude = (double)item["Longitude"];

                DConsole.WriteLine($"{nameof(description)}:{description} {Environment.NewLine} {nameof(latitude)}={latitude} {nameof(longitude)}={longitude}");

                _map.AddMarker(description, latitude, longitude, "red");
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

        internal void GetLocation_OnClick(object sender, EventArgs e)
        {
            if (GPS.CurrentLocation.NotEmpty)
            {
#if DEBUG
                DConsole.WriteLine($"{nameof(GPS.CurrentLocation.Latitude)}:{GPS.CurrentLocation.Latitude}" +
                                   $"{nameof(GPS.CurrentLocation.Longitude)}:{GPS.CurrentLocation.Longitude}");
#endif

                _clientLatitude = Convert.ToDecimal(GPS.CurrentLocation.Latitude);
                _clientLongitude = Convert.ToDecimal(GPS.CurrentLocation.Longitude);

#if DEBUG
                DConsole.WriteLine($"{nameof(_clientLatitude)}:{_clientLatitude} " +
                                   $"{Environment.NewLine}" +
                                   $"{nameof(_clientLongitude)}:{_clientLongitude}");
#endif

                var btn = (Button)sender;
                btn.Text = Translator.Translate("save_coordinates");
                btn.OnClick -= GetLocation_OnClick;
                btn.OnClick += SaveClientLocation_OnClick;
            }
            else
            {
                ((Button)sender).Text = Translator.Translate("failed_coordinates");
            }
        }

        internal void SaveClientLocation_OnClick(object sender, EventArgs e)
        {
            DBHelper.UpdateClientCoordinate(_clientId, _clientLatitude, _clientLongitude);
            var btn = (Button)sender;
            btn.Text = Translator.Translate("get_coordinates");
            btn.OnClick -= SaveClientLocation_OnClick;
            btn.OnClick += GetLocation_OnClick;
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