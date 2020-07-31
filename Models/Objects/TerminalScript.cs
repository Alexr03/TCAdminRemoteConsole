using System;
using System.Collections.Generic;
using System.Linq;
using TCAdmin.Interfaces.Database;
using TCAdmin.SDK.Objects;

namespace TCAdminRemoteConsole.Models.Objects
{
    public class TerminalScript : ObjectBase
    {
        public TerminalScript()
        {
            this.TableName = "tcmodule_terminal_scripts";
            this.KeyColumns = new[] {"scriptId"};
            this.SetValue("scriptId", -1);
            this.UseApplicationDataField = false;
            this.RowStatus = RowStatus.New;
            this.EnableCacheForObjectList = false;
        }

        public TerminalScript(int id) : this()
        {
            this.SetValue("scriptId", id);
            ValidateKeys();
            if (!this.Find())
            {
                throw new Exception("Cannot find terminal script with ID: " + id);
            }
        }

        public static List<TerminalScript> GetTerminalScripts()
        {
            return new TerminalScript().GetObjectList(new WhereList()).Cast<TerminalScript>().ToList();
        }
        
        public static TerminalScript GetTerminalScript(string name)
        {
            var whereList = new WhereList
            {
                {"name", name}
            };
            return new TerminalScript().GetObjectList(whereList).Cast<TerminalScript>().ToList().FirstOrDefault();
        }

        public int ScriptId
        {
            get => this.GetIntegerValue("scriptId");
            set => this.SetValue("scriptId", value);
        }
        
        public string Name
        {
            get => this.GetStringValue("name");
            set => this.SetValue("name", value);
        }

        public string Contents
        {
            get => this.GetStringValue("contents");
            set => this.SetValue("contents", value);
        }

        public TerminalType TerminalType
        {
            get => (TerminalType)this.GetIntegerValue("terminalType");
            set => this.SetValue("terminalType", (int)value);
        }
    }
}