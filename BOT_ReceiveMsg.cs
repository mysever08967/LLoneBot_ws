using BOT_API_List;
using GroupMessageDealWith;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using WindowsFormsApp1;
using static WindowsFormsApp1.MySvrForm;

namespace BOT_ReceiveMsg_T
{
    public class group_member
    {
        public string group_id;
        public string user_id;
        public string join_time;
        public string user_name;
        public string last_sent_time;
        public string card;
        public string role;
    }

    public class Group_list
    {
        public string group_id;
        public string group_name;
    }

    public class friend_list
    {
        public string user_id;
        public string user_name;
    }

    public class MsgData
    {
        public string raw_message;
        public string group_id;
        public string user_id;
        public string message_id;
        public string nickname;
        public string self_id;
        public WebSocket WebSocket;
    }

    internal class BOT_ReceiveMsg
    {
        public static List<group_member> group_member_List = new List<group_member>();
        public static List<friend_list> friend_List = new List<friend_list>();
        public static List<Group_list> Group_List = new List<Group_list>();
        public static List<string> ADDGroup_List = new List<string>();


        public static void BOT_MessageParsing(BOT_msgWS data)
        {
            dynamic jsonObject = JsonConvert.DeserializeObject(data.message);

            string eventType = GetEventType(jsonObject);// 获取事件类型
            if (eventType == null)
            {
                LOGdata lOGdata = new LOGdata();
                lOGdata.a = "BOT:" + data.Self_ID;
                lOGdata.b = "BOT:" + data.Self_ID;
                lOGdata.c = "BOT:" + data.Self_ID;
                lOGdata.d = "未解析类型:";
                lOGdata.e = data.message;
                BOT_LoglistADD(lOGdata);
                return;
            }

            switch (eventType)
            {
                case "meta_event_type":

                    BOT_meta_event_type(jsonObject, data);
                    break;

                case "message_type":
                    BOT_message_type(jsonObject, data);
                    break;

                case "echo":
                    BOT_API_ECHO(jsonObject, data);
                    break;

                case "sub_type":
                    BOT_sub_type(jsonObject, data);
                    break;

                case "notice_type":
                    BOT_notice_type(jsonObject, data);
                    break;

                default:
                    break;
            }
        }

        // 获取事件类型的方法
        private static string GetEventType(dynamic jsonObject)
        {
            if (jsonObject.meta_event_type != null) return "meta_event_type";
            if (jsonObject.message_type != null) return "message_type";
            if (jsonObject.echo != null) return "echo";
            if (jsonObject.sub_type != null) return "sub_type";
            if (jsonObject.notice_type != null) return "notice_type";
            return null;
        }

        private static void BOT_meta_event_type(dynamic jsonObject, BOT_msgWS data)
        {
            switch (jsonObject.meta_event_type.ToString())
            {
                case "lifecycle":
                    string self_id = jsonObject.self_id.ToString();
                    LOGdata lOGdata = new LOGdata();
                    lOGdata.a = "账号:" + self_id;
                    lOGdata.b = "账号:" + self_id;
                    lOGdata.c = "账号:" + self_id;
                    lOGdata.d = "已登录";
                    lOGdata.e = jsonObject.ToString();
                    BOT_LoglistADD(lOGdata);
                    BOT_API.Get_QQ_info(data._WebSocket);
                    BOT_API.Get_version_LLoneBot(data._WebSocket);
                    BOT_API.Get_friend_list(data._WebSocket);
                    BOT_API.Get_group_list(data._WebSocket);
                    break;

                case "heartbeat":
                    BOT_API.Get_QQstatus(data._WebSocket);
                    break;

                default:
                  
                    break;
            }
        }

        //收到消息  群或私聊
        private static void BOT_message_type(dynamic jsonObject, BOT_msgWS data)
        {
            BOT_API.Revint++;
            switch (jsonObject.message_type.ToString())
            {
                case "group":
                    string _message = BOT_API.Msg_Replace((string)jsonObject.raw_message);
                    MsgData group_Data = new MsgData
                    {
                        raw_message = _message,
                        group_id = jsonObject.group_id,
                        user_id = jsonObject.user_id,
                        message_id = jsonObject.message_id,
                        self_id = jsonObject.self_id,
                        nickname = jsonObject.sender.nickname,
                        WebSocket = data._WebSocket
                    };
                    BotMessage.Group_Message(group_Data);
                    break;

                case "private"://私聊
                    string _messag = BOT_API.Msg_Replace((string)jsonObject.raw_message);
                    MsgData private_Data = new MsgData
                    {
                        raw_message = _messag,
                        group_id = jsonObject.group_id,
                        user_id = jsonObject.user_id,
                        message_id = jsonObject.message_id,
                        self_id = jsonObject.self_id,
                        nickname = jsonObject.sender.nickname,
                        WebSocket = data._WebSocket
                    };
                    if (private_Data.user_id == private_Data.self_id)
                    {
                        return;
                    }
                    if (jsonObject.sub_type.ToString() == "group")
                    {
                        NOfriend_message(private_Data);//群临时会话
                    }
                    else if (jsonObject.sub_type.ToString() == "friend")
                    {
                        Friend_message(private_Data);//好友会话
                    }
                    break;

                default:
                 
                    break;
            }
        }

        private static void BOT_notice_type(dynamic json, BOT_msgWS data)
        {
            switch (json.notice_type.ToString())
            {
                case "group_recall":
                    if (json.self_id.ToString() != json.operator_id.ToString())
                    {
                        BOT_API.Get_msg(json.message_id.ToString(), data._WebSocket);//获取撤回的消息，bot自己的不获取
                    }
                    break;

                default:
              
                    break;
            }
        }

        public static string Get_Group_name(string GroupID)
        {
            foreach (var item in Group_List)
            {
                if (item.group_id == GroupID)
                {
                    return item.group_name;
                }
            }
            return GroupID;
        }

        //临时会话
        private static void NOfriend_message(MsgData private_Data)
        {
            LOGdata lOGdata = new LOGdata();
            lOGdata.a = private_Data.nickname;
            lOGdata.b = private_Data.user_id;
            lOGdata.c = "账号:" + private_Data.self_id;
            lOGdata.d = "收到临时消息";
            lOGdata.e = private_Data.raw_message;
            MySvrForm.BOT_LoglistADD(lOGdata);
        }

        //好友会话
        private static void Friend_message(MsgData private_Data)
        {
            LOGdata lOGdata = new LOGdata();
            lOGdata.a = private_Data.nickname;
            lOGdata.b = private_Data.user_id;
            lOGdata.c = "账号:" + private_Data.self_id;
            lOGdata.d = "收到临时消息";
            lOGdata.e = private_Data.raw_message;
            MySvrForm.BOT_LoglistADD(lOGdata);
        }

        //api结果
        private static void BOT_API_ECHO(dynamic jsonObject, BOT_msgWS data)
        {
            string echo = jsonObject.echo;
            LOGdata lOGdata = new LOGdata();
            switch (echo)
            {
                case "info":                  
                    lOGdata.a = "昵称:" + (string)jsonObject.data.nickname;
                    lOGdata.b = "账号:" + data.Self_ID;
                    lOGdata.c = "账号:" + data.Self_ID;
                    lOGdata.d = "登录成功";
                    lOGdata.e = jsonObject.ToString();
                    BOT_LoglistADD(lOGdata);
                    break;

                case "friendlist":
                    if (jsonObject.status != "ok")
                    {
                        lOGdata.a = "账号:" + data.Self_ID;
                        lOGdata.b = "账号:" + data.Self_ID;
                        lOGdata.c = "账号:" + data.Self_ID;
                        lOGdata.d = "需重启QQ";
                        lOGdata.e = jsonObject.ToString();
                        MySvrForm.BOT_LoglistADD(lOGdata);
                        break;
                    }
                    lOGdata.a = "账号:" + data.Self_ID;
                    lOGdata.b = "账号:" + data.Self_ID;
                    lOGdata.c = "获取好友";
                    lOGdata.d = "好友:" + jsonObject.data.Count;
                    lOGdata.e = jsonObject.ToString();
                    BOT_LoglistADD(lOGdata);
                    break;

                case "grouplist":
                    if (jsonObject.status != "ok")
                    {
                        lOGdata.a = "账号:" + data.Self_ID;
                        lOGdata.b = "账号:" + data.Self_ID;
                        lOGdata.c = "获取好友";
                        lOGdata.d = "需重启QQ";
                        lOGdata.e = jsonObject.ToString();
                        BOT_LoglistADD(lOGdata);
                        break;
                    }
                    for (int i = 0; i < jsonObject.data.Count; i++)
                    {
                        Group_list List = new Group_list
                        {
                            group_id = jsonObject.data[i].group_id.ToString(),
                            group_name = jsonObject.data[i].group_name.ToString()
                        };
                        Group_List.Add(List);
                    }
                    lOGdata.a = "账号:" + data.Self_ID;
                    lOGdata.b = "账号:" + data.Self_ID;
                    lOGdata.c = "获取群聊";
                    lOGdata.d = "群聊:" + jsonObject.data.Count;
                    lOGdata.e = jsonObject.ToString();
                    BOT_LoglistADD(lOGdata);
                    break;

                case "group_member_list":
                    if (jsonObject.status != "ok")
                    {
                        lOGdata.a = "账号:" + data.Self_ID;
                        lOGdata.b = "账号:" + data.Self_ID;
                        lOGdata.c = "账号:" + data.Self_ID;
                        lOGdata.d = "获取成员失败" ;
                        lOGdata.e = jsonObject.ToString();
                        BOT_LoglistADD(lOGdata);
                        break;
                    }
                    group_member_List.Clear();
                    for (int i = 0; i < jsonObject.data.Count; i++)
                    {
                        DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((long)jsonObject.data[i].last_sent_time);
                        DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((long)jsonObject.data[i].join_time);
                        string roleu;
                        if (jsonObject.data[i].role.ToString() == "owner")
                        {
                            roleu = "群主";
                        }
                        else if (jsonObject.data[i].role.ToString() == "admin")
                        {
                            roleu = "管理";
                        }
                        else
                        {
                            roleu = "成员";
                        }
                        group_member List = new group_member
                        {
                            group_id = jsonObject.data[i].group_id.ToString(),
                            user_id = jsonObject.data[i].user_id.ToString(),
                            user_name = jsonObject.data[i].nickname.ToString(),
                            join_time = dt.ToString("yyyy-MM-dd HH:mm:ss"),
                            last_sent_time = d.ToString("yyyy-MM-dd HH:mm:ss"),
                            role = roleu,//身份
                            card = jsonObject.data[i].card.ToString()//群昵称
                        };
                        string Group = jsonObject.data[i].group_id.ToString();
                        group_member_List.Add(List);
                    }
                    break;

                case "version":
                    MySvrForm.mForm.Invoke(new Action(() => { MySvrForm.mForm.BotVer.Text = $"{jsonObject.data.app_name} {jsonObject.data.protocol_version} {jsonObject.data.app_version}"; }));
                    break;

                case "status":
                    if (!(bool)jsonObject.data.good || !(bool)jsonObject.data.online)
                    {
                        lOGdata.a = "账号:" + data.Self_ID;
                        lOGdata.b = "账号:" + data.Self_ID;
                        lOGdata.c = "账号:" + data.Self_ID;
                        lOGdata.d = "状态异常";
                        lOGdata.e = jsonObject.ToString();
                        BOT_LoglistADD(lOGdata);
                    }

                    break;

                case "get_msg":
                    string _messag = BOT_API.Msg_Replace(jsonObject.data.raw_message.ToString());
                    lOGdata.a = Get_Group_name(jsonObject.data.group_id.ToString());
                    lOGdata.b = jsonObject.data.group_id.ToString();
                    lOGdata.c = jsonObject.data.user_id.ToString();
                    lOGdata.d = "撤回一条消息";
                    lOGdata.e = _messag;
                    BOT_LoglistADD(lOGdata);
                    break;

                case "send_group_msg":
                    if (jsonObject.status != "ok")
                    {
                        lOGdata.a = "账号:" + data.Self_ID;
                        lOGdata.b = "账号:" + data.Self_ID;
                        lOGdata.c = "账号:" + data.Self_ID;
                        lOGdata.d = "消息发送失败";
                        lOGdata.e = jsonObject.ToString();
                        BOT_LoglistADD(lOGdata);
                    }
                    break;

                default:
                    lOGdata.a = "账号:" + data.Self_ID;
                    lOGdata.b = "账号:" + data.Self_ID;
                    lOGdata.c = "账号:" + data.Self_ID;
                    lOGdata.d = echo;
                    lOGdata.e = jsonObject.ToString();
                    BOT_LoglistADD(lOGdata);
                    break;
            }
        }

        //事件上报
        private static void BOT_sub_type(dynamic json, BOT_msgWS data)
        {
            LOGdata lOGdata = new LOGdata();
            switch (json.sub_type.ToString())
            {
                case "approve": // 新人进入群聊
                   
                    lOGdata.a = Get_Group_name(json.group_id.ToString());
                    lOGdata.b = json.group_id.ToString();
                    lOGdata.c = "账号:" + data.Self_ID;
                    lOGdata.d = "新人进群";
                    lOGdata.e = $"{json.user_id.ToString()} 进入群聊";
                    BOT_LoglistADD(lOGdata);
                    break;

                case "leave": // 退群人
                    lOGdata.a = Get_Group_name(json.group_id.ToString());
                    lOGdata.b = json.group_id.ToString();
                    lOGdata.c = "账号:" + data.Self_ID;
                    lOGdata.d = "有人退群";
                    lOGdata.e = json.ToString();
                    BOT_LoglistADD(lOGdata);
                    break;

                case "add":
                    lOGdata.a = Get_Group_name(json.group_id.ToString());
                    lOGdata.b = json.group_id.ToString();
                    lOGdata.c = "账号:" + data.Self_ID;
                    lOGdata.d = "申请入群";
                    lOGdata.e = json.ToString();
                    BOT_LoglistADD(lOGdata);
                    break;

                case "ban":
                    lOGdata.a = Get_Group_name(json.group_id.ToString());
                    lOGdata.b = json.group_id.ToString();
                    lOGdata.c = "账号:" + data.Self_ID;
                    lOGdata.d = "禁言";
                    lOGdata.e = $"{json.user_id.ToString()} 被 {json.operator_id.ToString()} 禁言 {json.duration.ToString()}秒";
                    BOT_LoglistADD(lOGdata);
                    break;

                case "lift_ban":
                    lOGdata.a = Get_Group_name(json.group_id.ToString());
                    lOGdata.b = json.group_id.ToString();
                    lOGdata.c = "账号:" + data.Self_ID;
                    lOGdata.d = "解除禁言";
                    lOGdata.e = $"{json.user_id.ToString()} 被 {json.operator_id.ToString()} 解除禁言";
                    BOT_LoglistADD(lOGdata);
                    break;

                case "kick":
                    lOGdata.a = Get_Group_name(json.group_id.ToString());
                    lOGdata.b = json.group_id.ToString();
                    lOGdata.c = "账号:" + data.Self_ID;
                    lOGdata.d = "踢出群聊";
                    lOGdata.e = $"{json.user_id.ToString()} 被 {json.operator_id.ToString()} 踢出群聊";
                    MySvrForm.BOT_LoglistADD(lOGdata);
                    break;

                default:
                  
                    break;
            }
        }
    }
}