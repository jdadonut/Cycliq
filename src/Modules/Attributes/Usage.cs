using System;
using System.Threading.Tasks;


namespace DSharpPlus.CommandsNext.Attributes
{
    [AttributeUsage(AttributeTargets.Method ^ AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
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