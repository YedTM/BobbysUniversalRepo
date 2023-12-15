// This creates the grid that the game's tiles are placed
// The 0s are floors, the 1s are walls, and the 2s are shelves
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

// These are used to create classes in the span that
// allows the gameObjects to be printed on screen
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

// This class is what makes up all the fundamentals of the gameObjects
// They have an x and y value, a type (aka what it is), and a className (one of the
// values in cssClass).
class GameObject {
	constructor (x, y, type, className) {
		this.x = x;
		this.y = y;
		this.type = type;
		this.className = className;
	}
}
// This class is what makes up a Monster gameObject
// It uses the constructor from the GameObject class and adds on a 
// classPlace value, which shows what placement the Monster has in the 
// elements list (in relation to other Monsters).
// All monsters are of type "monster" and thus don't require that value to be taken in
// externally
class Monster extends GameObject{
	constructor (x, y, className, classPlace) {
		super(x, y, "monster", className);
		this.classPlace = classPlace;
	}
}

// All of the gameObjects are then created
let movingOne = new Monster(10, 5, cssClass.MONSTER1, "one");
let movingTwo = new Monster(12, 5, cssClass.MONSTER1, "two");
let movingThree = new Monster(3, 5, cssClass.MONSTER1, "three");
let stillOne = new Monster(5, 1, cssClass.MONSTER2, "one");
let stillTwo = new Monster(4, 9, cssClass.MONSTER2, "two");
let stillThree = new Monster(14, 10, cssClass.MONSTER2, "three");
let locked = new GameObject(15, 11, "locked", cssClass.LOCKED);
let key = new GameObject(1, 9, "key", cssClass.KEY1);
let power1 = new GameObject(1, 11, "power", cssClass.POWER1);
let power2 = new GameObject(1, 3, "power", cssClass.POWER2);
let power3 = new GameObject(15, 1, "power", cssClass.POWER3);
let portal = new GameObject(1, 1, "portal", cssClass.PORTAL);


// And then they are placed into allGameObjects, to be read by world.html later
const allGameObjects = {
	level1:[
		{entity: movingOne},
		{entity: movingTwo},
		{entity: movingThree},
		{entity: stillOne},
		{entity: stillTwo},
		{entity: stillThree},
		{entity: locked},
		{entity: key},
		{entity: power1},
		{entity: power2},
		{entity: power3},
		{entity: portal}
	]
}
