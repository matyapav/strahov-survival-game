#!/bin/bash
# Ask the user for commit message
echo Specify commit message:
read msg
git checkout master
git pull origin master
git checkout svnsync
git svn rebase
git merge --no-ff master
git commit -m "$msg"
git svn dcommit -m "$msg"