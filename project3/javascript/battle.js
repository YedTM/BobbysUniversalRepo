let worldLayout = {
    layout: [
        [3,1,1,1,1,1,1,1,1,1,1,1,1,1,3],
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
        [3,1,1,1,1,1,1,1,1,1,1,1,1,1,3]

    ]
};

// these map to the CSS classes in main.css
const cssClass = {
	LOCKED: 	"locked",
	KEY1: 		"key1",
	MONSTER1: 	"monster1",
	MONSTER2:	"monster2",
	PLAYER: 	"player",
	PORTAL: 	"portal",
	POWER1: 	"power1",
	POWER2: 	"power2",
	POWER3:		"power3"
};

const allGameObjects = {
	// we aren't using .type yet
	level1:[
		{x:10, y:5, type:"monster", 	className: cssClass.MONSTER1},
		{x:12, y:5, type:"monster",	className: cssClass.MONSTER1},
		{x:1, y:9,  type:"power",	className: cssClass.POWER1},
		{x:1,  y:3, type:"power",	className: cssClass.POWER2},
		{x:11,  y:5, type:"power",	className: cssClass.POWER3},
	]
}