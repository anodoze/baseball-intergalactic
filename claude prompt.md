# This Project: Baseball Intergalactic

## Core Concept

A fantasy baseball simulator with RPG mechanics. Players are fictional with attributes that weight probabilistic outcomes. Emphasis on min-maxing, weird builds, and emergent narratives.

### Key Design Principles

Probabilistic, not deterministic: Skill weights dice, never guarantees
Allow unrealistic extremes: operate on anime rules - peak players can run faster, jump higher, and hit harder than any human person.
Power/Grace as narrative foils: bruisers vs finesse wizards
Bullshit space is large: Any team can win any game - even if it's unlikely

### Terminology

Manager: Human player of the sim
Player: Fictional in-game athlete

### Specific Common Design Patterns
Outcome weights are stored in structs and then modified by circumstance and player attributes. After modifiers are applied, the die is rolled and that result determines the next step.

## Pitch Model

- Zone-based pitching (1-9 in-zone, 11-14 out-of-zone)
- Weighted ball/strike swinging/strike looking/contact outcomes based on pitch type and zone

What we have:
- Pitch model where pitcher chooses zone, throws pitch, and outcomes are determined by opposed player attributes setting weights for a final dice roll
- PA loop that tracks balls, strikes, and contact

What we're working on: 
- Pitch types and choices - types TBD
- adding information based on contact outcomes (eg. foul that continues the PA vs. catch that ends it)

Down the road: 
- pitcher and catcher interaction, esp. framing

## Field/Contact Model - bat on ball, now what? - ACTIVE PROJECT

### Contact Generation (IMPLEMENTED)
#### Contact properties:
- Direction (1-7): Modified by batter Aim. High Aim → gaps, low Aim → fouls
- Angle (Grounder/Line/Fly/Popup): Modified by batter Form. High Form → lines/flies, low Form → grounders/popups  
- Force (Weak/Clean/Blast): Modified by batter Power. Determines ball velocity/distance

### Fielding Logic (IN PROGRESS)
- Choose responsible defender based on contact properties
- Backup fielder determined by GetBackupFielder() - directional logic based on position
  - Infield → outfield behind them
  - Outfield → null (hit wall, auto-retrieve with penalties)
  - Catcher grounders → corner infielders
- FieldingAttempt struct contains: PrimaryFielder, SecondaryFielder?, ContactInfo, CanBeFoul bool
- FieldingOutcomeWeights by Angle: Foul/CaughtOut/Fielded/Bobbled/Miss
- Will be modified by Force and fielder attributes/skill

### Still TODO:
- Foul tips (handle in pitch logic, not fielding)
- Fielder attribute/skill modifiers on fielding outcomes
- Throw attempt logic (Arm + Precision vs distance)
- returning BIP outcomes to the PA logic to determine if it ends (on base/out) or continues (foul)
- Quality modifiers on "Fielded" outcome (clean vs scramble)

Down the road:
- work baserunning

## Player Attributes baseline values are 0-1 floats and grow from there

### BODY Attributes (grow then decline over a player's career)
- Vision: ball tracking
- Awareness: game state/positioning  
- Reaction: twitch response speed
- Power: force generation (throwing/hitting distance)
- Grace: mechanical execution quality, reduces injury
- Speed: raw movement speed
- Stamina: degradation over the course of a game (mostly affects pitchers/catchers, starts to get to everyone else in extras)

### SOUL Attributes (grow over a player's career) - not currently implemented, but crucial to the roadmap
- Charisma: interaction with fans
- Esprit: interaction with teammates
- Aggression: play hungry, steal bases hit whatever comes, charge fielding opportunities, charge the mound. draws out aggression in other players
- Judgement: plays smarter, especially tempers the worse outcomes of Aggression while enhancing the good ones
- Wisdom: improves play over the course of a game (especially affects the pitcher/batter matchup)
- Superstition: how much a player is affected by Luck (in both directions)
- Grit: weather unlucky events, improve performance at low Stamina, improve performance with minor injury, improve recovery from major injury

## Player Skills (grow throughout the player's career. have an effect on their own and also leverage attributes for full effect)

### Pitching Skills
- Deception: hide the next pitch. on catchers, improves framing ability - Awareness, Wisdom
- Control: throw to the most-favorable zones for the chosen pitch - Grace
- Mechanics: extra leverage on Power and Grace into other skills - primary pitching balance lever
- Velocity: throw fast, decrease contact - Power
- Movement: increase strikes - Grace, Power
- Presence: reduce the power of hits - Power
- Stuff: induce poor-quality hit types like popups and grounders - Grace

### Batting Skills
- Discipline: avoid swinging when it's a bad idea (risk: batter is too easy to scare) - Judgement, Wisdom
- Attack: swing more when it's good (risk: might become a whiffer) - Aggression, Judgement, Wisdom
- Contact: make bat-on-ball - Vision, Reaction
- Form: convert Power into clean hits - Grace, Power
- Aim: hit to advantageous locations - Awareness, Grace
- Intimidation: scare pitcher into easier zones - Power, Aggression, Charisma

### Baserunning Skills
- Sprint: round the basepaths - Speed
- Performance: effectively makes fielders worse - Reaction, Grace
- Stealth: for stealing - Speed, Awareness

### Defense Skills
- Sense: understand the game state and make good decisions - Awareness
- Precision: throw accurately to teammates - Vision
- Arm: throw hard and fast, especially across long distances - Power
- Agility: get in range of the ball - Speed
- Acrobatics: grab a ball that's near you, whether hit or thrown (can compensate for low-precision teammates) - Reaction, Grace
- Dexterity: grab the squirreliest ground balls and tag the slipperiest baserunners - Reaction

## Roster Management - not implemented, but major design space

### Rosters
Managers draft a full team at the start of their first season (batters, pitchers, benchwarmers)
- choose from 5 options per position: 3 rookies / 1 mid-career / 1 peak per position
- One chance midseason and offseason for roster turnover
- High control over post-draft roster - it's just where you start. Lineup changes, positions swaps, all on the table.
- Medium control over long-term player attribute and skill growth
- Low control over granular in-game events
- no plans for player trading between managers

### Player growth
Players have Durability representing the state of their career (reduced at the end of the season, and by events in/out of game)
- fast-growing rookies
- peak players with high SKILL and BODY 
- aging players - BODY will decline, MIND and SKILL can still grow, if slower. 
- NATURE (BODY and MIND) is harder to grow than SKILL, you're more at the mercy of base stat rolls and have to work around player strengths and weaknesses.
- theoretical maximum career length is 20 seasons, but most players will be in the 6-10 range.

### Injury
- Players can be injured. Minor injuries impact performance, and increase the risk of major injury
- Major injuries force a player onto the bench until they recover.
rare Catastrophic injuries can end a career outright if they happen too late in a career for them to recover.

# Active Problem:
Implementing field logic based on our model