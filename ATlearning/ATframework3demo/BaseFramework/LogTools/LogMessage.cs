using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace atFrameWork2.BaseFramework.LogTools
{
    public abstract class LogMessage
    {
        public string MsgType { get; protected set; }
        public string Text { get; protected set; }
        Color MessageColor { get; set; }

        protected LogMessage(string msgType, string text, Color messageColor)
        {
            MsgType = msgType ?? throw new ArgumentNullException(nameof(msgType));
            Text = text;
            MessageColor = messageColor;
        }

        public void WriteHtml(string filePath, string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                string hexColor = MessageColor.R.ToString("X2") + MessageColor.G.ToString("X2") + MessageColor.B.ToString("X2");
                File.AppendAllText(filePath, $"<div style=\"color: #{hexColor}\">{text?.Trim()}</div>\r\n");
            }
        }
    }
}
