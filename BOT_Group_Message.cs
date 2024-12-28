using BOT_API_List;
using BOT_ReceiveMsg_T;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using WindowsFormsApp1;

namespace GroupMessageDealWith
{
    public class UserMsgADD
    {
        public string user_id;
        public string group_id;
        public string msg;
        public string msg_id;
    }

    internal class BotMessage : BOT_ReceiveMsg
    {

 
        public static void Group_Message(MsgData Data)
        {
            MySvrForm.BOT_LoglistADD(Get_Group_name(Data.group_id), "BOT:"+Data.self_id, Data.nickname, Data.user_id, Data.raw_message);
            if (Data.self_id == Data.user_id)//bot自身消息不处理
            {
                return;
            }

 
        }
    }
}