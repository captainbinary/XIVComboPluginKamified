# Endwalker Is Here

# XIVComboPlugin Kamified

[![Build](https://github.com/grammernatzi/XIVComboPlugin/actions/workflows/build.yml/badge.svg)](https://github.com/grammernatzi/XIVComboPlugin/actions/workflows/build.yml)

This is a version of Daemitus's XIVComboExpanded with added features. The majority of these new additions are for button consolidation, and while they have logic behind them, they aren't really designed to play the game for you, and more replace options you have either already used, can't be used at that moment, have a lower priority, or would be completely detrimental to use at that moment, so that, while you still have to think about which buttons you are pressing and not just spamming a single one, you also get to have a much smaller hotbar. Quite a few of these features can be replicated by something like ReAction with ease (or even macros in some cases), but look prettier with actual icon replacement.

Also, for the first time that I know of in an XIVCombo plugin, alt leveling is fully supported. So for jobs (that aren't Black Mage) that get actions via Job Quest, the combos won't update until you actually unlock them. Black Mage unfortunately relies too much on casting, which grays actions out, for the solution I used to work well, but everything else works fine.

Also, this plugin is explicitly made for more advanced users; if you want something more curated, check out the original, at https://github.com/attickdoor/XIVComboPlugin, which you can easily install in-game from the official plugin list. If you want something in the middle between that plugin and this fork, check out https://github.com/daemitus/XIVComboPlugin. The grand majority of technical work here comes from Daemitus himself, and he is the one that made most of the actual combos that aren't in the original XIVCombo fork, so please give him some love.

## Old Readme

## About
XIVCombo is a plugin to allow for "one-button" combo chains, as well as implementing various other mutually-exclusive button consolidation and quality of life replacements. Some examples of the functionality it provides:
* Most weaponskill combos are put onto a single button (unfortunately not including MNK, though MNK gets a few good features too!).
* Enochian changes into Fire 4 and Blizzard 4 depending on your stacks, a la PvP Enochian.
* Hypercharge turns into Heat Blast while Overheated.
* Jump/High Jump turns into Mirage Dive while Dive Ready.
* And many, many more!

For some jobs, this frees a massive amount of hotbar space (looking at you, DRG). For most, it removes a lot of mindless tedium associated with having to press various buttons that have little logical reason to be separate.

## Installation
* Add the repo as a custom repo using /xlsettings in-game. Then, type `/xlplugins` in-game to access the plugin installer and updater. 

## In-game usage
* Type `/pcombo` to pull up a GUI for editing active combo replacements.
* Drag the named ability from your ability list onto your hotbar to use.
  * For example, to use DRK's Souleater combo, first check the box, then place Souleater on your bar. It should automatically turn into Hard Slash.
  * The description associated with each combo should be enough to tell you which ability needs to be placed on the hotbar.
  * Make sure you press "Save and close". Don't just X out.
### Examples
![](https://github.com/attickdoor/xivcomboplugin/raw/master/res/souleater_combo.gif)
![](https://github.com/attickdoor/xivcomboplugin/raw/master/res/hypercharge_heat_blast.gif)
![](https://github.com/attickdoor/xivcomboplugin/raw/master/res/eno_swap.gif)

---

Place `https://github.com/captainbinary/MyDalamudPlugins/raw/master/pluginmaster.json` in your /xlsettings 3rd party repo list to access this plugin.
