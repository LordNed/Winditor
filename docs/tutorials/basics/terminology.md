## Terminology
This guide will cover various terms used in Wind Waker hacking.

## Maps

* **Stage**: A collection of rooms. For example, `sea` for the Great Sea, or `M_NewD2` for Dragon Roost Cavern. Stages themselves have no collision, but have a skybox, and may or may not have actors. Any actors placed in the stage will always be loaded for that stage no matter what room the player is in, which may be desirable for e.g. treasure chests, if you want them to appear on the dungeon map even when the player is not in the same room as them.
* **Room**: An individual room map, with its own collision mesh, visual mesh, and actors in it.
* **Stage Save Info**: A part of the save file that stores flags different stages, as well as other variables like the number of small keys the player currently has in this stage. Note that each and every stage does **not** have its own stage save info - the save file only has 16 stage save infos in it, and all stages in the game must use one of them, so many stages will need to share a single set of flags. Which one is used by a stage can be changed by editing the Stage Save Info ID of the STAG (Stage Settings) for the stage in question.

## Flags

A flag is a type of variable in the game's memory used to indicate that something has happened. Wind Waker has many different kinds of flags:

* **Switch**: The most general-purpose type of flag in Wind Waker. These are set and checked by hundreds of different actors, especially enemies and puzzle elements. If you select an actor and look at its parameters in the right sidebar, you will often see some mention of switches. Switches are used to remember things that have happened which are local to a certain stage, and also used to communicate between multiple actors within a stage. There are several different types of switches:
  * **Mem Bit**: Range from 0-127. Permanent switches that are saved to the memory card. They are the only type of switches that are not cleared when saving and reloading the game. These are only unset if an actor specifically unsets them. These are stored in the current stage's stage save info.
  * **Dan Bit**: Range from 128-191. Temporary switches that are all reset when you exit a dungeon, but are remembered as long as you stay in the same dungeon, even if you change rooms or stages (e.g. going into the mini-boss room of a dungeon). To be specific, the way the game detects that you've exited a dungeon is by the stage save info ID - as long as you only go to stages that share the same stage save info, dan bits will not be cleared.
  * **Stage Zone Bit**: Range from 192-223. Temporary switches that are all reset when you change stages.
  * **Room Zone Bit**: Range from 224-239. Temporary switches that are all reset when you change rooms or stages.
  * **N/A Switch**: 255 is considered a special switch value - you can often set switch parameters to 255 to avoid any switch at all being checked/set.
* **Item Pickup Flag**: Flags used to remember which items lying on the ground the player has collected. Each stage save info has room for 64 item pick up flags, from 0-63.
* **Chest Open Flag**: Flags used to remember which treasure chests the player has opened. Each stage save info has room for 32 chest open flags, from 0-31. (Not used for salvaged ocean chests.)
* **Salvage Flag**: Used to keep track of which salvaged ocean chests the player has obtained. Each sector on the sea has space for 16 salvage flags, from 0-15.
* **Enemy Number**: A temporary enemy death flag. If this is not -1, the enemy will set this flag on death, and it won't respawn even if the player goes to a different room and comes back. If the player goes to a different stage, these flags are cleared, so all enemies will respawn. Note that each room has a separate set of enemy numbers, so enemy number 0 in room 5 is considered separate from enemy number 0 in room 6 for example.
* **Event Bit**: A type of permanent, global flag. These are usually used for storyline progress, and NPC questline progress. You can't set them with actor parameters, they are always set directly via code. However, you can check them via an actor's parameters - the Evsw (Event Bit Switch Setter) actor checks a certain event bit and sets a switch if it is set.

## Parameters

Although parameters are unique to each actor in the game, there are some common types of parameters that tend to be implemented for many different actors:

* **Death Switch**: The enemy will set this switch when it dies.
* **Enable Spawn Switch**: The enemy will initially not spawn in, but once something sets this switch, the enemy will start to spawn.
* **Disable Spawn Switch**: The enemy will initially spawn in, but once something sets this switch, the enemy will no longer spawn.
* **Disable Spawn on Death Switch**: The enemy will initially spawn in, and will set this switch when it dies. Once something sets this switch, the enemy will no longer spawn.
* **Sight Range**: The range within an enemy will notice the player and start attacking them. The exact way this works may vary from enemy to enemy.

<hr>
<p align="center">
  <a href="../tutorials.html">Back</a>
</p>
