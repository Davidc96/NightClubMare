# Pioneer CLI
This tool is used to control the CDJ devices using the command line. This is compiled using Visual Studio 2019 and .NET Framework 4.7.2
![intro_tool](https://user-images.githubusercontent.com/4979202/210185586-1034e79b-d380-437a-b3d3-c046bd5c4a73.jpg)

# Dependencies
- **ConsoleTables** Install using Nugget
- **ProLinkLib** Used on this project

# How to use it
To open it, compile it or download the binaries in Release section.</br>

When you open it, a quick start network configuration will guide you to connect to the CDJ devices</br>
IMAGE_HERE

When the config is done, you are allowed to use the tool

# Implemented commands
This commands are already implemented and are free to use

## Generic commands

You can use this commands without any restrictions
### devices
Command which lists all the Pioneer network devices (Only CDJ are supported)</br>
IMAGE HERE

### select
Command to select one Pioneer device by Pioneer ID, Pioneer ID are shown using the **devices** command</br>
```Usage: select <Device ID>```

If this command executes correctly, the main command prompt will change to the selected device and you are allowed to use specific device commands</br>
IMAGE HERE

### debug
Enables the debug mode which opens the PCLILogger server and shows all the incomming or outgoing packets.</br>
IMAGE HERE

## Specific-Device's commands
You can use this commands if a Pioneer specific device is selected using the **select** command.

### info
Shows the current status of the Pioneer CDJ</br>
IMAGE HERE

### play
Play a current loaded song into the specific CDJ</br>
IMAGE HERE

### pause
Pause the current song and returns to the beginning

### sync
Enables the sync mode ```Usage: sync <on|off>```

### load
Loads an specific track using Track ID
```Usage: load <trackID>```

## Special commands
In order to test or fuzz values, this tool has a feature called **custom** which allows an user to customize the command payload.</br>
To enable this feature you just need add the word **custom** next to the command you will like to custom.</br>
Current commands which supports this **custom** features are:
- play
- pause
- sync
- load</br>
![custom_command](https://user-images.githubusercontent.com/4979202/210185568-4f0efee1-7c83-4713-822b-7e6b9587d9af.jpg)
