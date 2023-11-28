window.onload = (e) => {(document.querySelector("#search").onclick = searchButtonClicked), localStorage.removeItem("rcc3452-favorite");};
let displayTerm = "";
let displaySubTerm = "";
let resultsArray;
let savedArray = [];

function searchButtonClicked(){
    console.log("searchButtonClicked() called");
    
    const image_url = "https://dog.ceo/api/breed/";

    let url = image_url;

    let term = document.querySelector("#dog-selector").value;
    displayTerm = term;

    term = term.trim();

    term= encodeURIComponent(term);

    if (term.length < 1) return;

    let subTerm = document.querySelector("#sub-selector").value;
    displaySubTerm = subTerm;
    subTerm = subTerm.trim();
    subTerm = encodeURIComponent(subTerm);

    url += term 

    if (subTerm.length > 1)
    {
        url += "/" + subTerm;
    }

    url += "/images/random/";

    let limit = document.querySelector("#limit").value;
    url += limit;
    
    if (displaySubTerm.length > 1)
    {
        document.querySelector("#status").innerHTML = "<b>Searching for '" + displaySubTerm + " " + displayTerm + "'</b>";
    }
    else
    {
        document.querySelector("#status").innerHTML = "<b>Searching for '" + displayTerm + "'</b>";
    }
    

    console.log(url);

    getData(url);

}

function getData(url){
    let xhr = new XMLHttpRequest();

    xhr.onload = dataLoaded;

    xhr.onerror = dataError;

    xhr.open("GET", url);
    xhr.send();
}

function dataLoaded(e){
    let xhr = e.target;
    console.log(xhr.responseText);

    let obj = JSON.parse(xhr.responseText);
    if(!obj.message || obj.message.length == 0){
        document.querySelector("#status").innerHTML = "<b>No results found for '" + displayTerm + "'</b>";
        return;
    }

    let results = obj.message;
    console.log("results.length = " + results.length);
    let bigString = "";

    for (let i=0;i<results.length;i++){
        let result = results[i];

        let smallURL = result;

        let line = `<div class='result'><img src='${smallURL}' title= '${smallURL}'></div>`;

        bigString += line;
    }

    document.querySelector("#content").innerHTML = bigString;
    if (displaySubTerm.length > 1)
    {
        document.querySelector("#status").innerHTML = "<b>Success!</b><p><i>Here are " + results.length + " results for '" + displaySubTerm + " " + displayTerm + "'</i></p>";
    }
    else
    {
        document.querySelector("#status").innerHTML = "<b>Success!</b><p><i>Here are " + results.length + " results for '" + displayTerm + "'</i></p>";
    }

    resultsArray = document.querySelectorAll("img");

    for (let i = 0; i < resultsArray.length; i++) {
        resultsArray[i].onclick = (e) => 
            {if(JSON.parse(localStorage.getItem("rcc3452-favorite")) != null)
             {
                if (JSON.parse(localStorage.getItem("rcc3452-favorite")).includes(resultsArray[i].src) == false)
                {
                    savedArray.push(resultsArray[i].src), localStorage.setItem(prefix + "favorite", JSON.stringify(savedArray))
                }
             }
             else if(savedArray.includes(resultsArray[i].src) == false)
             {
                savedArray.push(resultsArray[i].src), localStorage.setItem(prefix + "favorite", JSON.stringify(savedArray))
             }
                
            }
    }
}

function dataError(e){
    console.log("An error occured");
}

function getArray(url){
    let xhr = new XMLHttpRequest();

    xhr.onload = createInitialArray;

    xhr.onerror = dataError;

    xhr.open("GET", url);
    xhr.send();
}

function createInitialArray(e){
    let xhr = e.target;
    console.log(xhr.responseText);

    let obj = JSON.parse(xhr.responseText);
    console.log(obj);
    console.log(obj.message);
    let results = obj.message;
    
    let userInitialSelection = document.querySelector("#dog-selector");
    for (dog in results)
    {
        let breed = document.createElement("option");
        breed.text = dog;
        userInitialSelection.add(breed);
    }
    if (storedBreed){
        breedField.value = storedBreed;
    }
    else {
        breedField.value = "affenpinscher";
    }
    userInitialSelection.value = breedField.value;
    let subURL = "https://dog.ceo/api/breed/";
    subURL += breedField.value;
    subURL += "/list";
    getSubArray(subURL);

}

function getChangeArray(url){
    let xhr = new XMLHttpRequest();

    xhr.onload = createChangeArray;

    xhr.onerror = dataError;

    xhr.open("GET", url);
    xhr.send();
}

function createChangeArray(e){
    let xhr = e.target;
    // console.log(xhr.responseText);

    let obj = JSON.parse(xhr.responseText);
    // console.log(obj);
    // console.log(obj.message);
    let results = obj.message;
    
    let userInitialSelection = document.querySelector("#dog-selector");
    for (dog in results)
    {
        if (dog == userInitialSelection.value)
        {
            let subURL = "https://dog.ceo/api/breed/";
            subURL += dog;
            subURL += "/list";
            getSubArray(subURL);
        }
    }
}

function getSubArray(url){
    let xhr = new XMLHttpRequest();

    xhr.onload = createSubArray;

    xhr.onerror = dataError;

    xhr.open("GET", url);
    xhr.send();
}

function createSubArray(e){
    let xhr = e.target;
    console.log(xhr.responseText);

    let obj = JSON.parse(xhr.responseText);
    console.log(obj);
    console.log(obj.message);
    let results = obj.message;
    console.log(results[0]);
    
    let userSecondarySelection = document.querySelector("#sub-selector");
    if (userSecondarySelection.length > 0)
    {
        for (let i = userSecondarySelection.length - 1; i >= 0; i--)
        {
            userSecondarySelection.remove(i);
        }
    }
    for (let i = 0; i < results.length; i++)
    {
        let breed = document.createElement("option");
        breed.text = results[i];
        userSecondarySelection.add(breed);
    }
}

document.onload = getArray("https://dog.ceo/api/breeds/list/all"), localStorage.removeItem("rcc3452-favorite");

const breedField = document.querySelector("#dog-selector");
const prefix = "rcc3452-";
const breedKey = prefix + "breed";

const storedBreed = localStorage.getItem(breedKey);

function storingBreed(){
    localStorage.setItem(breedKey, breedField.value);
}

function selectorOnChange(){
    getChangeArray('https://dog.ceo/api/breeds/list/all');
    storingBreed();
}



