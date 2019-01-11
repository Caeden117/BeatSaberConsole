# Beat Saber Console
Displays both the Console and the Output Log in game, complete with fancy color highlighting.

![](https://cdn.discordapp.com/attachments/441819897941458944/533119304414265344/unknown.png)

# What are these?
The *Console* overrides the `Console.Out` property and moves all console output from the usual console window to the in-game console.

The *Output Log* uses Unity's `Application.logMessageReceivedThreaded` event to catch any logs that would make it to the final `output_log.txt` located in `C:\Users\<you>\AppData\LocalLow\Hyperbolic Magnetism\Beat Saber`.

# Color Highlights
Beat Saber Console will automatically highlight certain lines of code depending on their severity in the prefix when being logged. Currently these cannot be customizable, but if enough requests come by, maybe I'll consider.

It will also highlight different versions of the same severity (`Warn` and `Warning`, and `Err` and `Error`, for example.)

Beat Saber Console determines severity by the *Prefix* of the message:
* `[BS Console | WARN]` will be highlighted as Yellow.
* `Counters+ [Error] |` will be highlighted as Red.
* `Test Plugin | (warning):` will be highlighted as Yellow.
* `Test Plugin #2: Dont cause an Error, now!` will be highlighted as if it was regular logging.

|Type|Color|Hex Code|
|-|-|-|
|Regular Logging|Gray|#555555|
|Warning|Yellow|#FFA500|
|Error|Red|#FF0000|
|Fatal\*|Maroon|#550000|

*\*Any exception logged during gameplay, no matter where it is in the message, will automatically be highlighted as Fatal in game. Any stack trace afterwards will also be highlighted.*

# Configuration

You can drag both Console and Output Log to different positions by clicking and dragging the weird looking bar at the bottom of each of them

## Settings Submenu
**Lines:** Displays the number of lines in Console before it cuts off *(Output Log is not effected by this line maximum)*

**Show Movement Bars:** Displays the bars at the bottom that easily allow you to move them. The material I use causes issues in-game *(For an easy example, look at the Ripple effect in the main menu)*. The functionality, however, will not be effected, you still gotta click and drag below the console.

**Hide Common Errors:** Hides common exceptions in the Output Log (*ScoreSaber* exceptions, *CameraPlus* depending on your version, etc.)

**Show Stacktrace:** Shows the stacktrace in the Output Log (This *will* clutter the Output Log very very quickly)

**Reset Positions:** When you click Apply or OK with this enabled, it'll reset the positions and rotations of the Console and Output Log back to where they were before.
