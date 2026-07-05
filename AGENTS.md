# NewGame Unity Helper Agent Guide

This repository is a private Unity game made as a birthday present. Treat the
content, character names, personal jokes, audio, and dialogue as private project
material. Do not optimize for public distribution unless the user explicitly asks
for that.

## Project Snapshot

- Unity editor version: `6000.4.7f1`, from `ProjectSettings/ProjectVersion.txt`.
- Main enabled build scene: `Assets/Scenes/SampleScene.unity`.
- Active render stack: URP 17.4.0 with Pixel Perfect Camera, 2D lighting, local
  `com.subbu.urp-glitch`, and bundled `Unified-Universal-Blur-0.6.1`.
- Core packages: Input System 1.19.0, Visual Scripting 1.9.11, Timeline 1.8.12,
  UGUI, NavMeshPlus, Unity AI Navigation, Enhanced On-Screen Stick.
- The game is content-heavy and uses Unity Visual Scripting macros for story
  flow. Code changes should preserve prefab, scene, macro, and `.meta` links.

## Repository Map

- `Assets/Scripts/GlobalDirector.cs`: global runtime hub for map loading, game
  keys, health, audio, post effects, rain overlay, and coroutine helpers.
- `Assets/Scripts/Entities/`: player input, generated input actions, character
  movement/animation, NavMesh follower AI, and the spinning cat helper.
- `Assets/Scripts/MapInteractibles/`: interactible triggers, toggles, pressure
  plates, gates, and spawn points.
- `Assets/Scripts/Visual Scripting/`: custom Visual Scripting units for
  dialogue, choices, background changes, coroutine waiting, character switching,
  and interaction events.
- `Assets/Scripts/UI/`: dialogue box, typewriter effect, dialogue character
  portraits, HUD status items, safe area, and scrolling text.
- `Assets/Scripts/Tools/`: map/camera/rendering helpers such as dynamic sorting,
  NavMesh generation, invisible borders, and 2.5D map/sprite orientation.
- `Assets/Scripts/Props/`: cars and police lights.
- `Assets/Assets/Characters/`: `CharacterScriptableObject` assets. Some are
  playable with 12 walking sprites; some are dialogue-only and have empty
  tilesets.
- `Assets/Assets/Prefabs/Maps/`: active act maps, test map, continuation map,
  and legacy maps. Root map prefabs are identified by `Identifiable.objectId`.
- `Assets/Assets/Prefabs/Maps/*_.asset`: Visual Scripting macro assets that
  drive story flow for maps.
- `Packages/com.subbu.urp-glitch/` and `Assets/Unified-Universal-Blur-0.6.1/`:
  third-party/local rendering effects. Avoid editing unless the task is about
  those packages.

## Runtime Boot Flow

1. `SampleScene.unity` contains the persistent/global objects: `GlobalDirector`,
   `PlayerInputScript`, Unity `PlayerInput`, cameras, EventSystem, dialogue HUD,
   rain overlay, post-processing volume, and the Init Visual Scripting macro.
2. The Init macro at `Assets/Assets/Prefabs/Maps/Init.asset` calls
   `GlobalDirector.LoadMap(...)` for the selected/current starting map.
3. `GlobalDirector.LoadMap(string map)` clears the current map, destroys stashed
   objects, clears spawned characters, resets dialogue background visuals, then
   instantiates the map prefab from `GlobalDirector.maps` whose root
   `objectId` matches `map`.
4. Map prefabs usually have an object-level Script Machine pointing to their
   matching `*_.asset` macro. Those macros spawn characters, play dialogue,
   move characters, toggle game keys, change backgrounds, and load the next map.

## Active Map Set

The `GlobalDirector.maps` list in `SampleScene.unity` currently references:

- `IntroMap` at `Assets/Assets/Prefabs/Maps/Act1/IntroMap.prefab`
- `KirillIntroMap` at `Assets/Assets/Prefabs/Maps/Act2/KirillIntroMap.prefab`
- `CityMap` at `Assets/Assets/Prefabs/Maps/Act3/CityMap.prefab`
- `ToBeContMap` at `Assets/Assets/Prefabs/Maps/ToBeContMap.prefab`
- `TestWalkingMap` at `Assets/Assets/Prefabs/Maps/TestWalkMap.prefab`
- `Act4Map` at `Assets/Assets/Prefabs/Maps/Act4/Act4Map.prefab`
- `Act5Map` at `Assets/Assets/Prefabs/Maps/Act5/Act5Map.prefab`

Legacy maps remain under `Assets/Assets/Prefabs/Maps/Legacy/`, including older
intro/city/house/hospital/tavern/boss maps. They are useful reference material,
but they are not in the active map list unless re-added in the scene inspector.

## Static Runtime Hubs

- `GlobalDirector.Shared`: global map, state, UI, audio, rain, and post effects.
- `PlayerInputScript.Shared`: party/follower state, input enable/disable,
  character spawning, character switching, following, and cutscene movement.
- `CameraScript.Shared`: camera follow target, side-perspective camera tilt, and
  Pixel Perfect Camera PPU changes.
- `UIDialogMessage.Shared`: dialogue background, message UI, dialogue portraits,
  options, note label, and dialogue input polling.
- `Act4Script.Shared`: Act 4 "crazy level" overlay and analog glitch control.
- `ShootingManager.Shared`: legacy boss projectile/toggle phase controller.

For new code, prefer assigning singleton-like `Shared` references in `Awake` and
clearing them on destroy if needed. This project has some older constructors that
set `Shared`; avoid copying that pattern.

## Characters And Party State

- Character definitions use
  `Assets/Assets/Characters/CharacterScriptableObject.cs`.
- Important fields: `charName`, `nameColor`, `avatar`, `body`, `tileset`,
  `size`.
- In-game walking characters need a 12-sprite `tileset` arranged as:
  down 0-2, left 3-5, right 6-8, up 9-11.
- Dialogue-only characters may have `avatar`/`body` and an empty `tileset`.
  Do not spawn them as walkable party members unless tiles are added.
- Runtime health lives in `GlobalDirector.Shared.health` and is initialized to
  100 when a character is added through `PlayerInputScript.InternalAddCharacter`.
- `PlayerInputScript.CharactersInParty` and `CharactersInFolowers` are static
  lists used by Visual Scripting and spawn helpers.

## Input Map

The generated file `Assets/Scripts/Entities/PlayerControlMap.cs` should not be
edited by hand. Edit `Assets/Scripts/Entities/PlayerControlMap.inputactions`
inside Unity, then let Unity regenerate the C# wrapper.

Current important bindings:

- Move: WASD, arrows, gamepad left stick.
- Run: Left Shift, gamepad South.
- Next/previous character: E/Q, gamepad right/left shoulder.
- Toggle following: Space, gamepad North, but only when `CanToggleFollowing`.
- Interact: F, gamepad West.
- Dialogue next/fast: Space, left mouse, touch, and generic submit.
- UI navigation: WASD, arrows, sticks, dpad.

## Movement And AI

- `CharacterScript` owns sprite direction, frame animation, hit flash, and
  Rigidbody movement.
- `PlayerInputScript.FixedUpdate` moves only the active character directly.
- Followers use `FollowerAIScript` with a `NavMeshAgent`. AI toggles also switch
  the character `BoxCollider` to trigger mode.
- `GenerateNavMesh` waits one frame and then builds a `NavMeshSurface` at map
  start. Maps using follower movement need a valid NavMesh setup.
- Cutscene movement is coroutine-based. Preferred helpers:
  `MoveCoroutine`, `MoveToPointCoroutine`, `MoveDeltaCoroutine`,
  `MoveCharCoroutine`, `MoveCharToPointCoroutine`, and
  `MoveCharDeltaCoroutine`.
- Do not start two movement coroutines for the same character at the same time.
  The current `_charIsMoving` dictionary is not overlap-safe.

## Interaction System

- `Identifiable.objectId` is the project-wide string key for maps, spawns,
  interactibles, waypoints, and other important scene objects.
- `Identifiable.Awake` registers non-empty IDs in
  `GlobalDirector.Shared.GameObjectsStash`.
- `GlobalDirector.GetEntityById(id)` returns stashed objects for Visual
  Scripting macros.
- `Interactable` raises Visual Scripting events:
  - `InteractionEvent` when the player presses interact on it.
  - `InteractionTriggerEnterUnit` when a character enters its trigger.
  - `InteractionTriggerExitUnit` when a character exits its trigger.
- `ToggleItem` and `ToggleItemExtra` store their enabled state in
  `GlobalDirector` game keys using their `objectId`.
- `PressurePlateScript` counts characters inside the trigger and fires enter/exit
  only on first press and final release.
- `GateScript.IsOpen` switches closed/opened child prefabs.
- `SpawnPoint` is just an `Identifiable` plus spawn direction.

Keep `objectId` values unique within the currently loaded map unless deliberate
last-writer-wins behavior is desired.

## Dialogue And Visual Scripting

Prefer the existing custom units and static helpers for new story content:

- `VSMessageUnit`: generic avatar/name/color/message dialogue.
- `VSMessageCharUnit`: character-based dialogue using a
  `CharacterScriptableObject`.
- `VSMessageArrayUnit`: multiple messages with one speaker.
- `VSBackgroundImageUnit`: fades the backstage image.
- `RunAndWaitForCoroutineUnit`: starts an IEnumerator and waits for completion.
- `VNSelect`: creates UI buttons for choices and branches to the selected output.
- `SwitchOnCharacter`: branches by `CharacterScriptableObject`.
- `InteractionActionUnit`, `InteractionTriggerEnterUnit`,
  `InteractionTriggerExitUnit`: listen for project interaction events by ID.

Dialogue flow normally:

1. Call `GlobalDirector.ShowDialog()` to disable player movement.
2. Open `UIDialogMessage.OpenMessageView()`.
3. Yield `UIDialogMessage.SetMessage(...)`.
4. Wait until `UIDialogMessage.Shared.DialogNext`.
5. Close the view and call `GlobalDirector.CloseDialog()`.

The message units already do this. Use custom code only for special cases.

## UI And Presentation

- `UIDialogMessage` controls backstage color/image fades, message box, avatar,
  name label, typewriter, note label, dialogue portraits, and options.
- `UIMessageTypewriterEffect` reveals TextMeshPro characters over time and uses
  `GlobalDirector.typeWriterSpeedNormal/Fast`.
- `DialogCharacterScript` displays full-body dialogue portraits and supports
  position, alpha, black mask, and flip animations.
- `UIStatusHUD` rebuilds status rows from currently spawned characters.
- `UICharacterStatus` reads health from `GlobalDirector.Shared.health`.
- `UISafeArea` anchors UI to `Screen.safeArea`.
- `MapRelatedCanvas` points map-local canvases at `GlobalDirector.uiOverlayCamera`.

## Camera, Map Geometry, And Rendering

- `CameraScript` follows the active character unless an override target is set.
- `useSidePerspective` tilts the camera and offsets the followed position on Y.
- `MapPositioning3D` rotates map planes to `(-90, 0, 0)`.
- `SpriteMimicCameraAngle` keeps sprites aligned with the main camera X angle.
- `RenderOrderByCoordinate` sets sprite sorting order to
  `(int)(-transform.position.y * 10)`.
- `InvisibleBorder` hides its `SpriteRenderer` at runtime.

## Special Act And Boss Systems

- Act 4:
  - `Assets/Assets/Prefabs/Maps/MapScripts/Act4Script.cs` controls `crazyLevel`.
  - `Act4ShakeEffect` shakes and fades an overlay image.
  - `SetCrazyLevel` also adjusts `AnalogGlitchVolume` values on the current post
    effect profile.
- Legacy boss:
  - `ShootingManager` controls shooter lists, phases, boss health, toggles,
    shield visibility, game-over, and win events.
  - `ProjectileShooter` instantiates projectiles on a delay.
  - `ProjectileObject` moves by Rigidbody2D velocity, damages player health, and
    destroys itself after 10 seconds.

## Common Development Workflows

### Add A Walkable Character

1. Create a `CharacterScriptableObject` in `Assets/Assets/Characters/`.
2. Set `charName`, `nameColor`, `avatar`, `body`, `size`, and all 12 `tileset`
   sprites.
3. Use Visual Scripting or `PlayerInputScript.SpawnCharacters/AddCharacter` to
   add the character to the party or followers.
4. Verify animation in all four directions and HUD health display.

### Add A Dialogue-Only Character

1. Create a `CharacterScriptableObject`.
2. Set `charName`, `nameColor`, `avatar`, and `body`.
3. Empty `tileset` is acceptable only if the character is never spawned as a
   walkable `CharacterScript`.
4. Use `VSMessageCharUnit` and dialogue portrait helpers.

### Add A Map

1. Create a map prefab under `Assets/Assets/Prefabs/Maps/`.
2. Add `Identifiable` to the root and set a stable unique `objectId`.
3. Add an object-level Script Machine if the map has story flow, pointing to a
   matching macro asset.
4. Add spawn points and important waypoints as `Identifiable` objects.
5. Add `GenerateNavMesh`/NavMeshSurface if followers or cutscene movement need
   pathfinding.
6. Add the map prefab to `GlobalDirector.maps` in `SampleScene.unity`.
7. Load it only by root `objectId`, not by prefab filename.

### Add An Interaction

1. Add a trigger collider and an `Interactable`, `ToggleItem`,
   `ToggleItemExtra`, or `PressurePlateScript`.
2. Set a unique `objectId`.
3. Listen with the matching Visual Scripting event unit and the same ID.
4. For toggles, remember that the game key name is the same as `objectId`.

### Add A Cutscene Movement

1. Resolve target objects through direct object references or
   `GlobalDirector.GetEntityById`.
2. Use an existing `PlayerInputScript.Move*Coroutine` helper.
3. Wrap with `RunAndWaitForCoroutineUnit` when sequencing matters.
4. Avoid overlapping movement coroutines for the same character.
5. Always consider timeout values so a blocked NavMesh path cannot stall the
   story forever.

### Change Dialogue Or Story Flow

1. Prefer editing Visual Scripting macro assets in Unity.
2. Keep serialized macro references intact.
3. Use the existing message, background, choice, coroutine, and interaction
   units.
4. Keep player input disabled only while dialogue/choice UI needs exclusive
   control.

## Review Findings And Risk Log

- `GlobalDirector` and `PlayerInputScript` assign `Shared` in constructors.
  Unity MonoBehaviour constructors are not a reliable lifecycle hook. Future code
  should set shared instances in `Awake`.
- `Act4Script.SetDreamGlitchEffectWeight` can leave `_dreamGlitchEffect` null if
  the active profile lacks `AnalogGlitchVolume`; the next call can dereference
  null because `_firstTime` is already false.
- `PlayerInputScript._charIsMoving` uses `Add` and unchecked indexers, so
  overlapping move coroutines or stopping a non-moving character can throw.
- `DialogCharacterScript.Flip()` toggles `_flipped` but always sets Y rotation to
  180 degrees; repeated instant flips do not return to the unflipped state.
- `ShootingManager.ShootWaveOnce` computes a reversed enumerable but loops over
  the original list, so the `reverced` parameter currently has no effect.
- `GlobalDirector.LoadMap` uses `First` with no guard. A missing map ID or a
  map prefab whose root `objectId` does not match the load string will throw.
- `CharacterScript.FrameForDirection` assumes 12 tiles. Empty dialogue-only
  character tilesets will crash if used as walkable characters.
- `VNSelect` will wait forever if it has no options/branches. Ensure every
  choice graph defines at least one option.
- `Identifiable.OnDestroy` writes null into the stash instead of removing keys.
  `GetEntityById` callers must be null-safe.

## Coding And Asset Rules For Future Agents

- Keep edits scoped. This project is a personal gift game; speed and reliability
  for the birthday build matter more than broad rewrites.
- Preserve Unity `.meta` files and serialized references.
- Do not hand-edit generated `PlayerControlMap.cs`; edit the `.inputactions`
  asset through Unity.
- Avoid changing third-party/vendor package code unless directly requested.
- Use the existing coroutine and Visual Scripting patterns before adding new
  framework abstractions.
- For C# scripts, use Unity lifecycle methods (`Awake`, `Start`, `OnEnable`,
  `OnDisable`, `OnDestroy`) rather than constructors.
- For new static helpers, make failure behavior clear and null-safe. Visual
  Scripting often calls these methods without compile-time guardrails.
- When touching maps or prefabs, verify in Unity Play Mode. Text-only review of
  YAML is useful but cannot prove serialized graph behavior.

## Verification Checklist

After meaningful changes, verify in Unity 6000.4.7f1:

- `SampleScene.unity` opens without missing script/package errors.
- Play Mode starts and the Init macro loads the intended map.
- Player movement, running, interact, character switching, and following work.
- Dialogue can advance and speed up with keyboard/mouse/touch as expected.
- Any edited Visual Scripting macro reaches its next map/event.
- Map transitions clear old objects and spawn the correct characters.
- Followers move on the map NavMesh without blocking cutscenes.
- The console is free of new errors.
- For Act 4 or boss changes, explicitly test `SetCrazyLevel`, projectile phases,
  game-over, and win events.
