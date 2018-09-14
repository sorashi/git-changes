# Git Changes

Git Changes is a git alias or a standalone script, which allows you to quickly see how many insertions, deletions and changes the repository has up until the current `HEAD`.

To save it as an alias, use this command:

```bash
git config --global alias.changes '!git diff --shortstat $(git log --max-parents=0 HEAD | grep -o -m 1 "[0-9a-f]\{40\}") HEAD'
```

Example usage:

```
> git changes
< 26 files changed, 1402 insertions(+), 170 deletions(-)
```
