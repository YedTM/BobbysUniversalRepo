
  	window.onload = (e) => {document.querySelector("#search").onclick = searchButtonClicked};
	let displayTerm = "";
	
	function searchButtonClicked(){
		console.log("searchButtonClicked() called");
		
        const GIPHY_URL = "https://api.giphy.com/v1/gifs/search?";
        const GIPHY_KEY = "5PuWjWVnwpHUQPZK866vd7wQ2qeCeqg7";

        let url = GIPHY_URL;
        url += "api_key=" + GIPHY_KEY;

        let term = document.querySelector("#searchterm").value;
        displayTerm = term;

        term = term.trim();

        term= encodeURIComponent(term);

        if (term.length < 1) return;

        url += "&q=" + term;

        let limit = document.querySelector("#limit").value;
        url += "&limit" + limit;
        
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
            document.querySelector("#status").innerHTML = "<b>No reuslts found for '" + displayTerm + "'</b>";
            return;
        }

        let results = obj.data;
        console.log("results.length = " + results.length);
        let bigString = "<p><i>Here are " + results.length + " results for '" + displayTerm + "'</i></p>";

        
    }

    function dataError(e){
        console.log("An error occured");
    }
