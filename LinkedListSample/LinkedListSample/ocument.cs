using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListSample
{
    public class Document
    {
        public Document(string title, string content, byte priority)
        {
            Title = title;
            Content = content;
            Priority = priority;
        }

        public string Title { get; }
        public string Content { get; }
        public byte Priority { get; }
    }
}
