using System;

namespace Sport.Tennis
{
    public class Point
    {
        public Point()
        {
            this.Time = DateTime.Now;
        }

        public DateTime Time { get; set; }
        public string Value { get; set; }
    }
}