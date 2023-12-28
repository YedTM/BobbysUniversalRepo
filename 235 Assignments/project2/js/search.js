// When the website loads, the search button has its onclick assigned to searchButtonClicked 
// and the local storage key rcc3452-favorite has its data removed
window.onload = (e) => {(document.querySelector("#search").onclick = searchButtonClicked), localStorage.removeItem("rcc3452-favorite");};
let displayTerm = "";
let displaySubTerm = "";
let resultsArray;
let savedArray = [];

// This function occurs when the search button is clicked. It takes the initial breed and potential sub breed
// and sends a request for the images of the breed and subbreed in the api.
function searchButtonClicked(){
    
    const image_url = "https://dog.ceo/api/breed/";

    let url = image_url;

    // This takes the dog breed found in the dog-selector and then assigns it to a variable that will be 
    // displayed through normal text and a variable that will be used to create a URL for the API request.
    let term = document.querySelector("#dog-selector").value;
    displayTerm = term;

    term = term.trim();

    term = encodeURIComponent(term);

    // As a failsafe, the function will be ended if the term is nonexistent
    if (term.length < 1) return;

    // This takes the subbreed found in the sub-selector and follows the same steps as the dog-selector term creation
    let subTerm = document.querySelector("#sub-selector").value;
    displaySubTerm = subTerm;
    subTerm = subTerm.trim();
    subTerm = encodeURIComponent(subTerm);

    // The dog breed is then added to the url
    url += term 

    // And the subbreed is added to the url if it exists
    if (subTerm.length > 1)
    {
        url += "/" + subTerm;
    }

    url += "/images/random/";

    // The selected limit is then added to the end of the url
    let limit = document.querySelector("#limit").value;
    url += limit;
    
    // The website will show what dog is being searched in the status section 
    if (displaySubTerm.length > 1)
    {
        document.querySelector("#status").innerHTML = "<b>Searching for '" + displaySubTerm + " " + displayTerm + "'</b>";
    }
    else
    {
        document.querySelector("#status").innerHTML = "<b>Searching for '" + displayTerm + "'</b>";
    }

    // The getData function is then called with the fully created url
    getData(url);

}

// This creates a XMLHttpRequest in order to get the information in the API, it uses the
// url created in searchButtonClicked
function getData(url){
    //The request is created
    let xhr = new XMLHttpRequest();

    //The dataLoaded function is added to the xhr onload in order to get the images
    xhr.onload = dataLoaded;

    xhr.open("GET", url);
    xhr.send();
}

// This function uses the information found in the API to create the images the user of the website sees after
// searching for a specific breed and allows the images to be clicked in order to save them to the rcc3452-favorite key
function dataLoaded(e){
    let xhr = e.target;
    //obj takes the responseText from the XMLHttpRequest
    let obj = JSON.parse(xhr.responseText);
    //obj is then checked to see if there is a message to determine if any images were found. If there wasn't a message, 
    //then the function is ended prematurely
    if(!obj.message || obj.message.length == 0){
        document.querySelector("#status").innerHTML = "<b>No results found for '" + displayTerm + "'</b>";
        return;
    }

    //results is then set as the holder of obj's message and bigString is created to hold all of the images
    //that are about to be created
    let results = obj.message;
    let bigString = "";

    //results is then ran through, with each element in the array creating its own div and img, which is then added to bigString
    for (let i=0;i<results.length;i++){
        let result = results[i];

        let smallURL = result;

        let line = "";

        line = `<div class='result'><img src='${smallURL}' title= '${smallURL}' onerror='changeImage(this)'></div>`;

        bigString += line;
    }

    //The content section is then filled with the divs and imgs found in bigString
    document.querySelector("#content").innerHTML = bigString;
    //The status text then changes to reflect a successful search
    if (displaySubTerm.length > 1)
    {
        document.querySelector("#status").innerHTML = "<b>Success!</b><p><i>Here are " + results.length + " results for '" + displaySubTerm + " " + displayTerm + "'</i></p>";
    }
    else
    {
        document.querySelector("#status").innerHTML = "<b>Success!</b><p><i>Here are " + results.length + " results for '" + displayTerm + "'</i></p>";
    }

    resultsArray = document.querySelectorAll("img");

    // Each of the newly created images then has an onclick event added to it. This event will add the image to the
    // rcc3452-favorite key and the savedArray array, if the image wasn't already added previously. This is why both
    // savedArray and the rcc3452-favorite key are checked just in case the image exists in one and not the other.
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

//This serves the same purpose as getData, but instead calls the createInitialArray function
//when xhr is loaded
function getArray(url){
    let xhr = new XMLHttpRequest();

    xhr.onload = createInitialArray;

    xhr.open("GET", url);
    xhr.send();
}

//This function creates the initial array when the page is first loaded. It creates the drop down options
//in the normal breed selection and calls the function that creates the subbreed selection. It also
//decides what breed is displayed before the user picks a different one.
function createInitialArray(e){
    //The XMLHttpRequest is taken, its responseText is placed into obj, and its message is placed into results
    let xhr = e.target;

    let obj = JSON.parse(xhr.responseText);
    let results = obj.message;
    
    let userInitialSelection = document.querySelector("#dog-selector");
    //For each of the breeds found in the API's breed array, an option is created and placed into dog-selector
    for (dog in results)
    {
        let breed = document.createElement("option");
        breed.text = dog;
        userInitialSelection.add(breed);
    }

    //If a breed was chosen before, the initial selected breed will be that one.
    //Otherwise, the selection will default to affenpinscher
    if (storedBreed){
        breedField.value = storedBreed;
    }
    else {
        breedField.value = "affenpinscher";
    }
    //The initial selection is then set to the decided breedField value
    userInitialSelection.value = breedField.value;
    //A subURL is then created with the breedField value and is then used to find a subarray
    let subURL = "https://dog.ceo/api/breed/";
    subURL += breedField.value;
    subURL += "/list";
    getSubArray(subURL);

}

//This serves the same purpose as getData, but instead calls the createChangeArray function
//when xhr is loaded
function getChangeArray(url){
    let xhr = new XMLHttpRequest();

    xhr.onload = createChangeArray;

    xhr.open("GET", url);
    xhr.send();
}

//This function will search through the API's breed array and will attempt 
//call the getSubArray function when the breed in reuslts matches the user's initial selection value
function createChangeArray(e){
    //The XMLHttpRequest is taken, its responseText is placed into obj, and its message is placed into results
    let xhr = e.target;

    let obj = JSON.parse(xhr.responseText);
    let results = obj.message;
    
    let userInitialSelection = document.querySelector("#dog-selector");
    //The API's breed array is searched and the getSubArray function is called when the 
    //breed in the array matches the selected one
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

//This serves the same purpose as getData, but instead calls the createSubArray function
//when xhr is loaded
function getSubArray(url){
    let xhr = new XMLHttpRequest();

    xhr.onload = createSubArray;

    xhr.open("GET", url);
    xhr.send();
}

//This creates the options for the sub-selector and clears out any old subbreeds that may have existed
function createSubArray(e){
    //The XMLHttpRequest is taken, its responseText is placed into obj, and its message is placed into results
    let xhr = e.target;

    let obj = JSON.parse(xhr.responseText);
    let results = obj.message;
    
    let userSecondarySelection = document.querySelector("#sub-selector");
    //If sub-selector had previous options, they are removed to allow new subbreeds to be added properly
    if (userSecondarySelection.length > 0)
    {
        for (let i = userSecondarySelection.length - 1; i >= 0; i--)
        {
            userSecondarySelection.remove(i);
        }
    }
    //Then the API's subbreed array is ran through and is used to create options for the sub-selector
    for (let i = 0; i < results.length; i++)
    {
        let breed = document.createElement("option");
        breed.text = results[i];
        userSecondarySelection.add(breed);
    }
}

//When the page loads, getArray is called and rcc3452-favorite is cleared once again to make sure any leftover 
//favorited images are fully removed
document.onload = getArray("https://dog.ceo/api/breeds/list/all"), localStorage.removeItem("rcc3452-favorite");

const breedField = document.querySelector("#dog-selector");
const prefix = "rcc3452-";
const breedKey = prefix + "breed";

const storedBreed = localStorage.getItem(breedKey);

//This function stores the selected main breed into internal stroage
function storingBreed(){
    localStorage.setItem(breedKey, breedField.value);
}

//This function calls the getChangeArray function and storing breed function when the dog-selector changes
function selectorOnChange(){
    getChangeArray('https://dog.ceo/api/breeds/list/all');
    storingBreed();
}

//This changes the image of a broken image link to an image saying that no image was found
function changeImage(e){
    e.src = "images/no_image.png";
    e.title = "No Image Found";
}