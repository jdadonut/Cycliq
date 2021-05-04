using System;
using System.Threading.Tasks;


namespace DSharpPlus.CommandsNext.Attributes
{
    [AttributeUsage(AttributeTargets.Method ^ AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class CategoryAttribute : CheckBaseAttribute
    {
        public string Category { get; private set; }
        public string Subcategory { get; private set; }

        public CategoryAttribute( string category = "None", string subcategory = "None" )
        {
            Category = category;
            Subcategory = subcategory;
        }
        public override Task<bool> ExecuteCheckAsync(CommandContext context, bool help) { return Task.FromResult(true); }
    }
}