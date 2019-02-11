using System;
using System.IO;
using System.Linq;
using static System.String;

namespace LineCounting{
    internal class CountLines{
        private static void Main(string[] args){
            if (args.Length == 0){
                Console.WriteLine("Not enough arguments, enter extension.");
                return;
            }
            var countLines = new CountLines(args[0]);
            var numberOfLines = countLines.Count();
            Console.WriteLine($"Number of lines: {numberOfLines}");
        }

        private int Count(){
            var files = Directory.GetFiles(_directory, _extension, SearchOption.AllDirectories);
            return files.Sum(CountLinesInFile);
        }

        private int CountLinesInFile(string file){
            var result = 0;
            using (var fileReader = new StreamReader(file)){
                while (!fileReader.EndOfStream){
                    var line = fileReader.ReadLine();
                    if (line == null) continue;
                    line = DeleteComments(line);
                    if (!IsNullOrEmpty(line)) result++;
                }
            }

            return result;
        }

        private string DeleteComments(string line){
            line = line.Trim();
            if (_isInComment){
                if (!line.Contains(MultiLineCloseComment)) return null;
                line = line.Remove(0,
                    line.IndexOf(MultiLineCloseComment, StringComparison.Ordinal) + MultiLineCloseComment.Length);
                _isInComment = false;
                return DeleteComments(line);
            }

            if (line.StartsWith(SingleLineComment) ||
                (line.StartsWith(MultiLineOpenComment) && line.EndsWith(MultiLineCloseComment))) return null;
            if (line.Contains(MultiLineOpenComment)){
                if (!line.Contains(MultiLineCloseComment)){
                    _isInComment = true;
                    var index = line.IndexOf(MultiLineOpenComment, StringComparison.Ordinal);
                    line = line.Remove(index, line.Length - index);
                }
                else{
                    var index = line.IndexOf(MultiLineOpenComment, StringComparison.Ordinal);
                    line = line.Remove(index,
                        line.IndexOf(MultiLineCloseComment, StringComparison.Ordinal) + MultiLineCloseComment.Length -
                        index);
                    return DeleteComments(line);
                }
            }

            return line;
        }

        private CountLines(string extension){
            _extension = extension;
            _directory = Directory.GetCurrentDirectory();
        }

        private readonly string _extension;
        private readonly string _directory;
        private bool _isInComment;

        private const string SingleLineComment = "//";
        private const string MultiLineOpenComment = "/*";
        private const string MultiLineCloseComment = "*/";
    }
}