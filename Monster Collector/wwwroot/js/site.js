$(document).ready(function() {
    $(".saveButton").click(function() {
        var row = $(this).closest("tr");
        const id = row.data("id");
        const monster = {
            Name: row.find("td:eq(0)").text().trim(),
            Health: row.find("td:eq(1)").text(),
            Attack: row.find("td:eq(2)").text(),
            Defense: row.find("td:eq(3)").text()
        };

        $.ajax({
            url: `/api/monster/${id}`,
            type: "PUT",
            data: JSON.stringify(monster),
            contentType: "application/json",
            success: result => {
                console.log(`Monster saved: ${JSON.stringify(result)}`);
                $("#message").append(`<div id='innerMessage-${id}'>${result.name} saved successfully.</div>`);
                $(`#innerMessage-${id}`).fadeOut(2000, function() {
                    $(this).remove();
                });
            }
        });
    });
});