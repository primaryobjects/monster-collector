$(document).ready(function() {
    // Event delegation for save button click
    $(document).on('click', '.saveButton', function() {
        var row = $(this).closest("tr");
        const id = row.data("id");
        const monster = {
            Name: row.find("td:eq(0)").text().trim(),
            Health: row.find("td:eq(1)").text().trim(),
            Attack: row.find("td:eq(2)").text().trim(),
            Defense: row.find("td:eq(3)").text().trim()
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
                // Hide the save button after successful save
                row.find(".saveButton").hide();
            }
        });
    });

    // Event delegation for delete button click
    $(document).on('click', '.deleteButton', function() {
        var row = $(this).closest("tr");
        var rowDescription = row.next('.collapse-content');
        const id = row.data("id");
        const name = row.find("td:eq(0)").text().trim();
        const button = $(this);

        $.ajax({
            url: `/api/monster/${id}`,
            type: "DELETE",
            contentType: "application/json",
            success: result => {
                console.log(`Monster deleted: ${JSON.stringify(result)}`);

                row.fadeOut(400, function() {
                    row.remove();
                });
                rowDescription.fadeOut(400, function() {
                    rowDescription.remove();
                })

                $("#message").append(`<div id='innerMessage-${id}'>${name} deleted.</div>`);
                $(`#innerMessage-${id}`).fadeOut(2000, function() {
                    $(this).remove();
                });
            }
        });
    });

    $(".addButton").click(function() {
        $(this).attr('disabled', true);
        $('#prompt').attr('disabled', true);

        const createModel = {
            Prompt: $('#prompt').val().trim()
        };

        $.ajax({
            url: `/api/monster`,
            type: "POST",
            data: JSON.stringify(createModel),
            contentType: "application/json",
            success: monster => {
                console.log(`Monster created: ${JSON.stringify(monster)}`);

                // Insert a new row into the table.
                const table = $('.monster-table tbody');
                table.append(`
                    <tr data-id="${monster.id}" class='hover-tooltip'">
                        <td class="controls" contenteditable="true" title="${monster.description}">${monster.name}</td>
                        <td contenteditable="true">${monster.health}</td>
                        <td contenteditable="true">${monster.attack}</td>
                        <td contenteditable="true">${monster.defense}</td>
                        <td>
                            <button class="saveButton" style="display:none;">Save</button>
                            <button class="deleteButton">Delete</button>
                        </td>
                    </tr>
                    <tr class="collapse-content">
                        <td colspan="5" class='description'>${monster.description}</td>
                    </tr>
                `);

                $("#message").append(`<div id='innerMessage-${monster.id}'>${monster.name} created successfully.</div>`);
                $(`#innerMessage-${monster.id}`).fadeOut(2000, function() {
                    $(this).remove();
                });
            },
            complete: () => {
                $(this).removeAttr('disabled');
                $('#prompt').removeAttr('disabled');
            }
        });
    });

    // Detect changes in contenteditable fields and show the save button
    $(document).on('input', '[contenteditable]', function() {
        $(this).closest('tr').find('.saveButton').show();
    });
});

document.addEventListener('DOMContentLoaded', function() {
    var hoverTimeout;

    // Event delegation for mouseenter and mouseleave on rows with class 'hover-tooltip'
    document.addEventListener('mouseenter', function(event) {
        if (event.target.classList && event.target.classList.contains('hover-tooltip')) {
            var row = event.target.closest('tr.hover-tooltip[data-id]');
            if (row) {
                var monsterId = row.getAttribute('data-id');

                // Clear any existing timeout to avoid multiple triggers
                clearTimeout(hoverTimeout);

                // Set a timeout to delay showing the modal
                hoverTimeout = setTimeout(function() {
                    // Make an AJAX call to fetch the audit logs for this monster ID
                    fetch(`/api/audit/${monsterId}`)
                        .then(response => response.json())
                        .then(data => {
                            populateAuditLogTable(data);

                            // Show the modal or tooltip
                            document.getElementById('auditLogModal').style.display = 'block';
                        });
                }, 1000); // Delay in milliseconds
            }
        }
    }, true);

    document.addEventListener('mouseleave', function(event) {
        if (event.target.classList && event.target.classList.contains('hover-tooltip')) {
            var row = event.target.closest('tr.hover-tooltip[data-id]');
            if (row) {
                // Hide the modal or tooltip
                document.getElementById('auditLogModal').style.display = 'none';
                clearTimeout(hoverTimeout); // Clear the timeout when leaving the row
            }
        }
    }, true);

    // Close the modal when clicking outside of it
    window.addEventListener('click', function(event) {
        var modal = document.getElementById('auditLogModal');
        if (event.target === modal || event.target.className === 'close') {
            modal.style.display = 'none';
        }
    });

    $(document).on('click', '.controls', function() {
        // Toggle the 'd-none' class to show/hide the .collapse-content
        $(this).closest('tr').next('.collapse-content').toggleClass('d-none');
    });
});

function populateAuditLogTable(auditLogs) {
    var tableBody = document.getElementById('auditLogTable').querySelector('tbody');
    tableBody.innerHTML = ''; // Clear existing entries

    if (auditLogs.length) {
        auditLogs.forEach(function(log) {
            var row = tableBody.insertRow();
            row.insertCell(0).textContent = log.actionType;
            row.insertCell(1).textContent = log.changedColumns.join(', ');
            row.insertCell(2).textContent = JSON.stringify(log.oldValues);
            row.insertCell(3).textContent = JSON.stringify(log.newValues);
            row.insertCell(4).textContent = new Date(log.dateChanged).toLocaleString();
        });
    }
}
