GitStatter
===

GitStatter is a tool that allows you to see total changes, insertions and deletions in a Git repository.
It is a multi-platform solution since it runs on [DNX][1]

# First time setup
- Download and install [DNX][1] if you haven't done that already
- Add `dotnet.exe` to your `PATH` if you haven't used the installer (for example Linux distros don't have an installer)
- Make an empty directory. Open command line or terminal with `git` in its `PATH`
- Do `git init` and `git remote add origin https://github.com/Sorashi/GitStatter`
- Do `git pull origin master`
- Restore NuGet packages with `dotnet restore`

# Usage
Do `dotnet run <directory> (--generate-output (-g))`.
Examples:
- `dotnet run "../GitTest"` writes out stats from the `GitTest` repository into the console.
- `dotnet run "../GitTest" -g` writes out stats from the `GitTest` repository into the console and makes three files in the current directory:
    - `changes.txt` which contains something like `10 changes`
    - `insertions.txt` which contains something like `100 insertions`
    - `deletions.txt` which contains something like `20 deletions`

[1]: https://www.microsoft.com/net/download