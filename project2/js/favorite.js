let imageArea = document.querySelector("#content");

let currentLikedDogs = JSON.parse(localStorage.getItem("rcc3452-favorite"));

let savedDogs = [];


if(localStorage.getItem("rcc3452-savedImages") == "null" || localStorage.getItem("rcc3452-savedImages") == null)
{
    localStorage.setItem("rcc3452-savedImages", localStorage.getItem("rcc3452-favorite"));
    savedDogs = JSON.parse(localStorage.getItem("rcc3452-savedImages"));
}
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
else
{
    savedDogs = JSON.parse(localStorage.getItem("rcc3452-savedImages"));
}

if (savedDogs != null)
{
    let dog = "";
    for (let i = 0; i < savedDogs.length; i++)
    {
        dog += `<div class='result'><img src='${savedDogs[i]}' title= '${savedDogs}'></div>`;
    }
    imageArea.innerHTML = dog;
}

document.querySelector("#clear").onclick = removeImages;

function removeImages()
{
    localStorage.removeItem("rcc3452-savedImages");
    localStorage.removeItem("rcc3452-favorite");
    savedDogs = null;
}

