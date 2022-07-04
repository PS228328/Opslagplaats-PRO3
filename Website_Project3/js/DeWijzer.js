function showId(x) {
    if (x.checked) {
        const ids = document.querySelectorAll(".hide")

        for (let i = 0; i < ids.length; i++) {
            ids[i].className = "hideNot"
        }
    }
    else {
        const ids = document.querySelectorAll(".hideNot")
        
        for (let i = 0; i < ids.length; i++) {
            ids[i].className = "hide"
        }
    }
}