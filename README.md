# EMU twitch bot
## Introduction
This bot will read chat messages as input.
Will process them and do stuff on the host machine. 

The end goal is to run any _simple_ emulator (DS, nes, etc) by broadcasting keys to said emulator
A simple feature includes chaos / order (affects time between messages to let twitch chat take action)

## Current
The bot is for now in dev.
It should not be so hard to develop and should be a lot of fun

## Next steps:
- Run on youtube
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
channel: #streamer_live_name
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
Install SDK from https://docs.microsoft.com/en-us/dotnet/core/install/linux-ubuntu#2004-. I installed the runtime too, might not be needed

If you want to edit it on Windows:
Install SDK from https://dotnet.microsoft.com/download/dotnet/5.0
