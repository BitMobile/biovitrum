﻿using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using System;
using Test.Catalog;
using Test.Components;

namespace Test
{
    public class PhotoScreen : Screen
    {
        private TopInfoComponent _topInfoComponent;
        private Image _photo;

        public override void OnLoading()
        {
            _topInfoComponent = new TopInfoComponent(this)
            {
                Header = Translator.Translate("photo"),
                LeftButtonControl = new Image { Source = ResourceManager.GetImage("topheading_back") },
                RightButtonControl = new Image { Visible = false },
                ArrowVisible = false
            };
            _photo = (Image)Variables["Photo"];
        }

        internal void DeleteButton_OnClick(object sender, EventArgs args)
        {
            _photo.CssClass = "NoHeight";
            _photo.Refresh();
            var path = _photo.Source.StartsWith("~") ? _photo.Source.Substring(1) : _photo.Source;
            if (!FileSystem.Delete(path)) return;
            ChangePhotoInDB("");
            Navigation.Back();
        }

        internal void RetakeButton_OnClick(object sender, EventArgs args)
        {
            var guid = Guid.NewGuid();
            string path = $@"\private\{guid}.jpg";
            Camera.MakeSnapshot(path, Settings.PictureSize, (o, eventArgs) =>
            {
                if (!eventArgs.Result) return;
                _photo.Source = "~" + path;
                _photo.Refresh();
                ChangePhotoInDB(guid.ToString());
            });
        }

        private void ChangePhotoInDB(string newVal)
        {
            if (Variables.ContainsKey(nameof(ClientParametersScreen)))
            {
                var clientParameters =
                    (Client_Parameters)DBHelper.LoadEntity((string)Variables[nameof(ClientParametersScreen)]);
                clientParameters.Val = newVal;
                DBHelper.SaveEntity(clientParameters);
            }
            if (Variables.ContainsKey(nameof(CheckListScreen)))
            {
                var checkList =
                    (Document.Event_CheckList)DBHelper.LoadEntity((string)Variables[nameof(CheckListScreen)]);
                checkList.Result = newVal;
                DBHelper.SaveEntity(checkList);
            }
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs eventArgs)
        {
            Navigation.Back();
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs eventArgs)
        {
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs eventArgs)
        {
            _topInfoComponent.Arrow_OnClick(sender, eventArgs);
        }
    }
}