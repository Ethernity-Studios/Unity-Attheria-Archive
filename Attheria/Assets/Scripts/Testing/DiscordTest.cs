using System;
using System.Collections;
using System.Collections.Generic;
using CSharpDiscordWebhook.NET.Discord;
using UnityEngine;
public class DiscordTest : MonoBehaviour
{
    // Start is called before the first frame update
    private DiscordWebhook hook;
    void Start()
    {
        hook = new DiscordWebhook();
        hook.Uri = new Uri("https://discord.com/api/webhooks/1149613971238821888/2vn8lMULQXCYFp5hlFxM5VlHr7MzXkvLq13CbI08DL7i5CEB0yswBJ-eLj3ravrKgYgO");
        
        DiscordMessage message = new DiscordMessage();
        message.Content = $@"Attheria bug report {Guid.NewGuid()}";
        message.TTS = true; //read message to everyone on the channel
        message.Username = "Bugger";
        
        DiscordEmbed embed = new DiscordEmbed();
        embed.Title = "Embed title";
        embed.Description = "Embed description";
        embed.Timestamp = new DiscordTimestamp(DateTime.Now);
        embed.Color = new DiscordColor(4); //alpha will be ignored, you can use any RGB color
        embed.Image = new EmbedMedia() {Url= new Uri(@"https://as2.ftcdn.net/v2/jpg/02/62/94/09/1000_F_262940901_WWuitV3kBvSS4Lnw0TNJz4XnTAwZSjwp.jpg"), Width=150, Height=150}; //valid for thumb and video

//set embed
        message.Embeds.Add(embed);

        Send(message);
    }

    async void Send(DiscordMessage message)
    {
        await hook.SendAsync(message);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
