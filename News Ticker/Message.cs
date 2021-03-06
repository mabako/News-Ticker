﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News_Ticker
{
    public class Message : MessageBase
    {
        private string m_author;
        private string m_subreddit;
        private string m_text;

        public Message(string author, string subreddit, string text)
        {
            m_author = author;
            m_subreddit = subreddit;
            m_text = text;
        }

        public override uint GetSenderID()
        {
            return 0;
        }

        public override string GetSenderName()
        {
            return m_author;
        }

        public override string GetText()
        {
            return string.Format("{0} #{1}", m_text, m_subreddit);
        }

        /// <summary>
        /// We basically want to ensure the same messages aren't shown twice.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool IsSimilarMessage(MessageBase other)
        {
            var m = other as Message;
            return m != null && ((m.m_author == m_author && m.m_subreddit == m_subreddit) || m.m_text.Replace("#", "") == m_text.Replace("#", ""));
        }

        public override void Serialize(ColossalFramework.IO.DataSerializer s)
        {
            s.WriteSharedString(m_author);
            s.WriteSharedString(m_subreddit);
            s.WriteSharedString(m_text);
        }

        public override void Deserialize(ColossalFramework.IO.DataSerializer s)
        {
            m_author = s.ReadSharedString();
            m_subreddit = s.ReadSharedString();
            m_text = s.ReadSharedString();
        }

        public override void AfterDeserialize(ColossalFramework.IO.DataSerializer s)
        {
        }
    }
}
