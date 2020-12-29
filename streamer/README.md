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

Any key from this list can be changed to other text (default is itself)
[ 'up', 'down', 'left', 'right', 'l', 'r', 'a', 'b', 'x', 'y', 'l1', 'l2', 'r1', 'r2' ]
KEY:            [text]                  # if you want to 'up' with 'bruh' put 'up: bruh'
