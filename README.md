# EMU twitch bot
## Introduction
This bot will read chat messages as input.
Will process them and do stuff on the host machine.

The end goal is to run any _simple_ emulator (DS, nes, etc) by broadcasting keys to said emulator
A simple feature includes chaos / order (affects time between messages to let twitch chat take action)

## Current
The bot is for now in dev.
~It should not be so hard to develop and should be a lot of fun~.

I've reached a wall, I can make this program send keys to OverWatch, World of Warcraft, any recent game, but the DeSmuMe window doesn't handle Windows Message... Only DirectInput, and I don't know how to send keys to an inactive window via DirectInput

## Next steps:
- Run without any bugs
- Easy install & use for personnal projects
- Setup patreon

## How to use
### For Streamer
- Install any DS emulator (DeSmuME was used to test)
- Install the ROM for your game
- Create a 'settings' file next to executable
- Run executable

A default 'settings' looks like this:
```
channel: #streamer_live_name    # (For Example: #Xiaojiba)
password: oauth:.........       # (get this from https://twitchapps.com/tmi/)
window name: desmume            # (if you use DeSmuME)
```

More [here](https://github.com/erwanvivien/stream-chat-bot-dotnet/blob/main/streamer/README.md)

### For chat
- 'Up', 'Down', 'Right' and 'Left' can be used in the chat to move character (Can be changed by streamer)
- 'A', 'B', 'X' and 'Y' can be used to make character do some actions
- 'W' is L (behind left) and 'C' is R (behind right)

More [here](https://github.com/erwanvivien/stream-chat-bot-dotnet/blob/main/chat/README.md)

### Specifications
If you want to edit it on WSL:
Install SDK from [DotNet WSL](https://docs.microsoft.com/en-us/dotnet/core/install/linux-ubuntu#2004-). I installed the runtime too, might not be needed

If you want to edit it on Windows:
Install SDK from [DotNet Windows](https://dotnet.microsoft.com/download/dotnet/5.0)

### Installation
You will need ViGEm to emulate a real virtual joystick and the ViGEm driver is required to do so.

Get them here: [ViGEm Driver releases](https://github.com/ViGEm/ViGEmBus/releases)
- [ViGEm Driver x64](https://github.com/ViGEm/ViGEmBus/releases/download/setup-v1.17.333/ViGEmBusSetup_x64.msi), most users will need this one
- [ViGEm Driver x86](https://github.com/ViGEm/ViGEmBus/releases/download/setup-v1.17.333/ViGEmBusSetup_x86.msi)


## Dependencies
- [Nefarius.ViGEm.Client](https://www.nuget.org/packages/Nefarius.ViGEm.Client/)