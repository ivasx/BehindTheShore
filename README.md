# Behind The Shore

**[English](README.md)** | [Українська](README-UA.md)

A 2D action-adventure game built with Unity featuring dynamic enemy AI, combat mechanics, and exploration gameplay.

[![Unity Version](https://img.shields.io/badge/Unity-2022.3+-blue.svg)](https://unity.com/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

## About The Game

Behind The Shore is an action-packed 2D game where players must collect mystical runes while battling enemies in a mysterious world. Features include:

- **Dynamic Enemy AI** - Enemies with roaming, chasing, and attacking behaviors
- **Combat System** - Engage enemies with strategic combat mechanics
- **Health Management** - Health bar with regeneration system
- **Collectibles** - Find and collect runes to win the game
- **NavMesh Pathfinding** - Smart enemy navigation using Unity NavMesh

## How To Play

- **WASD** or **Arrow Keys** - Move your character
- **Mouse** - Aim direction
- **ESC** - Pause menu
- **Objective** - Collect all runes to win!

## Download & Installation

### Option 1: Play Compiled Game (Recommended)

1. Go to [Releases](../../releases)
2. Download the latest `BehindTheShore_Build.zip`
3. Extract the archive
4. Run `BehindTheShore.exe` (Windows) or the appropriate executable for your platform
5. Enjoy the game!

### Option 2: Open in Unity Editor

**Requirements:**
- Unity 2022.3 or newer
- Git installed on your system

**Steps:**

1. **Clone the repository:**
   ```bash
   git clone https://github.com/yourusername/behind-the-shore.git
   cd behind-the-shore
   ```

2. **Open in Unity:**
   - Launch Unity Hub
   - Click "Open" or "Add"
   - Navigate to the cloned repository folder
   - Select the project folder and click "Open"

3. **Play in Editor:**
   - Wait for Unity to import all assets
   - Open the `MainMenu` scene from `Assets/Scenes/`
   - Press the Play button in Unity Editor

## Technical Details

### Built With
- **Engine:** Unity 2022.3+
- **Language:** C#
- **AI Navigation:** Unity NavMesh Plus
- **Input System:** Unity's new Input System

### Key Systems

**Enemy AI (EnemyAI.cs)**
- State machine with Idle, Roaming, Chasing, and Attacking states
- NavMesh-based pathfinding
- Configurable detection and attack ranges
- Dynamic speed adjustments

**Player System (Player.cs)**
- Health management with regeneration
- Damage recovery mechanics
- Knockback physics
- Input handling via Unity's Input System

**Combat System**
- Hit detection using colliders
- Damage calculation
- Visual feedback with flash effects
- Knockback on damage

## Game Controls

| Action | Key/Input |
|--------|-----------|
| Move | WASD / Arrow Keys |
| Aim | Mouse |
| Pause | ESC |

## Development

### Prerequisites
- Unity 2022.3 LTS or newer
- Visual Studio 2022 or JetBrains Rider
- Git

### Setting Up Development Environment

1. Clone the repository
2. Open in Unity Hub
3. Let Unity import all packages
4. Open MainMenu scene
5. Start developing!

### Building the Game

1. Open Unity Editor
2. Go to File > Build Settings
3. Select your target platform
4. Click "Build" and choose output folder
5. The executable will be generated in the selected folder

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments

- Unity Technologies for the game engine
- NavMeshPlus for 2D navigation
- All asset creators and contributors

## Contact

Project Link: [https://github.com/yourusername/behind-the-shore](https://github.com/yourusername/behind-the-shore)

---

**Note:** Replace `yourusername` with your actual GitHub username in all links.
