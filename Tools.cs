using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Newtonsoft.Json;
public static class Tools
{
    public static string NekosLife = "https://nekos.life/api/v2/";
    public static bool IsMention(string mention)
    {
        return new Regex("(<@|<@!)[0-9]{1,30}>").IsMatch(mention);
    }
    public static async Task<string> GetNekosLifeEndpoint(CommandContext ctx, string s, bool img = true)
    {
        string url = NekosLife;
        if (img)
            url += "img/";
        url += s;
        IServiceProvider deps = ctx.Services;
        HttpClient client = deps.GetService<HttpClient>();
        HttpResponseMessage res =  await client.GetAsync(url);
        if (((int)res.StatusCode) != 200)
            return "";
        else
            return ((Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(await res.Content.ReadAsStringAsync())).GetValue("url").ToString();
    }
    public static async Task<string> GetMention(CommandContext ctx, string mention, string message = "Please send a valid mention.")
    {
        await ctx.RespondAsync($"{mention} isn't a valid tag! {message}");
        var mes = await ctx.Client.GetInteractivity().WaitForMessageAsync(
            c => c.Author.Id == ctx.Message.Author.Id,
            TimeSpan.FromSeconds(15));
        if (mes.Result.Content.ToLower() == "cancel")
        {
            await ctx.RespondAsync("Command Cancelled.");
            return ctx.Message.Author.Id.ToString();
        }
        else
        {
            mention = mes.Result.Content;
            if (!IsMention(mention))
            {
                return await GetMention(ctx, mention, message);
            }
            else
                return mention;
        }


    }


}
