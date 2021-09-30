const dropArea = document.querySelector(".drag-area"),
    dragText = dropArea.querySelector("header"),
    button = dropArea.querySelector("button"),
    theImage = dropArea.querySelector("img"),
    theFather = dropArea.querySelector("#fatherNumber"),
    GogoButton = dropArea.querySelector("#getMeThisFile"),
    input = dropArea.querySelector("#fileInput");
let file;
var formData = new FormData();


GogoButton.onclick = () => {
    formData.set("fatherId", theFather.value);
    if (formData.getAll("fileInput").length === 0){
        console.log("select a freaking file, mon")
    }
    else {
        var url = "FileExplorerUser/Upload";
        console.log(url);
        var request = new XMLHttpRequest();
        request.open("POST", url);
        request.send(formData);
        request.onload = function (){
            window.location.reload();
        }
    }
    // window.location.reload();
}
button.onclick = ()=>{
    input.click();
}
input.addEventListener("change", function(){
    file = this.files[0];
    dropArea.classList.add("active");
    formData.set("fileInput", file);
    showFile();
});
dropArea.addEventListener("dragover", (event)=>{
    dropArea.classList.add("active");
    dragText.textContent = "Release to Upload File";
    event.preventDefault();
});
dropArea.addEventListener("dragleave", ()=>{
    dropArea.classList.remove("active");
    dragText.textContent = "Drag & Drop to Upload File";
});
dropArea.addEventListener("drop", (event)=>{
    file = event.dataTransfer.files[0];
    formData.set("fileInput", file);
    showFile();
    event.preventDefault();
});


function showFile(){
    let fileType = file.type;
    let validExtensions = ["image/jpeg", "image/jpg", "image/png"];
    if(validExtensions.includes(fileType)){
        let fileReader = new FileReader();
        fileReader.onload = ()=>{
            let fileURL = fileReader.result;

            document.getElementById("fileShower").setAttribute("src", fileURL)
            document.getElementById("fileShower").setAttribute("alt", "image")
            let imgTag = `<img src="${fileURL}" alt="image">`;
            theImage.innerHTML = imgTag;
        }
        fileReader.readAsDataURL(file);
    }
    else{
        alert("This is not an Image File!");
        dropArea.classList.remove("active");
        dragText.textContent = "Drag & Drop to Upload File";
    }
}