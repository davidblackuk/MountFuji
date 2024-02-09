
<p align="center">
  <img src="Img/logo-round.png"  />
</p>

# Mount Fuji


Mount Fuji is a a front end for Hatari allowing you to edit different configurations for multiple 32 bit Atari machines and to execute them easily.

Fuji allow you to quickly find, see the configuration of and launch different Hatari Systems.

Fuji is open sourced under the GNU General Public license v3.0. So feel free to have at it!.

## The why?

Hatari is capable of emulating different 32 bit computers like the ST, TT and Falcon. When using Hatari it's common to have multiple config files for different systems. 

You might have a basic ST configuration for playing games: Floppy only, TOS 1.04 and just 512K of memory, perfect for maximum game compatibility.

Then again for productivity apps you might have a Mega STE running at 16MHz with a Gigabyte hard disk and 4MD of memory.

At the top end you might have a Falcon with 256 colour Videl graphics, a 68030 processor and 10MB of Memory.

And don't get me started on TTs with a gig of fast ram and a 68030 processor running at 32MHz!

Of course there's a metric crap ton of possible configurations in-between. And there in lies the rub. Managing all these configurations is not without it's problems. 

I always accrue loads of configs for different setups, rarely are they well named and I find myself hunting through them to find out what they are. Or I pick one that's named 'Gaming ST' and find out it's configured as a TT with a monochrome monitor :-)

To discover what the actual configuration of an emulated system is, involves clicking through the various Hatari config screens to see  what's what.

![A screen shot of Hatari's configuration dialog](Img/hatari-options.png)

Mount Fuji was birthed out of my lack of organization and like any problem that a developer has, the solution wasn't, get more organized, it was write some code!

So Fuji was born and has started growing. It's certainly not finished yet, but it's at a point where I am using it most of the time for most configurations. We'll cover the gaps later.

![Mount fuji running in all it's glory](Img/fuji-running.png)

Mount Fuji is implemented in .NET Maui a somewhat cross platform development system, at the moment I'm targeting MacOS (shown) and Windows. Windows as it stands isn't ready for release yet, but hopefully will be soon.

Unfortunately Maui does not support Linux currently, so that will remain a future goal if Microsoft get their finger out and support it. I wouldn't hold your breath for that.


## The what

