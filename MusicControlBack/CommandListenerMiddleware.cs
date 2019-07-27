using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using WindowsInput;
using WindowsInput.Native;
namespace MusicControlBack
{
    public static class CommandListenerMiddleware
    {
        public static IApplicationBuilder UseMediaCommandHandler(this IApplicationBuilder app)
        {
            var simulator = new InputSimulator();

            Dictionary<string, VirtualKeyCode> keys = new Dictionary<string, VirtualKeyCode>()
            {
                { Constants.Pause, VirtualKeyCode.MEDIA_PLAY_PAUSE },
                { Constants.Next, VirtualKeyCode.MEDIA_NEXT_TRACK },
                { Constants.Prev, VirtualKeyCode.MEDIA_PREV_TRACK }
            };

            Func<HttpContext, Func<Task>, Task> middleWareBody = async (context, next) =>
            {
                if (context.Request.Query.TryGetValue(Constants.Command, out StringValues value))
                {
                    var command = value.SingleOrDefault();
                    if (command != null && keys.TryGetValue(command, out VirtualKeyCode keyCode))
                    {
                        simulator.Keyboard.KeyPress(keyCode);
                        context.Items.Add(Constants.SuccessFlag, Enum.GetName(typeof(VirtualKeyCode), keyCode));
                    }
                }

                await next();
            };
            return app.Use(middleWareBody);
        }
    }
}
