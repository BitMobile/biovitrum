using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Entities.Catalog;
using BitMobile.DbEngine;

namespace Test
{
    internal class AddContactScreen : Screen
    {
        public override void OnLoading()
        {
            DConsole.WriteLine("Add Contackt Screen");
        }

        internal void Back_OnClick(object sender, EventArgs e)
        {
            //BusinessProcess.DoAction("Client");
            BusinessProcess.DoBack();
        }

        internal void AddContactButton_OnClick(object sender, EventArgs e)
        {
            DConsole.WriteLine("Trying to save");
           

            DConsole.WriteLine("Get controls");
            var name = ((EditText)GetControl("name"));

            DConsole.WriteLine("Get controls 2 name=" + name);

            DConsole.WriteLine("Get controls 2");
            var position = ((EditText)GetControl("position")).Text;
            var tel = ((EditText)GetControl("tel")).Text;
            var email = ((EditText)GetControl("email")).Text;

            DConsole.WriteLine("name=" + name + " pos=" + position);

            DConsole.WriteLine("create contact");
            var newContact = new Contacts()
            {
                Description = "" + name,
                Position = "" + position,
                EMail = "fgdgss",
                Tel = "fsfsfewfwe"
            };

            /*DConsole.WriteLine("Set propirties " + newContact + "   " + newContact.Id);
            newContact.Description = etName.Text.ToString();
            DConsole.WriteLine("Set propirties 1");
            newContact.Position = etPosition.Text;
            DConsole.WriteLine("Set propirties 2");
            newContact.EMail = etEmail.Text;
            DConsole.WriteLine("Set propirties 3");
            newContact.Tel = etTel.Text;
            DConsole.WriteLine("Set propirties 4");*/

            DConsole.WriteLine("save " + newContact);

            DBHelper.SaveEntity(newContact);
        }


        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
            //BusinessProcess.DoAction("BackToEvent");
            BusinessProcess.DoBack();
        }
    }
}