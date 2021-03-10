# TestGame - Small game im making in C# Monogame


v0.0201 - Minor changes. Added some interfaces in the code. Also changed acessability in the classes.

TODO remains the same 

------------------------------------------------------------------------------------------

v0.020 - The game now loads and unloads in a better way than before. It only loads the textures it needs currently and unloads anything it doesnt need.
The change zone logic is now completely different. A switch zone animation has been added. Way harder to implement than I thought. Fixed varius bugs when switching between the same
two zones. Loading now feels faster since the game doesnt load all textures at the start. Might be placebo.

TODO:
- Fix scaling (Not urgent).
- Remake bedroom sprite.
- Fix small bug when adding hitboxes. First one always turns blue.
- Change hitbox savefile to either JSON or XML.
- Add talk hitbox and interact hitbox.
- After bedroom sprite, create the next zone.
- Give zones ID:s so that you can read and write them easier


------------------------------------------------------------------------------------------

v0.019 - Remade the whole hallway sprite. Added startvector for hallwayRoom and made sure the hallway sprite spawns in the right place. Also added hitboxes for hallwayRoom.

TODO:
- Still need to comment Grid/Hitbox.	done
- Fix scaling.
- Add zone switch animation.	done ok
- Maybe make a better background sprite.
- Remake bedroom sprite.
- Create a save character and zone pos for easier zone adding. done
- Fix small bug when adding hitboxes. First one always turns blue.
- Read up on loading and unloading in Monogame. done
- Maybe change hitbox saving file type. 
- Add talk hitbox and interact hitbox.
- Loading feels like its taking a long time. Read up on how to make it faster.
- Start working on loading/unloading. done

------------------------------------------------------------------------------------------

v0.018 - Added background class. It has a texture that gets put in the foreground and moves slower than a regular zone. Makes it look like its further away. Sorted the textures in to folders so that
testgame/Content folder isnt cluttered. Also fixed a small bug when switching zone. Zone switch seems to work fully now. Cleaned up Grid class.

TODO:
- Comment new methods in Grid/Hitbox.
- Make hallway sprite better.
- Fix scaling.
- Make a better background sprite.
- Add zone switch animation.
- Add zone startvector.


------------------------------------------------------------------------------------------

v0.017 - Rewrote the whole Grid/Hitbox system. Changing rooms now work. Changed default res, now 1920x1080. Made the Grid save files alot more efficient.

		The Grid system now works kind of the same. But a grid now has an array of Hitboxes, which can have different properties. For now there is only Wall hitbox and Zone hitbox.
			The zone hitbox changes the zone when it collides with the player hitbox. Every zone hitbox has a connected zone.
			


TODO:
- Comment new methods in Grid/Hitbox.
- Make the hallway sprite better.
- Create zone background class and create a background sprite.
- Fix better scaling. Make it possible to switch between 1080p and 720p
- Add zone switch animation. (Fade to black)

------------------------------------------------------------------------------------------


v0.0161 - Commented methods. Added event class. Fixed up main class. Last commit from laptop for now.

------------------------------------------------------------------------------------------

v0.016 - Grid now working as intended. If developer settings is enabled, you can now show the hitbox, both enable and
disabled at the same time or seperate. You can edit the hitboxes with left mouse. Fixed AD and WS bug. Added settings.
For now there is only enable developer settings. Also the hitbox grid is now stored inside SavedList.txt, and gets loaded
when game starts.

TODO:
- Add hallway room.
- Maybe add ActionEvent class.
- Add zone startVector.
- Switching room functionality.

------------------------------------------------------------------------------------------


v0.015 - functionality of grid is now somewhat working, needs some fine tuning

TODO:
- Fix grid bug - fixed, but will fine tune
- Add grid maker - basically done
- finish room1 - basically done
- fix AD animation bug - DONE

------------------------------------------------------------------------------------------

v0.0141 - test commit from laptop

v0.014 - added hitbox grid class and added it to first room zone, functionality of grid not implemented yet

v0.013 - added textures and animations for pc

v0.012 - added menu and some fx for it

v0.011 - changed movementspeed

v0.01 - first commit, functioning zone and character movement
