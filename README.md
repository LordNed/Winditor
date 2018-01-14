# Winditor
The latest revision of a The Legend of Zelda: The Wind Waker map editor! This is an *in-progress editor* that tries to set up the structure for a modern editing environment such as viewport widgets and undo/redo support.

To re-iterate: *This is only partially functional and will probably not work for your needs* unless you're willing to start fixing things :)

This tool has partial support for Wind Waker map files currently, some maps may lose data on save so if you wish to attempt to use this tool open your map of choice, change nothing and export it again to see if the game can load it. If the game can't load it, time to figure out what data is being dropped, fix it, and make a Pull Request :)

![](https://i.imgur.com/eNDY23V.png)

# Usage
Winditor works on uncompressed archives. It assumes that you wish to edit all rooms and the stage in a map at once (as Wind Waker has many small rooms) so when you open it you need to open a folder that contains additional folders such as "Room0" and "Stage".

# Data Root
Winditor has the ability to look at an extracted version of the Wind Waker ISO to load object models on the fly (such as rocks, pots and NPC models). To enable this behavior:

`Edit > Set Data Root > <point the text field to an extracted version of the ISO containing the folder "res" inside of it>`

Once you have set the data root and hit okay, close the editor normally and then re-open it. Now maps should have models for most actors, though actors without models will be represented by checkerboard boxes.
