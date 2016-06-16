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
        private ClientLocation _clientLocation;
        private ArrayList _eventListLocation;
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
            if (_isEventListScreen)
            {
                foreach (var item in _eventListLocation)
                {
                    var current = (ClientLocation) item;

                    if (current.NotEmpty)
                    {
                        _isNotEmptyLocationData = Convert.ToBoolean("True");
                        break;
                    }
                }
            }
            else
            {
                _isNotEmptyLocationData = _clientLocation.NotEmpty;
            }

            DConsole.WriteLine(nameof(_isNotEmptyLocationData) + " = " +
                               _isNotEmptyLocationData);
        }

        internal bool IsZeroArrayLenght()
        {
            return _eventListLocation.Count == 0;
        }

        private bool Init()
        {
            DConsole.WriteLine("Start " + nameof(Init));
            var screenState = Variables.GetValueOrDefault("screenState", MapScreenStates.Default);
            var locationData = Variables.GetValueOrDefault("locationData");
            
            switch ((MapScreenStates) screenState)
            {
                case MapScreenStates.EventListScren:
                    _isEventListScreen = Convert.ToBoolean("True");
                    if (locationData != null)
                        _eventListLocation = (ArrayList) locationData;
                    break;

                case MapScreenStates.ClientScreen:
                    _isClientScreen = Convert.ToBoolean("True");
                    if (locationData != null)
                        _clientLocation = (ClientLocation) locationData;
                    break;

                case MapScreenStates.EventScreen:
                    _isEventScreen = Convert.ToBoolean("True");
                    if (locationData != null)
                        _clientLocation = (ClientLocation) locationData;
                    break;

                default:
                    _clientLocation = null;
                    _eventListLocation = null;
                    _isDefault = Convert.ToBoolean("True");
                    break;
            }

            _isInit = Convert.ToBoolean("True");
            DConsole.WriteLine("End " + nameof(Init));
            return true;
        }

        internal void FillMap()
        {
            if (_isEventListScreen)
            {
                DConsole.WriteLine(nameof(FillMap));
                if (!IsZeroArrayLenght())
                {
                    foreach (var item in _eventListLocation)
                    {
                        var current = (ClientLocation) item;

                        if (current.NotEmpty || current.IsEmpty)
                        //if (current.NotEmpty)
                        {
                            _map.AddMarker(current.ClientDescription, current.Latitude, current.Longitude,
                                current.MarkerColor);
                        }
                    }
                }
            }
            else
            {
                {
                    _map.AddMarker(_clientLocation.ClientDescription, _clientLocation.Latitude,
                        _clientLocation.Longitude, _clientLocation.MarkerColor);
                }
            }
            DConsole.WriteLine("End " + nameof(FillMap));
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
        EventListScren,
        ClientScreen,
        EventScreen,
        Default
    }
}