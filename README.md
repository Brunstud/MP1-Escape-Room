# MP1b — Shicheng's Room

**Team:** 17
**Developer:** Shicheng Hu
**Branch:**`<span>[mp1b-shicheng]</span>`
**Raw Rubric Claim:** **47 pts**

## Scene

A complete **100-years-ago Principal's Office** escape-room scene centered on repairing the  **AION Time Machine** . The room includes five puzzles, a hidden Secret Room, collectibles, a subjective-time loop, loss/restart logic, progress displays, and scene-to-scene travel with held-object persistence.

## Controls

* **Grip** — Grab / release objects
* **Trigger / Activate** — Use the clock, gramophone, metronome, Time Machine, and UI interactions

## Rubric

| Criterion                        | Pts          | Implementation                                                                                                                                                                                         |
| -------------------------------- | ------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| Key Props with Grab Affordances  | 1            | 3+ required Time Machine Key Props use XR Grab Interactables.                                                                                                                                          |
| Solid Objects                    | 1            | Key Props and many room props use Rigidbodies/Colliders; the room floor has collision.                                                                                                                 |
| Locks with Accepting Affordances | 3            | 3+ Time Machine sockets accept matching Key Props and trigger lock events.                                                                                                                             |
| Escaping the Room                | 3            | Required locks must be completed before the Time Machine can be used to escape.                                                                                                                        |
| Grab Signifiers                  | 2            | Gold = machine parts, Orange = start-up materials, Cyan = information/score, Green = miscellaneous interactables.                                                                                      |
| Escape Signifiers                | 2            | Missing-part ghosts, machine status UI, and start-up slots indicate the Time Machine is the escape objective.                                                                                          |
| Lock Signifiers                  | 4            | Matching ghost models, colors, shapes, and socket positions indicate Key-to-Lock relationships.                                                                                                        |
| Repetition & Variety             | 2            | Stage 1 uses missing-part shape matching; Stage 2 uses functional/material matching for Battery and Bell.                                                                                              |
| Eased State Changes              | 2            | All locks use eased key placement and ghost fade-out feedback when solved.                                                                                                                             |
| Reveals                          | 2            | Rotating the clock by 45° moves the bookshelf/wardrobe and reveals the Secret Room.                                                                                                                   |
| Gate Affordance                  | 2            | The repaired Time Machine acts as an interactable gate to the next area/scene.                                                                                                                         |
| Gated Content                    | 1            | 2 gated locations: the Principal's Office and the hidden Secret Room.                                                                                                                                  |
| Connected Scenes                 | 3            | The Time Machine loads the next Unity scene while preserving held objects.                                                                                                                             |
| Loss Timer                       | 1            | A visible 3:00 countdown reaches Game Over at 00:00.                                                                                                                                                   |
| Restart Option                   | 1            | Failure UI provides a Restart button that resets the Principal's Office challenge.                                                                                                                     |
| Collectibles                     | 2            | 5 collectible books are scattered through the room; collecting one increments the in-scene scoreboard.                                                                                                 |
| Puzzle System                    | 2            | Multiple distinct puzzle interactions advance access to required Time Machine keys/materials.                                                                                                          |
| Puzzle Content                   | 3            | 5 distinct puzzles correspond to Machine Parts 1–3, Power Source/Battery, and Resonant Metal/Bell.                                                                                                    |
| Puzzle Discoverability           | 3            | Wall writing, environmental props, the clock/bookshelf, gramophone loop, metronome, and machine visuals provide puzzle clues.                                                                          |
| Progress Scoreboard              | 1            | Right-side mirror displays remaining undiscovered Keys and unsolved Locks.                                                                                                                             |
| Puzzle Scoreboard                | 1            | Left-side mirror displays puzzle clue/progress information.                                                                                                                                            |
| Red Herrings                     | 5            | 13+ extra grabbable physics props, including books, vase, gramophone, globe, pillow, batteries, coins, bells, lamps, Tesla coils, energy cores, metronomes, glow tubes, violin, and time-anchor props. |
| **Raw Total**              | **47** |                                                                                                                                                                                                        |

## Asset Credits

* **Ring** — Lucia Lunadottir — Creative Commons Attribution (CC BY)
  [https://sketchfab.com/3d-models/ring-c7de57f1c22749c7bf4c969bdb69338c](https://sketchfab.com/3d-models/ring-c7de57f1c22749c7bf4c969bdb69338c)
* **Tesla Coil** — Kim Tsyhanovskyi — CC BY 4.0
  [https://sketchfab.com/3d-models/tesla-coil-13fd21edb4a94195889f9f90e2ed36ba](https://sketchfab.com/3d-models/tesla-coil-13fd21edb4a94195889f9f90e2ed36ba)
* **Sci Fi Energy Core 1** — FilipZelinka — CC BY 4.0
  [https://sketchfab.com/3d-models/sci-fi-energy-core-1-a5aefe7b281f46529aed2ec9d873bc52](https://sketchfab.com/3d-models/sci-fi-energy-core-1-a5aefe7b281f46529aed2ec9d873bc52)
* **Bell** — Mr. Eye — CC BY 4.0
  [https://sketchfab.com/3d-models/bell-087122bc1c7f4b8998b58b650605a67c](https://sketchfab.com/3d-models/bell-087122bc1c7f4b8998b58b650605a67c)
* **Battery** — Quaternius
  [https://poly.pizza/m/MYa3uWdwPU](https://poly.pizza/m/MYa3uWdwPU)
* **CC0 - Pencil** — plaggy — CC BY 4.0
  [https://sketchfab.com/3d-models/cc0-pencil-cb1b27db90eb469eb845017bb300b5d3](https://sketchfab.com/3d-models/cc0-pencil-cb1b27db90eb469eb845017bb300b5d3)
* **Victorian Office Mini Pack** — Thracco
  [https://thracco.itch.io/victorian-office-mini-pack](https://thracco.itch.io/victorian-office-mini-pack)
* **Victorian Study 3D Asset Pack** — FoxDevArt
  [https://foxdevart.itch.io/victorian-study-3d-asset-pack](https://foxdevart.itch.io/victorian-study-3d-asset-pack)
* **[Steins;Gate] Time machine** — mrTorch — CC BY 4.0
  [https://sketchfab.com/3d-models/steinsgate-time-machine-f0aa81a123b54f77ac2ff4a4630cc050](https://sketchfab.com/3d-models/steinsgate-time-machine-f0aa81a123b54f77ac2ff4a4630cc050)
* **Simple Wooden Door** — kusuma844 — CC BY 4.0
  [https://sketchfab.com/3d-models/simple-wooden-door-19e6a54d3d14466a9099d58c71619d5a](https://sketchfab.com/3d-models/simple-wooden-door-19e6a54d3d14466a9099d58c71619d5a)
* **Old Office Window** — sudreyskr — CC BY 4.0
  [https://sketchfab.com/3d-models/old-office-window-6851ada65b23464da79eb5468c1cee3d](https://sketchfab.com/3d-models/old-office-window-6851ada65b23464da79eb5468c1cee3d)
* **Grandfather Clock** — Lyskilde — CC BY 4.0
  [https://sketchfab.com/3d-models/grandfather-clock-cef39f1bd3df43578236f273f273a873](https://sketchfab.com/3d-models/grandfather-clock-cef39f1bd3df43578236f273f273a873)
* **Vintage Gramophone** — Maxim Mavrichev — CC BY 4.0
  [https://sketchfab.com/3d-models/vintage-gramophone-a7508a4233a344008029d21a2f0024bd](https://sketchfab.com/3d-models/vintage-gramophone-a7508a4233a344008029d21a2f0024bd)
* **[Steins;Gate] Divergence Meter** — G.Salmon — CC BY 4.0
  [https://sketchfab.com/3d-models/steinsgate-divergence-meter-65646a3a705642cca0dd290753cf481b](https://sketchfab.com/3d-models/steinsgate-divergence-meter-65646a3a705642cca0dd290753cf481b)
* **Vintage Metronome** — Weekless — CC BY 4.0
  [https://sketchfab.com/3d-models/vintage-metronome-5f76f9ecd6624681b148783fb76a4854](https://sketchfab.com/3d-models/vintage-metronome-5f76f9ecd6624681b148783fb76a4854)
