# My Atari Collection

To be noted
- We only import and support simple disks at the mo, the assumption is block size = 512
- We're not importing VDI resolutions yet

## Paths

This is where the data files associated with both Mount Fuji and Hatari are usually stored in the app. Check out the about dialog for the 
settings on your machine for the Mount Fuji's one.

### PC
- Mount fuji files
    - C:\Users\DAVID BLACK\AppData\Local\Packages\com.overtakenbyevents.mountfuji_9zz4h110yvjzm\LocalState\fuji\
- Hatari config file
    - C:\Users\DAVID BLACK\AppData\Local\Hatari\hatari.cfg

### MAC
- Mount fuji files
  - /Users/davidblack/Library/fuji/
- Hatari config file
  - /Users/davidblack/Library/Application Support/Hatari/hatari.cfg


## Thoughts

ROM image is an interesting example of a rule that nees to be global in nature. It 
can be null (as it is by default) but we need to indicate not runnable.

So do we need a global rules checker and an error indicator on the config panel and
the system list entry?

Does that get into the area of error ckecking CRCs for ROMS?

