using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static WindowsFormsApp1.MySvrForm;

namespace WindowsFormsApp1
{
    public class ClientData
    {
        public string raw_message;
        public string group_id;
        public string user_id;
        public string message_id;
        public string nickname;
        public string self_id;
    }

    public class Group_list
    {
        public string group_id;
        public string group_name;
    }

    internal class Self_Client
    {
        private static readonly Dictionary<string, string> replacements = new Dictionary<string, string>
        {
            { "&amp;", "&" },
            { "&#91;", "[" },
            { "&#93;", "]" },
            { "&#44;", "," }
        };

        public WebSocket webSocket;
        public string Self_ID;
        public string LLonebot_ver;
        public string status = "待获取";
        private static List<Group_list> Group_List = new List<Group_list>();
        public ConcurrentQueue<string> ReceiveQueue = new ConcurrentQueue<string>();
        public ConcurrentQueue<byte[]> SendQueue = new ConcurrentQueue<byte[]>();
        public int Receive;
        public int Send;

        public void Start(WebSocket ws, string Self)
        {
            webSocket = ws;
            Self_ID = Self;
            Msg_start();
            Send_start();
        }

        public void Close()
        {
            try
            {
                webSocket?.CloseAsync(WebSocketCloseStatus.NormalClosure, "Server shutting down", CancellationToken.None);
            }
            catch (Exception)
            {
            }
        }

        private async void Msg_start()
        {
            await Bot_MsgQueueAsync();
        }

        private async void Send_start()
        {
            await Bot_sendQueueAsync();
        }
        //转义符
        public static string Msg_Replace(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }
            StringBuilder sb = new StringBuilder(s);
            foreach (var replacement in replacements)
            {
                sb.Replace(replacement.Key, replacement.Value);
            }
            return sb.ToString();
        }
        //处理收到的消息
        private async Task Bot_MsgQueueAsync()
        {
            while (this != null)
            {
                if (ReceiveQueue.Count > 0)
                {
                    try
                    {
                        if (ReceiveQueue.TryDequeue(out string result))
                        {
                            BOT_MessageParsing(result);
                            Receive++;
                        }
                    }
                    catch (Exception ex)
                    {
                        LOGdata lOGdata = new LOGdata
                        {
                            a = Self_ID,
                            b = Self_ID,
                            c = Self_ID,
                            d = "消息处理异常",
                            e = $"{ex.Message}\n\n{ex.StackTrace}"
                        };
                        MySvrForm.BOT_LoglistADD(lOGdata);
                    }
                }
                await Task.Delay(10);
            }
            Console.WriteLine("接收结束");
        }
        //消息发送队列
        private async Task Bot_sendQueueAsync()
        {
            int delayTime;
            while (this != null)
            {
                delayTime = 10;
                try
                {
                    if (SendQueue.TryDequeue(out byte[] result))
                    {
                        _ = webSocket.SendAsync(new ArraySegment<byte>(result), WebSocketMessageType.Text, true, CancellationToken.None);

                        delayTime = 100;
                    }
                }
                catch (Exception ex)
                {
                    LOGdata lOGdata = new LOGdata
                    {
                        a = Self_ID,
                        b = Self_ID,
                        c = Self_ID,
                        d = "消息发送异常",
                        e = $"{ex.Message}\n\n{ex.StackTrace}"
                    };
                    MySvrForm.BOT_LoglistADD(lOGdata);
                    delayTime = 10;
                }
                await Task.Delay(delayTime);
            }
            Console.WriteLine("发送结束");
        }
        //将要发送的消息放入队列
        private void Bot_SendMsg(object obj)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            SendQueue.Enqueue(Encoding.UTF8.GetBytes(json));
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
        //消息类型解析
        private void BOT_MessageParsing(string json)
        {
            dynamic jsonObject = JsonConvert.DeserializeObject(json);

            string eventType = GetEventType(jsonObject);// 获取事件类型
            if (eventType == null)
            {
                LOGdata lOGdata = new LOGdata();
                lOGdata.a = "BOT:" + Self_ID;
                lOGdata.b = "BOT:" + Self_ID;
                lOGdata.c = "BOT:" + Self_ID;
                lOGdata.d = "未解析类型:";
                lOGdata.e = json;
                BOT_LoglistADD(lOGdata);
                return;
            }

            switch (eventType)
            {
                case "meta_event_type":

                    BOT_meta_event_type(jsonObject);
                    break;

                case "message_type":
                    BOT_message_type(jsonObject);
                    break;

                case "echo":
                    BOT_API_ECHO(jsonObject);
                    break;

                case "sub_type":
                    BOT_sub_type(jsonObject);
                    break;

                case "notice_type":
                    BOT_notice_type(jsonObject);
                    break;

                default:
                    break;
            }
        }

        //撤回消息  事件
        private void BOT_notice_type(dynamic json)
        {
            switch (json.notice_type.ToString())
            {
                case "group_recall":
                    if (json.self_id.ToString() != json.operator_id.ToString())
                    {
                        Get_msg(json.message_id.ToString());//获取撤回的消息，bot自己的不获取
                    }
                    break;

                default:

                    break;
            }
        }

        //事件上报
        private void BOT_sub_type(dynamic json)
        {
            LOGdata lOGdata = new LOGdata();
            switch (json.sub_type.ToString())
            {
                case "approve": // 新人进入群聊

                    lOGdata.a = Get_Group_name(json.group_id.ToString());
                    lOGdata.b = json.group_id.ToString();
                    lOGdata.c = "账号:" + Self_ID;
                    lOGdata.d = "新人进群";
                    lOGdata.e = $"{json.user_id.ToString()} 进入群聊";
                    BOT_LoglistADD(lOGdata);
                    break;

                case "leave": // 退群人
                    lOGdata.a = Get_Group_name(json.group_id.ToString());
                    lOGdata.b = json.group_id.ToString();
                    lOGdata.c = "账号:" + Self_ID;
                    lOGdata.d = "有人退群";
                    lOGdata.e = json.ToString();
                    BOT_LoglistADD(lOGdata);
                    break;

                case "add":
                    lOGdata.a = Get_Group_name(json.group_id.ToString());
                    lOGdata.b = json.group_id.ToString();
                    lOGdata.c = "账号:" + Self_ID;
                    lOGdata.d = "申请入群";
                    lOGdata.e = json.ToString();
                    BOT_LoglistADD(lOGdata);
                    break;

                case "ban":
                    lOGdata.a = Get_Group_name(json.group_id.ToString());
                    lOGdata.b = json.group_id.ToString();
                    lOGdata.c = "账号:" + Self_ID;
                    lOGdata.d = "禁言";
                    lOGdata.e = $"{json.user_id.ToString()} 被 {json.operator_id.ToString()} 禁言 {json.duration.ToString()}秒";
                    BOT_LoglistADD(lOGdata);
                    break;

                case "lift_ban":
                    lOGdata.a = Get_Group_name(json.group_id.ToString());
                    lOGdata.b = json.group_id.ToString();
                    lOGdata.c = "账号:" + Self_ID;
                    lOGdata.d = "解除禁言";
                    lOGdata.e = $"{json.user_id.ToString()} 被 {json.operator_id.ToString()} 解除禁言";
                    BOT_LoglistADD(lOGdata);
                    break;

                case "kick":
                    lOGdata.a = Get_Group_name(json.group_id.ToString());
                    lOGdata.b = json.group_id.ToString();
                    lOGdata.c = "账号:" + Self_ID;
                    lOGdata.d = "踢出群聊";
                    lOGdata.e = $"{json.user_id.ToString()} 被 {json.operator_id.ToString()} 踢出群聊";
                    MySvrForm.BOT_LoglistADD(lOGdata);
                    break;

                default:

                    break;
            }
        }

        //api结果
        private void BOT_API_ECHO(dynamic jsonObject)
        {
            string echo = jsonObject.echo;
            LOGdata lOGdata = new LOGdata();
            switch (echo)
            {
                case "info":
                    lOGdata.a = "昵称:" + (string)jsonObject.data.nickname;
                    lOGdata.b = "账号:" + Self_ID;
                    lOGdata.c = "账号:" + Self_ID;
                    lOGdata.d = "登录成功";
                    lOGdata.e = jsonObject.ToString();
                    BOT_LoglistADD(lOGdata);
                    break;

                case "friendlist":
                    if (jsonObject.status != "ok")
                    {
                        lOGdata.a = "账号:" + Self_ID;
                        lOGdata.b = "账号:" + Self_ID;
                        lOGdata.c = "账号:" + Self_ID;
                        lOGdata.d = "需重启QQ";
                        lOGdata.e = jsonObject.ToString();
                        MySvrForm.BOT_LoglistADD(lOGdata);
                        break;
                    }
                    lOGdata.a = "账号:" + Self_ID;
                    lOGdata.b = "账号:" + Self_ID;
                    lOGdata.c = "获取好友";
                    lOGdata.d = "好友:" + jsonObject.data.Count;
                    lOGdata.e = jsonObject.ToString();
                    BOT_LoglistADD(lOGdata);
                    break;

                case "grouplist":
                    if (jsonObject.status != "ok")
                    {
                        lOGdata.a = "账号:" + Self_ID;
                        lOGdata.b = "账号:" + Self_ID;
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
                    lOGdata.a = "账号:" + Self_ID;
                    lOGdata.b = "账号:" + Self_ID;
                    lOGdata.c = "获取群聊";
                    lOGdata.d = "群聊:" + jsonObject.data.Count;
                    lOGdata.e = jsonObject.ToString();
                    BOT_LoglistADD(lOGdata);
                    break;

                case "group_member_list":
                    if (jsonObject.status != "ok")
                    {
                        lOGdata.a = "账号:" + Self_ID;
                        lOGdata.b = "账号:" + Self_ID;
                        lOGdata.c = "账号:" + Self_ID;
                        lOGdata.d = "获取成员失败";
                        lOGdata.e = jsonObject.ToString();
                        BOT_LoglistADD(lOGdata);
                        break;
                    }

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
                        //group_member List = new group_member
                        //{
                        //    group_id = jsonObject.data[i].group_id.ToString(),
                        //    user_id = jsonObject.data[i].user_id.ToString(),
                        //    user_name = jsonObject.data[i].nickname.ToString(),
                        //    join_time = dt.ToString("yyyy-MM-dd HH:mm:ss"),
                        //    last_sent_time = d.ToString("yyyy-MM-dd HH:mm:ss"),
                        //    role = roleu,//身份
                        //    card = jsonObject.data[i].card.ToString()//群昵称
                        //};
                        //string Group = jsonObject.data[i].group_id.ToString();
                    }
                    break;

                case "version":
                    LLonebot_ver = $"{jsonObject.data.app_name} {jsonObject.data.protocol_version} {jsonObject.data.app_version}";
                    break;

                case "status":
                    if (!(bool)jsonObject.data.good || !(bool)jsonObject.data.online)
                    {
                        lOGdata.a = "账号:" + Self_ID;
                        lOGdata.b = "账号:" + Self_ID;
                        lOGdata.c = "账号:" + Self_ID;
                        lOGdata.d = "状态异常";
                        lOGdata.e = jsonObject.ToString();
                        BOT_LoglistADD(lOGdata);
                        status = "异常";
                        break;
                    }
                    status = "在线";
                    break;

                case "get_msg":
                    string _messag = Msg_Replace(jsonObject.data.raw_message.ToString());
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
                        lOGdata.a = "账号:" + Self_ID;
                        lOGdata.b = "账号:" + Self_ID;
                        lOGdata.c = "账号:" + Self_ID;
                        lOGdata.d = "消息发送失败";
                        lOGdata.e = jsonObject.ToString();
                        BOT_LoglistADD(lOGdata);
                    }
                    break;

                default:
                    lOGdata.a = "账号:" + Self_ID;
                    lOGdata.b = "账号:" + Self_ID;
                    lOGdata.c = "账号:" + Self_ID;
                    lOGdata.d = echo;
                    lOGdata.e = jsonObject.ToString();
                    BOT_LoglistADD(lOGdata);
                    break;
            }
        }

        //私聊消息类型
        private void BOT_message_type(dynamic jsonObject)
        {
            switch (jsonObject.message_type.ToString())
            {
                case "group":
                    string _message = Msg_Replace((string)jsonObject.raw_message);
                    ClientData group_Data = new ClientData
                    {
                        raw_message = _message,
                        group_id = jsonObject.group_id,
                        user_id = jsonObject.user_id,
                        message_id = jsonObject.message_id,
                        self_id = jsonObject.self_id,
                        nickname = jsonObject.sender.nickname,
                    };
                    Group_Message(group_Data);
                    break;

                case "private"://私聊
                    string _messag = Msg_Replace((string)jsonObject.raw_message);
                    ClientData private_Data = new ClientData
                    {
                        raw_message = _messag,
                        group_id = jsonObject.group_id,
                        user_id = jsonObject.user_id,
                        message_id = jsonObject.message_id,
                        self_id = jsonObject.self_id,
                        nickname = jsonObject.sender.nickname,
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

        //获取已保存的群名
        private string Get_Group_name(string GroupID)
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

        //好友会话
        private void Friend_message(ClientData private_Data)
        {
            LOGdata lOGdata = new LOGdata();
            lOGdata.a = private_Data.nickname;
            lOGdata.b = private_Data.user_id;
            lOGdata.c = "账号:" + private_Data.self_id;
            lOGdata.d = "收到临时消息";
            lOGdata.e = private_Data.raw_message;
            MySvrForm.BOT_LoglistADD(lOGdata);
        }

        //群临时会话
        private void NOfriend_message(ClientData private_Data)
        {
            LOGdata lOGdata = new LOGdata();
            lOGdata.a = private_Data.nickname;
            lOGdata.b = private_Data.user_id;
            lOGdata.c = "账号:" + private_Data.self_id;
            lOGdata.d = "收到临时消息";
            lOGdata.e = private_Data.raw_message;
            MySvrForm.BOT_LoglistADD(lOGdata);
        }

        //群消息处理
        private void Group_Message(ClientData Data)
        {
            LOGdata lOGdata = new LOGdata
            {
                a = Get_Group_name(Data.group_id),
                b = "账号:" + Self_ID,
                c = Data.nickname,
                d = Data.user_id,
                e = Data.raw_message
            };
            BOT_LoglistADD(lOGdata);
            if (Data.self_id == Data.user_id)//bot自身消息不处理
            {
                return;
            }
            // 在这里写BOT群指令功能回复等等
        }
        //收到链接和心跳
        private void BOT_meta_event_type(dynamic jsonObject)
        {
            switch (jsonObject.meta_event_type.ToString())
            {
                case "lifecycle":
                    LOGdata lOGdata = new LOGdata();
                    lOGdata.a = "账号:" + Self_ID;
                    lOGdata.b = "账号:" + Self_ID;
                    lOGdata.c = "账号:" + Self_ID;
                    lOGdata.d = "已登录";
                    lOGdata.e = jsonObject.ToString();
                    BOT_LoglistADD(lOGdata);
                    Get_QQ_info();
                    Get_version_LLoneBot();
                    Get_friend_list();
                    Get_group_list();
                    break;

                case "heartbeat":
                    Get_QQstatus();
                    break;

                default:

                    break;
            }
        }

        internal static string CQ_at_Member(string QQ)
        {
            return $"[CQ:at,qq={QQ}]";
        }

        internal static string CQ_at_ALL()
        {
            return "[CQ:at,qq=all]";
        }

        internal string CQ_image(byte[] jpg)
        {
            string strbase64 = Convert.ToBase64String(jpg);
            return $"[CQ:image,file=base64://{strbase64},summary=❤️]";
        }

        internal string CQ_imageBese64(string Bese64)
        {
            return $"[CQ:image,file=base64://{Bese64},summary=❤️]";
        }

        internal byte[] Get_user_Image(string user_id)
        {
            string url = $"https://q4.qlogo.cn/g?b=qq&nk={user_id}&s=140";
            using (WebClient webClient = new WebClient())
            {
                byte[] imageData = webClient.DownloadData(url);
                return imageData;
            }
        }

        internal void Send_group_msg(string message, string group_id)
        {
            if (message == null)
            {
                return;
            }
            var json = new
            {
                action = "send_group_msg",
                echo = "send_group_msg",
                @params = new
                {
                    message,
                    group_id,
                }
            };
            Send++;
            Bot_SendMsg(json);
        }

        internal void Send_private_msg(string message, string user_id)
        {
            if (message == null)
            {
                return;
            }
            var json = new
            {
                action = "send_private_msg",
                echo = "发送私聊消息",
                @params = new
                {
                    message,
                    user_id,
                }
            };
            Send++;
            Bot_SendMsg(json);
        }

        internal void Get_version_LLoneBot()
        {
            var json = new
            {
                action = "get_version_info",
                echo = "version",
            };
            Bot_SendMsg(json);
        }

        internal void Get_stranger_info(string user_id)
        {
            var json = new
            {
                action = "get_stranger_info",
                echo = "stranger_info",
                @params = new
                {
                    user_id,
                    no_cache = true,
                }
            };
            Bot_SendMsg(json);
        }

        internal void Delete_group_msg(string message_id)
        {
            var json = new
            {
                action = "delete_msg",
                echo = "撤回群消息",
                @params = new
                {
                    message_id,
                }
            };
            Bot_SendMsg(json);
        }

        internal void set_group_ban(string group_id, string user_id, string duration)
        {
            var json = new
            {
                action = "set_group_ban",
                echo = "禁言群成员",
                @params = new
                {
                    group_id,
                    user_id,
                    duration, // 单位 秒
                }
            };
            Bot_SendMsg(json);
        }

        internal void Send_group_video(string group_id, byte[] videoByte)
        {
            var json = new
            {
                action = "send_group_msg",
                echo = "发送群短视频",
                @params = new
                {
                    group_id,
                    message = new
                    {
                        type = "video",
                        data = new
                        {
                            file = "base64://" + Convert.ToBase64String(videoByte),
                        }
                    }
                }
            };
            Bot_SendMsg(json);
        }

        internal void Send_group_music(string group_id, string url, string content, string audio, string title, string image)
        {
            var json = new
            {
                action = "send_group_msg",
                echo = "自定义音乐",
                @params = new
                {
                    group_id,
                    message = new
                    {
                        type = "music",
                        data = new
                        {
                            type = "custom",
                            url,
                            content,
                            audio,
                            title,
                            image,
                        }
                    }
                }
            };
            Send++;
            Bot_SendMsg(json);
        }

        internal void Get_group_member_list(string group_id)

        {
            var json = new
            {
                action = "get_group_member_list",
                echo = "group_member_list",
                @params = new
                {
                    group_id,
                }
            };
            Bot_SendMsg(json);
        }

        internal void Set_group_kick(string group_id, string user_id, bool reject_add_request)
        {
            var json = new
            {
                action = "set_group_kick",
                echo = "踢出成员",
                @params = new
                {
                    group_id,
                    user_id,
                    reject_add_request,  //是否不在接收此人请求
                }
            };
            Bot_SendMsg(json);
        }

        internal void Set_group_add_request(string flag, string reason, bool approve)
        {
            var json = new
            {
                action = "set_group_add_request",
                echo = "处理加群请求",
                @params = new
                {
                    add = "add", //add 或 invite，请求类型（需要和上报消息中的 sub_type 字段相符
                    flag, //加群请求的 flag（需从上报的数据中获得）
                    approve,//同意  拒绝
                    reason,  //拒绝理由
                }
            };
            Bot_SendMsg(json);
        }

        internal void Set_group_admin(string group_id, string user_id, bool enable)
        {
            var json = new
            {
                action = "set_group_admin",
                echo = "设置管理员",
                @params = new
                {
                    group_id,
                    user_id,
                    enable,
                }
            };
            Bot_SendMsg(json);
        }

        internal void Get_friend_list()
        {
            var json = new
            {
                action = "get_friend_list",
                echo = "friendlist",
            };
            Bot_SendMsg(json);
        }

        internal void Get_group_list()
        {
            var json = new
            {
                action = "get_group_list",
                echo = "grouplist",
            };
            Bot_SendMsg(json);
        }

        internal void Clean_cache()
        {
            var json = new
            {
                action = "clean_cache",
                echo = "清理缓存",
            };
            Bot_SendMsg(json);
        }

        internal void Send_group_JSON(string group_id, string JSONstring)
        {
            var json = new
            {
                action = "send_group_msg",
                echo = "发送群JSON",
                @params = new
                {
                    group_id,
                    message = new
                    {
                        type = "json",
                        data = new
                        {
                            data = JSONstring,
                        }
                    }
                }
            };
            Send++;
            Bot_SendMsg(json);
        }

        internal void Send_group_XML(string group_id, string XMLstring)
        {
            var json = new
            {
                action = "send_group_msg",
                echo = "发送群XML",
                @params = new
                {
                    group_id,
                    message = new
                    {
                        type = "xml",
                        data = new
                        {
                            data = XMLstring,
                        }
                    }
                }
            };
            Send++;
            Bot_SendMsg(json);
        }

        internal void Get_QQstatus()
        {
            var json = new
            {
                action = "get_status",
                echo = "status",
            };
            Bot_SendMsg(json);
        }

        internal void Get_QQ_info()
        {
            var json = new
            {
                action = "get_login_info",
                echo = "info",
            };
            Bot_SendMsg(json);
        }

        internal void Get_msg(string message_id)
        {
            var json = new
            {
                action = "get_msg",
                echo = "get_msg",
                @params = new
                {
                    message_id,
                }
            };
            Bot_SendMsg(json);
        }

        internal void Set_group_MuteALL(string group_id, bool enable)
        {
            var json = new
            {
                action = "set_group_whole_ban",
                echo = "全体禁言",
                @params = new
                {
                    group_id,
                    enable,
                }
            };
            Bot_SendMsg(json);
        }

        internal void Set_msg_emoji_like(string message_id, string emoji_id)
        {
            var json = new
            {
                action = "set_group_whole_ban",
                echo = "表情回应",
                @params = new
                {
                    message_id,
                    emoji_id,
                }
            };
            Bot_SendMsg(json);
        }

        internal void Send_group_file(string group_id, string file, string name, string folder)
        {
            var json = new
            {
                action = "upload_group_file",
                echo = "上传群文件",
                @params = new
                {
                    group_id,
                    file,
                    name,
                    //folder,
                }
            };
            Send++;
            Bot_SendMsg(json);
        }

        internal void set_group_leave(string group_id, bool is_dismiss)
        {
            var json = new
            {
                action = "set_group_leave",
                echo = "退出群聊",
                @params = new
                {
                    group_id,
                    is_dismiss,//是否解散, 如果登录号是群主, 则仅在此项为 true 时能够解散
                }
            };
            Bot_SendMsg(json);
        }

        internal void Send_private_file(string user_id, string file, string name)
        {
            var json = new
            {
                action = "upload_private_file",
                echo = "上传私聊文件",
                @params = new
                {
                    user_id,
                    file,//文件路径
                    name,
                }
            };
            Bot_SendMsg(json);
        }

        internal void Get_cookies(string domain)
        {
            var json = new
            {
                action = "get_cookies",
                echo = "cookies",
                @params = new
                {
                    domain,
                }
            };
            Bot_SendMsg(json);
        }

        internal void Set_group_special_title(string group_id, string user_id, string special_title)
        {
            var json = new
            {
                action = "set_group_special_title",
                echo = "专属头衔",
                @params = new
                {
                    group_id,
                    user_id,
                    special_title,
                }
            };
            Bot_SendMsg(json);
        }
    }
}