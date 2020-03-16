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

## Extensions
The basic server logic can be expanded to suit your needs using extensions. Extensions are created as .NET Core DLL libraries. You can find an example extension project and more detailed instructions [here](https://github.com/djmuted/Redfox_Extension).
