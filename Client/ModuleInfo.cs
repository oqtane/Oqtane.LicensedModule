using Oqtane.Models;
using Oqtane.Modules;

namespace Oqtane.LicensedModule
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Owner = "Oqtane Labs Inc",
            Name = "Licensed Module", 
            Description = "Licensed Module",
            Version = "5.0.0",
            PackageName = "Oqtane.LicensedModule",
            Dependencies = "Oqtane.Licensing.Client.Oqtane,Oqtane.Licensing.Shared.Oqtane"
        };
    }
}
