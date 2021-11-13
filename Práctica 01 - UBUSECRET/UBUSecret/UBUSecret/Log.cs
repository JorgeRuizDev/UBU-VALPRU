using System;
using System.Collections.Generic;
using System.Text;

namespace UBUSecret
{
    public class Log
    {
        private DateTime timestamp;
        private String entry;
        private Level level;

        public Log(String entry, Level level, DateTime timestamp = default)
        {
            this.level = level;
            this.entry = entry;
            timestamp = DateTime.Now;
            this.timestamp = timestamp;
        }

        public DateTime Timestamp { get => timestamp; set => timestamp = value; }
        public string Entry { get => entry; set => entry = value; }
        public Level Level { get => level; set => level = value; }
    }
}
