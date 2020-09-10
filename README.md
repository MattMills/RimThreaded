# RimThreaded
RimThreaded enables Rimworld to utilize multiple threads and thus greatly increases the speed of the game.

Version 1.0.3

CHANGE LOG:
Version 1.0.3
-Fixed the dreaded "Thread did not finish..." causing game to completely freeze (threads are now aborted and auto-restarted)
-Fixed Projectiles not hitting
-Removed some old unused methods

This project is still a work in progress and will likely not play well with other mods. I am uploading this hoping to get feedback and identify some bugs as I continue development. If you would like to contribute, I have provided a github link below.

This is my first mod submission, and there is definitely some messy code, so I welcome all kinds of input.

I hope others are as excited as I am to see this great game get multi-threading support!

NOTE: The number of threads to utilize should be set in the mod settings, according to your specific computer's core count.

LOAD ORDER:
The long answer is that this code replaces many of the game's built in methods using Harmony "prefix" (for modders out there). I know this has made some other modders not happy about how I did this. Maybe there is a better way I should have done this? maybe a transpiler? I don't exactly know what I'm doing - being the first mod and all.

That being said, since it replaces methods, I *think* towards the top? Someone please correct me if they have had better luck using a different mod order. I am actually only testing this with vanilla+royalty+tickspersecond right now, so I am probably not the best person to ask. I'll update this description as people reply with different experiences.

BUGS:
https://github.com/cseelhoff/RimThreaded/issues
Your (likely) log location: C:\Users\username\AppData\LocalLow\Ludeon Studios\RimWorld by Ludeon Studios\player.log

CREDITS:

Bug testing:
Special thank you for helping me test Austin (Stanui)!
And thank you to others in Rimworld community who have posted their bug findings!

Logo:
Thank you ArchieV1 for the logo! (https://github.com/ArchieV1)
Logo help from: Marnador (https://ludeon.com/forums/index.php?action=profile;u=36313) and JKimsey (https://pixabay.com/users/jkimsey-253161/)

Video Review:
Thank you BaRKy for reviewing my mod! I am honored! (https://www.youtube.com/watch?v=EWudgTJksMU)
