$(document).ready(function () {
    const selectBtn = document.querySelector(".select-btn");
    const items = document.querySelectorAll(".item");
    const selectedTeamsInput = document.getElementById("selected-teams-input");

    selectBtn.addEventListener("click", () => {
        selectBtn.classList.toggle("open");
    });

    items.forEach((item) => {
        item.addEventListener("click", () => {
            let numberofteams = parseInt($("#NumberOfTeams").val());

            if (item.classList.contains("checked")) {
                let checkedItems = document.querySelectorAll(".checked");
                console.log(numberofteams);

                if (checkedItems.length > numberofteams) {
                    item.classList.remove("checked");
                    return;
                }
            } else {
                let checkedItems = document.querySelectorAll(".checked");
                if (checkedItems.length >= numberofteams) {
                    return;
                }
            }

            item.classList.toggle("checked");
            let checkedItems = document.querySelectorAll(".checked");
            let selectedTeamIds = Array.from(checkedItems).map(
                (item) => item.querySelector(".check-icon").id
            );
            selectedTeamsInput.value = JSON.stringify(selectedTeamIds);
        });
    });
});

function createCompetition() {
    var selectedTeams = [];
    $(".item.checked").each(function () {
        var teamId = $(this).attr("id");
        selectedTeams.push(teamId);
    });

    let model = {
        Name: $("#Name").val(),
        NumberOfTeams: $("#NumberOfTeams").val(),
        Address: $("#Address").val(),
        StartDate: $("#StartDate").val(),
        EndDate: $("#EndDate").val(),
        SelectedTeamsIds: selectedTeams,
    }

    $.ajax({
        type: 'POST',
        timeout: 10000,
        url: '@Url.Action("Create", "Competition")',
        contentType: "application/json; charset=utf-8",
        processData: false,
        cache: false,
        data: JSON.stringify(model),
        success: function (response) {
            alert("Competitia a fost creata cu succes");
            //window.location.href = "/Competition/Index";
        },
        error: function (error) {
            alert("A aparut o eroare");
            console.log(error);
            //window.location.href = "/Competition/Index";
        }
    });
}