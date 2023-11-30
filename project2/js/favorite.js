let imageArea = document.querySelector("#content");

let currentLikedDogs = JSON.parse(localStorage.getItem("rcc3452-favorite"));

let savedDogs = [];

// This if statement checks if there is data in the key rcc3452-savedImages. If there isn't, 
// then the key is filled with the data found in the key rcc3452-favorite and then has 
// the savedDogs array take the newly acquired data and fill itself with it
if(localStorage.getItem("rcc3452-savedImages") == "null" || localStorage.getItem("rcc3452-savedImages") == null)
{
    localStorage.setItem("rcc3452-savedImages", localStorage.getItem("rcc3452-favorite"));
    savedDogs = JSON.parse(localStorage.getItem("rcc3452-savedImages"));
}
// If there is data in rcc3452-savedImages and there is data in rcc3452-favorite, then 
// savedDogs is filled with the data from rcc3452-savedImages and has the data from 
// rcc3452-favorite (aka currentLikedDogs) pushed into it. rcc3452-savedImages is then set
// to savedDogs so that the newly selected images are saved in local storage
else if (localStorage.getItem("rcc3452-favorite") != null)
{
    savedDogs = JSON.parse(localStorage.getItem("rcc3452-savedImages"));
    for (let i = 0; i < currentLikedDogs.length; i++)
    {
        if (savedDogs.includes(currentLikedDogs[i]) == false)
        {
            savedDogs.push(currentLikedDogs[i]);
        }
    }
    localStorage.setItem("rcc3452-savedImages", JSON.stringify(savedDogs));
}
// If there is data in rcc3452-savedImages and no data in rcc3452-favorite, then savedDogs is
// simply filled with the data in rcc3452-savedImages and that is all.
else
{
    savedDogs = JSON.parse(localStorage.getItem("rcc3452-savedImages"));
}

// This will then check if the savedDogs array is empty and will proceed to create 
// divs and images that will be placed onto the favorite dogs page if savedDogs isn't empty.
if (savedDogs != null)
{
    let dog = "";
    for (let i = 0; i < savedDogs.length; i++)
    {
        dog += `<div class='result'><img src='${savedDogs[i]}' title= '${savedDogs}'></div>`;
    }
    imageArea.innerHTML = dog;
}

// The remaining two sections of code allow the clear button to remove all the data within 
// rcc3452-savedImages, rcc3452-favorite, and savedDogs to create a clean slate with no favorite 
// images remaining on the page.
document.querySelector("#clear").onclick = removeImages;

function removeImages()
{
    localStorage.removeItem("rcc3452-savedImages");
    localStorage.removeItem("rcc3452-favorite");
    savedDogs = null;
}

