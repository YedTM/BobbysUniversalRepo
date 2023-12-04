let worldLayout = {
    layout: [
        [1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1],
        [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
        [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
        [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
        [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
        [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
        [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
        [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
        [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
        [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
        [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
        [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
        [1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1]
    ]
};

// these map to the CSS classes in main.css
const cssClass = Object.freeze({
	CHEST1: 	"chest1",
	KEY1: 		"key1",
	MONSTER1: 	"monster1",
	MONSTER2: 	"monster2",
	MONSTER3: 	"monster3",
	PLAYER: 	"player",
	RING1: 		"ring1",
	TREASURE1: 	"treasure1",
	TREASURE2: 	"treasure2"
});

const allGameObjects = {
	// we aren't using .type yet
	level1:[
		{x:10, y:5, type:"monster", 	className: cssClass.MONSTER1},
		{x:12, y:5, type:"monster",	className: cssClass.MONSTER1},
		{x:25, y:15, type:"monster",	className: cssClass.MONSTER2},
		{x:10, y:3,  type:"monster",	className: cssClass.MONSTER3},
		{x:24, y:15, type:"chest",		className: cssClass.CHEST1},
		{x:1, y:9,  type:"key",		className: cssClass.KEY1},
		{x:17, y:5,  type:"treasure",	className: cssClass.TREASURE1},
		{x:2,  y:17, type:"treasure",	className: cssClass.TREASURE2},
		{x:10, y:16, type:"ring",		className: cssClass.RING1}
	]
}

