# SRX.HeadAdmin
Remote control tool for Counter-Strike servers.

Simple C# windows forms application that is used by head admins / owners of counter-strike servers.\
The tool allows to remotely access your server using your servers' RCON password.

# How it started
As a big fan of the game and server owner, I've made this tool long time ago.\
Now I wanted to make it opensource so if anyone wants to use it and also improve it with me, can join :)

# Features
- Server status (various fields)
- Change current map
- Change next map
- Server logs
- Slap
- Slay
- Kick
- Ban/Unban

# Can I help to improve it and/or fix bugs?
First of all, if you are willing to help but don't have a server, contact me I will give you a test server with RCON access.

As this is very beginner friendly project, everyone can contribute to this project.
All you need is a passion for the game Counter-Strike :)

Please feel free to raise issues, fork the source code, send pull requests, etc.
Every kind of help is appreciated, even whitespace fixes.

Also note that there are a ton of AMX mod X commands that can be implemented in this app to make it richer.

# How to use
Open and build the project, set configuration to release, copy the output from **Release** into another folder with your choice, open the **Config.txt** and set IP address, port and RCON password from your server and run the application.

# How to debug from visual studio
Build the project, edit the config file in the bin/debug or bin/release folder depending on your configuration and run the project.
