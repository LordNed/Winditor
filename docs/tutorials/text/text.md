## Text Editor
This tutorial will go over Winditor's integrated text editor.

## Opening the Text Editor
In order to use the text editor, you must have a Game Root set in the Options menu. To set that up, see the [Getting Started](../basics/gettingstarted.html) guide.

To open the editor, go to Tools -> Text Editor:

<p align="center">
  <img src="./texteditor_option.png" alignment="center">
</p>

## The Editor
<p align="center">
  <img src="./texteditor.png" alignment="center">
</p>

The text editor is a single window with four components: the menu bar, the Message View, the Message Properties panel, and the Text Box.

### Menu Bar
The menu bar has three options:

* **File**
  * **Save**: This saves the modified message data to the copy of the game specified by the Game Root property in the options menu. You can use the shortcut **ctrl + S** to quickly save the message data.
  * **Save as..**: This allows you to save your message data to another archive rather than replacing the original `bmgres.arc`.
* **Edit**:
  * **Add Message**: This adds a new message to the game's message list. It will use its own unique message ID and it will be placed in a location that does not change the indices in the list.
* **Help**
  
**Note**: When there are changes to the data waiting to be saved, the file name in the window's title bar will have an asterisk (\*) in front of it.

### Message View
The Message View takes up the left side of the window, and consists of a search box and a message list. You can search for messages using the search box in three ways:

* **Text**: You can search for specific words by simply typing them into the box. This will search the content of the message for those words.
* **Message ID**: Every message in the game has a unique ID. Most references to text in the game use this ID. You can search for a specific ID by typing `msgid:` into the search box followed by the ID number, e.g. `msgid:4118`.
* **Message Index**:  This is the index of the message within the text file. Only STB cutscenes use this value to reference messages. Similarly to the message ID, you can search for a specific index by typing `index:` into the search box followed by the index number.

### Message Properties
Each message has a number of properties that determine how it is drawn on screen. Here is a key describing them:

* **Style**: This is the kind of dialog box that the text is contained in.
* **Draw Type**: This property has three values:
  * **By Char Slow**: When drawn, the text will be typed out character by character, like a typewriter.
  * **By Char Skippable**: This is the same as the above option, except the player can skip the typewriter effect by pressing the A button.
  * **Instantly**: Instead of being typed out, the entire message is shown at once.
* **Screen Position**: This determines where on the screen the textbox is shown. This isn't arbitrary; there are preset locations to choose from.
* **Item Image**: If the Style property is set to "Item Get", this is what item icon is displayed in the space on the left side of the textbox.
* **Line Count**: This is how many lines can be displayed in each textbox. The default is 4 lines. Any lines that go over this limit will be pushed into another textbox. If you want to have less than the maximum amount of lines in a textbox without modifying this value, just put enough empty lines in the message to fill in the empty space; the game automatically adjusts the positions of the lines to center them in the textbox while ignoring the empty lines.
* **Initial Animation/Camera Position/Sound**: These properties define an animation, camera position, or sound that is played when the textbox is first displayed. While we don't know much about how these values work, we do know that the animation is based on the actor that is speaking.
* **Item Price**: This is the price of an item from a shop, excluding the Windfall Island Bomb Shop before the pirates ransack it. Changing this value will change the amount of rupees required to purchase the item, but note that the text itself must be updated to match the new price.

### Text Box
This is where you can edit the text that is displayed to the player.

<hr>
<p align="center">
  <a href="../tutorials.html">Back</a>
</p>
