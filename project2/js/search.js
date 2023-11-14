window.onload = (e) => {document.querySelector("#search").onclick = searchButtonClicked};
let displayTerm = "";

function searchButtonClicked(){
    console.log("searchButtonClicked() called");
    
    const GIPHY_URL = "https://dog.ceo/api/breed/";

    let url = GIPHY_URL;

    let term = document.querySelector("#dog-selector").value;
    displayTerm = term;

    term = term.trim();

    term= encodeURIComponent(term);

    if (term.length < 1) return;

    url += term + "/images/random/";

    let limit = document.querySelector("#limit").value;
    url += limit;
    
    document.querySelector("#status").innerHTML = "<b>Searching for '" + displayTerm + "'</b>";

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
    if(!obj.data || obj.data.length == 0){
        document.querySelector("#status").innerHTML = "<b>No results found for '" + displayTerm + "'</b>";
        return;
    }

    let results = obj.data;
    console.log("results.length = " + results.length);
    let bigString = "";

    for (let i=0;i<results.length;i++){
        let result = results[i];

        let smallURL = result.images.fixed_width.url;
        if (!smallURL) smallURL = "images/no-image-found.png";

        let url = result.url;


        let line = `<div class='result'>Rating: ${result.rating.toUpperCase()}<br /><img src='${smallURL}' title= '${result.id}' />`;
        line += `<span><a target='_blank' href='${url}'>View on Giphy</a></span></div>`;

        bigString += line;
    }

    document.querySelector("#content").innerHTML = bigString;
    document.querySelector("#status").innerHTML = "<b>Success!</b><p><i>Here are " + results.length + " results for '" + displayTerm + "'</i></p>";
}

function dataError(e){
    console.log("An error occured");
}