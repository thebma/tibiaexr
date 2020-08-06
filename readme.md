A pet project to extract Tibia sprite and dat files (soon™️) via a command line interface.
It's far from done, don't expect too much of it (yet!).

Features:
- Extract sprites for various versions in either bitmap or png output.
- Compare sprites from different versions.

Planned:
- Create a library to support in-memory editing of sprites.
- Extract .dat file.
- Generate spritesheets, unity ready.
- Bake our own metadata to break up the spritesheets into smaller ones.
- A "sprite as a whole" mode, creates composite sprites if they require 2x2. (i.e. a cyclops sprite is usually 4 sprites, with this mode you can extract it as one sprite.)
- Support Tibia 11 or higher, requires a LZMA step.
- Repacking the .spr file with modified sprites (and subsequently update the .dat file to accomendate for the new data).

Supported:
Tibia 7.0, Signature: 0x3D6D5CA3
Tibia 7.4, Signature: 0x41B9EA86
Tibia 7.6, Signature: 0x439852BE
Tibia 8.6 (old?), Signature: 0x4C220594 

If you want additional versions, please make a request by making an issue.
This way I can double check it if it works or not.