﻿namespace Hale.Core.Utils
{
    using System;
    using NLog;

    public class TraceLogger
    {
        private ILogger log;
        private DateTime last;

        public TraceLogger(string name)
        {
#if DEBUG
            this.log = LogManager.GetLogger("TL:" + name);
            this.last = DateTime.Now;
#endif
        }

        public void Trace(string label)
        {
#if DEBUG
            this.log.Trace(label + ": " + (DateTime.Now - this.last).ToString("ss\\.ffff"));
            this.last = DateTime.Now;
#endif
        }

        public void Reset()
        {
#if DEBUG
            this.last = DateTime.Now;
#endif
        }
    }
}
