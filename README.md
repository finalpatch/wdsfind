wdsfind
=======

Locate files using windows search.

To make it work with Emacs M-x locate command, put this in the .emacs file:

(setq locate-command "wdsfind.exe")

or if you use Helm: (setq helm-locate-command "wdsfind %s %s")
