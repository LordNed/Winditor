## Importing a Collision Mesh
This tutorial will go over how to import a collision mesh into a map using the Import Collision Mesh dialog.

## Preparation
Here are a few things to keep in mind when importing collision:

* Only models in DAE format are acceptable for collision meshes.
* In order for water and lava collision to work correctly, the model must be organized into three levels - a root node, any number of "category" nodes, and the meshes themselves, as shown below:

<p align="center">
  <img src="./import_collision_hierarchy.png" alignment="center">
</p>

  * `r33` is the root node
  * `cabana`, `ground`, and `lanterns` are the category nodes
  * `leaves`, `porch`, etc. are the actual meshes containing geometry data
  * In Blender, this can be achieved by having an empty object as the root with empties as children representing the categories. Each category can then have the meshes in the model as children.
  * **If the model is not structured like this, water and lava WILL NOT work correctly!**

## Where to Look
The Import Collision Mesh option can be found in the File menu, at File -> Import -> Collision Mesh.

<p align="center">
  <img src="./import_collision_mesh_menu.png" alignment="center">
</p>

## The Dialog
Upon clicking the menu option, you will be presented with the Import Collision Mesh dialog.

<p align="center">
  <img src="./import_collision_mesh_dialog.png" alignment="center">
</p>

The options are:
* **File**: This is where you will choose the file to import. Clicking on the "..." button to the right of the textbox will open a file chooser. Winditor supports *only* .dae files for collision meshes.
* **Scene**: Each Room in a map can have one collision mesh. This box allows you to choose which Room to import the chosen mesh into. Selecting a Room that already has a mesh will replace its mesh with the chosen one.

<hr>
<p align="center">
  <a href="../tutorials.html">Back</a>
</p>
