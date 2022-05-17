# Debug UI

Debug UI library provides two things:
1. UI for interacting with the scene, at runtime, using a command line like interface
2. Framework for custom commands that work in the UI.  Referred to as the debug engine.


## The UI

The command line like UI piece is built as a unity prefab in the demo project, called `DeveloperConsoleObject`.  Along with some
UI elements, there is a MonoBehavior derived controller which initializes and interacts the the debug engine.  

You can use this prefab and controller class in your projects; or you can create your own UI.

## Command Library

The library provides a couple of commands, mostly as examples but you might find them useful as well.


| Command          | Function            |
|------------------|---------------------|
| DebugLog         | Sends all logging to Unity debug window |
| NoLog            | Effectively turns off all logging |
| PlayerPos        | Manipulate player character position.  Please read beloew for more details |
| help             | high level help (built in) |
| clear            | clears the history (built in)|


### PlayerPos Command
This command allows controller the player charactor through the Debug-Ui.  Below are the arguments accepted. To use
type `PlayerPos {arguments}`.  

| Arguments        | Function            |
|------------------|---------------------|
| help             | lists these arguments |
| remove           | unloads this command |
| (no argumments   | installs this command |
| jumpTo           | performs functions moving player character to another location |
| useKeyboard      | toggles use of keyboard shortcuts for jumpTo commands |

### JumpTo
```
   missing information at this time
```



## Creating your own commands
```
   missing information at this time
```

