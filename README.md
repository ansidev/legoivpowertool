LegoIV Power Tool
===============
> * Current stable version: [1.4.14.0901 Stable](https://github.com/ansidev/legoivpowertool/releases/tag/1.4.14.0901 "1.4.14.0901 Stable").
> * Release date: 1st September 2014.
> * Description: Simple tool to shutdown, restart, sleep, hibernate, sign out, lock your computer and more.
> * License: [GPLv3](http://www.gnu.org/licenses/gpl-3.0.html "GNU General Public License Version 3")

###List of current features:

####Basic features
1. Shut down computer.
2. Restart computer.
3. Sleep computer.
4. Hibernate computer.
5. Sign out of current user account.
6. Lock computer screen.
7. Switch between user accounts.
8. Turn off monitor.

####Advanced features
1. Set delay time for selected actions.
2. Quick access via Jump List menu, system tray menu.
3. Support command line mode.
4. Check for new release version from GitHub and download correct version based on your OS architecture.

###CHANGELOG
####Version 1.4.14.0901
* Added changelog to README.MD.
* Change version name format.
    * Now format is <major>.<minor>.<release year>.<release month><release date>
* Support check for new release and download update.
    * Program will detect your OS is 32 or 64 bit to download the correct version.
    * File will be saved in folder that contain your program.
* Fix basic features.
* Write my own GitHub API.
* Using JSON.NET to process data from official GitHub API.

