using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if(args.Length < 1){
                Console.WriteLine("Usage: gitstatter <directory> [--generate-output (-g)]");
                return;
            }
            var dir = args[0];
            var makeOutput = args.Length == 2 && (args[1] == "--generate-output" || args[1] == "-g");
            if(!Directory.Exists(dir)){
                Console.WriteLine("Specified directory doesn't exist.");
                return;
            }
            var gitStartInfo = new ProcessStartInfo{
                FileName = "git",
                Arguments = "log --max-parents=0 HEAD",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                WorkingDirectory = dir
            };
            var firstCommitProcess = new Process();
            firstCommitProcess.StartInfo = gitStartInfo;
            firstCommitProcess.Start();
            var firstCommitOutput = firstCommitProcess.StandardOutput.ReadToEnd();
            var firstCommitRegex = new Regex(@"commit (?<commit>[a-f0-9]+)");
            var firstCommitMatch = firstCommitRegex.Match(firstCommitOutput);
            if(!firstCommitMatch.Success){
                Console.WriteLine("An error occured. Possible causes:\nThe directory provided is not a git repository\nThere are not any commits in the repository yet");
                return;
            }
            var firstCommit = firstCommitMatch.Groups["commit"].Value;
            gitStartInfo.Arguments = $"diff --shortstat {firstCommit} HEAD";
            var diffProcess = new Process();
            diffProcess.StartInfo = gitStartInfo;
            diffProcess.Start();
            var diffProcessOutput = diffProcess.StandardOutput.ReadToEnd();
            //  2032 files changed, 371441 insertions(+), 704 deletions(-)
            var diffRegex = new Regex(@"(?<changes>\d+) files? changed(?:, (?<insertions>\d+) insertions?\(\+\))?(?:, (?<deletions>\d+) deletions?\(\-\))?");
            var diffMatch = diffRegex.Match(diffProcessOutput);
            if(!diffMatch.Success){
                Console.WriteLine("An error occured. Possible causes:\nThere is only one commit in the repository, so there aren't any changes");
                return;
            }
            var changes = diffMatch.Groups["changes"].Value;
            var insertions = diffMatch.Groups["insertions"].Value;
            var deletions = diffMatch.Groups["deletions"].Value;
            if(string.IsNullOrEmpty(changes)) changes = "0";
            if(string.IsNullOrEmpty(insertions)) insertions = "0";
            if(string.IsNullOrEmpty(deletions)) deletions = "0";
            Console.WriteLine($"{changes} changes");
            Console.WriteLine($"{insertions} insertions");
            Console.WriteLine($"{deletions} deletions");
            if(makeOutput){
                File.WriteAllText("changes.txt", $"{changes} changes");
                File.WriteAllText("insertions.txt", $"{insertions} insertions");
                File.WriteAllText("deletions.txt", $"{deletions} deletions");
            }
        }
    }
}
