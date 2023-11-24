let imageArea = document.querySelector("#content");

let currentLikedDogs = JSON.parse(localStorage.getItem("rcc3452-favorite"));

let savedDogs = "";

if(localStorage.getItem("rcc3452-savedImages") == null)
{
    localStorage.setItem("rcc3452-savedImages", "");
}


if(localStorage.getItem("rcc3452-savedImages") == "")
{
    localStorage.setItem("rcc3452-savedImages", localStorage.getItem("rcc3452-favorite"));
    savedDogs = JSON.parse(localStorage.getItem("rcc3452-savedImages"));
}
else if (localStorage.getItem("rcc3452-favorite") != null)
{
    savedDogs = JSON.parse(localStorage.getItem("rcc3452-savedImages"));
    for (let i = 0; i < currentLikedDogs.length; i++)
    {
        savedDogs.push(currentLikedDogs[i]);
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

let clearButton = document.querySelector("#clear");

clearButton.onclick = localStorage.removeItem("rcc3452-savedImages");



