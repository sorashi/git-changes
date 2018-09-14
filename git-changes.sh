#!/bin/bash

# SHA of the first found commit without any parents when backtracking from the current HEAD
SHA=`git log --max-parents=0 HEAD | grep -o -m 1 "[0-9a-f]\{40\}"`
# shows the number of insertions, deletions and changed lines from SHA to HEAD
echo `git diff --shortstat "$SHA" HEAD`