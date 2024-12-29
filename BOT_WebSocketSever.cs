using BOT_API_List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;

internal class WebSocketServer
{
    private string wsURL;
    private BOTMsgQueue BOTMsg;
    private HttpListener listener = null;

    public async void Start(int port)
    {
        string url = $"http://127.0.0.1:{port}/";
        if (BOT_API.severstart)
        {
            MessageBox.Show("WebSocketSever: 运行中");
            return;
        }

        listener = new HttpListener();
        listener.Prefixes.Add(url);
        try
        {
            listener.Start();
        }
        catch
        {
            BOT_API.severstart = false;
            MessageBox.Show($"{port}:开启失败,请检查端口是否开放");
            return;
        }
        BOT_API.BOTList_WebSocket.Clear();
        wsURL = url;
        //异步 消息收发处理队列
        BOTMsg = new BOTMsgQueue();
        BOTMsg.Msgstart();
        BOTMsg.sendstart();
        MySvrForm.BOT_LoglistADD("WebSocket", "WebSocket", "WebSocket", "Start", wsURL);
        MySvrForm.mForm.TabListGmaneSet.SelectedTab = MySvrForm.mForm.TabListGmaneSet.TabPages[0];
        while (MySvrForm.mForm.openWS)
        {
            try
            {
                HttpListenerContext context = await listener.GetContextAsync();

                if (context.Request.IsWebSocketRequest && context.Request.IsLocal)
                {
                    var headers = context.Request.Headers;
                    // var userAgent = headers["User-Agent"];
                    var Self_ID = headers["X-Self-ID"];
                    Console.WriteLine(Self_ID);
                    HttpListenerWebSocketContext wtext = await context.AcceptWebSocketAsync(null);
                    BOT_LIST bOT_LIST = new BOT_LIST
                    {
                        Self_ID = Self_ID,
                        Self_WebSocket = wtext.WebSocket
                    };
                    BOT_API.BOTList_WebSocket.Add(bOT_LIST);
                    _ = ReceiveMessages_S(bOT_LIST);
                }
                else
                {
                    context.Response.StatusCode = 400;
                    context.Response.Close();
                }
            }
            catch (Exception)
            {
            }
        }
    }

    private async Task ReceiveMessages_S(BOT_LIST BOT)
    {
        byte[] buffer = new byte[1024];
        List<byte> messageBuffer = new List<byte>();
        WebSocketReceiveResult result = await BOT.Self_WebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        try
        {
            while (!result.CloseStatus.HasValue && BOT.Self_WebSocket.State == WebSocketState.Open)
            {
                messageBuffer.AddRange(buffer.Take(result.Count));
                if (result.EndOfMessage)
                {
                    string message = Encoding.UTF8.GetString(messageBuffer.ToArray());

                    BOT_msgWS bOT_MsgWS = new BOT_msgWS();
                    bOT_MsgWS.message = message;
                    bOT_MsgWS._WebSocket = BOT.Self_WebSocket;
                    bOT_MsgWS.Self_ID = BOT.Self_ID;
                    BOT_API.ReceiveMessage_Queue.Enqueue(bOT_MsgWS);
                    messageBuffer.Clear();
                }
                result = await BOT.Self_WebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
        }
        catch (Exception Ex)
        {
            for (int i = 0; i < BOT_API.BOTList_WebSocket.Count; i++)
            {
                if (BOT_API.BOTList_WebSocket[i].Self_ID == BOT.Self_ID)
                {
                    MySvrForm.BOT_LoglistADD("WebSocket", "WebSocket", BOT.Self_ID, "已断开", Ex.Message);
                    BOT_API.BOTList_WebSocket.RemoveAt(i);
                    break;
                }
            }
        }
    }

    public void stop()
    {
        foreach (var item in BOT_API.BOTList_WebSocket)
        {
            item.Self_WebSocket?.CloseAsync(WebSocketCloseStatus.NormalClosure, "Server shutting down", CancellationToken.None);
        }
        listener?.Stop();
        listener?.Close();
        //BOT_API.botWebSocket?.CloseAsync(WebSocketCloseStatus.NormalClosure, "Server shutting down", CancellationToken.None);
    }
}