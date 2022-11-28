using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{

    public class RfidChannelState
    {
        public int  ChannelId;
        public bool Connect;
        public bool Busy;
        public bool Error;
        public bool Tp;
        public int  ErrorCode;
    }
    
}
