// This creates the grid that the game's tiles are placed
// The 0s are floors, 1s are horizontal walls, 2s are vertical walls,
// and 3-6 are all corner walls that have a different rotation depending on
// the number
let worldLayout = {
    layout: [
        [3,1,1,1,1,1,1,1,1,1,1,1,1,1,4],
        [2,0,0,0,0,0,0,0,0,0,0,0,0,0,2],
        [2,0,0,0,0,0,0,0,0,0,0,0,0,0,2],
        [2,0,0,0,0,0,0,0,0,0,0,0,0,0,2],
        [2,0,0,0,0,0,0,0,0,0,0,0,0,0,2],
        [2,0,0,0,0,0,0,0,0,0,0,0,0,0,2],
        [2,0,0,0,0,0,0,0,0,0,0,0,0,0,2],
        [2,0,0,0,0,0,0,0,0,0,0,0,0,0,2],
        [2,0,0,0,0,0,0,0,0,0,0,0,0,0,2],
        [2,0,0,0,0,0,0,0,0,0,0,0,0,0,2],
        [2,0,0,0,0,0,0,0,0,0,0,0,0,0,2],
        [2,0,0,0,0,0,0,0,0,0,0,0,0,0,2],
        [2,0,0,0,0,0,0,0,0,0,0,0,0,0,2],
        [2,0,0,0,0,0,0,0,0,0,0,0,0,0,2],
        [2,0,0,0,0,0,0,0,0,0,0,0,0,0,2],
        [2,0,0,0,0,0,0,0,0,0,0,0,0,0,2],
        [6,1,1,1,1,1,1,1,1,1,1,1,1,1,5]

    ]
};

// These are used to create classes in the span that
// allows the gameObjects to be printed on screen
const cssClass = {
	LOCKED: 	"locked",
	KEY1: 		"key1",
	MONSTER1: 	"monster1",
	PLAYER: 	"player",
	PORTAL: 	"portal",
	POWER1: 	"power1",
	POWER2: 	"power2",
	POWER3:		"power3",
    PLAYER_SHOT:"pShot",
	ENEMY_SHOT: "eShot"
};

// The gameObjects are created and placed into allGameObjects in order to
// printed onto the screen of battle.html.
// (It was planned to have classes, similar to world.js, but they would constantly ruin 
// the code present in battle.html so ultimately they were scrapped)
const allGameObjects = {
	level1:[
		{x:8, y:5, type:"monster", className: cssClass.MONSTER1, lives: 5, classPlace: "one"},
		{x:12, y:5, type:"monster",	className: cssClass.MONSTER1, lives: 5, classPlace: "two"}
	],
    playerShot: {x:5,  y:5, type:"shot",	className: cssClass.PLAYER_SHOT},
    enemyShot: {x:-1,  y:-1, type:"shot",	className: cssClass.ENEMY_SHOT, numShot: "shot0"},
    powerOne: {x:1, y:9,  type:"power",	className: cssClass.POWER1},
    powerTwo: {x:6,  y:6, type:"power",	className: cssClass.POWER2},
    powerThree: {x:12,  y:9, type:"power",	className: cssClass.POWER3}
}