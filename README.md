Disable Sidebar
===================

A mod for Survivalist: Invisible Strain.

Disables the entire sidebar. Someone wanted this for some reason.

Requirements
------------

* Survivalist: Invisible Strain v109 or later.

Caveats
-------

* Tab panels (e.g. inventory) are not resized.
* This mod currently can't be unloaded correctly without restarting the game. 
  I might fix this eventually, or you could submit a pull request.


Install
-------

1. Subscribe to a Steam Workshop item containing a precompiled DLL version of
   this mod (for instance, the one [here](https://steamcommunity.com/sharedfiles/filedetails/?id=2368628441)).
2. Enable it in your game.

Build (Linux)
-------------

1. Checkout the repository (for best results, to `dev/DisableHUD`
   relative to your Survivalist: Invisible Strain directory, otherwise you have
   to re-point a bunch of assembly references).
2. Create a directory called `3rdparty` and copy `0Harmony.dll` from any source
   providing Harmony 2.0.4 into this new directory.
3. Run `dotnet build` inside `src` directory.
4. Create a Story using S:IS's editor and list as a dependency any Mod 
   providing Harmony 2.0.4 (e.g. [this one](https://steamcommunity.com/sharedfiles/filedetails/?id=2368628441&tscn=1611333251)).
5. From the `src/bin/Debug/net40/` directory, copy `DisableHUD.dll` to your 
   Story's `DLLs` folder (create this folder if it doesn't exist).

License
-------

This mod contains code from Survivalist: Invisible Strain. Bob the P.R. Bot
has confirmed that use of small amounts of such code in non-commercial mods
for Survivalist: Invisible Strain is permissible.

You may use other code from this mod in non-commercial mods for Survivalist: 
Invisible Strain if doing so meets your needs.

You may use compiled versions of this mod in your own instance of Survivalist:
Invisible Strain, and declare Steam Workshop versions of this mod as 
dependencies for your own mods or Stories that you publish to Steam Workshop.

