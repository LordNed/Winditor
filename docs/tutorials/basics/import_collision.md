## Basics
This tutorial will go over how to import a visual mesh into a map using the Import Visual Mesh dialog.

## Where to Look
The Import Visual Mesh option can be found in the File menu, at File -> Import -> Visual Mesh.

<p align="center">
  <img src="./import_visual_mesh_menu.png" alignment="center">
</p>

## Dialog
Upon clicking the menu option, you will be presented with the Import Visual Mesh dialog.

<p align="center">
  <img src="./import_visual_mesh_dialog.png" alignment="center">
</p>

The options are:\
* File: This is where you will choose the file to import. Clicking on the "..." button to the right of the textbox will open a file chooser. Winditor supports any file formats that SuperBMD supports, including .dae files and .fbx files. You can also import existing .bmd/.bdl files, whether they are from the game or they were created with SuperBMD beforehand.
* Scene: This is where the model will be imported into the map. You can select the map's Stage, or one of the map's rooms. Your selection here will change the next box, Slot.
* Slot: Each scene has a fixed number of meshes that can be imported into it, and this box determines where your selected mesh ends up. The slots change depending on whether you have a Stage or a Room selected in the Scene box.
	* For Stages, these are the available model slots:
		* Main Skybox: This is the model that makes up the majority of the sky. In-game, this is a cylinder with a solid color. The model name for this mesh is `vr_sky.bdl`.
		* Horizon Clouds: This is the model that makes up the clouds that ring the horizon. The model name for this mesh is `vr_back_cloud.bdl'.
		* Horizon Sea: This model simulates the ocean at a distance, making the water look like it goes farther than it does. The model name for this mesh is `vr_uso_umi.bdl`.
		* Horizon Gradient: This model is a gradient 
	* For Rooms, these are the available model slots:

<hr>
<p align="center">
  <a href="../tutorials.html">Back</a>
</p>