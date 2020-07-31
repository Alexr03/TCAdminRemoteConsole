using System;
using System.Data;
using System.Linq;
using TCAdmin.SDK.Web.MVC.Controllers;

namespace TCAdminRemoteConsole
{
    public class Test
    {
        public void LoadSiteMap(DataTable table)
        {
            Console.WriteLine("Loading Remote Console Sitemap...");
            var siteMap = new SiteMap(new object[]
            {
                1040, "d3b2aa93-7e2b-4e0d-8080-67d14b2fa8a9", null, null, 2, "/RemoteConsole", "/RemoteConsole",
                "RemoteConsole", "Index", "RemoteConsole", "MenuIcons/GameHosting/Steam.png",
                "MenuIcons/GameHosting/Steam.png", 1, 1000,
                "({07405876-e8c2-4b24-a774-4ef57f596384,0,8})",
                "({07405876-e8c2-4b24-a774-4ef57f596384,0,8})",
                "", null, ""
            });
            table.Rows.Add(siteMap.MergeIntoRow(table.NewRow()));
            Console.WriteLine("Loaded Remote Console Sitemap.");
        }
    }

    public class SiteMap
    {
        private object[] RowData;
        
        public SiteMap(object[] data)
        {
            for (var i = 0; i < data.Length; i++)
            {
                if (data[i] == null)
                {
                    data[i] = DBNull.Value;
                }
            }

            RowData = data;
        }

        public DataRow MergeIntoRow(DataRow row)
        {
            for (var i = 0; i < RowData.Length; i++)
            {
                row[i] = RowData[i];
            }

            return row;
        }

        public int PageId
        {
            get => (int)RowData[0];
            set => RowData[0] = value;
        }
        
        public string ModuleId
        {
            get => (string)RowData[2];
            set => RowData[2] = value;
        }
        
        public int ParentPageId
        {
            get => (int)RowData[3];
            set => RowData[3] = value;
        }
        
        public string ParentPageModuleId
        {
            get => (string)RowData[4];
            set => RowData[4] = value;
        }
        
        public int CategoryId
        {
            get => (int)RowData[5];
            set => RowData[5] = value;
        }
        
        public string Url
        {
            get => (string)RowData[6];
            set => RowData[6] = value;
        }
        
        public string MvcUrl
        {
            get => (string)RowData[7];
            set => RowData[7] = value;
        }
        
        public string Controller
        {
            get => (string)RowData[8];
            set => RowData[8] = value;
        }
        
        public string Action
        {
            get => (string)RowData[9];
            set => RowData[9] = value;
        }
        
        public string DisplayName
        {
            get => (string)RowData[10];
            set => RowData[10] = value;
        }
        
        public string PageSmallIcon
        {
            get => (string)RowData[11];
            set => RowData[11] = value;
        }
        
        public string PanelBarIcon
        {
            get => (string)RowData[12];
            set => RowData[21] = value;
        }
        
        public bool ShowInSideBar
        {
            get => (bool)RowData[13];
            set => RowData[13] = value;
        }
        
        public int ViewOrder
        {
            get => (int)RowData[14];
            set => RowData[14] = value;
        }
        
        public string RequiredPermissions
        {
            get => (string)RowData[15];
            set => RowData[15] = value;
        }
        
        public string MenuRequiredPermissions
        {
            get => (string)RowData[16];
            set => RowData[16] = value;
        }
        
        public string PageManager
        {
            get => (string)RowData[17];
            set => RowData[17] = value;
        }
        
        public string PageSearchProvider
        {
            get => (string)RowData[18];
            set => RowData[18] = value;
        }
        
        public string CacheName
        {
            get => (string)RowData[19];
            set => RowData[19] = value;
        }
    }
}