using System;
using System.Threading.Tasks;


namespace DSharpPlus.CommandsNext.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class UsageAttribute : CheckBaseAttribute
    {
        public string Usage { get; private set; }

        public UsageAttribute( string usage = "Unset" )
        {
            Usage = usage;
        }
        public override Task<bool> ExecuteCheckAsync(CommandContext context, bool help) { return Task.FromResult(true); }
    }
}