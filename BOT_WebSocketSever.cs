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
using static WindowsFormsApp1.MySvrForm;

internal class WebSocketServer
{
    private string wsURL;
    private HttpListener listener = null;
    public static object objec = new object();

    public async void Start(int port)
    {
        string url = $"http://127.0.0.1:{port}/";

        listener = new HttpListener();
        listener.Prefixes.Add(url);
        try
        {
            listener.Start();
        }
        catch
        {
            MySvrForm.mForm.openWS = false;
            MessageBox.Show($"{url}   开启失败,请检查端口是否开放");
            return;
        }
        wsURL = url;
        LOGdata lOGdata = new LOGdata
        {
            a = "WebSocketSever",
            b = "WebSocketSever",
            c = "WebSocketSever",
            d = "Start",
            e = wsURL
        };
        MySvrForm.BOT_LoglistADD(lOGdata);
        MySvrForm.TimerStatus();
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
                    HttpListenerWebSocketContext wtext = await context.AcceptWebSocketAsync(null);
                    Self_Client self_Client = new Self_Client();
                    self_Client.Start(wtext.WebSocket, Self_ID);
                    mForm.List_Self_ClientADD(self_Client);
                    _ = ReceiveMessages_S(self_Client);
                }
                else
                {
                    context.Response.StatusCode = 400;
                    context.Response.Close();
                }
            }
            catch (Exception)
            {
                LOGdata la = new LOGdata
                {
                    a = "WebSocketSever",
                    b = "WebSocketSever",
                    c = "WebSocketSever",
                    d = "Stop",
                    e = wsURL
                };
                MySvrForm.BOT_LoglistADD(la);
            }
        }
    }

    private async Task ReceiveMessages_S(Self_Client BOT)
    {
        byte[] buffer = new byte[1024];
        List<byte> messageBuffer = new List<byte>();
        WebSocketReceiveResult result = await BOT.webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        try
        {
            while (!result.CloseStatus.HasValue && BOT.webSocket.State == WebSocketState.Open)
            {
                messageBuffer.AddRange(buffer.Take(result.Count));
                if (result.EndOfMessage)
                {
                    lock (BOT)
                    {
                        string message = Encoding.UTF8.GetString(messageBuffer.ToArray());
                        BOT.ReceiveQueue.Enqueue(message);
                    }

                    messageBuffer.Clear();
                }
                result = await BOT.webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
        }
        catch (Exception Ex)
        {
            mForm.List_Self_ClientDel(BOT, Ex);
        }
    }

    public void stop()
    {
        listener?.Stop();
        listener?.Close();
        //BOT_API.botWebSocket?.CloseAsync(WebSocketCloseStatus.NormalClosure, "Server shutting down", CancellationToken.None);
    }
}