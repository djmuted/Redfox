# Redfox Server
Redfox is a universal open-source MMO game server created in .NET Core, providing a basic [Zones and Rooms architecture](http://docs2x.smartfoxserver.com/Overview/zones-room-architecture).

## Features
Redfox in its current form offers the following features:
- TCP server
- WebSocket server
- Extension support
- Custom Message Handlers
- Custom Zone Authenticators
- User Variables

## Planned features
- Admin Panel

## Configuration
Server configuration file path is Config/Server.json

## Built-in messages
### Global scope (global) - messages working no matter if user is in a zone or not
- Join Zone Request (rfx#jz)
  - Zone Name (zoneName) __required__
  - Username (username) _optional_
  - Password (password) _optional_
### Zone scope (zone) - messages working only once user is in a zone
- Join Room Request (rfx#jr)
  - Room Name (roomName) __required__
- Public Message Request (rfx#pm)
  - Message content (message) __required__

## Extensions
The basic server logic can be expanded to suit your needs using extensions. Extensions are created as .NET Core DLL libraries. You can find an example extension project and more detailed instructions [here](https://github.com/djmuted/Redfox_Extension).
