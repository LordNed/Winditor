## Basics
This tutorial will go over the general features of Winditor and how to set it up for use.

## Main Window
<img src="./mainwindow.png">

When you download Winditor and open it up, you will be presented with the main window. This is where you will do most of your actor and collision editing. It is split into 4 main parts: the toolbars, the scene view, the viewport, and the mode panel.

### Toolbars
<p align="center">
  <img src="./toolbars.png" alignment="center">
</p>

The toolbars contain various options and features that you can use while editing maps.

* File
  * Open
    * Stage: This will open a folder selection dialog, asking you to select a folder containing a map. Typically, you will be opening a folder within game's `Stage` directory, which is located at `files/res/Stage` in the ISO. When you select a folder, Winditor will open all the rooms contained within it. Opening all rooms at once can result in lag and potentially crash Winditor, so it is best to use the option below for large maps, such as the overworld map `sea`. 
    * Rooms: This will open a file selection dialog, asking you to select one or more room archives to open. When you confirm the dialog, Winditor will open the selected rooms in addition to their Stage.arc, if one is present in the folder.
  * Save: This will save the map as individual files to a location on your computer. These are not archive files (.arc), but rather what is *in* the archives - visual meshes (.bmd/.bdl), collision meshes (.dzb), and actor setups (.dzr/.dzs). If the map was opened from archive files, this option will ask you to choose a folder to save the extracted files to. If the map was opened from a folder containing these individual files, Winditor will silently update them to match what is currently in the editor.
  * Save as: Performs the same function as the above, except it will always ask for a location to save the individual files to. This is useful if you want to duplicate a map without manually copying and pasting.
  * Import
    * Visual Mesh: This option allows you to import a visual mesh - a model that the player can see - into the map. It will open a dialog asking you to choose a file to import, which scene to place it in, and which slot it should occupy within the target scene. The dialog will accept any files that SuperBMD can accept, including .dae files, .fbx files, and .bmd/.bdl files.
    * Collision Mesh: This option allows you to import a collision mesh - a model that the player cannot see that determines what is solid in the map. Similarly to importing a visual mesh, selecting this option will open a dialog asking you to select a file to import and the scene that it should be placed into. A room can only have one collision mesh.
  * Export:
    * Visual Mesh and Collision Mesh: These are the opposite of the Import options above. They allow you to export the visual and collision meshes of a specific scene as .dae files.
  * Close: This closes the current map, while keeping Winditor open.
  * Exit: This closes Winditor entirely, exactly like clicking the X in the top right corner.
  
### Scene View
<p align="center">
  <img src="./sceneview.png">
</p>

### Viewport
<p align="center">
  <img src="./viewport.png">
</p>

The map and rooms that are currently loaded will be displayed in the **viewport**. It shows all of the entities within the map that have positions within the world, even if they are not displayed in-game. The controls are as follows:

#### Movement
* **Right-click and drag** on the viewport to look around the scene.
* While holding the right mouse button, **use the W, A, S, and D keys** to move around the map.
* While moving around the map, **hold shift** to move faster.
* You can also adjust movement speed by **scrolling in and out with the scroll wheel.**

#### Object Manipulation
* Select an object by **left-clicking** on it.
* To select multiple objects, hold **shift** while left-clicking.
* To undo an action, **press ctrl + Z**.
* To redo an action, **press ctrl + Y**.

##### Translation
<p align="center">
  <img src="./basics_move_axes.png">
</p>

* When an object is selected, the Translation Widget will appear. **Left-click and drag** on any of the arrows on the widget to move the object along that axis.
* Left-click and drag **on the squares between arrows** to move the object on both of those axes at once.
* **Press the W key** to switch back to the Translation widget if you have switched it to the Rotation or Scaling widgets.

##### Rotation
<p align="center">
  <img src="./basics_rot_axes.png">
</p>

* With an object selected, **press the E key** to switch the axes to the Rotation Widget.
* **Left-click and drag an arc** on the widget to rotate the object on that axis. Note that many objects do not support rotating in all directions.

##### Scaling
<p align="center">
  <img src="./basics_scale_axes.png">
</p>

* With an object selected, **press the R key** to switch the axes to the Scaling Widget.
* **Left-click and drag a cube** on the widget to scale the object on that axis. Note that many objects do not support scaling.

### Mode Panel
<p align="center">
  <img src="./modepanel.png">
</p>

<hr>
<p align="center">
  <a href="../tutorials.html">Back</a>
</p>
