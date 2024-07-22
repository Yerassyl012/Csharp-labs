using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var pdm = new PriorityDocumentManager();
            pdm.AddDocument(new Document("one", "Sample", 8));
            pdm.AddDocument(new Document("two", "Sample", 3));
            pdm.AddDocument(new Document("three", "Sample", 4));
            pdm.AddDocument(new Document("four", "Sample", 8));
            pdm.AddDocument(new Document("five", "Sample", 1));
            pdm.AddDocument(new Document("six", "Sample", 9));
            pdm.AddDocument(new Document("seven", "Sample", 1));
            pdm.AddDocument(new Document("eight", "Sample", 1));

            pdm.DisplayAllNodes();
        }
    }
}
