# Crown of Eternity

A 2D action RPG built in Unity 6 (URP). Focused on responsive player control, tight combat, and a clean UI with save/load.

## Tech Stack
- Unity 6 (URP), C#
- Git + GitHub (Visible Meta Files, Force Text)
- Git LFS (optional later for big assets)

## Getting Started
1. **Clone**
   ```bash
   git clone git@github.com-edu:sdsouz12/crown-of-eternity.git

Open in Unity 6 (LTS) and let it import.

Play from Assets/Scenes/Main.unity (placeholder).


Project Structure
Assets/
  Scripts/
  Art/
  Scenes/
ProjectSettings/
Packages/


Branching Strategy

main → protected (release-ready)

Feature branches: feat/<area>-<short-desc>

e.g., feat/player-controller, feat/combat-hitstop

Fix branches: fix/<area>-<issue>

Chore: chore/<task>

Workflow

Create a branch from main.

Commit small, meaningful changes.

Open a PR → 1 review required → squash merge.

Milestones

M1: Player controller & camera

M2: Combat loop (attack, hit, enemy)

M3: Inventory/crafting, stats

M4: UI, save/load

M5: Polish & build

Contributors

@sdsouz12 (Maintainer)

Add teammates when ready.



Quick add & push:
```bash
printf '%s\n' "# Crown of Eternity" > README.md  # or paste the full content above
git add README.md
git commit -m "docs: add project README"
git push




