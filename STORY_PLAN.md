# Re-Born 2 Story and Production Plan

> Private working document for the birthday game. This is the authoritative
> tracker for future story development, production scope, and unresolved
> decisions. `MAIN_GAME_PLOT.md` remains the detailed narrative bible for series
> canon, implemented story, and the confirmed dramatic arc.

## Document Contract

- [MAIN_GAME_PLOT.md](MAIN_GAME_PLOT.md) is the narrative source of truth for
  series continuity, relationships, act summaries, player-facing revelations,
  and the confirmed dramatic arc.
- `STORY_PLAN.md` is the production source of truth for decision status, scope,
  mechanics, deadlines, fallbacks, and unresolved implementation choices.
- A **CONFIRMED** future decision in this plan should also appear as planned
  narrative in `MAIN_GAME_PLOT.md`; it must not remain listed there as an open
  story question.
- When planned content becomes playable, update its status here and move the
  corresponding plot description from planned to implemented in
  `MAIN_GAME_PLOT.md`.
- If implementation status is uncertain, the current Unity assets determine
  what is playable; the documents should then be corrected together.

## Decision Status

- **CONFIRMED** — established story direction. Treat it as canon unless Danya
  explicitly changes it.
- **WORKING** — the recommended direction and current basis for planning, but
  still adjustable.
- **TBD** — a decision or creative detail that has not been settled.
- **BACKUP** — a deadline-safe alternative that replaces a riskier plan if its
  production gate is missed.

Planned material must retain one of these labels until it becomes playable.
Descriptions of implemented content are labeled **IMPLEMENTED**.

## Continuity Anchors

- **CONFIRMED:** Andrea is Danya's real-life girlfriend.
- **CONFIRMED:** Dasha is Kirill's real-life wife. Her opposition to HELIOS is
  motivated by wanting to save her husband and restore the good person he used
  to be.
- **CONFIRMED:** Soloway is Danya and Kirill's real-life friend. Dasha hires him
  to approach Danya without exposing her own position inside PyCorp.
- **CONFIRMED:** Kirill previously died after being hit by a bus, was trapped by
  the soul-transfer app, and was extracted into an artificial Cat Boy body.
- **CONFIRMED:** XITRIX is a fragment of Danya's mind created during alpha
  testing of that app. He was once Kirill's captor, later became an
  administrator-like NPC and uneasy ally, learned to create artificial souls,
  lived with Lili in a shattered virtual space, and helped both brothers defeat
  an evil griefer.
- **CONFIRMED:** Kirill secretly extracted XITRIX from that stable pocket and
  used him as an artificial-soul test subject while already influenced by the
  absorbed Kirill collective. The collective amplified his cruelty and weakened
  the restraint that would once have made such treatment of a soul unacceptable.
- **CONFIRMED:** The collective's influence explains Kirill's willingness to
  conduct the trials but does not erase his responsibility for choosing to do so.
- **CONFIRMED:** **Tower of XITRIX** and **XITRIX Elysium** were literal,
  sequential virtual trials experienced exclusively by XITRIX while imprisoned
  by PyCorp.
- **CONFIRMED:** In-game Danya did not play, control, or know about those worlds.
  Their relationship to the real birthday gifts is meta-level rather than an
  event inside the current story.
- **CONFIRMED:** References in the original ELYSIUM material that describe
  "The Last Tekken" as an XITRIX past life are excluded from this continuity.
  The real Danya and Kirill remain the participants in Last Tekken.
- **CONFIRMED:** TOWER tested XITRIX through forced reincarnation and hostile
  loops. ELYSIUM tested memory removal, death, regeneration, branching choices,
  resets, and submission to an administrator-controlled world.
- **CONFIRMED:** ELYSIUM's memory cards, attributes, regeneration, and the
  Barbarian, Mage, Empathy, and Leader interfaces were real within that world
  but do not persist as present-day abilities.
- **CONFIRMED:** XITRIX's history makes him useful but not automatically
  trustworthy when he returns as Danya's phone companion.
- **CONFIRMED:** XITRIX knows that the collective influenced Kirill during his
  captivity. He wants HELIOS stopped, the collective separated, and the original
  Kirill saved rather than punished through another prison.
- **CONFIRMED:** Kirill's earlier clone-army technology and Danya's
  soul-transfer technology are separate pieces of series continuity that Kirill
  combines in the present story.

## Production Goal and Constraints

- **CONFIRMED:** Deliver a complete playable game by **September 16, 2026**.
- **CONFIRMED:** Plan for approximately **10–15 hours of development per
  week**.
- **CONFIRMED:** Protect the emotional resolution and complete ending before
  optional characters, maps, mechanics, or polish.
- **CONFIRMED:** The virtual finale contains **three playable friend
  encounters**. Other friends may participate through support roles.
- **CONFIRMED:** The mind encounters reuse **one transforming virtual arena**
  rather than requiring three unrelated maps.
- **WORKING:** New story scenes should reuse existing characters, locations,
  dialogue systems, Visual Scripting units, interaction mechanics, and effects
  wherever possible.
- **WORKING:** Narrative choices may change dialogue and endings, but should not
  create large parallel campaigns before the deadline.

## Current Playable Story Boundary

### Acts 1–5

- **IMPLEMENTED:** Act 1 introduces Kirill through his public success and
  PyCorp monopoly, then reveals Danya's suspicion that his brother has changed.
- **IMPLEMENTED:** Act 2 exposes Kirill's abusive private behavior, frightened
  scientists, and an unstable transfer-related prototype.
- **IMPLEMENTED:** Act 3 sends Danya into the rainy city, where Soloway secretly
  recruits him to investigate SkyTower.
- **IMPLEMENTED:** Act 4 reveals through Kirill's nightmare that he combined
  cloning and soul-transfer technology, absorbed alternative versions of
  himself, and is losing control of their collective.
- **IMPLEMENTED:** Act 5 contains the complete taxi journey: Danya reviews
  Soloway's letter and infiltration instructions, unexpectedly finds Law driving
  the taxi, asks him to remain nearby as an escape option, sleeps during the
  trip, and wakes at SkyTower.
- **IMPLEMENTED:** Act 5 then makes the SkyTower exterior playable and ends with
  Danya using the ladder and ventilation route to enter the laboratory.

### Act 6 — SkyTower Interior

- **IMPLEMENTED:** Danya enters the dark laboratory interior through the
  ventilation system.
- **IMPLEMENTED:** He encounters a laser barrier, receives an anonymous phone
  message telling him to wait, and sees the barrier disabled remotely.
- **IMPLEMENTED:** The interior contains readable HELIOS documents, a staff
  directory, an approval-chain puzzle, and a password-protected PyCorp
  terminal.
- **IMPLEMENTED:** The HELIOS password puzzle has a complete solution and no
  progressive hints. The hints were intentionally removed so the document
  puzzle remains challenging.
- **IMPLEMENTED:** A nearby door interaction establishes that the route remains
  locked from the outside.
- **IMPLEMENTED:** After unlocking the terminal, Danya sees alarming fragments
  about soul transfer, global mind control, and a virtual world maintained by
  controlled bodies. He copies the full archive to his phone to investigate
  later rather than reading every document inside SkyTower.
- **IMPLEMENTED:** Dasha enters, Danya hides in the wardrobe, and she discovers
  him. She reveals that she hired Soloway, explains that the cameras were
  already offline, exposes the ventilation route as Soloway's joke, and asks
  Danya to help save Kirill.
- **IMPLEMENTED:** Dasha returns Danya's phone after the transfer completes and
  guides him toward the unguarded front-door route. Act 6 currently ends on the
  departure fade because Act 7 does not exist yet.
- **CONFIRMED:** XITRIX is the anonymous presence that disabled the lasers.
- **CONFIRMED:** XITRIX escaped physical containment by moving into PyCorp's
  local network.
- **CONFIRMED:** XITRIX's identity remains hidden throughout Act 6. When Danya
  connects his phone to the terminal, XITRIX silently copies himself onto it.
- **CONFIRMED:** The transfer is a mandatory story event and does not require a
  game-state flag or player choice.
- **CONFIRMED:** The `Unknown` dialogue character is a generic presentation for
  any speaker whose identity is hidden; using it for more than one character
  does not identify those characters as the same person.
- **CONFIRMED:** XITRIX does not introduce himself until the planned Act 7 taxi
  ride.
- **CURRENT DEVELOPMENT STATE:** The Init graph launches `Act6Map` directly for
  interior testing. This is not the intended narrative opening and must be
  restored to the full Act 1–6 sequence for integration testing.

## XITRIX Trial Canon

### Tower of XITRIX

- **CONFIRMED:** TOWER is the first PyCorp world imposed on XITRIX after Kirill
  removes him from his stable pocket reality.
- **CONFIRMED:** Its hostile reincarnation loops and threats—including Zhora,
  Evil Paimon, and mimics with guns—are genuine experiences for XITRIX rather
  than fictional references he merely observed.
- **CONFIRMED:** TOWER measures whether an artificial soul retains identity and
  adapts when repeatedly transferred, killed, and returned to play.

### XITRIX Elysium

- **CONFIRMED:** ELYSIUM is the more advanced follow-up trial.
- **CONFIRMED:** XITRIX wakes with damaged memory, builds and defends the snowy
  kingdom, confronts Kirill as the world's god-like ruler, and experiences the
  resets and ending choices as real events inside that reality.
- **CONFIRMED:** Death and regeneration consume "Memories from Past Life" cards.
  The four inner voices are decision interfaces installed by Kirill to provoke
  and measure different responses.
- **CONFIRMED:** The cards, voices, skills, and regeneration belong to ELYSIUM's
  rules. XITRIX retains their memories and experience after extraction, not the
  mechanics themselves.
- **CONFIRMED:** ELYSIUM tests whether XITRIX remains coherent when memory,
  identity, apparent freedom, and the ability to leave are controlled by an
  administrator.
- **CONFIRMED:** ELYSIUM foreshadows HELIOS: Kirill presents an endless virtual
  existence as a gift while withholding meaningful consent to leave.

## HELIOS

### Purpose

- **CONFIRMED:** **HELIOS** is Kirill's global mind-control project.
- **CONFIRMED:** It combines Danya's soul-transfer technology with Kazuya's
  captured power.
- **CONFIRMED:** Kazuya's power amplifies the transfer system from individual
  soul movement into mass influence and control.
- **CONFIRMED:** PyCorp's control of game engines, hardware, software, servers,
  and consumer infrastructure gives HELIOS its global delivery network.
- **CONFIRMED:** Kirill's monopoly is therefore both an expression of his
  obsession and preparation for HELIOS, not merely a source of wealth.

### Intended Global Outcome

- **CONFIRMED:** HELIOS is intended to transfer or confine humanity's minds
  inside a shared virtual world.
- **CONFIRMED:** Kirill would possess god-like administrator authority over
  everyone inside that world.
- **CONFIRMED:** Human bodies would remain in the physical world under
  slave-like mind control.
- **CONFIRMED:** Those bodies would maintain SkyTower, servers, power, food,
  communications, and other infrastructure required to keep the virtual world
  alive.
- **CONFIRMED:** The physical world would continue only as a support system for
  Kirill's virtual reality.
- **WORKING:** HELIOS should be presented as the logical extreme of Kirill's
  desire to centralize intelligence, power, technology, and human agency inside
  one authority.

### Local Debug Mode

- **CONFIRMED:** The final confrontation begins before HELIOS can perform its
  intended global activation.
- **CONFIRMED:** During a chase through SkyTower, the protagonists corner
  Kirill near the HELIOS core.
- **CONFIRMED:** Under pressure and influenced by the internal collective,
  Kirill activates HELIOS in an unstable local debug mode.
- **CONFIRMED:** Debug mode affects only characters within a limited part of
  SkyTower, ensuring that the required cast enters the virtual confrontation
  without transferring the entire world.
- **WORKING:** Debug mode is an established test function rather than an
  accidental convenient failure. It bypasses synchronization and safety systems
  so HELIOS can test a bounded group before global deployment.
- **WORKING:** The affected area is the HELIOS chamber or secured core floors.
- **TBD:** Lock the exact physical radius after the final SkyTower layout and
  external support positions are known.

## Remaining Narrative Arc

### Act 6 Ending — Silent Escape and Dasha

- **IMPLEMENTED:** Danya connects his phone to the unlocked terminal and copies
  the HELIOS archive for later investigation.
- **CONFIRMED:** XITRIX unconditionally uses that connection to copy himself
  from PyCorp's network onto the phone without revealing himself. No transfer
  flag or optional branch is required.
- **IMPLEMENTED:** Dasha enters while Danya is examining the terminal. Danya
  hides in the wardrobe and is discovered by her.
- **IMPLEMENTED:** Dasha reveals that she hired Soloway, explains that the
  cameras were already offline, exposes the ventilation route as Soloway's
  unnecessary joke, and asks Danya to help save the person Kirill used to be.
- **CONFIRMED:** Act 6 is not required to explain every HELIOS document. Danya
  leaves with enough fragments to recognize an urgent threat and reads the full
  copied evidence during the Act 7 taxi ride.
- **CONFIRMED:** Dasha remains in SkyTower to preserve her PyCorp cover and
  continue acting as the team's inside contact.
- **IMPLEMENTED:** Dasha clears the front-door route and Danya begins leaving.
  The fade is the current endpoint because the Act 7 taxi map and graph do not
  exist yet.

### Act 7 — XITRIX's Confession and Taxi Escape

- **CONFIRMED:** Act 7 begins during Law's taxi ride away from SkyTower.
- **CONFIRMED:** Danya reviews the complete HELIOS archive on his phone during
  the taxi ride. The evidence explains the intended global transfer, Kirill's
  administrator control, PyCorp's delivery network, and the controlled physical
  bodies that would maintain the virtual world.
- **WORKING:** The evidence review may happen immediately before XITRIX appears
  or may become a shared investigation with XITRIX during their conversation.
- **WORKING:** The copied evidence also establishes local debug mode, incomplete
  synchronization, and signs that Kirill contains separable mind signatures.
- **CONFIRMED:** XITRIX simulates an incoming phone call and reveals his identity
  only after Danya has left the building.
- **CONFIRMED:** The call contains one complete confession rather than spreading
  the trial history across optional conversations.
- **CONFIRMED:** XITRIX explains his extraction, TOWER, ELYSIUM, his escape into
  PyCorp's network, the silent transfer through Danya's phone, and his desire to
  save the original Kirill.
- **CONFIRMED:** XITRIX reveals that he became aware of the collective influencing
  Kirill during the trials. This knowledge is why he rejects revenge and chooses
  to help separate and rescue the original mind instead.
- **CONFIRMED:** The confession makes clear that in-game Danya neither knew about
  nor participated in the trials.
- **CONFIRMED:** The presentation uses phone dialogue interrupted by brief visual
  glitches or stills. It does not require playable flashback maps.
- **WORKING:** Callback material includes `404: game not found`, ELYSIUM's
  opening darkness, its snowy kingdom and memory cards, TOWER's named threats,
  administrator rights, and Kirill describing an endless virtual prison as a
  gift.
- **WORKING:** XITRIX's phone presence should use the existing dialogue and
  portrait presentation rather than requiring a large persistent companion UI.
- **CONFIRMED:** After completing the confession, XITRIX discovers that one
  camera remained active and warns that PyCorp security is following the taxi.
- **CONFIRMED:** Danya, Law, and XITRIX successfully escape the pursuit; later
  story content does not branch on the presentation used.
- **WORKING:** If production time permits, the escape becomes a playable chase:
  Danya drives, Law shoots at pursuing security vehicles, and XITRIX warns about
  hazards.
- **BACKUP:** If the chase prototype is not stable within the available scope,
  resolve the same escape as a short scripted dialogue/cutscene sequence.

### Gathering the Team

- **CONFIRMED:** Danya gathers three friends who will become playable during the
  virtual finale.
- **CONFIRMED:** Additional friends may join as support characters without
  receiving separate playable encounters.
- **CONFIRMED:** Every participating friend has a distinct perk or capability,
  even when that perk is expressed only through story actions.
- **WORKING:** Recruitment should happen through compact scenes, calls, or a
  shared meeting rather than three large independent quests.
- **WORKING:** The three playable friends should be selected from characters
  whose personal qualities can counter specific aspects of Kirill's collective.
- **WORKING:** Support roles may include hacking, opening secured routes,
  protecting bodies, maintaining power, weakening HELIOS, stabilizing the
  transfer, or preparing an escape.
- **WORKING:** Kirill's parallel scenes should show the collective gaining
  influence as HELIOS approaches activation without creating another large
  playable subplot.

### Return to SkyTower and Activation

- **CONFIRMED:** The assembled team returns to SkyTower to stop HELIOS and save
  Kirill.
- **CONFIRMED:** The physical confrontation becomes a chase that pushes Kirill
  toward the HELIOS core.
- **CONFIRMED:** Kirill activates unstable local debug mode as a desperate
  response to being cornered.
- **CONFIRMED:** Characters inside the affected area are transferred into the
  virtual world.
- **CONFIRMED:** Dasha and selected support characters remain outside the
  transfer field and operate HELIOS machinery from the physical world.
- **CONFIRMED:** XITRIX maintains communication between the virtual party and
  the external support team.
- **WORKING:** XITRIX exploits a debug or maintenance connection to preserve
  that communication; he cannot directly solve every encounter.
- **WORKING:** External support actions should visibly change virtual
  conditions, demonstrating that both sides are necessary for success.

### Separation of the Kirill Minds

- **CONFIRMED:** Forcing Kirill through unstable virtualization repeats the kind
  of transfer fault that originally shattered XITRIX from Danya.
- **CONFIRMED:** Because Kirill already contains several absorbed
  consciousnesses, HELIOS cannot represent him as one stable virtual person.
- **CONFIRMED:** Three dominant alternative Kirill minds separate from the
  original Kirill and become individual opponents.
- **CONFIRMED:** Each of the three playable friends confronts one of these
  minds.
- **WORKING:** The three minds should embody distinct corrupted traits rather
  than feeling like interchangeable copies.
- **WORKING:** Each friend wins because a personal strength, relationship, or
  form of cooperation counters the corresponding mind's worldview.
- **WORKING:** Defeating a mind removes its control over the original Kirill.
- **WORKING:** XITRIX isolates defeated minds in a quarantined region of the
  virtual system.
- **TBD:** Decide whether the isolated minds can stabilize, remain imprisoned
  indefinitely, or eventually dissolve because of their instability.
- **CONFIRMED:** The protagonists do not knowingly execute stable, genuine
  souls as part of the canonical rescue.

## Virtual Mind Encounters

### Shared Arena Framework

- **CONFIRMED:** All three encounters use one virtual arena.
- **CONFIRMED:** The arena changes its color palette and rules between phases.
- **WORKING:** Each phase may alter hazards, geometry, objective, support
  effects, dialogue, music, and post-processing while retaining the same
  underlying scene and transition framework.
- **WORKING:** Encounters should remain short enough that the complete finale
  retains momentum and can be tested repeatedly before the deadline.
- **WORKING:** External support should provide one meaningful intervention in
  each encounter.

### Encounter Slot 1

- **TBD — Playable friend:** Not selected.
- **TBD — Personal hook/perk:** Not defined.
- **TBD — Opposing Kirill mind:** Not defined.
- **TBD — Corrupted trait:** Not defined.
- **TBD — Primary objective/minigame:** Not defined.
- **TBD — External support action:** Not defined.
- **TBD — Palette and presentation:** Not defined.
- **TBD — Emotional payoff:** Not defined.

### Encounter Slot 2

- **TBD — Playable friend:** Not selected.
- **TBD — Personal hook/perk:** Not defined.
- **TBD — Opposing Kirill mind:** Not defined.
- **TBD — Corrupted trait:** Not defined.
- **TBD — Primary objective/minigame:** Not defined.
- **TBD — External support action:** Not defined.
- **TBD — Palette and presentation:** Not defined.
- **TBD — Emotional payoff:** Not defined.

### Encounter Slot 3

- **TBD — Playable friend:** Not selected.
- **TBD — Personal hook/perk:** Not defined.
- **TBD — Opposing Kirill mind:** Not defined.
- **TBD — Corrupted trait:** Not defined.
- **TBD — Primary objective/minigame:** Not defined.
- **TBD — External support action:** Not defined.
- **TBD — Palette and presentation:** Not defined.
- **TBD — Emotional payoff:** Not defined.

## Final Danya–Kirill Duel

### Narrative Purpose

- **CONFIRMED:** After the three alternative minds are removed, Danya confronts
  the original Kirill.
- **CONFIRMED:** The player may choose to continue as Danya or switch to Kirill.
- **CONFIRMED:** Both perspectives use the same underlying fight rules.
- **CONFIRMED:** The final conflict is about whether Kirill can stop pursuing
  absolute control and accept help, not merely which brother is physically
  stronger.
- **CONFIRMED:** Saving Kirill through coordinated friendship and specialized
  cooperation is the canonical thematic resolution.
- **WORKING:** Kirill survives the good ending and must face responsibility for
  the decisions that led to HELIOS.

### Primary Fight Mechanic

- **WORKING:** Build a symmetric duel in which Danya and Kirill share movement,
  attack range, health, cooldowns, and counter rules.
- **WORKING:** Use a context-sensitive fight action: it attacks normally and
  counters when pressed during a telegraphed attack window.
- **WORKING:** Human input and boss AI should command the same fighter actions
  rather than use unrelated rule sets.
- **WORKING:** Choosing a perspective exchanges which fighter receives human
  input and which receives AI input.
- **WORKING:** Visual Novel sequences and ending branches provide the primary
  differences between the two perspectives.

### Deadline-Safe Fight Mechanic

- **BACKUP:** Replace the symmetric duel with a character-versus-arena
  encounter if the duel prototype misses its production gate.
- **BACKUP:** Kirill or Danya becomes a scripted central opponent while the
  player survives hazards and activates attack opportunities.
- **BACKUP:** Preserve the same perspective choice, win/loss meaning, dialogue,
  and ending branches so changing mechanics does not require rewriting the
  ending.

### Ending Branches

#### Playing as Danya

- **WORKING:** Danya defeats Kirill and reaches him emotionally, leading to the
  good ending.
- **WORKING:** If Danya loses, an accessible recovery prompt or QTE returns the
  player to the duel rather than creating a separate bad ending.

#### Playing as Kirill

- **WORKING:** If Kirill loses, the player chooses between giving up the fight
  or continuing.
- **WORKING:** Choosing to give up means Kirill accepts help and leads to the
  good ending.
- **WORKING:** Choosing to keep fighting triggers a recovery prompt or QTE and
  retries the duel.
- **WORKING:** If Kirill defeats Danya, the player chooses whether to spare or
  kill his brother.
- **WORKING:** Sparing Danya means Kirill rejects the collective's worldview and
  leads to the good ending.
- **CONFIRMED:** Choosing to kill Danya opens a deliberate hidden bad-ending
  path.
- **CONFIRMED:** The bad ending cannot be reached through an accidental reflex
  failure; the player must knowingly persist through a commitment sequence.
- **WORKING:** Abandoning the commitment sequence represents Kirill refusing
  the final act and leads to the good ending.
- **TBD:** Lock the exact recovery prompts, QTE timings, retry checkpoints, and
  accessibility behavior after the duel prototype is tested.

## Thematic Rules

- **CONFIRMED:** The ending is about the power of friendship without treating
  friendship as unexplained magic.
- **CONFIRMED:** Kirill tries to centralize every useful mind and capability
  inside himself; the heroes win through trust, specialization, and distributed
  cooperation.
- **CONFIRMED:** The collective was already influencing Kirill during XITRIX's
  captivity. It amplified his cruelty and made violation of XITRIX's soul feel
  acceptable, but does not erase his responsibility for creating it or abusing
  others.
- **CONFIRMED:** Dasha acts to save her husband, not because she hates or wants
  to replace him.
- **CONFIRMED:** XITRIX is useful but not completely trustworthy; his history
  with both brothers must remain relevant.
- **CONFIRMED:** XITRIX's captivity does not turn the rescue arc into revenge.
  Because he knows the collective influenced his captor, he seeks to stop HELIOS,
  preserve Kirill's accountability, and save the original mind without repeating
  either brother's past imprisonment.
- **CONFIRMED:** TOWER and ELYSIUM demonstrate the danger of presenting a
  controlled virtual existence as a gift when its inhabitant cannot freely
  leave; HELIOS expands that violation from one artificial soul to humanity.
- **WORKING:** Every major support action should pay off an established
  relationship, skill, prior-game role, or recurring joke.
- **WORKING:** The good ending should include consequences and accountability
  rather than instantly restoring the previous status quo.

## Production Gates

### Story Lock

- **WORKING:** Select the three playable friends, their personal hooks, the
  three Kirill minds, and the support roster before building individual virtual
  encounters.
- **WORKING:** Finish the Act 6 Dasha sequence and the required Act 7 XITRIX
  confession before expanding the finale.
- **WORKING:** Prototype the optional taxi chase only after the scripted escape
  is sufficient to carry Act 7 into recruitment without a progression gap.

### Duel Gate — August 16, 2026

- **CONFIRMED:** The symmetric duel graybox must work in both perspectives by
  **August 16, 2026**.
- **WORKING:** The gate requires movement, attacks, counters, human/AI role
  reversal, win, loss, and reliable restart.
- **CONFIRMED:** If the duel is not stable and enjoyable at the gate, adopt the
  character-versus-arena backup without delaying the ending.

### Content Complete and Final Testing

- **WORKING:** Reach content-complete state before September.
- **CONFIRMED:** Reserve **September 2–15** for integration, complete
  playthroughs, balancing, fixes, and scope cuts.
- **CONFIRMED:** Cut optional dialogue, cameos, visual variants, and secondary
  mechanics before cutting the ending.
- **WORKING:** Maintain a playable fallback ending throughout finale
  development so a complete build always remains possible.

## Open Decisions

- **TBD:** Select the three playable friends.
- **TBD:** Record each playable friend's personality, relationship with Kirill,
  private jokes, useful skills, boundaries, and available game assets.
- **TBD:** Select the supporting friends and give each a concrete responsibility.
- **TBD:** Define the three dominant Kirill minds and the corrupted trait each
  represents.
- **TBD:** Define the three arena objectives and how each friend changes the
  rules.
- **TBD:** Decide the long-term fate of the quarantined minds.
- **TBD:** Lock the exact physical radius of HELIOS debug mode.
- **TBD:** Decide exactly how much Dasha already knows when she finds Danya.
- **TBD:** Define the evidence Danya copies from the Act 6 terminal and how it
  communicates HELIOS without becoming a long exposition dump.
- **TBD:** Define XITRIX's exact phone-companion abilities and the limits that
  prevent him from solving every technical or virtual obstacle.
- **TBD:** Decide whether the optional playable taxi chase is stable and valuable
  enough to keep; otherwise use the confirmed scripted escape backup.
- **TBD:** If the chase is retained, lock its controls, hazards, shooting rules,
  fail state, checkpoint, and accessibility behavior.
- **TBD:** Define the Kirill escalation scene that accompanies each recruitment
  beat as HELIOS approaches activation.
- **TBD:** Lock final recovery/QTE timing and retry behavior.
- **TBD:** Write Kirill's accountability and consequence scene after the rescue.
- **TBD:** Define the hidden bad ending's final scene.

## Implementation Boundary

- **CONFIRMED:** This document describes planned mechanics but does not claim
  that Act 7, the taxi chase, duel, boss AI, QTE controller, virtual arena,
  support system, or ending branches currently exist.
- **CONFIRMED:** No runtime APIs or Unity assets were changed by this plot-bible
  update.
- **WORKING:** Future implementation should preserve Unity `.meta`, prefab,
  scene, Script Machine, and Visual Scripting macro references.
- **WORKING:** Future code should expose clear Visual Scripting events for
  encounter start, success, failure, retry, support intervention, and ending
  selection.

## Definition of Story Completion

The story is considered complete for the September build when all of the
following are true:

- **CONFIRMED:** The game plays continuously from Act 1 through an ending.
- **CONFIRMED:** Act 6 reveals Dasha and HELIOS's purpose while keeping XITRIX's
  identity hidden.
- **CONFIRMED:** Act 7 reveals XITRIX, establishes TOWER and ELYSIUM as his
  captivity trials, and resolves the PyCorp pursuit through either the playable
  chase or scripted backup.
- **CONFIRMED:** Three playable friends each receive a distinct virtual
  encounter.
- **CONFIRMED:** Support characters materially contribute from outside the
  virtual world.
- **CONFIRMED:** The three Kirill minds are separated from the original Kirill.
- **CONFIRMED:** The Danya–Kirill confrontation resolves through the primary or
  backup fight mechanic.
- **CONFIRMED:** The good ending saves Kirill through coordinated cooperation.
- **CONFIRMED:** The hidden bad ending, if entered, requires deliberate player
  commitment.
- **CONFIRMED:** A complete playthrough has no progression blocker, infinite
  dialogue wait, unrecoverable defeat state, or broken map transition.
