using BOT_ReceiveMsg_T;

using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsFormsApp1;

namespace BOT_API_List
{
    internal class BOTMsgQueue
    {
        public static object objectint = new object();

        public async void Msgstart()
        {
            await Bot_MsgQueueAsync();
        }

        public async void sendstart()
        {
            await Bot_sendQueueAsync();
        }

        private async Task Bot_MsgQueueAsync()
        {
            while (MySvrForm.mForm.openWS)
            {
                if (BOT_API.ReceiveMessage_Queue.Count > 0)
                {
                    try
                    {
                       
                        if (BOT_API.ReceiveMessage_Queue.TryDequeue(out BOT_msgWS result))
                        {//处理收到的消息
                            BOT_ReceiveMsg.BOT_MessageParsing(result);
                            MySvrForm.mForm.BeginInvoke(new Action(() => { MySvrForm.mForm.label_Revsend.Text = $"收:{BOT_API.Revint}  发:{BOT_API.sendint}"; }));
                        }
                    }
                    catch (Exception ex)
                    {
                        MySvrForm.BOT_LoglistADD("BOT", "BOT", "BOT", "处理异常", $"{ex.Message}\n\n{ex.StackTrace}");
                    }
                }
                await Task.Delay(10);
            }
            Console.WriteLine("接收结束");
        }

        private async Task Bot_sendQueueAsync()
        {
            int delayTime;
            while (MySvrForm.mForm.openWS)
            {
                delayTime = 10;
                try
                {
                    if (BOT_API.SendMessage_Queue.TryDequeue(out byte[] result))
                    {
                        _ = BOT_API.botWebSocket.SendAsync(new ArraySegment<byte>(result), WebSocketMessageType.Text, true, CancellationToken.None);
                       
                        delayTime = 20;
                    }
                }
                catch (Exception ex)
                {
                    MySvrForm.BOT_LoglistADD("BOT", "BOT", "BOT", "发送异常", $"{ex.Message}\n\n{ex.StackTrace}");
                    delayTime = 10;
                }
                await Task.Delay(delayTime);
            }
            Console.WriteLine("发送结束");
        }
    }
    class BOT_LIST
    {
        public string Self_ID;
        public WebSocket Self_WebSocket;
    }
    class BOT_msgWS
    {
        public string message;
        public WebSocket _WebSocket;
        public string Self_ID;
    }
    internal class BOT_API
    {
        public static bool severstart = false;
        public static int Revint;
        public static int sendint;
        public static WebSocket botWebSocket = null;
        public static ConcurrentQueue<byte[]> SendMessage_Queue = new ConcurrentQueue<byte[]>();
        public static ConcurrentQueue<BOT_msgWS> ReceiveMessage_Queue = new ConcurrentQueue<BOT_msgWS>();
        public static List<BOT_LIST> BOTList_WebSocket = new List<BOT_LIST>();
        private static readonly Dictionary<string, string> replacements = new Dictionary<string, string>
        {
            { "&amp;", "&" },
            { "&#91;", "[" },
            { "&#93;", "]" },
            { "&#44;", "," }
        };

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

        private static void Bot_SendMsg(object obj, WebSocket WebSocket)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            //SendMessage_Queue.Enqueue(Encoding.UTF8.GetBytes(json));
            WebSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(json)), WebSocketMessageType.Text, true, CancellationToken.None);
            if (MySvrForm.mForm.checkBox_BOTAPIMSG.Checked)
            {
                MySvrForm.BOT_LoglistADD("BOT", "BOT", "BOT", "原始消息(发)", json);
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

        internal static string CQ_image(byte[] jpg)
        {
            string strbase64 = Convert.ToBase64String(jpg);
            return $"[CQ:image,file=base64://{strbase64},summary=❤️]";
        }

        internal static string CQ_imageBese64(string Bese64)
        {
            return $"[CQ:image,file=base64://{Bese64},summary=❤️]";
        }

        internal static byte[] Get_user_Image(string user_id)
        {
            string url = $"https://q4.qlogo.cn/g?b=qq&nk={user_id}&s=140";
            using (WebClient webClient = new WebClient())
            {
                byte[] imageData = webClient.DownloadData(url);
                return imageData;
            }
        }

        internal static void Send_group_msg(string message, string group_id, WebSocket WebSocket)
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
            sendint++;
            Bot_SendMsg(json, WebSocket);
        }

        internal static void Send_private_msg(string message, string user_id, WebSocket WebSocket)
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
            sendint++;
            Bot_SendMsg(json, WebSocket);
        }

        internal static void Get_version_LLoneBot(WebSocket WebSocket)
        {
            var json = new
            {
                action = "get_version_info",
                echo = "version",
            };
            Bot_SendMsg(json, WebSocket);
        }

        internal static void Get_stranger_info(string user_id, WebSocket WebSocket)
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
            Bot_SendMsg(json, WebSocket);
        }

        internal static void Delete_group_msg(string message_id, WebSocket WebSocket)
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
            Bot_SendMsg(json, WebSocket);
        }

        internal static void set_group_ban(string group_id, string user_id, string duration, WebSocket WebSocket)
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
            Bot_SendMsg(json, WebSocket);
        }

        internal static void Send_group_video(string group_id, byte[] videoByte, WebSocket WebSocket)
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
            Bot_SendMsg(json, WebSocket);
        }

        internal static void Send_group_music(string group_id, string url, string content, string audio, string title, string image, WebSocket WebSocket)
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
            sendint++;
            Bot_SendMsg(json, WebSocket);
        }

        internal static void Get_group_member_list(string group_id, WebSocket WebSocket)

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
            Bot_SendMsg(json, WebSocket);
        }

        internal static void Set_group_kick(string group_id, string user_id, bool reject_add_request, WebSocket WebSocket)
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
            Bot_SendMsg(json, WebSocket);
        }

        internal static void Set_group_add_request(string flag, string reason, bool approve, WebSocket WebSocket)
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
            Bot_SendMsg(json, WebSocket);
        }

        internal static void Set_group_admin(string group_id, string user_id, bool enable, WebSocket WebSocket)
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
            Bot_SendMsg(json, WebSocket);
        }

        internal static void Get_friend_list( WebSocket WebSocket)
        {
            var json = new
            {
                action = "get_friend_list",
                echo = "friendlist",
            };
            Bot_SendMsg(json, WebSocket);
        }

        internal static void Get_group_list( WebSocket WebSocket)
        {
            var json = new
            {
                action = "get_group_list",
                echo = "grouplist",
            };
            Bot_SendMsg(json, WebSocket);
        }

        internal static void Clean_cache( WebSocket WebSocket)
        {
            var json = new
            {
                action = "clean_cache",
                echo = "清理缓存",
            };
            Bot_SendMsg(json,  WebSocket);
        }

        internal static void Send_group_JSON(string group_id, string JSONstring, WebSocket WebSocket)
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
            sendint++;
            Bot_SendMsg(json, WebSocket);
        }

        internal static void Send_group_XML(string group_id, string XMLstring, WebSocket WebSocket)
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
            sendint++;
            Bot_SendMsg(json, WebSocket);
        }

        internal static void Get_QQstatus( WebSocket WebSocket)
        {
            var json = new
            {
                action = "get_status",
                echo = "status",
            };
            Bot_SendMsg(json, WebSocket);
        }

        internal static void Get_QQ_info(WebSocket WebSocket)
        {
            var json = new
            {
                action = "get_login_info",
                echo = "info",
            };
            Bot_SendMsg(json, WebSocket);
        }

        internal static void Get_msg(string message_id, WebSocket WebSocket)
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
            Bot_SendMsg(json, WebSocket);
        }

        internal static void Set_group_MuteALL(string group_id, bool enable, WebSocket WebSocket)
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
            Bot_SendMsg(json, WebSocket);
        }

        internal static void Set_msg_emoji_like(string message_id, string emoji_id, WebSocket WebSocket)
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
            Bot_SendMsg(json, WebSocket);
        }

        internal static void Send_group_file(string group_id, string file, string name, string folder, WebSocket WebSocket)
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
            sendint++;
            Bot_SendMsg(json, WebSocket);
        }

        internal static void set_group_leave(string group_id, bool is_dismiss, WebSocket WebSocket)
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
            Bot_SendMsg(json, WebSocket);
        }

        internal static void Send_private_file(string user_id, string file, string name, WebSocket WebSocket)
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
            sendint++;
            Bot_SendMsg(json,  WebSocket);
        }

        internal static void Get_cookies(string domain, WebSocket WebSocket)
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
            Bot_SendMsg(json, WebSocket);
        }

        internal static void Set_group_special_title(string group_id, string user_id, string special_title, WebSocket WebSocket)
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
            Bot_SendMsg(json, WebSocket);
        }
    }
}