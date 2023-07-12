//const selectBtn = document.querySelector(".select-btn");
//const items = document.querySelectorAll(".item");
//const selectedTeamsInput = document.getElementById("selected-teams-input");

//selectBtn.addEventListener("click", () => {
//    selectBtn.classList.toggle("open");
//});

//items.forEach((item) => {
//    item.addEventListener("click", () => {
//        let numberofteams = parseInt($("#NumberOfTeams").prop("value")); // Modificare aici

//        if (item.classList.contains("checked")) {
//            // Verifică numărul maxim de opțiuni selectate
//            let checkedItems = document.querySelectorAll(".checked");
//            console.log($("#NumberOfTeams"))
//            console.log(numberofteams);

//            if (checkedItems.length > numberofteams) {
//                item.classList.remove("checked"); // Deselectează elementul dacă numărul maxim a fost atins
//                return;
//            }
//        } else {
//            // Verifică numărul maxim de opțiuni selectate
//            let checkedItems = document.querySelectorAll(".checked");
//            if (checkedItems.length >= numberofteams) {
//                return; // Nu permite selectarea altor opțiuni dacă numărul maxim a fost atins
//            }
//        }

//        item.classList.toggle("checked");
//        let checkedItems = document.querySelectorAll(".checked");
//        let selectedTeamIds = Array.from(checkedItems).map(
//            (item) => item.querySelector(".check-icon").id
//        );
//        selectedTeamsInput.value = JSON.stringify(selectedTeamIds);
//    });
//});
