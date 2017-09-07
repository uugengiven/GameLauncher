## Server for hosting Steam users ##

This project is the server side of a two part project that allows LAN centers to put their steam games into a hosted area and allow their computers to check out games via a web interface. This server runs either on premises or somewhere online. The client runs on each individual LAN center PC.

### General Usage ###

Client is sent a request to start a game via Steam ID number (ex 570 for Dota 2)
Client sends request to the Server requesting a valid Username and Password ex: `/games/startgame/123` along with posting the following data

```
{
  'computerkey': 'abcdef',
  'securitycode': 'zxy'
}
```

The `securitycode` is a hash made up of the key, ip, and current time


 * Failure
  * Server responds with "No User Available for Game" (may be in use, may not have game in library)
  * Server responds with "Invalid Security" (the hash does not match)

 * Success:
  * Server responds with JSON object in following format
```
{
  'username': 'abc',
  'password': 'def',
  'exe': 'somegame.exe',
  'steamId': 123
}
```

Client takes Username and Password and SteamID and uses the `-login` and `-applaunch` commands for Steam
