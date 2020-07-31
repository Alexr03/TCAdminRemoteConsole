namespace TCAdminRemoteConsole.Models
{
    public class RequestCommandModel
    {
        public TerminalType TerminalType { get; set; }
        
        public string Script { get; set; }
        
        public int ServerId { get; set; }
    }

    public enum TerminalType
    {
        CommandPrompt,
        Powershell,
        Shell
    }
}