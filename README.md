# TestGame


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
