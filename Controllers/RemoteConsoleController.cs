using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using TCAdmin.SDK.Web.FileManager;
using TCAdmin.SDK.Web.MVC.Controllers;
using TCAdminRemoteConsole.HttpResponses;
using TCAdminRemoteConsole.Models;
using TCAdminRemoteConsole.Models.Objects;
using OperatingSystem = TCAdmin.SDK.Objects.OperatingSystem;
using Server = TCAdmin.GameHosting.SDK.Objects.Server;

namespace TCAdminRemoteConsole.Controllers
{
    [Authorize]
    public class RemoteConsoleController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RequestCommand(RequestCommandModel model)
        {
            if (string.IsNullOrEmpty(model.Script))
            {
                return JsonMessage("Please provide a script", HttpStatusCode.BadRequest);
            }

            var server = new Server(model.ServerId);
            RemoteConsole remoteConsole = null;
            var fileSystem = server.FileSystemService;
            string tempFileName;
            switch (model.TerminalType)
            {
                case TerminalType.Powershell:
                    if (server.OperatingSystem != OperatingSystem.Windows)
                    {
                        return JsonMessage("Linux is not supported with this terminal.", HttpStatusCode.BadRequest);
                    }

                    tempFileName = Path.Combine(server.ServerUtilitiesService.GetTemporaryDirectory(),
                        Path.GetTempFileName() + ".ps1");
                    // fileSystem.CreateTemporaryFile(tempFileName, model.Script);
                    fileSystem.CreateTextFile(tempFileName, Encoding.ASCII.GetBytes(model.Script));
                    remoteConsole = new RemoteConsole(server, "C:\\Windows\\System32\\cmd.exe",
                        $"/c powershell \"{tempFileName}\"", "Powershell", true);
                    break;
                case TerminalType.CommandPrompt:
                    if (server.OperatingSystem != OperatingSystem.Windows)
                    {
                        return JsonMessage("Linux is not supported with this terminal.", HttpStatusCode.BadRequest);
                    }

                    tempFileName = Path.Combine(server.ServerUtilitiesService.GetTemporaryDirectory(),
                        Path.GetTempFileName() + ".bat");
                    // fileSystem.CreateTemporaryFile(tempFileName, model.Script);
                    fileSystem.CreateTextFile(tempFileName, Encoding.ASCII.GetBytes(model.Script));
                    remoteConsole = new RemoteConsole(server, tempFileName, "", "Command Prompt", true);
                    break;
                case TerminalType.Shell:
                    if (server.OperatingSystem != OperatingSystem.Linux)
                    {
                        return JsonMessage("Windows is not supported with this terminal.", HttpStatusCode.BadRequest);
                    }

                    tempFileName = TCAdmin.SDK.Misc.FileSystem.CombinePath(server.ServerUtilitiesService.GetTemporaryDirectory(),
                        "rConsole.sh", server.OperatingSystem);
                    // fileSystem.CreateTemporaryFile(tempFileName, model.Script);
                    fileSystem.CreateTextFile(tempFileName, Encoding.ASCII.GetBytes(model.Script));
                    remoteConsole = new RemoteConsole(server, "./" + tempFileName, $"", "Shell", true);
                    break;
            }

            return Json(new
            {
                url = remoteConsole?.GetUrl()
            });
        }

        public ActionResult SaveScript(RequestCommandModel model, string name)
        {
            TerminalScript terminalScript;
            var savedScript = TerminalScript.GetTerminalScript(name);
            if (savedScript != null)
            {
                terminalScript = savedScript;
                terminalScript.Name = name;
                terminalScript.Contents = model.Script;
                terminalScript.TerminalType = model.TerminalType;
            }
            else
            {
                terminalScript = new TerminalScript
                {
                    Name = name,
                    TerminalType = model.TerminalType,
                    Contents = model.Script
                };
            }

            terminalScript.GenerateKey();
            terminalScript.Save();

            return Json(new
            {
                Message = $"Successfully saved script as '{name}'"
            });
        }
        
        public ActionResult LoadScript(int id)
        {
            var terminalScript = new TerminalScript(id);

            return Json(new
            {
                terminalScript.Name,
                terminalScript.Contents,
                terminalScript.ScriptId,
                terminalScript.TerminalType
            }, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult DeleteScript(int id)
        {
            var terminalScript = new TerminalScript(id);
            terminalScript.Delete();

            return Json(new
            {
                Message = "Successfully deleted script."
            }, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult GetScripts()
        {
            var terminalScripts = TerminalScript.GetTerminalScripts();
            return Json(terminalScripts.Select(x => new
            {
                x.ScriptId,
                x.Name,
                x.Contents,
                x.TerminalType
            }), JsonRequestBehavior.AllowGet);
        }

        private ActionResult JsonMessage(string message, HttpStatusCode code)
        {
            return new JsonHttpStatusResult(new
            {
                Message = message
            }, code);
        }
    }
}