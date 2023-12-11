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

// these map to the CSS classes in main.css
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

const allGameObjects = {
	// we aren't using .type yet
	level1:[
		{x:8, y:5, type:"monster", className: cssClass.MONSTER1, lives: 10},
		{x:12, y:5, type:"monster",	className: cssClass.MONSTER1, lives: 1}
	],
    playerShot: {x:5,  y:5, type:"shot",	className: cssClass.PLAYER_SHOT},
    enemyShot: {x:-1,  y:-1, type:"shot",	className: cssClass.ENEMY_SHOT},
    powerOne: {x:1, y:9,  type:"power",	className: cssClass.POWER1},
    powerTwo: {x:6,  y:6, type:"power",	className: cssClass.POWER2},
    powerThree: {x:12,  y:9, type:"power",	className: cssClass.POWER3}
}