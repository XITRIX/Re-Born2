# Main Game Plot

> Working story bible for the current birthday game. This document describes
> the private series canon supplied by Danya and the story currently implemented
> in the active Act 1–5 Visual Scripting graphs. Act 6 is included only as a
> work-in-progress boundary.

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

- **Andrea** is with Danya during the morning scene. She tries to pull him away
  from dwelling on the past and provides a grounded counterpoint to his anger.

- **Lo** (`Law` and `Law_Taxi` in the assets) is Danya's friend, house-worker,
  cupcake baker, and once-again taxi driver. He provides comic relief but also
  acts as practical support during the infiltration.

- **Dasha** (`Daria` in the assets) is Kirill's aide. She accepts his order to
  reinforce security and silently observes his increasingly abusive behavior.
  Her true opinion and future loyalty are not yet established.

- **Soloway** is the covert contact who intercepts Danya in the city. Depending
  on the player's choice, he either keeps his identity hidden or is immediately
  recognized by Danya.

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
| WIP continuation | Same night | Danya | `Act6Map` | Danya enters the laboratory; the story beyond this point is unfinished |

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

The currently connected Act 5 opening begins as Lo's taxi reaches SkyTower. Lo
wakes Danya, who is still too tired to open his eyes. Danya gets out of the car,
tries to reassure himself that this will be a simple "in and out" thirty-minute
adventure, and receives Lo's warning not to get caught.

The exterior becomes playable. Returning to the taxi produces a joke from Lo
about how impressively fast Danya would have been if he had actually entered the
building.

At the main entrance, Danya remembers that entering through the front would be
unwise. His instructions say to use a ladder on the left side of the building.
Trying the wrong boundaries either makes him turn back or comment that the
chosen direction is not the building's left side.

Danya finds the ladder. Despite his fear of heights, he climbs it and proceeds
into the laboratory. The map then loads the unfinished Act 6 interior.

### Drafted but Currently Disconnected Taxi Setup

The Act 5 graph also contains an earlier car-ride setup that is not connected to
the active Start path. It provides useful intended context:

- Danya did receive Soloway's promised letter.
- It orders him to reach SkyTower in the middle of the night.
- It includes a map for avoiding security cameras and guards.
- It says to crawl through the ventilation system, reach the laboratory, and
  meet the contact waiting inside.
- Lo unexpectedly arrives as Danya's taxi driver, jokes that "a taxi driver once
  is a taxi driver forever," and offers to let Danya sleep during the ride.
- Danya asks Lo to remain near SkyTower, report anything suspicious, and keep
  the car available in case an escape is necessary.

This sequence fits the connected arrival scene, but it should be reconnected and
tested before being treated as part of the playable cutscene.

**Act function:** Danya's investigation becomes direct action. The daylight
conflict and evening warning lead into a nighttime break-in, placing Danya on a
collision course with Kirill's project and the promised undercover agent.

## Work-in-Progress Interior — `Act6Map`

The interior map is present and is the next target loaded by the Act 5 ladder,
but its graph and prefab are currently under active development. It should not
yet be treated as a finished sixth act.

The material already present suggests the following provisional progression:

- Danya enters through dark ventilation and worries that he may be lost. This
  introductory chain is currently disconnected from the active graph.
- He encounters a laser barrier.
- His phone buzzes. An unknown voice tells him to wait, and the laser object is
  removed.
- Danya wonders who contacted him and how that person knew he was there.
- A PyCorp terminal uses documents from "Project HELIOS — R6" as a password
  puzzle.
- A nearby door remains locked from the outside.

No current scene explains what Project HELIOS is, reveals the undercover agent,
or gives Danya the promised evidence against Kirill. Those are open story beats,
not established answers. The Init graph also currently launches `Act6Map`
directly, which appears to be a development shortcut rather than the intended
opening of the game.

## Established Revelations

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
10. Soloway and at least one undercover operative know enough about the project
    to send Danya into SkyTower, but their full organization and motives remain
    unknown.

## Open Questions for Future Plot Development

- What exactly is Kazuya's power, and where is it physically stored?
- Has Kirill already used that power, or is the unstable prototype meant to
  complete the process?
- What is the scientists' planned "mass use," and why is there only one attempt?
- How much of Kirill's behavior is his own obsession, and how much is controlled
  by the absorbed collective?
- Can the clone souls be separated from Kirill without killing him?
- Does Kirill want to be rescued, even if he cannot admit it?
- What does Dasha know, and will she remain loyal when the project escalates?
- Who recruited Soloway?
- Who is the undercover agent inside SkyTower?
- Is the unknown caller at the laser barrier the promised agent, one of
  Soloway's allies, Dasha, or someone else?
- What is Project HELIOS?
- What evidence will finally show Danya the full experiment?
- What event will bring Danya and Kirill face to face?
- Is the final goal to stop Kirill, save Kirill, destroy Kazuya's power, or find
  a way to do all three?

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

## Current Story Boundary

The completed dramatic arc presently ends when Danya climbs into SkyTower. The
interior mechanics establish obstacles and another mysterious helper, but the
next major revelation, confrontation, and ending have not yet been written.

That boundary is a strong handoff for future development: Danya has entered the
physical center of Kirill's conspiracy at the same moment the player understands
that Kirill may be both the antagonist and a victim who needs to be saved.
