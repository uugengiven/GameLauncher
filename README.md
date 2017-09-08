# Server for hosting Steam users #

This project is a game launching service for LAN centers. This allows for different computers to log into games by logging computers into the specific licensing software (steam only currenty).

## General Usage ##

User logs into a website, currently hosted within the Server software. This website lists all games that are available to launch from this software. Each game has a link to a local api, something like `localhost:8099/api/games/gamestart/123`. 

Client is sent a request to start a game via Steam ID number (ex 570 for Dota 2)
Client sends request to the Server requesting a valid Username and Password ex: `/games/startgame/123` along with posting the following data

``` json
{
  'computerkey': 'abcdef',
  'time': current time,
  'securitycode': 'zxy',
}
```

The `securitycode` is a hash made up of the computerkey, ip or shared secret, and current time

* Failure:
  * Server responds with "No User Available for Game" (may be in use, may not have game in library)
  * Server responds with "This Computer already has a user checked out"
  * Server responds with "Invalid Security" (the hash does not match)
  * Server responds with "Invalid Time" (the current time is more than 5 minutes off of server time)

* Success:
  * Server responds with JSON object in following format

``` json
{
  'username': 'abc',
  'password': 'def',
  'exe': 'somegame.exe',
  'steamId': 123
}
```

Client takes Username and Password and SteamID and uses the `-login` and `-applaunch` commands for Steam

## Required Functionality ##

### Admin ###

* Add games to game list
* Add steam users and their games to game list
* Admin can check user back in
* Add computer as verified

### Client/Server Interaction ###

* Request User/Pass for game [done]
  * Check that hash is correct
* Return user/pass to server/check in user
* Return all games from PC
* Prevent computer from logging out two games

### Interface/UX ###

* Check status from server (do I have a game checked out)
* Check status from clientservice (am I running a game)
* Request game list from server (with availibility info)

## API Calls ##

### Server ###

* `\api`
  * `\checkout\{id}\`
  * `\checkin\`
  * `\startup`
  * `\gamelist`
  * `\currentstatus`

### Client Service ###

* `\api`
  * `\startSteam\{id}`
  * `\currentstatus`