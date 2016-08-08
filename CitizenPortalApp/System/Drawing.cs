using System.IO;

namespace System
{
    public class Drawing
    {
        public static object Imaging { get; internal set; }

        public class Image
        {
            internal void Save(MemoryStream ms, object gif)
            {
                throw new NotImplementedException();
            }
        }
    }
}