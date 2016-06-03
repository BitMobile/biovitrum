using System.Collections.Generic;
using BitMobile.ClientModel3;

namespace Test
{
    public static class ResourceManager
    {
        private const string ImageNotFound = @"Image\not_found.png";
        private const string ComponentNotFound = @"Components\NotFoundComponent.xml";

        private static readonly Dictionary<string, object> ImagesPaths = new Dictionary<string, object>
        {
            {"notfound", ImageNotFound},
            //
            {"tabbar_bag", @"Image\_Components\TabBar\Bag.png"},
            {"tabbar_bag_active", @"Image\_Components\TabBar\BagActive.png"},
            {"tabbar_clients", @"Image\_Components\TabBar\Clients.png"},
            {"tabbar_clients_active", @"Image\_Components\TabBar\ClientsActive.png"},
            {"tabbar_events", @"Image\_Components\TabBar\Events.png"},
            {"tabbar_events_active", @"Image\_Components\TabBar\EventsActive.png"},
            {"tabbar_settings", @"Image\_Components\TabBar\Settings.png"},
            {"tabbar_settings_active", @"Image\_Components\TabBar\SettingsActive.png"},
            //
            {"topinfo_downarrow", @"Image\_Components\TopInfoComponent\down_arrow_full_width.png"},
            {"topinfo_uparrow", @"Image\_Components\TopInfoComponent\up_arrow_full_width.png"},
            {"topinfo_extra_map", @"Image\top_map.png"},
            {"topinfo_extra_person", @"Image\top_person.png"},
            //
            {"topheading_back", @"Image\top_back.png"},
            {"topheading_filter", @"Image\top_eventlist_filtr_button.png"},
            {"topheading_map", @"Image\top_eventlist_map_button.png"},
            {"topheading_info", @"Image\top_info.png"},
            {"topheading_edit", @"Image\ClientScreen\top_ico_edit.png"},
            //
            {"closeevent_wtb", @"Image\CloseEvent\Pokupka.png"},
            {"closeevent_wtb_selected", @"Image\CloseEvent\Pokupka_Selected.png"},
            {"closeevent_problem", @"Image\CloseEvent\Problema.png"},
            {"closeevent_problem_selected", @"Image\CloseEvent\Problema_Selected.png"},
            //
            {"tasklist_done", @"Image\TaskList\done.png"},
            {"tasklist_notdone", @"Image\TaskList\notDone.png"},
            {"tasklist_specdone", @"Image\TaskList\spec_done.png"},
            //
            {"eventscreen_clist", @"Image\es_clist.png"},
            {"eventscreen_coc", @"Image\es_coc.png"},
            {"eventscreen_tasks", @"Image\es_tasks.png"},
            // Client Screen
            {"clientscreen_phone", @"Image\ClientScreen\Phone.png"},
            {"clientscreen_plus", @"Image\ClientScreen\Plus.png"},
            //
            {"longtext_expand", @"Image\down_arrow_full_message.png"},
            {"longtext_close", @"Image\up_arrow_full_message.png"}
        };

        private static readonly Dictionary<string, object> ComponentPaths = new Dictionary<string, object>
        {
            {"tabbar", @"Components\TabBarComponent.xml"},
            {"topinfo", @"Components\TopInfoComponent.xml"}
        };

        public static string GetComponent(string tag)
        {
            object res;
            if (ComponentPaths.TryGetValue(tag, out res)) return (string) res;
            DConsole.WriteLine($"Component {tag} is not found in ResourceManager");
            return ComponentNotFound;
        }

        public static string GetImage(string tag)
        {
            object res;
            if (!ImagesPaths.TryGetValue(tag, out res))
            {
                DConsole.WriteLine($"{tag} is not found in ResourceManager!");
                return ImageNotFound;
            }
            try
            {
                Application.GetResourceStream(res.ToString());
            }
            catch
            {
                DConsole.WriteLine($"{tag}:{res} does not exists!");
                return ImageNotFound;
            }
            return (string) res;
        }
    }
}