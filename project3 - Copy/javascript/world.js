let worldLayout = {
    layout: [
        [1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1],
        [1,0,0,0,0,0,0,2,2,2,0,0,0,0,0,0,1],
        [1,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,1],
        [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
        [1,0,0,0,0,0,0,2,2,2,0,0,0,0,0,0,1],
        [1,0,0,0,0,0,0,2,2,2,0,0,0,0,0,0,1],
        [1,0,0,0,0,0,0,2,2,2,0,0,0,0,0,0,1],
        [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,1],
        [1,2,2,2,0,0,0,0,0,0,0,0,0,0,0,2,1],
        [1,0,0,0,0,0,0,2,2,2,0,0,0,0,0,2,1],
        [1,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,1],
        [1,0,0,0,0,0,0,0,0,0,0,2,2,2,0,0,1],
        [1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1]
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
		{x:5, y:1, type:"monster",	className: cssClass.MONSTER2},
		{x:4, y:9, type:"monster",	className: cssClass.MONSTER2},
		{x:14, y:10, type:"monster",	className: cssClass.MONSTER2},
		{x:15, y:11, type:"locked",		className: cssClass.LOCKED},
		{x:1, y:9,  type:"key",		className: cssClass.KEY1},
		{x:1, y:11,  type:"power",	className: cssClass.POWER1},
		{x:1,  y:3, type:"power",	className: cssClass.POWER2},
		{x:15,  y:1, type:"power",	className: cssClass.POWER3},
		{x:1, y:1, type:"portal",		className: cssClass.PORTAL}
	]
}

