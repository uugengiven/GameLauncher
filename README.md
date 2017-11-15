# Server for hosting Steam users #

This project is a game launching service for LAN centers. This allows for different computers to log into games by logging computers into the specific licensing software (Steam only currenty).

## General Usage ##

User logs into a website, currently hosted within the Server software. This website lists all games that are available to launch from this software. Each game has a link to a local api, something like `localhost:8099/api/games/gamestart/123`. 

Client is sent a request to start a game via Steam ID number (ex 570 for Dota 2)
Client sends request to the Server requesting a valid Username and Password ex: `/games/startgame/123` along with posting the following data

``` json
{
  "computer_key": "abcdef",
  "time": "current time",
  "security_code": "zxy",
}
```

The `securitycode` is a hash made up of the computerkey, ip or shared secret, and current time

* Failure:
  * Server responds with "No User Available for Game" (may be in use, may not have game in library)
  * Server responds with "Invalid computer_key"
  * Server responds with "This Computer already has a user checked out"
  * Server responds with "Invalid Security" (the hash does not match)
  * Server responds with "Invalid Time" (the current time is more than 5 minutes off of server time)

* Success:
  * Server responds with JSON object in following format

``` json
{
  "username": "abc",
  "password": "def",
  "exe": "somegame.exe",
  "steamId": 123
}
```

Client takes Username and Password and SteamID and uses the `-login` and `-applaunch` commands for Steam

## Required Functionality ##

### Admin User ###

* Add games to game list
* Add Steam users and their games to game list
* Admin can check user back in
* Add computer as verified

### Client/Server Interaction ###

* Client checks out user/pass for game [done]
  * Check that hash is correct
* Client checks in user/pass for game to server [done]
* Server checks in all games from client [done]
* Server prevents client from checking out another game if it's already got one checked out [done]

### Interface/UX ###

* Check status from server (do I have a game checked out)
* Return my checked out game
* Check status from client (am I running a game)
* Request game list from server (with availability info)
* check out a game

## API Calls ##

### Server ###

* `\api`
  * `\checkout\{game_id}\`
  * `\checkin_all`
  * `\games` - list all games and availability status
  * `\current_status` - client calls to ask: does the server believe I have something logged out right now?

### Client Service ###

* `\api`
  * `\start\steam\{game_id}`
  * `\current_status` - interface calls to ask: does the client believe it has something logged out right now?

## API In Depth ##

### Server ###

* `\api`
  * `\checkout\{game_id}\`
    * Post
    ``` json
    {
      "computer_key": "key",
      "time": "current_time",
      "security_code": "hash",
    }
    ```
    * Response
    ``` json
    {
      "username": "abc",
      "password": "def",
      "exe": "somegame.exe",
      "steamId": 123
    }
    ```
    * Server should mark steam user as checked out by computer that matches computer_key
  * `\checkin_all`
    * Post
    ``` json
    {
      "computer_key": "key",
      "time": "current_time",
      "security_code": "hash",
    }
    ```
    * Response
    ``` json
    {
      "status": "ok"
    }
    ```
  * `\games` - list all games and availability status
  * `\current_status` - client calls to ask: does the server believe I have something logged out right now?

### Client Service ###

* `\api`
  * `\start\steam\{game_id}`
  * `\current_status` - interface calls to ask: does the client believe it has something logged out right now?
