<div align="center">

# Miserable Idle Game

**Simple game for simple person**

![Unity](https://img.shields.io/badge/Unity_6.6-gray?style=for-the-badge&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/C%23-purple?style=for-the-badge&logo=csharp&logoColor=white)

<br>

<img width="800" alt="Gameplay preview" src="https://github.com/user-attachments/assets/26a83387-7251-4b8a-a6c5-a116e97083cc" />

<br>

[Quick Start](#quick-start) · [Controls](#controls) · [Project structure](#project-structure)

</div>

## About

Miserable Idle Game is a small idle/clicker game I made as **programming practice**, while also learning a bit of art and UI. 

Beatable in less than 1 hour.

### How to progress

Gain points and buy all available upgrades to the max level.
After reaching this point click the heart multiple times.

### Note

Unlikely this game will be updated in the future.
If you reading after completing this game, I am alright.

## Features

- Idle progression: earn while you wait or click the heart
- Shop with upgrades
- Save system
- Small dynamic events

## Quick Start

**Requirements:** Unity **6.6** (`6000.6.0f1`)

```bash
git clone https://github.com/Issiins/IdleGame.git
```

1. Open **Unity Hub** → **Add** → **Add project from disk**
2. Select the cloned `IdleGame` folder
3. Open it with Unity `6000.6.0f1`
4. Open the scene `[Assets/Scenes/Main.unity]` and press **Play**

## Controls

| Control        | Input                           | State        |
|----------------|---------------------------------|--------------|
| Save game file | `S`                             | In Game      |
| Leave the game | `ESC`                           | In Game      |
| Open Shop      | Mouse right click / `B`         | In Game      |
| Close Shop     | Mouse right click / `B` / `ESC` | In Game Shop |

## Project Structure

```
Assets/
├── Animation/              # animation controllers and clips
├── Art/                    # sprites for UI and icons
├── Audio/                  # audio files
├── Resources/
│   └── Upgrades/           # upgrade data files
├── Scenes/
├── Scripts/
│   ├── Buttons/            # button event scripts for main menu
│   └── Player/             # player and system based scripts
│       ├── IO/             # input and output scripts
│       ├── Point System/   # point controlling system
│       ├── States/         # state machine and state scripts
│       └── Upgrades/       # upgrade scripts
└── UI/                     # UI based scripts
```
## Credits

### Third-party assets

**Audio** (via [Freesound](https://freesound.org))

| Sound | Author | License | Source |
|-------|--------|---------|--------|
| Heart Beat | [ChamoneSteyn](https://freesound.org/people/ChamoneSteyn/) | [CC BY 4.0](https://creativecommons.org/licenses/by/4.0/) | [Link](https://freesound.org/people/ChamoneSteyn/sounds/542218/) |
| Glass Breaking | [HenKonen](https://freesound.org/people/HenKonen/) | [CC0 1.0](https://creativecommons.org/publicdomain/zero/1.0/) | [Link](https://freesound.org/people/HenKonen/sounds/757209/) |

**Other assets were made by me**
