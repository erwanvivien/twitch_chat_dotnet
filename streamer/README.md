# For Streamer
- Install any DS emulator (DeSmuME was used to test)
- Install the ROM for your game
- Create a 'settings' file next to executable
- Run executable

A default 'settings' looks like this:
```
channel: #streamer_live_name
password: oauth:.........   (get this from https://twitchapps.com/tmi/)
window name: desmume (if you use DeSmuME)
```

All other options for 'settings':
```
Note: something in [] is optionnal
Note: everything after # is comments

platform:       [twitch / youtube]      # works only twitch now
channel:        #stream_live_name       # mine is #xiaojiba
bot name:       [name]                  # does not do anything
server:         [ip]                    # should probably never change this
port:           [port]                  # same
password:       password                # looks like 'oauth:......' get it from
                                        # https://twitchapps.com/tmi/
log_irc:        [true|false]            # see message from twitch in the console
log_fct:        [true|false]            # see which action was done last second
window name:    name                    # A running application (DeSmuME generally)

Any key from this list can be changed to other text, non-case-sensitive (default is itself)
KEYS = [ 'up', 'down', 'left', 'right', 'l', 'r', 'a', 'b', 'x', 'y', 'l1', 'l2', 'r1', 'r2', 'help' ]
KEY:            [text]                  # if you want to 'up' with 'bruh' put 'up: bruh'
```

If you change any key, don't forget to adapt your help file.

Default 'help' file: (only text, edit as wish)
```
You can control movements by typing 'up', 'down', 'right' or 'left'
You can do any action from the DS ('A', 'B', 'X' or 'Y')
L is 'l'
R is 'r'

help is 'help'

Checkout this page for more informations
https://github.com/erwanvivien/stream-chat-bot-dotnet#for-chat
```

This is the exact message that will be sent privately to anyone typing 'help' (or other if you changed it)
This message will contain my patreon link.

https://www.patreon.com/bePatron?u=37554585
