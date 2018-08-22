# Zero Localization Tool

This is a command-line tool for modifying Star Wars Battlefront II (2005) localization files. It was designed for use with automated build CLIs like Jenkins but can obviously be used outside of that as well.

## Usage

### Syntax

The syntax for executing commands (except the Help command) is: 
`ZeroLocalizationTool <localization file path> <command> [arguments]` 

Furthermore, file paths, key paths, and key values must always be wrapped in quotes.

Correct: `ZeroLocalizationTool "C:\BF2_ModTools\data_ABC\Common\Localize\english.cfg" -gv "whatever.scope.key"`

Incorrect: `ZeroLocalizationTool C:\BF2_ModTools\data_ABC\Common\Localize\english.cfg -sv whatever.scope.key new key value`

## Commands

### Help

To display the command list or the usage for a command, use the `help`, `-h`, or `--help` command.

Usage:

`help [command name]`

Examples:

`ZeroLocalizationTool help`  
`ZeroLocalizationTool help -sv`

### Set Value

To set a new value for a key, use the `-sv` or `--set-value` command. This will automatically save the localization file with the modified key.

Usage:

`-sv <key path> <key value>`

Example:

`ZeroLocalizationTool "C:\BF2_ModTools\data_ABC\Common\Localize\english.cfg" -sv "whatever.scope.key" "new key value"`

### Get Value

To print the value of a key, use the `-gv` or `--get-value` command. This will print the key's value to the console.

Usage:

`-gv <key path>`

Example:

`ZeroLocalizationTool "C:\BF2_ModTools\data_ABC\Common\Localize\english.cfg" -gv "whatever.scope.key"`