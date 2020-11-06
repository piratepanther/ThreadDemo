using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    public class MessageArrivedEventArgs:EventArgs
    {
        public ChatMessage Message { get; set; }
        
        public DateTime ArrivedTime { get; set; }

    }
}
