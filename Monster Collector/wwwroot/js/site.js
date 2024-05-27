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

// JavaScript to handle row hover and fetch audit logs
document.addEventListener('DOMContentLoaded', function() {
    var hoverTimeout;
    var rows = document.querySelectorAll('tr[data-id]');
    rows.forEach(function(row) {
        row.addEventListener('mouseenter', function() {
            var monsterId = this.getAttribute('data-id');

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
        });

        row.addEventListener('mouseleave', function() {
            // Hide the modal or tooltip
            document.getElementById('auditLogModal').style.display = 'none';
            clearTimeout(hoverTimeout); // Clear the timeout when leaving the row
        });
    });

    // Close the modal when clicking outside of it
    window.addEventListener('click', function(event) {
        var modal = document.getElementById('auditLogModal');
        if (event.target == modal || event.target.className == 'close') {
            modal.style.display = 'none';
        }
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