using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChatApp.Client.Services.Message
{
    class MessageInitializeResponse : BaseResponse
    {
	    public User FriendInfo { get; set; }
        public IEnumerable <Models.Message> Messages { get; set; }
    }
}
