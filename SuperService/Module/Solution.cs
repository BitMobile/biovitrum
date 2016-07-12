using BitMobile.ClientModel3;
using BitMobile.DbEngine;
using System;
using Test.Catalog;

namespace Test
{
    public class Solution : Application
    {
        public override void OnCreate()
        {
            DConsole.WriteLine("DB init...");
            DBHelper.Init();
            DConsole.WriteLine("Loading first screen");
            Navigation.Move("AuthScreen");
        }
    }
}