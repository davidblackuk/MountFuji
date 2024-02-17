This is a mofified build of hatari 2.4.1 to allow Mount Fuji to run Hatari via a
file association.

It's a requirement because of how sandboxed processors like Mount Fuji operate

This build of Hatari does not support png graphics or portmide. Let me know if 
this is an issue for you.

Instructions on how to install and configure Maount Fuji for windows is 
available on youtube 

In line with the GPL v3 license I've included the modified sources, in the src 
folder. The only modification is to disable the processing of command line 
arguments, this change is on line 916.
