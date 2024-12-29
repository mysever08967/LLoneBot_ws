using BOT_ReceiveMsg_T;
using WindowsFormsApp1;
using static WindowsFormsApp1.MySvrForm;

namespace GroupMessageDealWith
{
 
    internal class BotMessage : BOT_ReceiveMsg
    {
        public static void Group_Message(MsgData Data)
        {
            LOGdata lOGdata = new LOGdata
            {
                a = Get_Group_name(Data.group_id),
                b = "账号:" + Data.self_id,
                c = Data.nickname,
                d = Data.user_id,
                e = Data.raw_message
            };
            MySvrForm.BOT_LoglistADD(lOGdata);
            if (Data.self_id == Data.user_id)//bot自身消息不处理
            {
                return;
            }
            // 在这里写BOT群指令功能回复等等
        }
    }
}