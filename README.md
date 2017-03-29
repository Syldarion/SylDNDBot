# SylDNDBot
Discord bot for retrieving information about Dungeons and Dragons

## Current Features
* Spell library - Provides information on all spells available in D&D 5th Ed.
* Features library - Provides information on all race / class features in D&D 5th Ed.
* Trait library - Provides information on all traits available in D&D 5th Ed.
* Equipment library - Provides information on all basic equipment in D&D 5th Ed.
* Definition library - Provides definitions for common terms in D&D 5th Ed.

## Future Features
* Character Information - Store non-calculated character information in the database, and allow players to retrieve that information.
* Login System - Allow players to log in to the system for special functionality.
  * Players could be restricted to only accessing information on their character and things their character is knowledgable of.
* Command Permissions - Based on server role in Discord, restrict certain commands that that user can use.

## Chat Commands
Commands are used in the format ![command type] [sub-command] [args]
* spell
  * info - !spell info [name] - If the database contains information on a spell with the given name, that information is printed to the chat.
  * search - !spell search [query] - Prints a list of all spells whose name contains the search query.
  * add - !spell add [name] [level] [school] [cast time] [range] [components] [materials] [duration] [description] - Adds a spell to the spell database with the given information.
  * remove - !spell remove [name] - If the database contains a spell with the given name, the spell is removed.
* trait
  * info - !trait info [name] - If the database contains information on a trait with the given name, that information is printed to the chat.
  * search - !trait search [query] - Prints a list of all traits whose name contains the search query.
  * add - !trait add [name] [description] - Adds a trait to the trait library with the given information.
  * remove - !trait remove [name] - If the database contains a trait with the given name, the trait is removed.
* equipment
  * info - !equipment info [name] - If the database contains information on equipment with the given name, that information is printed to the chat.
  * search - !equipment search [query] - Prints a list of all equipment whose name contains the search query.
  * add - !equipment add [name] [cost] [weight] [description] - Adds equipment to the equipment library with the given information.
  * remove - !equipment remove [name] - If the database contains equipment with the given name, the equipment is removed.
* definition
  * info - !definition info [name] - If the database contains the definition with the given name, that definition is printed to the chat.
  * search - !definition search [query] - Prints a list of all definitions whose name contains the search query.
  * add - !definition add [name] [definition] - Adds a definition to the definition library with the given information.
  * remove - !definition remove [name] - If the database contains a definition with the given name, the definition is removed.
* roll - !roll [roll string] - Parses the roll string, rolls the necessary dice, and prints the sum.
  * The roll string can be any combination of XdY and constant number terms, delimited by plus signs (e.g. 4d6+14+3d100)
