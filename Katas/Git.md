# Source
https://gitexercises.fracz.com/

# [Correction](https://logicatcore.github.io/scratchpad/git/2021/03/04/git-exercises.html)
## Exercise 1: master
```sh
$> git start
$> git verify
```

## Exercise 2: commit-one-file
```sh
$> git add A.txt
# or 
$> git add B.txt
$> git commit -m "add one file"
$> git verify
```

## Exercise 3: commit-one-file-staged

```sh
$> git reset HEAD A.txt
# or
$> git reset HEAD B.txt
$> git commit -m "destage one file"
$> git verify
```

## Exercise 4: ignore-them
```sh
$> nano .gitignore
*.exe
*.o
*.jar
libraries/
$> git add .
$> git commit -m "commit useful files"
$> git verify
```

## Exercise 5: chase-branch
```sh
$> git checkout chase-branch
$> git merge escaped
$> git verify
```

## Exercise 6: merge-conflict
```sh
$> git checkout merge-conflict
$> git merge another-piece-of-work
$> nano equation.txt

2 + 3 = 5

$> git add equation.txt
$> git commit -m "merge and resolve"
$> git verify
```

## Exercise 7: save-your-work

To learn about stashing and cleaning [https://git-scm.com/book/en/v2/Git-Tools-Stashing-and-Cleaning](https://git-scm.com/book/en/v2/Git-Tools-Stashing-and-Cleaning)
```sh
$> git stash
# or
$> git stash push
$> nano bug.txt # in the text editor delete the bud line
$> git commit -am "remove bug"
$> git stash apply
# or
$> git stash apply stash@{0}
$> nano bug.txt # in the text editor add the line "Finally, finished it!" to the end
$> git commit -am "finish"
$> git verify
```

## Exercise 8: change-branch-history

To learn about git rebase [https://git-scm.com/docs/git-rebase](https://git-scm.com/docs/git-rebase)
À parcourrir pour édition historique: https://www.atlassian.com/git/tutorials/rewriting-history#:~:text=The%20git%20commit%20%2D%2Damend,message%20without%20changing%20its%20snapshot.
```sh
$> git checkout change-branch-history
$> git rebase hot-bugfix
$> git verify
```

## Exercise 9: remove-ignored

Solution and explanation [https://stackoverflow.com/questions/1274057/how-to-make-git-forget-about-a-file-that-was-tracked-but-is-now-in-gitignore](https://stackoverflow.com/questions/1274057/how-to-make-git-forget-about-a-file-that-was-tracked-but-is-now-in-gitignore)
```sh
$> git rm --cached ignored.txt
$> git commit -am "untrack ignored.txt"
$> git verify
```

## Exercise 10: case-sensitive-filename
Selector: https://git-scm.com/docs/git-rev-parse#Documentation/git-rev-parse.txt-emltrevgtltngtemegemHEADv1510em 
et explication textuelles: https://stackoverflow.com/a/2222920
```sh
$> git reset HEAD^
$> mv File.txt file.txt
$> git add file.txt
$> git commit -m "lowercase filename"
$> git verify
```

## Exercise 11: fix-typo
Note: **--amend** replaces the tip of the current branch by creating a new commit.
Revoir explications: https://www.atlassian.com/git/tutorials/rewriting-history#:~:text=The%20git%20commit%20%2D%2Damend,message%20without%20changing%20its%20snapshot.
```sh
# fix typo in the file
$> git commit -a --amend
# fix the typo in commit message
$> git verify
```

## Exercise 12: forge-date (most useless exercise, but shows that git is not a monolith)
```sh
$> git commit --amend --no-edit --date="1987-08-03"
```

## Exercise 13: fix-old-typo
```sh
$> git rebase -i HEAD^^
# change "pick" to "edit" where the typo is in the commit message
# fix the typo in the file
$> git add file.txt
$> git rebase --continue
# fix the rebase conflict
$> git add file.txt
$> git reabse --continue
$> git verify
```

## Exercise 14: commit-lost
```sh
$> git reflog
$> git reset --hard HEAD@{1}
$> git verify
```

## Exercise 15: split-commit
```sh
$> git reset HEAD^
$> git add first.txt
$> git commit -m "First.txt"
$> git add second.txt
$> git commit -m "Second.txt"
$> git verify
```

## Exercise 16: too-many-commits
```sh
$> git rebase -i HEAD~4
# replace "pick" with "squash" for the commit with the message "Crap, I have forgotten to add this line." 
# leave a commit message same as the one with which the marked commit is getting squashed with i.e.,
# "Add file.txt"
$> git verify
```

## Exercise 17: executable

Under the hood details [https://stackoverflow.com/questions/40978921/how-to-add-chmod-permissions-to-file-in-git](https://stackoverflow.com/questions/40978921/how-to-add-chmod-permissions-to-file-in-git)
```sh
$> git update-index --chmod=+x script.sh
$> git commit -m "make executable"
$> git verify
```

## Exercise 18: commit-part
```sh
$> git add --patch file.txt
# split the hunks with 's'
# Stage this hunk [y,n,q,a,d,/,j,J,g,s,e,?]?
# 
# Here is a description of each option:
# 
#     y stage this hunk for the next commit
#     n do not stage this hunk for the next commit
#     q quit; do not stage this hunk or any of the remaining hunks
#     a stage this hunk and all later hunks in the file
#     d do not stage this hunk or any of the later hunks in the file
#     g select a hunk to go to
#     / search for a hunk matching the given regex
#     j leave this hunk undecided, see next undecided hunk
#     J leave this hunk undecided, see next hunk
#     k leave this hunk undecided, see previous undecided hunk
#     K leave this hunk undecided, see previous hunk
#     s split the current hunk into smaller hunks
#     e manually edit the current hunk
#     ? print hunk help

# select each hunk with 'y' or 'n'
$> git commit -m "task 1 related"
$> git commit -am "rest of the content"
$> git verify
```

## Exercise 19: pick-your-features
```sh
# get an idea of the logs currently and know the SHA-1's needed
$> git log --oneline --decorate --graph --all -8
$> git checkout pick-your-features

$> git cherry-pick feature-a 
# or
$> git cherry pick SHA-1 of feature-a commit

$> git cherry-pick feature-b 
# or
$> git cherry pick SHA-1 of feature-b commit

$> git merge --squash feature-c
# resolve conflict
$> git commit -am "Complete Feature C"
$> git verify
```

## Exercise 20: reabse-complex

Explanation from git-book [https://git-scm.com/book/en/v2/Git-Branching-Rebasing](https://git-scm.com/book/en/v2/Git-Branching-Rebasing)
```sh
$> git rebase --onto your-master issue-555 rebase-complex 
# This basically says, “Take the rebase-complex branch, figure out the patches since it diverged from the issue-555 branch, 
# and replay these patches in the rebase-complex branch as if it was based directly off the your-master branch instead.” 
$> git verify
```

## Exercise 21: invalid-order
```sh
$> git rebase -i HEAD~4
# reorder the commit messages as needed
$> git verify
```

## Exercise 22: find-swearwords
```sh
$> git log -S shit
# make a note of the commits where a word "shit" was introduced
$> git rebase -i HEAD~105
# replace 'pick' with 'edit' for those commits

# check which files were modified
$> git log -p -1
# replace 'shit' with 'flower' in list.txt
$> git add list.txt
$> git commit --amend
$> git rebase --continue

# check which files were modified
$> git log -p -1
# replace 'shit' with 'flower' in words.txt
$> git add words.txt
$> git commit --amend
$> git rebase --continue

# check which files were modified
$> git log -p -1
# replace 'shit' with 'flower' in words.txt
$> git add words.txt
$> git commit --amend
$> git rebase --continue

$> git verify
```

## Exercise 23: find-bug

1.  First method using _git bisect_
```sh  
    $> git checkout find-bug
    $> git bisect start
    $> git bisect bad
    $> git bisect good 1.0
    # the grep documentation for -v flag doesn't make sense with what the author fracz mentioned and also
    # I couldn't see the binary output of '1' or '0' as given in the hints
    $> git bisect run sh -c "openssl enc -base64 -A -d < home-screen-text.txt | grep -v jackass"
    $> git push origin 4d2725ac4c874dbb207770001def27aed48e9ddb:find-bug
```
2.  Second method using **gitpython**
    a. Brute-force method iterate through the commits one-by-one from oldest to the newest and break when the word 'jackass' got introduced
```python
#!/usr/bin/env python
# -*- coding:utf-8 -*-

from git import Repo
import base64

# create a repo object to query
repo = Repo("./exercises/")
# switch to the required branch
repo.git.checkout('find-bug')
# get all commits between HEAD and tag 1.0
commits = repo.iter_commits('HEAD...1.0')

for x in reversed(list(commits)):
    # move the head to the commit 'x'
    repo.head.set_reference(x)
    # get a tree object and query the blog based on the file name and read the content from it's data stream
    read_this = repo.tree(x)['home-screen-text.txt'].data_stream.read()

    # reader = repo.config_reader()
    # read_this = open('./exercises/home-screen-text.txt')

    # decode the base64 bytes
    content = base64.b64decode(read_this).decode('ascii')
    # check if 'jackass' is present in the file, if yes, print the SHA-1 and exit
    if 'jackass' in content:
        print("The commit where the bug 'jackass' was introduced is:", x.hexsha)    
        break

```

b. Binary search method (newest first), very similar to what the *git bisect* command does
```python

#!/usr/bin/env python
# -*- coding:utf-8 -*-

from git import Repo
import base64

# create a repo object to query
repo = Repo("./exercises/")
# switch to the required branch
repo.git.checkout('find-bug')
# get all commits between HEAD and tag 1.0
commits = repo.iter_commits('HEAD...1.0')

commits = list(commits)
search_length = len(commits)
idx = search_length//2
last_idx = 0
found_at = None

for i in range(0, round(math.log2(search_length))):
    repo.head.set_reference(commits[idx])
    read_this = repo.tree(commits[idx])['home-screen-text.txt'].data_stream.read()
    content = base64.b64decode(read_this).decode('ascii')

    if 'jackass' in content:
        found_at = idx
        print(commits[idx].hexsha)
        if i == 0:
            update = (search_length - idx)//2
        else:
            update = (last_idx - idx)//2
        last_idx = idx
        idx += abs(update)
    else:
        update = (idx - last_idx)//2 
        last_idx = idx
        idx -= abs(update) 

print("The commit where the bug 'jackass' was introduced is:", commits[found_at].hexsha)
```