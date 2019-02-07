using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace LineCounting
{
    internal class CountLines
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Not enough arguments, enter extension.");
                return;
            }
            var countLines = new CountLines(args[0]);
            var numberOfLines = countLines.Count();
            Console.WriteLine($"Number of lines: {numberOfLines}");
        }
        private int Count()
        {
            var files = Directory.GetFiles(_directory, _extension, SearchOption.AllDirectories);
            return files.Sum(CountLinesInFile);
        }

        private static int CountLinesInFile(string file)
        {
            var result = 0;
            using (var fileReader = new StreamReader(file))
            {
                var isInComment = false;
                while (!fileReader.EndOfStream)
                {
                    var line = fileReader.ReadLine();
                    if (line == null) continue;
                    if (isInComment)
                    {
                        if (!EndMultilineCommentRegex.IsMatch(line)) continue;
                        line = EndMultilineCommentRegex.Replace(line, "");    
                        isInComment = false;
                    }
                    else if (SingleLineCommentRegex.IsMatch(line)) continue;
                    else if(MultilineCommentRegex.IsMatch(line))
                    {
                        line = MultilineCommentRegex.Replace(line, "");
                    }
                    else if(StartMultilineCommentRegex.IsMatch(line))
                    {
                        isInComment = true;
                        line = StartMultilineCommentRegex.Replace(line, "");
                    }
                    if (!SpacesRegex.IsMatch(line)) result++; 
                }
            }
           return result;
        }
        private CountLines(string extension)
        {
            _extension = extension;
            _directory = Directory.GetCurrentDirectory();
        }
        
        private readonly string _extension;
        private readonly string _directory;
        
        private static readonly Regex SingleLineCommentRegex = new Regex("//.*$", RegexOptions.Compiled);     
        private static readonly Regex MultilineCommentRegex = new Regex("^.*/\\*.*\\*/.*$", RegexOptions.Compiled);     
        private static readonly Regex SpacesRegex = new Regex("^\\s*$", RegexOptions.Compiled);
        private static readonly Regex EndMultilineCommentRegex = new Regex("^.*\\*/", RegexOptions.Compiled);                      
        private static readonly Regex StartMultilineCommentRegex = new Regex("/\\*.*$", RegexOptions.Compiled);                     
    }
}