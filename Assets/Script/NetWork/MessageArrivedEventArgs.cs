using System;


namespace Network
{
    public class MessageArrivedEventArgs:EventArgs
    {
        public ChatMessage Message { get; set; }
        
        public DateTime ArrivedTime { get; set; }

    }
}
