# Re-Born 2 — Main Game Plot

> Working story bible for the current birthday game. This document describes
> the private series canon supplied by Danya and the story currently implemented
> in the active Visual Scripting graphs. Material beyond the current Act 6
> implementation is explicitly labeled as planned rather than playable.

This file is the narrative source of truth: series continuity, character
relationships, act-by-act events, player-facing revelations, and the confirmed
dramatic arc. [STORY_PLAN.md](STORY_PLAN.md) is the production source of truth
for decision status, scope, mechanics, deadlines, fallbacks, and unresolved
implementation choices. When a planned scene becomes playable, both files
should be updated together.

## High-Concept Premise

After Danya and Kirill defeated Kazuya in the previous game, Kirill became
obsessed with the power they had taken from him. Danya refused to help study it
because it was too dangerous. Kirill proceeded alone, combining earlier cloning
technology with Danya's soul-transfer technology.

Kirill created clones of himself and transferred their souls into his own
consciousness. He expected several versions of his mind to make him vastly more
intelligent and powerful. Instead, the absorbed consciousnesses amplified his
desire for power exponentially and began forming a hostile collective within
him. By the start of the current game, Kirill is a celebrated billionaire and
the head of a near-total game-industry monopoly, but he is also unstable,
secretive, and gradually losing control of the minds he treated as tools.

The story follows one day from two viewpoints. Danya investigates the public
consequences of Kirill's transformation, while Kirill's scenes reveal the secret
project and the psychological horror behind it. Their plotlines converge when
Danya infiltrates Kirill's laboratory at night.

Kirill's prototype, **HELIOS**, is the global expression of the same obsession.
It combines Danya's soul-transfer system with Kazuya's captured power to place
humanity inside one shared virtual world under Kirill's administrator control.
The population's physical bodies would remain mind-controlled in the real world
to maintain the servers, energy, food, communications, and other infrastructure
keeping Kirill's virtual reality alive.

## Series Continuity

### The Soul-Transfer Game

In an earlier game, Kirill was hit by a bus and died. Danya had given him the
iPhone on which Danya developed an app capable of transferring real souls into
game worlds. Kirill's soul became trapped in a buggy game world, and the goal
was to reach a debug point so Danya could extract him. Danya ultimately placed
Kirill's soul into a new artificial "Cat Boy" body: still Kirill, but with cat
ears.

This establishes that souls can be digitized, copied or moved between a real
body, an artificial body, and a game world. It is the foundation of the
technology Kirill later abuses.

### XITRIX, the Shattered Danya

The final boss of the game in which Kirill died was **Evil Danya**, later known
as **XITRIX**. XITRIX was a representation of a shattered piece of Danya's mind
created during alpha testing of the soul-transfer app.

XITRIX wanted to satisfy Danya's desire to play games, but the test world was
populated entirely by unintelligent NPCs. Kirill was the first real person to
enter it. XITRIX finally had somebody genuine to play with and therefore refused
to let Kirill escape.

In later games, XITRIX learned to hack the game world and disguise himself as an
ordinary NPC with administrator rights. He settled in a shattered pocket of
virtual space containing his house, garden, and "wife," Lili from Tekken. After
learning to create NPCs and fill them with artificial souls, XITRIX became an
uneasy ally. He eventually helped Danya and Kirill defeat an evil griefer inside
the virtual world.

This history makes XITRIX neither a simple villain nor a safe companion. He is a
fragment of Danya, a former captor of Kirill, an artificial-soul creator, and an
experienced administrator of virtual reality.

### The Clone-Army Game

In the next game, made by Kirill for Danya, Kirill was able to create an army of
clones of Danya's friend. This establishes cloning as an existing part of the
shared universe and makes Kirill's later self-cloning experiment an escalation
of something the brothers have already seen.

### The Tekken Tournament

In the previous year's game, the heroes assembled a team and entered the Tekken
Tournament. Their objective was to kill Kazuya before he could destroy the
world. They succeeded.

The current conflict begins immediately after that victory. Kirill regards
Kazuya's power as a trophy that should be studied. Danya believes it is too
dangerous and insists that it must be destroyed. Kirill takes Danya's refusal as
both a personal betrayal and an obstacle to limitless power.

## Main Characters

- **Danya** (`Xitrix_IRL` in the Unity assets) is the protagonist and original
  inventor of the soul-transfer app. He recognizes that Kirill's public success
  is hiding something dangerous and decides to investigate.

- **Kirill** is Danya's brother, the survivor of the soul-transfer incident, and
  now the head of PyCorp. He presents himself as the savior of the game industry
  while secretly developing a dangerous transfer project. His self-clone souls
  are no longer passive additions to his mind.

- **Andrea** is Danya's real-life girlfriend. She is with him during the morning
  scene, tries to pull him away from dwelling on the past, and provides a
  grounded counterpoint to his anger.

- **Lo** (`Law` and `Law_Taxi` in the assets) is Danya's friend, house-worker,
  cupcake baker, and once-again taxi driver. He provides comic relief but also
  acts as practical support during the infiltration.

- **Dasha** (`Daria` in the assets) is Kirill's real-life wife and works with him
  inside PyCorp. She accepts his order to reinforce security and silently
  observes his increasingly abusive behavior. Behind his back, she wants the
  good person she married to return and begins organizing an intervention.

- **Soloway** is Danya and Kirill's real-life friend. Dasha secretly hires him to
  intercept Danya in the city. Depending on the player's choice, he either keeps
  his identity hidden or is immediately recognized by Danya.

- **XITRIX** (`Xitrix_Evil`/`Xitrix` in the assets) is the digital fragment of
  Danya who once trapped Kirill in the game world. The planned story brings him
  back as the voice in the ventilation system and later as a companion living
  on Danya's phone.

- **The scientists** are developing Kirill's unstable prototype. Their fear of
  him demonstrates how different his private behavior is from his public image.

- **The absorbed Kirills** are the clone-consciousnesses inside Kirill. They
  were created as instruments for improving him, but now speak as a collective
  and claim ownership of him.

## Timeline at a Glance

| Act | Time | Viewpoint | Current map | Main movement |
| --- | --- | --- | --- | --- |
| 1 | Morning | Danya, framed by Kirill's TV interview | `IntroMap` | Public success gives way to Danya's suspicion |
| 2 | Midday | Kirill | `KirillIntroMap` | The secret laboratory and unstable prototype are revealed |
| 3 | Evening | Danya | `CityMap` | Soloway recruits Danya into the investigation |
| 4 | Night | Kirill | `Act4Map` | A nightmare reveals the origin and cost of Kirill's obsession |
| 5 | Night | Danya | `Act5Map` | Danya reaches SkyTower and begins the infiltration |
| 6, partially implemented | Same night | Danya | `Act6Map` | Danya enters the laboratory, bypasses lasers, and unlocks the HELIOS terminal |

The automatic map chain is currently
`IntroMap` → `KirillIntroMap` → `CityMap` → `Act4Map` → `Act5Map` →
`Act6Map`.

## Act 1 — Morning: The Man on Television

The game opens with a breaking-news broadcast. Elon Musk's entire fortune will
be inherited by billionaire playboy Kirill Vinogradov. Kirill already owns
PyCorp, a conglomerate that has absorbed major game-development and hardware
companies including RenPy Corp, RockStar, Nvidia, Unity, and Epic Games.

During a live interview, Kirill claims that his goal is simply to make good
games. He says he bought different companies for their writers, artists, and
programmers, then bought Nvidia because the best way to optimize games for
specific hardware is to manufacture the hardware as well. He abruptly ends the
interview because he is late for a meeting.

The viewpoint pulls back to reveal Danya and Andrea watching the broadcast at
home. The public sees Kirill as the game industry's savior; Danya sees a man who
has destroyed all competition. Kirill has made game engines private, buried
graphics APIs beneath restrictive licenses, and gained control over the hardware
needed to build an independent alternative. Attempting to compete with PyCorp
now leads directly to a lawsuit.

Lo interrupts the argument with cupcakes. The joke also shows how far Kirill's
monopoly reaches: Lo lost his home to debt and now lives with Danya and Andrea,
works around the house, and sleeps in one of Andrea's many dressing rooms.

After Andrea leaves, Lo recognizes that the situation is genuinely troubling.
Danya admits that Kirill has changed and become obsessed with something. He
decides that he cannot ignore it and must discover what is happening. Lo wishes
him luck.

**Act function:** Kirill is introduced first through a polished public image,
then through the damage his empire has caused to Danya's life and the wider
world. Danya receives no proof of the secret project yet; he acts because he
knows his brother well enough to recognize the change.

## Act 2 — Midday: Behind the Public Image

Kirill returns from the interview to an Apple-styled laboratory. His first
reaction is anger that the reporters wasted his time. He orders Dasha to
reinforce the security checkpoint and ensure that nobody bothers him again.

Kirill summons three frightened scientists and asks for a progress report. One
answers that everything is in Jira. Kirill responds by striking or violently
throwing that scientist out of the conversation, then calmly repeats the
question.

The remaining scientist reports that they have an unstable prototype. It is not
ready for mass use and still has too many side effects. Kirill insists that they
will have only one attempt: there can be no day-one patch and failure is
unacceptable. He sends them back to work.

Once they are gone, Kirill complains that a whole group of scientists cannot
reproduce a technology originally built by one person. In the context of the
series, that person is Danya and the technology is the soul-transfer system,
although the scene deliberately does not name either one yet.

Dasha watches in silence before leaving.

**Act function:** The audience learns before Danya does that Kirill is running a
secret, high-risk project. The exact operation remains hidden, but its transfer
technology, scale, side effects, and one-chance requirement connect it to the
larger soul-and-clone plot.

## Act 3 — Evening: A Lead in the Rain

Danya walks through the city in heavy rain, trying to clear his head. He knows
that something must be done before it is too late.

An unseen person approaches from behind and tells Danya to stand still without
turning around or have his head blown off. Danya answers with a joke about
wanting to clear his head, "but not this much." The player then chooses whether
to obey.

### If Danya Stands Still

The contact remains visually anonymous. He tells Danya to listen carefully and
delivers his instructions. At the end, he leaves with a brief "Adios."

### If Danya Turns Around

Danya exposes the stranger as Soloway and explains that he recognized his
voice. Soloway is frustrated that Danya ignored the warning, but continues with
the same mission. When Danya asks why everything must remain secret, Soloway
says Danya has to see the truth with his own eyes.

### Information Shared on Both Paths

Soloway says that Kirill is hiding something very important. Danya must sneak
into Kirill's office and discover it himself. A letter will arrive with the
details of the infiltration, and an undercover agent will meet Danya inside.

After Soloway leaves, Danya finally has a lead. The rain stops and the story
switches back to Kirill.

**Act function:** The two sides of the narrative begin to converge. Danya still
does not know what Kirill built, but he now has a destination, assistance from a
covert network, and a reason to enter the laboratory personally.

## Act 4 — Night: Kirill's Nightmare

Kirill's nightmare begins with fragments of silence and a return to the moment
after Kazuya's defeat.

Kirill argues that Kazuya's power is their trophy and must be studied. Danya's
name is visually corrupted in the dream, but the supplied series context
identifies him as the other speaker. Danya refuses: the power is too dangerous
and must be destroyed. He also asks why Kirill is wearing Kazuya's costume,
turning a serious disagreement into a personal joke.

The memory dissolves. Kirill broods over Danya's refusal and asks why his brother
cannot understand the possibilities. The scene moves to the laboratory, where
Kirill's plan becomes explicit:

1. Use the transfer technology to copy a soul.
2. Insert the copied soul into an existing consciousness.
3. Combine several versions of Kirill in one mind.
4. Become so intelligent and powerful that nobody can equal him.

Broken whispers gradually form the phrase "shut up." Kirill tells the voices to
stop talking: they are supposed to help him, and they are nothing but his tools.
The collective answers that it is no longer his tool. It declares, "You are
ours."

The visual corruption and whispering intensify until Kirill abruptly wakes in
his bedroom.

**Act function:** This is the central reveal. Kirill's hunger for Kazuya's power
led him to misuse Danya's transfer invention on clones of himself. The experiment
did not merely make him smarter. It created a collective presence that magnifies
his obsession and is now fighting him for control.

## Act 5 — Night: The SkyTower Infiltration

The full taxi sequence is now connected to Act 5's main Start.

During the journey, Danya reviews the letter promised by Soloway. It orders him
to reach SkyTower in the middle of the night and provides a map for avoiding
security cameras and guards. His target is the laboratory, where an inside
contact is supposed to meet him. The route requires crawling through the
ventilation system, which Danya is not excited about.

Lo notices that Danya is lost in thought. Danya admits that Lo was the last
person he expected to see when ordering a taxi. Lo explains that "a taxi driver
once is a taxi driver forever": after seeing Danya looking for a car, he
canceled his other ride and came to help.

Danya asks Lo to wait near SkyTower, watch for anything suspicious, and call him
if necessary. Keeping the taxi nearby also guarantees an escape vehicle if
something goes wrong. Lo agrees and lets Danya sleep for the rest of the trip.

When the taxi reaches SkyTower, Lo wakes Danya, who is still too tired to open
his eyes. Danya gets out of the car, tries to reassure himself that this will be
a simple "in and out" thirty-minute adventure, and receives Lo's warning not to
get caught.

The exterior becomes playable. Returning to the taxi produces a joke from Lo
about how impressively fast Danya would have been if he had actually entered the
building.

At the main entrance, Danya remembers that entering through the front would be
unwise. His instructions say to use a ladder on the left side of the building.
Trying the wrong boundaries either makes him turn back or comment that the
chosen direction is not the building's left side.

Danya finds the ladder. Despite his fear of heights, he climbs it and proceeds
into the laboratory. The map then loads the unfinished Act 6 interior.

**Act function:** Danya's investigation becomes direct action. The daylight
conflict and evening warning lead into a nighttime break-in, placing Danya on a
collision course with Kirill's project and the promised undercover agent.

## Act 6 — Work-in-Progress SkyTower Interior

The interior map is the next target loaded by the Act 5 ladder. The following
portion of the sixth act is implemented:

- Danya enters the dark laboratory interior through the ventilation system and
  worries that he may be lost.
- He encounters a laser barrier.
- His phone buzzes. An unknown voice tells him to wait, and the laser object is
  removed.
- Danya wonders who contacted him and how that person knew he was there.
- He can read HELIOS records, an obsolete R5 archive, the current R6 approval
  chain, a staff directory, and an IT recovery reminder.
- Those documents form a password puzzle for a protected PyCorp terminal. The
  puzzle has progressive hints and a persistent unlock state.
- Unlocking the terminal triggers Danya's initial reaction that the project is
  insane and must be stopped.
- A nearby door remains locked from the outside.

The current scene does not yet reveal the caller, the undercover agent, or the
full evidence stored on the computer. The confirmed plan below supplies those
answers, but they remain future revelations for the player.

The Init graph still launches `Act6Map` directly, which appears to be a
development shortcut for testing the interior rather than the intended opening
of the finished game.

## Planned Story Progression

### The Voice in the Ventilation

The unknown caller is XITRIX.

Kirill previously found a way to extract XITRIX from the virtual game world and
brought him into the PyCorp laboratory for experimentation. XITRIX escaped
physical containment by moving into the laboratory's local network. From there,
he notices Danya inside the ventilation system, contacts his phone, and disables
the laser barrier.

After Danya solves the HELIOS password puzzle, he connects his phone to the
terminal to copy the project documents. XITRIX uses that connection to copy
himself from the PyCorp network onto the phone. He then becomes Danya's digital
companion for the rest of the game.

The copied evidence reveals HELIOS's purpose. Kazuya's captured power amplifies
Danya's individual soul-transfer technology into global influence and control.
PyCorp's ownership of game engines, hardware, software, servers, and consumer
infrastructure is not merely the result of Kirill's greed; it is the delivery
network for HELIOS.

Kirill intends to transfer or confine humanity's minds inside a shared virtual
world where he holds god-like administrator authority. Their physical bodies
would remain in the real world as controlled workers, sustaining the machinery
and infrastructure required to keep the virtual population alive.

This reunion should carry deliberate uncertainty. XITRIX has helped both
brothers before, but he is also the fragment of Danya who once refused to let
Kirill escape. Danya needs his access and knowledge without being able to treat
him as completely harmless.

### Dasha, the Undercover Agent

After Danya unlocks the computer, Dasha enters the room. Danya does not know who
is coming and hides inside a wardrobe. Dasha discovers and exposes him.

She then reveals that she is the inside contact:

- She hired Soloway to approach Danya.
- She arranged for the letter and SkyTower infiltration instructions.
- She has watched Kirill transform and wants her husband to become a good
  person again.
- She shares her current understanding of Kirill's cloning, soul-transfer, and
  HELIOS project.

Dasha is therefore not betraying Kirill out of hatred. She is acting against
his project because she wants to save him from what he has become.

### Gathering the Team

After the SkyTower revelations, Danya gathers three real-life friends who will
become playable during the virtual finale. Additional friends may join in
support roles. Every participant needs a distinct personal strength or
capability rather than existing only as a cameo.

Recruitment uses compact scenes, calls, or a shared meeting rather than three
large independent quests. It runs in parallel with Kirill's viewpoint: while
Danya assembles allies, Kirill pushes the scientists toward activation and the
collective gains more influence over him.

### Return to SkyTower and Local Debug Activation

The assembled team returns to SkyTower to stop HELIOS and save Kirill. A
physical confrontation becomes a chase that corners Kirill near the HELIOS
core. Under pressure and the collective's influence, Kirill activates an
unstable local debug mode before the global system is ready.

Debug mode transfers only characters inside a limited secured area of SkyTower.
Dasha and selected support characters remain in the physical world to operate
HELIOS machinery, protect the bodies, maintain power, open routes, and weaken or
stabilize the transfer. XITRIX preserves communication between the virtual
party and the outside team through a maintenance connection.

### Separation of the Kirill Minds

The unstable virtualization repeats the kind of transfer fault that originally
shattered XITRIX from Danya. Because Kirill already contains several absorbed
consciousnesses, HELIOS cannot render him as one stable virtual person. Three
dominant alternative Kirill minds separate from the original.

Each playable friend confronts one separated mind inside a single transforming
virtual arena. The three minds embody different corrupted traits, and each
friend succeeds because a personal quality or relationship counters the
corresponding worldview. External support materially changes each encounter.

Defeating a mind removes its control over the original Kirill. XITRIX quarantines
the defeated minds instead of the heroes knowingly destroying stable souls.
Their long-term fate is not yet decided.

### Final Danya–Kirill Duel

After the three alternative minds are removed, Danya confronts the original
Kirill. The player may continue as Danya or switch perspective and play as
Kirill. Both perspectives are planned to use the same underlying duel rules,
with human and AI control exchanged between the brothers.

The confrontation tests whether Kirill can reject absolute control and accept
help. Coordinated friendship, specialization, and trust—not unexplained magical
sentiment—make the rescue possible.

The good ending saves Kirill but does not erase his responsibility. When playing
as Kirill, knowingly choosing to kill Danya after defeating him opens a hidden
bad-ending path. That ending requires deliberate commitment rather than an
accidental failed input.

## Story Truths and Planned Reveals

### Already Revealed in the Implemented Story

1. Danya created the original technology capable of transferring real souls.
2. Cloning is already possible in this universe.
3. Kirill became obsessed with Kazuya's power after the tournament.
4. Danya refused to study that power and wanted it destroyed.
5. Kirill combined cloning with soul-transfer technology without Danya's help.
6. Kirill absorbed the souls or copied consciousnesses of several versions of
   himself in an attempt to increase his intelligence and power.
7. The experiment amplified his obsession instead of satisfying it.
8. The absorbed minds now identify themselves as a collective and are
   attempting to control Kirill.
9. PyCorp's public monopoly gives Kirill the money, hardware, staff, and secrecy
   needed to continue the project.

### Author-Confirmed but Not Yet Revealed to the Player

1. HELIOS combines Danya's transfer system with Kazuya's power to control minds
   on a global scale.
2. HELIOS would confine humanity inside Kirill's shared virtual world while
   mind-controlled bodies maintain its physical infrastructure.
3. PyCorp's monopoly is the intended global delivery network for HELIOS.
4. Dasha hired Soloway and is the undercover agent inside SkyTower.
5. Dasha's motive is to save Kirill and restore the person he used to be.
6. Kirill extracted XITRIX from the virtual world to experiment on him.
7. XITRIX escaped into PyCorp's local network.
8. XITRIX is the caller who removes the ventilation laser barrier.
9. XITRIX will copy himself onto Danya's phone and become a companion.
10. Danya will gather three playable friends plus possible support allies.
11. Kirill will activate HELIOS in unstable local debug mode before global
    activation.
12. Three dominant alternative Kirill minds will separate in the virtual world.
13. Each playable friend will confront one separated mind in a transforming
    shared arena.
14. Danya will then confront the original Kirill, with a player choice to
    continue as Danya or switch to Kirill.
15. Saving Kirill through distributed cooperation is the canonical resolution.
16. A deliberate choice by player-controlled Kirill to kill Danya opens a
    hidden bad ending.

## Open Questions for Future Plot Development

- Which three friends become playable, and what personal strength does each
  bring?
- Which additional friends join as support, and what concrete responsibility
  does each receive?
- Which three corrupted traits define the separated Kirill minds?
- What objective or minigame drives each virtual encounter?
- How does each external support action alter its corresponding encounter?
- What is the long-term fate of the quarantined Kirill minds?
- What exact physical area does HELIOS local debug mode affect?
- How much does Dasha know when she finds Danya, and which conclusions are still
  her theory?
- What evidence does Danya copy from the terminal, and how can it explain
  HELIOS without becoming a long exposition dump?
- What limited abilities does XITRIX provide as a phone companion and during the
  finale?
- Which Kirill escalation scene accompanies each recruitment beat as HELIOS
  approaches activation?
- What recovery prompts, retry behavior, and accessibility rules will the final
  duel use?
- What consequences and accountability does Kirill face after the good ending?
- What exactly happens in the hidden bad ending after Kirill chooses to kill
  Danya?

## Recurring Jokes and Continuity Payoffs

- The supposedly impossible transfer project was originally built by Danya
  alone on an iPhone, while Kirill's entire laboratory team struggles to
  reproduce it.
- Kirill's artificial Cat Boy body makes his later obsession with replacing,
  combining, and improving bodies and minds especially personal.
- The former clone army is escalated into an army of Kirills compressed into
  one consciousness.
- Kirill wearing Kazuya's costume turns his dangerous fascination into an
  immediately recognizable joke from the tournament.
- The corporate empire exaggerates real game-development frustrations:
  monopolies, engine ownership, hardware control, licensing, Jira, and the
  impossibility of fixing a soul-transfer disaster with a day-one patch.
- Lo losing his home to the monopoly, becoming a house-worker, baking cupcakes,
  and returning to taxi driving makes him both a running joke and a visible
  consequence of Kirill's power.
- Danya's "thirty-minute adventure" line deliberately understates an infiltration
  involving guards, cameras, vents, lasers, and an unstable soul experiment.
- Kirill once needed rescue from XITRIX's virtual prison; now XITRIX has escaped
  from Kirill's physical laboratory and returns inside Danya's phone.
- Danya's shattered gaming impulse becomes an actual party companion, allowing
  XITRIX to comment on the game from inside the game.
- Kirill loses because he tries to centralize every useful mind and capability
  inside himself, while the heroes distribute responsibility among people who
  trust one another and specialize in different things.

## Current Story Boundary

The implemented dramatic arc currently reaches the SkyTower interior, laser
encounter, HELIOS document puzzle, terminal unlock, and Danya's initial reaction
to the project. The next major planned sequence is XITRIX's transfer onto the
phone followed by Dasha entering the room, finding Danya in the wardrobe, and
revealing her role in the investigation.

That sequence will turn the story from investigation into preparation for the
final conflict. Danya will leave SkyTower with evidence, an unexpected digital
companion, the truth about Dasha and Soloway, and a reason to assemble their
friends before Kirill completes HELIOS.
