# Zero Localization Tool
 
[![Release](https://img.shields.io/github/release/marth8880/ZeroLocalizationTool.svg?label=latest%20release&maxAge=300)](https://github.com/marth8880/ZeroLocalizationTool/releases/latest)
![Downloads](https://img.shields.io/github/downloads/marth8880/ZeroLocalizationTool/latest/total.svg?maxAge=60)
[![Issues](https://img.shields.io/github/issues/marth8880/ZeroLocalizationTool.svg?maxAge=60)](https://github.com/marth8880/ZeroLocalizationTool/issues)
[![License](https://img.shields.io/badge/License-BSD%203--Clause-blue.svg?label=license)](https://opensource.org/licenses/BSD-3-Clause)

This is a command-line tool for modifying Star Wars Battlefront II (2005) localization files. It was designed for use with automated build CLIs like Jenkins but can obviously be used outside of that as well.

## Installation

Download the [latest release](https://github.com/marth8880/ZeroLocalizationTool/releases/latest), extract the archive to any directory, and done! 

Please note that [.NET Framework 4](https://www.microsoft.com/en-us/download/details.aspx?id=17718) is required to run the application.

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
